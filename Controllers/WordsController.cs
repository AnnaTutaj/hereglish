using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Core.Models;
using Hereglish.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Text.RegularExpressions;

namespace Hereglish.Controllers
{
    [Route("api/words")]
    public class WordsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWordRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public WordsController(IMapper mapper, IWordRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWord(int id)
        {
            var word = await repository.GetWord(id);

            if (word == null)
            {
                return NotFound();
            }

            var wordResource = mapper.Map<Word, WordResource>(word);

            return Ok(wordResource);
        }


        [HttpGet]
        public async Task<QueryResultResource<WordResource>> GetWords(WordQueryResource filterResource)
        {
            var filter = mapper.Map<WordQueryResource, WordQuery>(filterResource);
            var queryResult = await repository.GetWords(filter);

            return mapper.Map<QueryResult<Word>, QueryResultResource<WordResource>>(queryResult);
        }

        [HttpPost]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> CreateWord([FromBody] SaveWordResource wordResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commasCount = wordResource.Meaning.TakeWhile(c => c == ',').Count();
            var limitOfCommas = 3;

            if (commasCount > limitOfCommas)
            {
                ModelState.AddModelError("error", "Cannot add a word with more than three commas in meaning");
                return BadRequest(ModelState);
            }

            var word = mapper.Map<SaveWordResource, Word>(wordResource);

            word.CreatedAt = DateTime.Now;
            word.UpdatedAt = null;

            repository.Add(word);

            await unitOfWork.CompleteAsync();

            word = await repository.GetWord(word.Id);

            var result = mapper.Map<Word, WordResource>(word);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> UpdateWord(int id, [FromBody] SaveWordResource wordResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commasCount = wordResource.Meaning.TakeWhile(c => c == ',').Count();
            var limitOfCommas = 3;

            if (commasCount > limitOfCommas)
            {
                ModelState.AddModelError("error", "Cannot add a word with more than three commas in meaning");
                return BadRequest(ModelState);
            }

            var word = await repository.GetWord(id);

            if (word == null)
            {
                return NotFound();
            }

            mapper.Map<SaveWordResource, Word>(wordResource, word);

            word.UpdatedAt = DateTime.Now; ;

            await unitOfWork.CompleteAsync();

            word = await repository.GetWord(word.Id);

            var result = mapper.Map<Word, WordResource>(word);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> DeleteWord(int id)
        {
            var word = await repository.GetWord(id, includeRelated: false);

            if (word == null)
            {
                return NotFound();
            }

            repository.Remove(word);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("pdf")]
        public async Task<FileResult> GetPdf()
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            var titleFont = workbook.CreateFont();
            titleFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            var titleStyle = workbook.CreateCellStyle();
            titleStyle.SetFont(titleFont);

            IRow row = sheet1.CreateRow(0);
            string[] headers = new string[] { "Word", "Meaning", "UK", "US", "Example", "Subcategory", "Category", "Date add" };
            for (var i = 0; i < headers.Length; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(headers[i]);
                cell.CellStyle = titleStyle;
            }

            var filter = new WordQuery();
            filter.WithoutPagination = true;
            var queryResult = await repository.GetWords(filter);

            for (var i = 0; i < queryResult.Items.Count(); i++)
            {
                row = sheet1.CreateRow(i + 1);
                var word = queryResult.Items.ElementAt(i);
                row.CreateCell(0).SetCellValue(word.Name);
                row.CreateCell(1).SetCellValue(word.Meaning);
                row.CreateCell(2).SetCellValue(word.PronunciationUK);
                row.CreateCell(3).SetCellValue(word.PronunciationUS);
                row.CreateCell(4).SetCellValue(Regex.Replace(word.Example, "<.*?>", String.Empty));
                row.CreateCell(5).SetCellValue(word.Subcategory.Name);
                row.CreateCell(6).SetCellValue(word.Subcategory.Category.Name);
                row.CreateCell(7).SetCellValue(String.Format("{0:MM-dd-yyyy}", word.CreatedAt));
            }

            for (var i = 0; i < headers.Length; i++)
            {
                sheet1.AutoSizeColumn(i);
            }

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.ToArray()), "application/vnd.ms-excel", "plik.xls");
        }
    }
}
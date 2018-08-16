using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Core;
using Hereglish.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hereglish.Controllers
{
    [Route("/api/words/{wordId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IWordRepository wordRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly PhotoSettings photoSettings;
        private readonly IMapper mapper;
        private readonly IPhotoRepository photoRepository;
        public PhotosController(IHostingEnvironment host, IWordRepository wordRepository, IPhotoRepository photoRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoRepository = photoRepository;
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.wordRepository = wordRepository;
            this.host = host;
        }
        
        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int wordId)
        {
            var photos = await photoRepository.GetPhotos(wordId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int wordId, IFormFile file)
        {
            var word = await wordRepository.GetWord(wordId, includeRelated: false);
            if (word == null)
            {
                return NotFound();
            }

            if (file == null)
            {
                return BadRequest("Null file");
            }

            if (file.Length == 0)
            {
                return BadRequest("Empty file");
            }

            if (file.Length > photoSettings.MaxBytes)
            {
                return BadRequest("Max file size (5 MB) exceeded");
            }

            if (!photoSettings.IsSupported(file.FileName))
            {
                return BadRequest("Invalid file type");
            }

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            word.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }

    }
}
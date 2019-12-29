using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hereglish.Core.Models;

namespace Hereglish.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Word> ApplyFiltering(this IQueryable<Word> query, WordQuery queryObj)
        {
            if (!String.IsNullOrEmpty(queryObj.Query))
            {
                query = query.Where(w => w.Name.Contains(queryObj.Query)
                || w.Meaning.Contains(queryObj.Query)
                || w.Example.Contains(queryObj.Query)
                || w.PronunciationUK.Contains(queryObj.Query)
                || w.PronunciationUS.Contains(queryObj.Query));
            }

            if (queryObj.CategoryId.HasValue)
            {
                query = query.Where(w => w.Subcategory.CategoryId == queryObj.CategoryId.Value);
            }

            if (queryObj.SubcategoryId.HasValue)
            {
                query = query.Where(w => w.SubcategoryId == queryObj.SubcategoryId.Value);
            }

            if (queryObj.PartOfSpeechId.HasValue)
            {
                query = query.Where(w => w.PartOfSpeechId == queryObj.PartOfSpeechId.Value);
            }

            if (queryObj.PartOfSpeechIds.Length > 0)
            {
                query = query.Where(w => queryObj.PartOfSpeechIds.Contains(w.PartOfSpeechId));
            }

             if (queryObj.FeatureIds.Length > 0)
            {
                query = query.Where(w => w.Features.Any(x => queryObj.FeatureIds.Contains(x.FeatureId)));
            }

            if (queryObj.SubcategoryIds.Length > 0)
            {
                query = query.Where(w => queryObj.SubcategoryIds.Contains(w.SubcategoryId));
            }

            if (!String.IsNullOrEmpty(queryObj.Name))
            {
                query = query.Where(w => w.Name.Contains(queryObj.Name));
            }

            if (!String.IsNullOrEmpty(queryObj.Meaning))
            {
                query = query.Where(w => w.Meaning.Contains(queryObj.Meaning));
            }

            if (!String.IsNullOrEmpty(queryObj.Example))
            {
                query = query.Where(w => w.Example.Contains(queryObj.Example));
            }

            if (queryObj.IsLearned.HasValue)
            {
                query = query.Where(w => w.IsLearned == queryObj.IsLearned.Value);
            }

            return query;
        }
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
            {
                return query;
            }

            if (queryObj.IsSortAscending)
            {
                return query = query.OrderBy(columnsMap[queryObj.SortBy]);
            }
            else
            {
                return query = query.OrderByDescending(columnsMap[queryObj.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.WithoutPagination == true)
            {
                return query;
            }

            if (queryObj.Page <= 0)
            {
                queryObj.Page = 1;
            }

            if (queryObj.PageSize <= 0)
            {
                queryObj.PageSize = 10;
            }


            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }

    }
}
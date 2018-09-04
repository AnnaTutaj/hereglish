using System;
using System.IO;
using System.Threading.Tasks;
using Hereglish.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Hereglish.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoStorage photoStorage;
        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this.photoStorage = photoStorage;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Photo> UploadPhoto(Word word, IFormFile file, string uploadsFolderPath)
        {
            var fileName = await photoStorage.StorePhoto(uploadsFolderPath, file);

            var photo = new Photo { FileName = fileName };
            word.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return photo;
        }
    }
}
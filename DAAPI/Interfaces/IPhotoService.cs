using CloudinaryDotNet.Actions;

namespace DAAPI.Interfaces
{
    public interface IPhotoService
    {
         Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

         Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
namespace MotorcycleRentalSystem.Domain.Services;

public interface ISimpleStorageService
{
    Task<string> UploadImagemAsync(byte[] imagemBytes, string filename, string bucketName);
}
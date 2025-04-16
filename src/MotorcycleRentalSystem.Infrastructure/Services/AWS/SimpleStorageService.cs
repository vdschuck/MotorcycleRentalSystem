using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using MotorcycleRentalSystem.Domain.Services;
using MotorcycleRentalSystem.Infrastructure.Configuration;

namespace MotorcycleRentalSystem.Infrastructure.Services.AWS;

public class SimpleStorageService : ISimpleStorageService
{
    private readonly IAmazonS3 _s3Client;

    public SimpleStorageService(IOptions<AWSOptions> awsOptions)
    {
        var awsConfig = new AmazonS3Config { RegionEndpoint = RegionEndpoint.GetBySystemName(awsOptions.Value.Region) };
        _s3Client = new AmazonS3Client(awsConfig);
    }

    public async Task<string> UploadImagemAsync(byte[] imagemBytes, string filename, string bucketName)
    {
        using var stream = new MemoryStream(imagemBytes);
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = filename,
            BucketName = bucketName,
            ContentType = "image/jpeg",
            CannedACL = S3CannedACL.Private
        };

        var transferUtility = new TransferUtility(_s3Client);
        await transferUtility.UploadAsync(uploadRequest);

        return $"https://{bucketName}.s3.amazonaws.com/{filename}";
    }
}
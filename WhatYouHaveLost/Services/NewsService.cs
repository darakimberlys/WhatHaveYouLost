using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.Services;

public class NewsService
{
    private readonly INewsRepository _newsRepository;
    
    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task HandleImages(string news)
    {
        var selected = await _newsRepository.GetNewsContent(news);
        
        var blobName = selected?.Image;

        BlobServiceClient blobServiceClient = new BlobServiceClient("connectionString");
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("containerName");

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        Response<BlobDownloadInfo> downloadResponse = await blobClient.DownloadAsync();
        BlobDownloadInfo blobDownloadInfo = downloadResponse.Value;

        using (var memoryStream = new MemoryStream())
        {
            await blobDownloadInfo.Content.CopyToAsync(memoryStream);
    
            // Use o conteúdo do arquivo aqui
            byte[] arquivoBytes = memoryStream.ToArray();
            // Faça o processamento necessário com os dados do arquivo
        }

    }
}
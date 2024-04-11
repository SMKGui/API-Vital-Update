﻿using Azure.Storage.Blobs;

namespace WebAPI.Utils.BlobStorage
{
    public static class AzureBlobStorageHelper
    {
        public static async Task<string> UploadImageBLobAsync(IFormFile arquivo, string stringConexao, string nomeContainer)
        {
			try
			{
                //verifica se existe um arquivo
                if (arquivo != null)
                {
                    //gera um nome único + extensão do arquivo
                    var blobName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(arquivo.FileName);

                    //cria uma instância do client Blob Service e passa a string de conexão
                    var blobServiceClient = new BlobServiceClient(stringConexao);

                    //obtem um container client usando o nome do container do blob
                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(nomeContainer);

                    //obtem um blob client usando o blob name
                    var blobClient = blobContainerClient.GetBlobClient(blobName);

                    //abre o fluxo de entrada do arquivo(foto)
                    using (var stream = arquivo.OpenReadStream())
                    {
                        //carrega o arquivo(foto) para o blob storage de forma assíncrona
                        await blobClient.UploadAsync(stream, true);
                    }
                    //retorna a uri do blob como uma string
                    return blobClient.Uri.ToString();
                }
                else
                {
                    //retorna a uri de uma imagem padrao caso nenhum arquivo seja enviado
                    return "https://blobvitalhubg6guiluiz.blob.core.windows.net/containervitalhubguiluiz/white-sedan-sport-car-driving-bridge.jpg";
                }
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}

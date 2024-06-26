﻿using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Org.BouncyCastle.Security;

namespace WebAPI.Utils.OCR
{
    public class OcrService
    {
        private readonly string _subscriptkey = "d52dc4efb9ec45be9ca1c10781c56d08";

        private readonly string _endpoint = "https://cvvvitalhubg6gui.cognitiveservices.azure.com/";

        public async Task<string> RecognizeTextAsync(Stream imageStream)
        {
            try
            {
                var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptkey))
                {
                    Endpoint = _endpoint
                };

                var ocrResult = await client.RecognizePrintedTextInStreamAsync(true, imageStream);

                return ProcessRecognitionResult(ocrResult);
            }
            catch (Exception ex)
            {
                return "Erro ao reconhecer o texto: " + ex.Message;
            }

        }

        private static string ProcessRecognitionResult(OcrResult result)
        {
            try
            {
                string recognizedText = "";

                foreach (var region in result.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            recognizedText += word.Text + " ";
                        }
                        recognizedText += "\n";
                    }
                }

                return recognizedText;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

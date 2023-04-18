using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Rest;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CorrectEssayV2;

public class OCRHelper
{
    private readonly string subscriptionKey = "f00b8bbd9999486cb6ecf5f6ec2c77a6";
    private readonly string endpoint = "https://ocrforlee.cognitiveservices.azure.com/";

    private const int numberOfCharsInOperationId = 36;
    private const int imageMaxSize = 4 * 1024 * 1024;
    private const long imageMaxQuality = 50L;
    private const string CompressedDirectoryName = "Compressed";

    private readonly ComputerVisionClient _client;

    public OCRHelper()
    {
        _client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey)) { Endpoint = endpoint };
    }

    public async Task<string> ImageToText(string filePath)
    {
        var stream = new FileStream(filePath, FileMode.Open);
        var response = await _client.ReadInStreamWithHttpMessagesAsync(stream, language: "zh-Hans");
        var operationLocation = response.Headers.OperationLocation;
        var operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

        HttpOperationResponse<ReadOperationResult> results;

        do
        {
            results = await _client.GetReadResultWithHttpMessagesAsync(Guid.Parse(operationId));
        }
        while (results.Body.Status is OperationStatusCodes.Running or OperationStatusCodes.NotStarted);

        var textResults = string.Join("\n",
            results.Body.AnalyzeResult.ReadResults.Select(x => string.Join(string.Empty, x.Lines.Select(y => y.Text))));

        // to add paragraph detect in the future version
        // https://learn.microsoft.com/zh-cn/azure/applied-ai-services/form-recognizer/how-to-guides/use-sdk-rest-api?view=form-recog-3.0.0&preserve-view=true%3Fpivots%3Dprogramming-language-csharp&tabs=windows&pivots=programming-language-csharp#read-model

        return textResults;
    }

    public async Task<List<string>> DirectoryToText(string fileDirectory)
    {
        var results = new List<string>();
        var files = Directory.GetFiles(fileDirectory);
        var sortedFiles = from file in files
                          let fileInfo = new FileInfo(file)
                          where fileInfo.Extension != ".txt"
                          orderby fileInfo.LastWriteTime ascending
                          select file;

        foreach (var file in sortedFiles)
        {
            var newFile = PreProcess(file);
            results.Add(await ImageToText(newFile));
        }

        return results;
    }

    private string PreProcess(string file)
    {
        var fileInfo = new FileInfo(file);
        if (fileInfo.Length > imageMaxSize)
        {
            var currentDirectory = Path.GetDirectoryName(fileInfo.FullName) ?? throw new Exception("Compress file error");
            var outPutDirectory = Path.Combine(currentDirectory, CompressedDirectoryName);

            if (!Directory.Exists(outPutDirectory))
            {
                Directory.CreateDirectory(outPutDirectory);
            }

            var imageByte = CompressImage(file);
            var outputPath = Path.Combine(outPutDirectory, Path.GetFileName(file));
            var ms = new MemoryStream(imageByte);
            var image = Image.FromStream(ms);
            image.Save(outputPath);
            ms.Close();
            ms.Dispose();
            image.Dispose();

            return outputPath;
        }

        return file;
    }

    private byte[] CompressImage(string imagePath)
    {
        using (var fileStream = new FileStream(imagePath, FileMode.Open))
        {
            using (var img = Image.FromStream(fileStream))
            {
                using (var bitmap = new Bitmap(img))
                {
                    var codecInfo = GetEncoder(img.RawFormat);
                    var myEncoder = Encoder.Quality;
                    var myEncoderParameters = new EncoderParameters(1);
                    var myEncoderParameter = new EncoderParameter(myEncoder, imageMaxQuality);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, codecInfo, myEncoderParameters);
                        myEncoderParameters.Dispose();
                        myEncoderParameter.Dispose();
                        return ms.ToArray();
                    }
                }
            }
        }
    }

    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        var codecs = ImageCodecInfo.GetImageDecoders();
        return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
    }
}

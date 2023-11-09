using System.IO;
using DynamoTask.Services.Contracts;
using Newtonsoft.Json;

public class JsonFileService : IJsonFileService
{
    public class JsonFileServiceResult
    {
        public string Error { get; set; } = string.Empty;
    }

    public JsonFileServiceResult SaveJsonFile(string json, string directoryPath, string fileName)
    {
        var result = new JsonFileServiceResult();

        try
        {
            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, fileName);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
        }

        return result;
    }

    public string FormatFileName(string inputFileName)
    {
        string newFileName = Path.ChangeExtension(inputFileName, ".json");

        return newFileName;
    }
}
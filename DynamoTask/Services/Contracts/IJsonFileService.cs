using static JsonFileService;

namespace DynamoTask.Services.Contracts
{
    public interface IJsonFileService
    {
        JsonFileServiceResult SaveJsonFile(string json, string directoryPath, string fileName);

        string FormatFileName(string inputFileName);
    }
}

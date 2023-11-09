using Microsoft.AspNetCore.Mvc;
using static DynamoTask.Services.Converter;

namespace DynamoTask.Services.Contracts
{
    public interface IConverter
    {
        JsonResultModel ConvertXmlToJson(IFormFile file);
    }
}

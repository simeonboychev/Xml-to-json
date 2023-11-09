using DynamoTask.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DynamoTask.ApiControllers
{
    [Route("api/converter")]
    [ApiController]
    public class ConverterApiController : ControllerBase
    {
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IJsonFileService _jsonFileService;

        public ConverterApiController(IConverter converter, IWebHostEnvironment hostingEnvironment, IJsonFileService jsonFileService)
        {
            _converter = converter;
            _hostingEnvironment = hostingEnvironment;
            _jsonFileService = jsonFileService;
        }

        [HttpPost("xml-to-json")]
        public IActionResult UploadFile([FromForm]string fileName, IFormFile file)
        {
            var jsonResult = _converter.ConvertXmlToJson(file);

            if (!string.IsNullOrEmpty(jsonResult.Error))
            {
                return BadRequest(jsonResult.Error);
            }

            var fileNameJson = _jsonFileService.FormatFileName(fileName);

            string directoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, "JsonConvertedFiles");

            var jsonFileServiceResult = _jsonFileService.SaveJsonFile(jsonResult.Json, directoryPath, fileNameJson);
            if (!string.IsNullOrEmpty(jsonFileServiceResult.Error))
            {
                return BadRequest(jsonFileServiceResult.Error);
            }

            return new JsonResult(new { FileName = fileNameJson, JsonData = jsonResult.Json });
        }
    }
}

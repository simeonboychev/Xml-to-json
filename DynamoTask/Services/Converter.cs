using DynamoTask.Services.Contracts;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DynamoTask.Services
{
    public class Converter : IConverter 
    {
        public class JsonResultModel
        {
            public string Json { get; set; } = string.Empty;
            public string Error { get; set; } = string.Empty;
        }

        public Converter()
        {
            
        }

        public JsonResultModel ConvertXmlToJson(IFormFile file)
        {
            var result = new JsonResultModel();

            if (file != null)
            {
                try
                {
                    using (var streamReader = new StreamReader(file.OpenReadStream()))
                    {
                        string xmlContent = streamReader.ReadToEnd();

                        XDocument xmlDocument = XDocument.Parse(xmlContent);

                        string json = JsonConvert.SerializeXNode(xmlDocument, Formatting.Indented);

                        result.Json = json;
                        
                    }
                }
                catch (Exception ex)
                {
                    result.Error = $"Error: {ex.Message}";
                }
            }
            else
            {
                result.Error = "No file provided";
            }

            return result;
        }
    }
}

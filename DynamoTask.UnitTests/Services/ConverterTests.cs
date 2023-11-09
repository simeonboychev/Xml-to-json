using DynamoTask.Services;
using Microsoft.AspNetCore.Http;

namespace DynamoTask.UnitTests.Services
{
    public class ConverterTests
    {
        [Fact]
        public void ConvertXmlToJson_ValidFile_ConvertsSuccessfully()
        {
            // Arrange
            var service = new Converter();
            string xmlContent = "<root><item>Test</item></root>";
            var fileMock = new FormFile(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlContent)), 0, xmlContent.Length, "file", "sample.xml");

            // Act
            var result = service.ConvertXmlToJson(fileMock);

            // Assert
            Assert.Equal(result.Error, string.Empty);
            Assert.NotNull(result.Json);
        }

        [Fact]
        public void ConvertXmlToJson_NullFile_ReturnsError()
        {
            // Arrange
            var service = new Converter();
            IFormFile file = null;

            // Act
            var result = service.ConvertXmlToJson(file);

            // Assert
            Assert.NotNull(result.Error);
            Assert.Equal(result.Json, string.Empty);
            Assert.Equal("No file provided", result.Error);
        }

        [Fact]
        public void ConvertXmlToJson_InvalidXml_ReturnsError()
        {
            // Arrange
            var service = new Converter();
            string invalidXml = "<root>";
            var fileMock = new FormFile(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(invalidXml)), 0, invalidXml.Length, "file", "invalid.xml");

            // Act
            var result = service.ConvertXmlToJson(fileMock);

            // Assert
            Assert.NotNull(result.Error);
            Assert.Equal(result.Json, string.Empty);
        }
    }
}

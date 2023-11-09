namespace DynamoTask.UnitTests.Services
{
    public class JsonFileServiceTests
    {
        [Fact]
        public void SaveJsonFile_WithValidData_CreatesFile()
        {
            // Arrange
            var service = new JsonFileService();
            string json = "{\"key\": \"value\"}";
            string directoryPath = "path/to/test-directory";
            string fileName = "test-file.json";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                // Act
                service.SaveJsonFile(json, directoryPath, fileName);

                // Assert
                Assert.True(File.Exists(filePath), "File should exist.");
                Assert.Equal(json, File.ReadAllText(filePath));
            }
            finally
            {
                // Cleanup: Delete the test file if it was created
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        [Fact]
        public void SaveJsonFile_WithInvalidData_HandlesException()
        {
            // Arrange
            var service = new JsonFileService();
            string json = "invalid-json"; 
            string directoryPath = "J://path/to/test-directory"; // Invalid Path
            string fileName = "test-file.json";
            string filePath = Path.Combine(directoryPath, fileName);

            // Act 
            var result = service.SaveJsonFile(json, directoryPath, fileName);

            // Assert
            Assert.NotEqual(result.Error, string.Empty);
            Assert.False(File.Exists(filePath), "File should not exist.");
        }
    }
}

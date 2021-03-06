using System.IO;
using FluentAssertions;
using Microsoft.CodeAnalysis.Sarif;
using Microsoft.CodeAnalysis.Sarif.Multitool;
using Moq;
using Xunit;

namespace Sarif.Multitool.UnitTests
{
    public class ValidateCommandTests
    {
        // Regression test for Issue #1064, "ValidatorCommand fails when target file name contains a space"
        [Fact]
        public void ValidateCommand_AcceptsTargetFileWithSpaceInName()
        {
            // A minimal valid log file.
            string LogFileContents =
@"{
  ""$schema"": """ + SarifUtilities.SarifSchemaUri + @""",
  ""version"": ""2.0.0"",
  ""runs"": [
    {
      ""tool"": {
        ""name"": ""TestTool""
      },
      ""results"": []
    }
  ]
}";
            // A simple schema against which the log file successfully validates.
            // This way, we don't have to read the SARIF schema from disk to run this test.
            const string SchemaFileContents =
@"{
  ""$schema"": ""http://json-schema.org/draft-04/schema#"",
  ""type"": ""object""
}";
            // Here's the space:
            const string LogFileDirectory = @"c:\Users\John Smith\logs";

            const string LogFileName = "example.sarif";
            string logFilePath = Path.Combine(LogFileDirectory, LogFileName);

            const string SchemaFilePath = @"c:\schemas\SimpleSchemaForTest.json";

            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(x => x.DirectoryExists(LogFileDirectory)).Returns(true);
            mockFileSystem.Setup(x => x.GetDirectoriesInDirectory(It.IsAny<string>())).Returns(new string[0]);
            mockFileSystem.Setup(x => x.GetFilesInDirectory(LogFileDirectory, LogFileName)).Returns(new string[] { logFilePath });
            mockFileSystem.Setup(x => x.ReadAllText(logFilePath)).Returns(LogFileContents);
            mockFileSystem.Setup(x => x.ReadAllText(SchemaFilePath)).Returns(SchemaFileContents);

            var validateCommand = new ValidateCommand(mockFileSystem.Object);

            var options = new ValidateOptions
            {
                SchemaFilePath = SchemaFilePath,
                TargetFileSpecifiers = new string[] { logFilePath }
            };

            int returnCode = validateCommand.Run(options);
            returnCode.Should().Be(0);
        }
    }
}

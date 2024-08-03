using System.Reflection;
using Moq;

namespace RollingFileWriter.Tests;

public class FileWriterTests
{
    [Fact]
    public void Test_File_WasCreated_Successfully()
    {
        var writerMock = new Mock<RollingFileWriterService>().Object;

        const string testString = "TEST-STRING-TO-SAVE";
        writerMock.WriteData(testString);
        
        var path = Path.Combine("StoredData", $"Data_{DateTime.Today:dd_MM_yyyy}_0.json");
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

        var doFileExist = File.Exists(filePath);
        var fileContent = File.ReadAllText(filePath);
        
        Assert.True(doFileExist);
        //Assert.Contains because "append = true" adding automatically an additional space after every text write.
        Assert.Contains(testString, fileContent);
        
        File.Delete(filePath);
    }

    [Fact]
    public void Test_FileAppend_Was_Successful()
    {
        var writerMock = new Mock<RollingFileWriterService>().Object;

        var testStringCollection = new List<string>
        {
            "TEST-STRING-TO-SAVE-1",
            "TEST-STRING-TO-SAVE-2",
            "TEST-STRING-TO-SAVE-3"
        };

        foreach (var testString in testStringCollection)
        {
            writerMock.WriteData(testString);
        }
        
        var path = Path.Combine("StoredData", $"Data_{DateTime.Today:dd_MM_yyyy}_0.json");
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

        //FilePath = filePath;

        var doFileExist = File.Exists(filePath);
        var fileContent = File.ReadAllText(filePath);

        
        Assert.True(doFileExist);
        Assert.Contains("TEST-STRING-TO-SAVE-1", fileContent);
        Assert.Contains("TEST-STRING-TO-SAVE-2", fileContent);
        Assert.Contains("TEST-STRING-TO-SAVE-3", fileContent);
        
        File.Delete(filePath);
    }

    [Fact]
    public void Test_ParamaterCtor_FileSize_WasExceeded()
    {
        const string directory = "TestDir";
        const string fileName = "TestFile";
        const double exceedFileSize = 0.000049;
        
        var writerMock = new Mock<RollingFileWriterService>(directory, fileName, exceedFileSize).Object;
        // var writerMock = new Mock<RollingFileWriterService>().Object;
        
        var testString1 = "This is test string which exceed defined size declared in mocked ctor.";
        
        //Do first write to exceed mocked size
        writerMock.WriteData(testString1);
        
        var testString2 = "This is test string which should be written into file with incremented index.";
        
        //Create a new file with incremented index
        writerMock.WriteData(testString2);
        
        var path1 = Path.Combine(directory, $"{fileName}_0.json");
        var filePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path1);

        var doFileOneExist = File.Exists(filePath1);
        
        Assert.True(doFileOneExist);
        
        var path2 = Path.Combine(directory, $"{fileName}_0.json");
        var filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path2);

        var doFileTwoExist = File.Exists(filePath2);
        Assert.True(doFileTwoExist);
        
        File.Delete(filePath1);
        File.Delete(filePath2);
    }
    
    [Fact]
    public void Test_DefaultCtor_FileSize_WasExceeded()
    {
        const double exceedFileSize = 0.000049;
        
        var writer = new Mock<RollingFileWriterService>();
        var writerMock = writer.Object;

        var field = typeof(RollingFileWriterService).GetField("_maximalFileSizeInMb",
            BindingFlags.NonPublic | BindingFlags.Instance);
            
        field!.SetValue(writerMock, exceedFileSize);
        
        var testString1 = "This is test string which exceed defined size declared in mocked ctor.";
        
        //Do first write to exceed mocked size
        writerMock.WriteData(testString1);
        
        var testString2 = "This is test string which should be written into file with incremented index.";
        
        //Create a new file with incremented index
        writerMock.WriteData(testString2);
        
        var path1 = Path.Combine("StoredData", $"Data_{DateTime.Today:dd_MM_yyyy}_0.json");
        var filePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path1);

        var doFileOneExist = File.Exists(filePath1);
        
        Assert.True(doFileOneExist);
        
        var path2 = Path.Combine("StoredData", $"Data_{DateTime.Today:dd_MM_yyyy}_1.json");
        var filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path2);

        var doFileTwoExist = File.Exists(filePath2);
        Assert.True(doFileTwoExist);
        
        File.Delete(filePath1);
        File.Delete(filePath2);
    }
}
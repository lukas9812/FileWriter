namespace RollingFileWriter;

public class RollingFileWriterService : IRollingFileWriterService
{
    private string _currentFilePath;
    private  const string DirectoryPath = "BroadcastData";
    private readonly string _fileNamePrefix = $"Data_{DateTime.Today:dd_MM_yyyy}";
    private int _fileIndex;

    public RollingFileWriterService()
    {
        Directory.CreateDirectory(DirectoryPath); // Ensure the directory exists
        CreateNewFile();
    }
    
    public void WriteData(string data) 
    {
        if (IsFileSizeExceeded())
            CreateNewFile();

        using var writer = new StreamWriter(_currentFilePath, append: true);
        writer.WriteLine(data);
    }

    private void CreateNewFile()
    {
        _currentFilePath = Path.Combine(DirectoryPath, $"{_fileNamePrefix}_{_fileIndex}.json");
        _fileIndex++;
    }

    private bool IsFileSizeExceeded()
    {
        if (!File.Exists(_currentFilePath))
            return false;

        var fileInfo = new FileInfo(_currentFilePath);
        
        //ToDo: Add to configuration (app.config)
        return fileInfo.Length >= ConvertMbToBytes(500);
    }
    
    /// <summary>
    /// Converts megabytes to bytes.
    /// </summary>
    /// <param name="megabytes">The number of megabytes to convert.</param>
    /// <returns>The equivalent number of bytes.</returns>
    private long ConvertMbToBytes(double megabytes)
    {
        const long bytesPerMegabyte = 1024 * 1024; // 1 MB = 1024 * 1024 bytes
        return (long)(megabytes * bytesPerMegabyte);
    }
}
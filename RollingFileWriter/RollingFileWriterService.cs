namespace RollingFileWriter;

public class RollingFileWriterService : IRollingFileWriterService
{
    private static string _directoryPath = "StoredData";
    private string _currentFilePath;
    private readonly string _hardcodedFileName = $"Data_{DateTime.Today:dd_MM_yyyy}";
    private int _fileIndex;
    private readonly double _maximalFileSizeInMb;
    private readonly string _customCurrentFilePath;

    public RollingFileWriterService()
    {
        _customCurrentFilePath = _hardcodedFileName;
        _maximalFileSizeInMb = 500;
        Directory.CreateDirectory(_directoryPath);
        SetupFileName(string.Empty);
    }

    public RollingFileWriterService(string directoryPath, string fileName, double maximalFileSizeInMb = 500)
    {
        _maximalFileSizeInMb = maximalFileSizeInMb;
        _currentFilePath = fileName;
        _customCurrentFilePath = fileName;
        _directoryPath = directoryPath;
        Directory.CreateDirectory(_directoryPath); // Ensure the directory exists
        SetupFileName(fileName);
    }
    
    private void SetupFileName(string customFileName)
    {
        _currentFilePath = Path.Combine(_directoryPath,
            customFileName == string.Empty
                ? $"{_hardcodedFileName}_{_fileIndex}.json"
                : $"{customFileName}_{_fileIndex}.json");

        _fileIndex++;
    }
    
    public void WriteData(string data) 
    {
        if (IsFileSizeExceeded())
            SetupFileName(_customCurrentFilePath);

        using var writer = new StreamWriter(_currentFilePath, append: true);
        writer.WriteLine(data);
    }
    
    public void WriteData(object dataAsObject) 
    {
        if (IsFileSizeExceeded())
            SetupFileName(_customCurrentFilePath);

        using var writer = new StreamWriter(_currentFilePath, append: true);
        writer.WriteLine(dataAsObject);
    }

    private bool IsFileSizeExceeded()
    {
        if (!File.Exists(_currentFilePath))
            return false;

        var fileInfo = new FileInfo(_currentFilePath);
        
        return fileInfo.Length >= ConvertMbToBytes(_maximalFileSizeInMb);
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
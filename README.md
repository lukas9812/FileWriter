# NuGet usage:

NuGet: https://www.nuget.org/packages/RollingFileWriter

1. Reference NuGet package via using in your class:
 ```cs
using RollingFileWriter;
 ```
2. Choose among default or parameter constructor

- **Default ctor:** Path is automatically set to "StoredData/Data_{DateTime.Today:dd_MM_yyyy}" and maximal file size is set to 500 MB. Values are not possible to configure.
 ```cs
public RollingFileWriterService()
    {
        _maximalFileSizeInMb = 500;
        Directory.CreateDirectory(_directoryPath);
        SetupFileName(string.Empty);
    }
 ```
- **Parameter ctor:** Fully configurable values. If you'll not fill last parameter **maximalFileSizeInMb** maximal file size will be automatically set to 500 MB.
```cs
public RollingFileWriterService(string directoryPath, string fileName, int maximalFileSizeInMb = 500)
    {
        _maximalFileSizeInMb = maximalFileSizeInMb;
        _directoryPath = directoryPath;
        Directory.CreateDirectory(_directoryPath);
        SetupFileName(fileName);
    }
 ```
3. From created instance call method with string incoming parameter:

```cs
WriteData(string data)
```

4. All written data you can find in **/bin** folder under mentioned folder name.

# Whole solution structure

The aim of this project is to develop an application that saves files with a preset size limit, specified in the appsettings.json file. If a file exceeds this size limit (in MB), a new file will be created with an incremented index added to the end of the file name (e.g., Data_26_07_2024_1, Data_26_07_2024_2, etc.).

Additionally, the application offers the option to filter specific properties to prevent the storage of unnecessary data.

Architecture:
![image](https://github.com/user-attachments/assets/fa4ceab9-82f8-425f-a362-0120d88e9dc9)


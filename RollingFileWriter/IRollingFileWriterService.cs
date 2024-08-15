namespace RollingFileWriter;

public interface IRollingFileWriterService
{
    void WriteData(string data);

    void WriteData(object dataAsObject, bool xml);
}
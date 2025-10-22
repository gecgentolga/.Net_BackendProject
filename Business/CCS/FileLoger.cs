namespace Business.CCS;

public class FileLoger:ILogger
{
    public void Log()
    {
        Console.WriteLine("Dosyaya loglandı");
    }
}
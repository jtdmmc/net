namespace DependencyInjection.Example;
using Microsoft.Extensions.Logging;
public class MessageWriter : IMessageWriter
{
    public void Write(string message)
    {
        Console.WriteLine($"MessageWriter.Write(message: \"{message}\")");
    }
}
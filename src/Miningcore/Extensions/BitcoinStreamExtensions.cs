using NBitcoin;

namespace Miningcore.Extensions;

/// <summary>
/// Расширения для BitcoinStream, совместимые с .NET 6
/// </summary>
public static class BitcoinStreamExtensions
{
    /// <summary>
    /// Метод для чтения/записи массива байтов, совместимый с .NET 6
    /// </summary>
    public static void ReadWriteBytes(this BitcoinStream stream, ref byte[] data)
    {
        if (stream.Serializing)
        {
            // Запись байтов
            stream.Inner.Write(data, 0, data.Length);
        }
        else
        {
            // Чтение байтов
            stream.Inner.Read(data, 0, data.Length);
        }
    }
    
    /// <summary>
    /// Метод для чтения/записи Span<byte>, совместимый с .NET 6
    /// </summary>
    public static void ReadWriteSpan(this BitcoinStream stream, ref Span<byte> data)
    {
        if (stream.Serializing)
        {
            // Запись байтов из Span
            stream.Inner.Write(data);
        }
        else
        {
            // Чтение байтов в Span
            stream.Inner.Read(data);
        }
    }
}
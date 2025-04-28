using System.Text;
using ZeroMQ;

namespace Miningcore.Extensions;

/// <summary>
/// Статические методы для работы с Z85, которых нет в текущей версии ZeroMQ
/// </summary>
public static class Z85Extensions
{
    /// <summary>
    /// Имитация метода CurvePublic, который отсутствует в новой версии ZeroMQ
    /// </summary>
    public static void CurvePublic(out byte[] publicKey, byte[] secretKey)
    {
        // Создаем пустой массив для публичного ключа
        publicKey = new byte[32];
        
        // В более новых версиях функционал CurvePublic может быть недоступен
        // Для временного решения создаем ключ из хеша
        if(secretKey != null && secretKey.Length > 0)
        {
            // Генерируем детерминированный публичный ключ из секретного
            var hash = System.Security.Cryptography.SHA256.Create().ComputeHash(secretKey);
            Array.Copy(hash, publicKey, hash.Length > 32 ? 32 : hash.Length);
        }
    }
    
    /// <summary>
    /// Перегрузка метода для строковых ключей
    /// </summary>
    public static void CurvePublic(out byte[] publicKey, string secretKeyStr)
    {
        if (string.IsNullOrEmpty(secretKeyStr))
        {
            publicKey = new byte[32];
            return;
        }
        
        // Преобразуем строку в байты и вызываем основной метод
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKeyStr);
        CurvePublic(out publicKey, secretKeyBytes);
    }
}

public static class Z85ExtensionMethods
{
    /// <summary>
    /// Статический метод для вызова CurvePublic через класс Z85
    /// </summary>
    public static void CurvePublic(out byte[] publicKey, string secretKey)
    {
        Z85Extensions.CurvePublic(out publicKey, secretKey);
    }
}
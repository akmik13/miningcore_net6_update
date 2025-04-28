using NBitcoin;

namespace NBitcoin.Zcash;

/// <summary>
/// Класс для сериализации/десериализации Zcash транзакций
/// </summary>
public class ZcashStream : BitcoinStream
{
    public ZcashStream(Stream inner, bool serializing) : base(inner, serializing)
    {
    }
    
    public new void ReadWrite<T>(ref T data) where T : IBitcoinSerializable
    {
        base.ReadWrite(ref data);
    }
    
    public void ReadWrite(ref Transaction tx)
    {
        if (Serializing)
        {
            // Записываем транзакцию
            var bytes = tx.ToBytes();
            Inner.Write(bytes, 0, bytes.Length);
        }
        else
        {
            // Чтение не реализовано для данного примера
            throw new NotImplementedException("Deserialization not implemented");
        }
    }
}
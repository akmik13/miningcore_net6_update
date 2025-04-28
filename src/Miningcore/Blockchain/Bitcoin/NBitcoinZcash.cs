using NBitcoin;

namespace NBitcoin.Zcash;

/// <summary>
/// Класс транзакции Zcash, совместимый с .NET 6
/// </summary>
public class ZcashTransaction : Transaction
{
    protected bool fOverwintered;
    protected uint nVersionGroupId;
    
    // Конструктор, совместимый с новой версией NBitcoin
    public ZcashTransaction() : base()
    {
    }
    
    // Фиктивные поля и методы для совместимости
    public static ZcashTransaction FromBytes(byte[] bytes)
    {
        var tx = new ZcashTransaction();
        tx.FromBytes(bytes);
        return tx;
    }
}
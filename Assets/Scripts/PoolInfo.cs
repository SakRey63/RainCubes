public struct PoolInfo
{
    public PoolInfo(int amountAllTime, int poolCountAll, int poolCountActive)
    {
        AmountAllTime = amountAllTime;
        PoolCountAll = poolCountAll;
        PoolCountActive = poolCountActive;
    }
    
    public int AmountAllTime { get; private set; }
    public int PoolCountAll { get; private set; }
    public int PoolCountActive { get; private set; }
}
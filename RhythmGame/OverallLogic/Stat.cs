public struct Stat
{
    public float MaxAmount { get; private set; }
    public float MinAmount { get; private set; }
    public float CurrentAmount { get; private set; }
    public Stat(float amount, float maxAmount, float minAmount = 0)
    {
        MaxAmount = maxAmount;
        MinAmount = minAmount;
        CurrentAmount = amount;
    }
    public Stat(float amount, float minAmount = 0)
    {
        MaxAmount = amount;
        MinAmount = minAmount;
        CurrentAmount = amount;
    }
    public void ChangeAmount(float amount)
    {
        CurrentAmount += amount;
        Refresh();
    }
    public void SetMin(float newMin)
    {
        MinAmount = newMin;
        Refresh();
    }
    public void SetMax(float newMax)
    {
        MaxAmount = newMax;
        Refresh();
    }
    private void Refresh()
    {
        if (CurrentAmount > MaxAmount) CurrentAmount = MaxAmount;
        if (CurrentAmount < MinAmount) CurrentAmount = MinAmount;
    }
}

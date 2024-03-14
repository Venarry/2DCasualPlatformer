using System;

public class HealthModel : IHealthProvider
{
    public event Action HealthChanged;
    public event Action HealthOver;

    public HealthModel(float maxValue)
    {
        MaxValue = maxValue;
        Value = maxValue;
    }

    public HealthModel(float maxValue, float value)
    {
        MaxValue = maxValue;
        Value = value;
    }

    public float Value { get; private set; }
    public float MaxValue { get; private set; }

    public float HealthNormalized => (float)Value / MaxValue;
    public float Health => Value;
    public double RoundedHealth => Math.Ceiling(Value);
    public float MaxHealth => MaxValue;
    public double RoundedMaxHealth => Math.Ceiling(MaxValue);

    public void Restore()
    {
        Value = MaxValue;
        HealthChanged?.Invoke();
    }

    public void SetHealth(float value)
    {
        if (value < 0)
        {
            value = 0;
        }

        Value = value;
        HealthChanged?.Invoke();

        if (Value <= 0)
            HealthOver?.Invoke();
    }

    public void SetMaxHealth(float value)
    {
        if (value < 1)
            value = 1;

        MaxValue = value;
    }

    public float TakeDamageWithOverflowValue(float value)
    {
        if (Value <= 0)
            return value;

        if (value < 0)
            value = 0;

        Value -= value;

        if (Value <= 0)
        {
            float overflowValue = Value;
            Value = 0;

            HealthOver?.Invoke();
            return overflowValue;
        }

        HealthChanged?.Invoke();
        return 0;
    }

    public void Add(float value)
    {
        if(value < 0)
            value = 0;

        Value += value;

        if(Value > MaxValue)
            Value = MaxValue;

        HealthChanged?.Invoke();
    }
}

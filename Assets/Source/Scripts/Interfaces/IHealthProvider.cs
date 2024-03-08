using System;

public interface IHealthProvider
{
    public event Action HealthChanged;
    public event Action HealthOver;

    public float HealthNormalized { get; }
    public float Health { get; }
    public double RoundedHealth { get; }
    public float MaxHealth { get; }
    public double RoundedMaxHealth { get; }
}

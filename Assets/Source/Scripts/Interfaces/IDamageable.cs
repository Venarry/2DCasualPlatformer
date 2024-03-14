using System;

public interface IDamageable : ITarget
{
    public event Action<IDamageable> HealthOver;
    public float TakeDamageWithOverflowValue(float value);
}

using System;

public class HealthPresenter : IHealthProvider
{
    private readonly HealthModel _healthModel;

    public HealthPresenter(HealthModel healthModel)
    {
        _healthModel = healthModel;
        _healthModel.HealthChanged += OnHealthChange;
        _healthModel.HealthOver += OnHealthOver;
    }

    ~HealthPresenter()
    {
        _healthModel.HealthChanged -= OnHealthChange;
        _healthModel.HealthOver -= OnHealthOver;
    }

    public event Action HealthChanged;
    public event Action HealthOver;

    public float HealthNormalized => (float)_healthModel.Value / _healthModel.MaxValue;
    public float Health => _healthModel.Value;
    public double RoundedHealth => Math.Ceiling(_healthModel.Value); 
    public float MaxHealth => _healthModel.MaxValue;
    public double RoundedMaxHealth => Math.Ceiling(_healthModel.MaxValue); 

    public void Add(float value)
    {
        _healthModel.Add(value);
    }

    public void Restore()
    {
        _healthModel.Restore();
    }

    public void SetHealth(float value)
    {
        _healthModel.SetHealth(value);
    }

    public void SetMaxHealth(float value)
    {
        _healthModel.SetMaxHealth(value);
    }

    public void TakeDamage(float value)
    {
        _healthModel.TakeDamage(value);
    }

    private void OnHealthChange()
    {
        HealthChanged?.Invoke();
        //UnityEngine.Debug.Log($"Health changed - current health {Health}");
    }

    private void OnHealthOver()
    {
        HealthOver?.Invoke();
        //UnityEngine.Debug.Log($"Health over");
    }
}

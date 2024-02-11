using System;

public class HealthPresenter
{
    private readonly HealthModel _healthModel;
    private readonly DeathHandler _deathHandler;

    public event Action HealthChanged;
    public event Action HealthOver;

    public HealthPresenter(HealthModel healthModel)
    {
        _healthModel = healthModel;
    }

    public HealthPresenter(HealthModel healthModel, DeathHandler deathHandler = null)
    {
        _healthModel = healthModel;
        _deathHandler = deathHandler;
    }

    public float HealthNormalized => (float)_healthModel.Value / _healthModel.MaxValue;
    public int Health => _healthModel.Value;
    public int MaxHealth => _healthModel.MaxValue;

    public void Enable()
    {
        _healthModel.HealthChanged += OnHealthChanged;
        _healthModel.HealthOver += OnHealthOver;
        _deathHandler?.Enable();
    }

    public void Disable()
    {
        _healthModel.HealthChanged -= OnHealthChanged;
        _healthModel.HealthOver -= OnHealthOver;
        _deathHandler?.Disable();
    }

    public void Add(int value)
    {
        _healthModel.Add(value);
    }

    public void Restore()
    {
        _healthModel.Restore();
    }

    public void SetHealth(int value)
    {
        _healthModel.SetHealth(value);
    }

    public void SetMaxHealth(int value)
    {
        _healthModel.SetMaxHealth(value);
    }

    public void TakeDamage(int value)
    {
        _healthModel.TakeDamage(value);
    }

    private void OnHealthChanged()
    {
        HealthChanged?.Invoke();
        // ihealthbar?.Change(count);
    }

    private void OnHealthOver()
    {
        HealthOver?.Invoke();
    }
}

using System;

public class HealthPresenter
{
    private readonly HealthModel _healthModel;
    private IHealthView _healthView;

    public event Action HealthChanged;
    public event Action HealthOver;

    public HealthPresenter(HealthModel healthModel)
    {
        _healthModel = healthModel;
    }

    public void Init(IHealthView healthView)
    {
        _healthView = healthView;
        ShowHealth();
    }

    public float HealthNormalized => (float)_healthModel.Value / _healthModel.MaxValue;
    public int Health => _healthModel.Value;
    public int MaxHealth => _healthModel.MaxValue;

    public void Enable()
    {
        _healthModel.HealthChanged += OnHealthChange;
        _healthModel.HealthOver += OnHealthOver;
    }

    public void Disable()
    {
        _healthModel.HealthChanged -= OnHealthChange;
        _healthModel.HealthOver -= OnHealthOver;
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

    private void ShowHealth()
    {
        _healthView?.OnHealthChange(_healthModel.Value, _healthModel.MaxValue);
    }

    private void OnHealthChange()
    {
        ShowHealth();
        HealthChanged?.Invoke();
        UnityEngine.Debug.Log($"Health changed - current health {Health}");
    }

    private void OnHealthOver()
    {
        HealthOver?.Invoke();
        UnityEngine.Debug.Log($"Health over");
    }
}

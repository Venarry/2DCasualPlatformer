using System;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    private HealthModel _healthModel;

    protected IHealthProvider HealthProvider => _healthModel;

    protected void SetModel(
        HealthModel healthModel)
    {
        _healthModel = healthModel;
        _healthModel.HealthChanged += OnHealthChange;
        _healthModel.HealthOver += OnHealthOver;
    }

    private void OnDestroy()
    {
        _healthModel.HealthChanged -= OnHealthChange;
        _healthModel.HealthOver -= OnHealthOver;
    }

    protected virtual void InitViews()
    {
    }

    protected virtual void OnHealthChange()
    {
    }

    protected virtual void OnHealthOver()
    {
    }

    public void TakeDamage(float value)
    {
        _healthModel.TakeDamage(value);
    }

    public void Heal(float value)
    {
        _healthModel.Add(value);
    }
}

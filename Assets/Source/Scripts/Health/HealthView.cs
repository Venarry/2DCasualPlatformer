using System;
using UnityEngine;

public class HealthView : MonoBehaviour, IDamageable, IHealable
{
    private HealthModel _healthModel;
    private int _teamIndex;

    public event Action<IDamageable> HealthOver;

    protected IHealthProvider HealthProvider => _healthModel;
    public int TeamIndex => _teamIndex;

    public Vector3 Position => transform.position;

    protected void InitBaseView(
        HealthModel healthModel,
        int teamIndex)
    {
        _healthModel = healthModel;
        _teamIndex = teamIndex;

        _healthModel.HealthChanged += OnHealthChange;
        _healthModel.HealthOver += OnHealthOver;
    }

    private void OnDestroy()
    {
        _healthModel.HealthChanged -= OnHealthChange;
        _healthModel.HealthOver -= OnHealthOver;
    }

    protected virtual void OnHealthChange()
    {
    }

    protected virtual void OnHealthOver()
    {
        HealthOver?.Invoke(this);
    }

    public float TakeDamageWithOverflowValue(float value) =>
        _healthModel.TakeDamageWithOverflowValue(value);

    public void Heal(float value)
    {
        _healthModel.Add(value);
    }
}

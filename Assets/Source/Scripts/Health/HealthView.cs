using System;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    private HealthPresenter _healthPresenter;

    public void Init(
        HealthPresenter healthPresenter)
    {
        _healthPresenter = healthPresenter;
    }

    public void TakeDamage(float value)
    {
        _healthPresenter.TakeDamage(value);
    }

    public void Heal(float value)
    {
        _healthPresenter.Add(value);
    }
}

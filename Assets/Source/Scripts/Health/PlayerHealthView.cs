using System;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour, IHealable, IDamageable
{
    private HealthPresenter _healthPresenter;
    private bool _isInitialized;

    public void Init(
        HealthPresenter healthPresenter)
    {
        gameObject.SetActive(false);

        _healthPresenter = healthPresenter;
        _isInitialized = true;

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            return;

        _healthPresenter.Enable();
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _healthPresenter.Disable();
    }

    public void TakeDamage(int value)
    {
        _healthPresenter.TakeDamage(value);
    }

    public void Heal(int value)
    {
        _healthPresenter.Add(value);
    }
}

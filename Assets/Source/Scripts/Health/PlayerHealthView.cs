using System;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour, IHealable, IDamageable
{
    private HealthPresenter _healthPresenter;
    // private HealthBar _healthBar;
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
        _healthPresenter.HealthChanged += OnHealthChange;
        _healthPresenter.HealthOver += OnHealthOver;
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _healthPresenter.Disable();
        _healthPresenter.HealthChanged -= OnHealthChange;
        _healthPresenter.HealthOver -= OnHealthOver;
    }

    public void TakeDamage(int value)
    {
        _healthPresenter.TakeDamage(value);
    }

    public void Heal(int value)
    {
        _healthPresenter.Add(value);
    }

    private void OnHealthChange()
    {
        Debug.Log($"Health changed - current health {_healthPresenter.Health}");
        //_healthBar
    }

    private void OnHealthOver()
    {
        Debug.Log($"Health over");
    }
}

public class DeathHandler
{
    private HealthPresenter _healthPresenter;

    public void Enable()
    {
        _healthPresenter.HealthOver += OnHealthOver;
    }

    public void Disable()
    {
        _healthPresenter.HealthOver -= OnHealthOver;
    }

    private void OnHealthOver()
    {
        throw new NotImplementedException();
    }
}

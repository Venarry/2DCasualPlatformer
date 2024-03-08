using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthView : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private IHealthProvider _healthProvider;

    public void Init(IHealthProvider healthProvider)
    {
        _healthProvider = healthProvider;
        _healthProvider.HealthChanged += OnHealthChange;

        OnHealthChange();
    }

    private void OnDestroy()
    {
        _healthProvider.HealthChanged -= OnHealthChange;
    }

    private void OnHealthChange()
    {
        _healthBar.fillAmount = _healthProvider.HealthNormalized;
    }
}

using System;
using UnityEngine;

public class EnemyDeathView : MonoBehaviour
{
    private HealthModel _healthModel;
    private TargetsProvider _targetsProvider;

    public void Init(HealthModel healthModel, TargetsProvider targetsProvider)
    {
        _healthModel = healthModel;
        _targetsProvider = targetsProvider;

        _healthModel.HealthOver += OnHealthOver;
    }

    private void OnDestroy()
    {
        _healthModel.HealthOver -= OnHealthOver;
    }

    private void OnHealthOver()
    {
        _targetsProvider.Remove(transform);
        Destroy(gameObject);
    }
}

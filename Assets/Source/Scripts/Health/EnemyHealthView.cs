using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthView : HealthView
{
    [SerializeField] private Image _healthBar;

    public void Init(HealthModel healthModel)
    {
        SetModel(healthModel);
        _healthBar.fillAmount = HealthProvider.HealthNormalized;
        HealthProvider.HealthChanged += OnHealthChange;
    }

    private void OnDestroy()
    {
        HealthProvider.HealthChanged -= OnHealthChange;
    }

    protected override void OnHealthChange()
    {
        _healthBar.fillAmount = HealthProvider.HealthNormalized;
    }
}

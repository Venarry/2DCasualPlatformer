using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour, IHealthView
{
    private const string HealthLabelMiddleSign = "/";

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _smoothedHealthBar;

    private TMP_Text _healthLabel;
    private Coroutine _healthBarCoroutine;
    private IHealthProvider _healthProvider;

    public void Init(IHealthProvider healthProvider, TMP_Text label)
    {
        _healthProvider = healthProvider;
        _healthLabel = label;
        _healthProvider.HealthChanged += OnHealthChange;

        InitViews();
    }

    private void OnDestroy()
    {
        _healthProvider.HealthChanged -= OnHealthChange;
    }

    public void OnHealthChange()
    {
        RefreshLabel();
        RefreshBars();
    }

    private void InitViews()
    {
        RefreshLabel();
        _healthBar.fillAmount = _healthProvider.HealthNormalized;
        _smoothedHealthBar.fillAmount = _healthProvider.HealthNormalized;
    }

    private void RefreshLabel()
    {
        _healthLabel.text = 
            $"{_healthProvider.RoundedHealth} {HealthLabelMiddleSign} {_healthProvider.MaxHealth}";
    }

    private void RefreshBars()
    {
        _healthBar.fillAmount = _healthProvider.HealthNormalized;
        RefreshSmoothBar(_healthProvider.HealthNormalized);
    }

    private void RefreshSmoothBar(float normalizedValue)
    {
        float lerpDuration = 1;

        if (_healthBarCoroutine != null)
            StopCoroutine(_healthBarCoroutine);

        _healthBarCoroutine = StartCoroutine(
            ShowSmoothedHealthBar(normalizedValue, lerpDuration));
    }

    private IEnumerator ShowSmoothedHealthBar(
        float targetNormalizedValue,
        float duration)
    {
        if(duration < 0)
            duration = 0;

        float startHealthValue = _smoothedHealthBar.fillAmount;
        float timer = 0;

        while(timer < duration)
        {
            timer += Time.deltaTime;

            if (timer > duration)
                timer = duration;

            _smoothedHealthBar.fillAmount = Mathf
                .Lerp(
                startHealthValue,
                targetNormalizedValue,
                timer / duration);

            //Debug.Log($"current value {_smoothedHealthBar.fillAmount}, duration {timer}");
            yield return null;
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : HealthView
{
    private const string HealthLabelMiddleSign = "/";

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _smoothedHealthBar;

    private TMP_Text _healthLabel;
    private Coroutine _healthBarCoroutine;

    public void Init(HealthModel healthModel, int teamIndex, TMP_Text label)
    {
        _healthLabel = label;
        InitBaseView(healthModel, teamIndex);
        InitViews();
    }

    protected override void OnHealthChange()
    {
        RefreshLabel();
        RefreshBars();
    }

    private void InitViews()
    {
        RefreshLabel();
        _healthBar.fillAmount = HealthProvider.HealthNormalized;
        _smoothedHealthBar.fillAmount = HealthProvider.HealthNormalized;
    }

    private void RefreshLabel()
    {
        _healthLabel.text = 
            $"{HealthProvider.RoundedHealth} " +
            $"{HealthLabelMiddleSign} " +
            $"{HealthProvider.MaxHealth}";
    }

    private void RefreshBars()
    {
        _healthBar.fillAmount = HealthProvider.HealthNormalized;
        RefreshSmoothBar(HealthProvider.HealthNormalized);
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

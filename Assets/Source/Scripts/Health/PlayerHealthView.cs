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
    private Coroutine _healthCoroutine;

    public void SetHealthLabel(TMP_Text label)
    {
        _healthLabel = label;
    }

    public void OnHealthChange(int value, int maxValue)
    {
        RefreshLabel(value, maxValue);

        float normalizedValue = (float)value / (maxValue);
        RefreshBars(normalizedValue);
    }

    private void RefreshLabel(int value, int maxValue)
    {
        _healthLabel.text = $"{value} {HealthLabelMiddleSign} {maxValue}";
    }

    private void RefreshBars(float normalizedValue)
    {
        _healthBar.fillAmount = normalizedValue;

        float lerpDuration = 4;

        if(_healthCoroutine != null)
            StopCoroutine(_healthCoroutine);

        _healthCoroutine = StartCoroutine(ShowSmoothedHealthBar(normalizedValue, lerpDuration));
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

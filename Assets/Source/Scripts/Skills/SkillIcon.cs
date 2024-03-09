using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _cooldown;
    [SerializeField] private TMP_Text _timeToReadyLabel;

    public bool TimeToReadyLabelState => _timeToReadyLabel.enabled;

    public void Set(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetCooldown(float normalizedValue, float timeToReady)
    {
        _cooldown.fillAmount = normalizedValue;
        _timeToReadyLabel.text = Math.Ceiling(timeToReady).ToString();
    }

    public void EnableTimeToReadyLabel()
    {
        _timeToReadyLabel.enabled = true;
    }

    public void DisableTimeToReadyLabel()
    {
        _timeToReadyLabel.enabled = false;
    }
}

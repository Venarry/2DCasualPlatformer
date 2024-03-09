using System;
using UnityEngine;

public abstract class BaseActiveSkill : MonoBehaviour, ISkill
{
    public Sprite Sprite { get; private set; }

    private float _baseCooldown = 10f;
    private float _timeLeft;

    public event Action<ISkill> TimeLeftChanged;
    public event Action<ISkill> SkillIsReady;
    protected int TeamIndex { get; private set;}
    public float TimeToReady => _baseCooldown - _timeLeft;
    public float Cooldown => _baseCooldown;
    public float TimeToReadyNormalized => TimeToReady / Cooldown;

    protected void InitBaseParamenters(Sprite sprite, int teamIndex, float baseCooldown)
    {
        Sprite = sprite;
        TeamIndex = teamIndex;
        _baseCooldown = baseCooldown;
        _timeLeft = _baseCooldown;
    }

    private void Update()
    {
        if (_timeLeft < _baseCooldown)
        {
            _timeLeft += Time.deltaTime;
            TimeLeftChanged?.Invoke(this);
        }
        else
        {
            _timeLeft = _baseCooldown;
            SkillIsReady?.Invoke(this);
        }
    }

    protected void ResetTimer()
    {
        _timeLeft = 0;
    }

    public abstract void TryCast();
}

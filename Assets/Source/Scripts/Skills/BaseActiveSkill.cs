using System;
using System.Collections;
using UnityEngine;

public abstract class BaseActiveSkill : MonoBehaviour, ISkill
{
    private float _baseCooldown = 10f;
    private float _timeLeft;
    private Coroutine _coolDownCoroutine;

    public event Action<ISkill> TimeLeftChanged;
    public event Action<ISkill> SkillIsReady;
    public Sprite Sprite { get; private set; }
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
        if (_coolDownCoroutine == null && _timeLeft < _baseCooldown)
        {
            _coolDownCoroutine = StartCoroutine(ReducingCooldownTime());
        }
    }

    private IEnumerator ReducingCooldownTime()
    {
        while (_timeLeft < _baseCooldown)
        {
            _timeLeft += Time.deltaTime;
            TimeLeftChanged?.Invoke(this);

            yield return null;
        }

        _timeLeft = _baseCooldown;
        SkillIsReady?.Invoke(this);
        _coolDownCoroutine = null;
    }

    protected void ResetTimer()
    {
        _timeLeft = 0;
    }

    public abstract void TryCast();
}

using System;
using System.Collections;
using UnityEngine;

public class PlayerLifestealSkill : BaseActiveSkill, ISkill
{
    private readonly float _raduis = 5f;
    private readonly float _baseCooldown = 10f;
    private readonly float _baseDuration = 6f;
    private readonly float _baseHealthLifestealPerSecond = 5f;

    private HealthModel _healthModel;
    private TartgetsFinderForSkillCast _tartgetsFinderForSkillCast;
    private Coroutine _activeCast;

    public void Init(
        Sprite sprite,
        HealthModel healthModel,
        TartgetsFinderForSkillCast tartgetsFinderForSkillCast,
        int teamIndex)
    {
        _healthModel = healthModel;
        _tartgetsFinderForSkillCast = tartgetsFinderForSkillCast;
        InitBaseParamenters(sprite, teamIndex, _baseCooldown);
    }

    public override void TryCast()
    {
        if (TimeToReady > 0)
            return;

        _tartgetsFinderForSkillCast.CastToRandomEnemyInRadius<IDamageable>(
            TeamIndex,
            transform.position,
            _raduis,
            Cast);
    }

    private void Cast(IDamageable damageable)
    {
        if (TimeToReady > 0)
            return;

        damageable.HealthOver += OnTargetHealthOver;
        _activeCast = StartCoroutine(CastingSkill(damageable));
        ResetTimer();
    }

    private void OnTargetHealthOver(IDamageable target)
    {
        target.HealthOver -= OnTargetHealthOver;
        StopCoroutine(_activeCast);
    }

    private IEnumerator CastingSkill(IDamageable damageable)
    {
        float timeLeft = 0;

        while (timeLeft <= _baseDuration)
        {
            if (Vector3.Distance(transform.position, damageable.Position) > _raduis)
                StopCoroutine(_activeCast);

            float lifestealMultiply = Time.deltaTime;

            timeLeft += Time.deltaTime;

            if (timeLeft > _baseDuration)
            {
                lifestealMultiply -= (timeLeft - _baseDuration);
            }

            float lifestealValue = _baseHealthLifestealPerSecond * lifestealMultiply;

            float overflowValue = damageable
                .TakeDamageWithOverflowValue(lifestealValue);
            _healthModel?.Add(lifestealValue - overflowValue);

            yield return null;
        }
    }
}

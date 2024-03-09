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

        StartCoroutine(CastingSkill(damageable));
        ResetTimer();
    }

    private IEnumerator CastingSkill(IDamageable damageable)
    {
        float timeLeft = 0;

        while (timeLeft <= _baseDuration)
        {
            float targetMultiply = Time.deltaTime;

            timeLeft += Time.deltaTime;

            if (timeLeft > _baseDuration)
            {
                targetMultiply -= (timeLeft - _baseDuration);
            }

            float targetValue = _baseHealthLifestealPerSecond * targetMultiply;
            _healthModel?.Add(targetValue);
            damageable?.TakeDamage(targetValue);

            yield return null;
        }
    }
}

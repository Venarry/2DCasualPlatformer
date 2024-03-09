using System;
using System.Collections;
using UnityEngine;

public class PlayerLifestealSkill : BaseActiveSkill, ISkill
{
    private readonly float _raduis = 5f;
    private readonly float _baseCooldown = 10f;
    private readonly float _baseDuration = 6f;
    private readonly float _baseHealthLifestealPerSecond = 5f;

    private HealthPresenter _healthPresenter;
    private TartgetsFinderForSkillCast _tartgetsFinderForSkillCast;

    public void Init(
        Sprite sprite,
        HealthPresenter healthPresenter,
        TartgetsFinderForSkillCast tartgetsFinderForSkillCast,
        int teamIndex)
    {
        _healthPresenter = healthPresenter;
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
        
        while(timeLeft < _baseDuration)
        {
            float targetValue = _baseHealthLifestealPerSecond * Time.deltaTime;
            _healthPresenter?.Add(targetValue);
            damageable?.TakeDamage(targetValue);
            timeLeft += Time.deltaTime;

            yield return null;
        }
    }
}

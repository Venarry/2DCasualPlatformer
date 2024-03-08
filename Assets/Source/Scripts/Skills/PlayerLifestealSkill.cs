using System.Collections;
using UnityEngine;

public class PlayerLifestealSkill : MonoBehaviour, ISkill
{
    private readonly float _raduis = 5f;
    private readonly float _baseCooldown = 10f;
    private readonly float _baseDuration = 6f;
    private readonly float _baseHealthLifestealPerSecond = 5f;
    private HealthPresenter _healthPresenter;
    private TartgetsFinderForSkillCast _tartgetsFinderForSkillCast;
    private int _teamIndex;
    private float _timeLeft;

    public float TimeToReady => _baseCooldown - _timeLeft;

    public void Init(
        HealthPresenter healthPresenter,
        TartgetsFinderForSkillCast tartgetsFinderForSkillCast,
        int teamIndex)
    {
        _healthPresenter = healthPresenter;
        _tartgetsFinderForSkillCast = tartgetsFinderForSkillCast;
        _teamIndex = teamIndex;
        _timeLeft = _baseCooldown;
    }

    private void Update()
    {
        if(_timeLeft < _baseCooldown)
            _timeLeft += Time.deltaTime;
        else
            _timeLeft = _baseCooldown;
    }

    public void TryCast()
    {
        if (TimeToReady > 0)
            return;

        Debug.Log($"try cast {nameof(PlayerLifestealSkill)}");

        _tartgetsFinderForSkillCast.CastToRandomEnemyInRadius<IDamageable>(
            _teamIndex,
            transform.position, _raduis, Cast);
    }

    private void Cast(IDamageable damageable)
    {
        if (TimeToReady > 0)
            return;
        Debug.Log($"cast {nameof(PlayerLifestealSkill)}");
        StartCoroutine(CastingSkill(damageable));

        _timeLeft = 0;
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

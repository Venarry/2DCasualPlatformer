using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(HealthView))]
[RequireComponent(typeof(EnemyHealthView))]
public class EnemySpike : BaseHero, IHealable, IDamageable
{
    private StateMachine _stateMachine;
    private EnemyHealthView _enemyHealthView;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _enemyHealthView = GetComponent<EnemyHealthView>();
    }

    public void Init(
        TargetsProvider targetsProvider,
        HealthModel healthModel,
        int teamIndex,
        float speed,
        float chaseSpeed,
        float chaseDistance,
        Transform[] patroolingTargets,
        Transform targetToChase)
    {
        InitBaseParamenters(teamIndex);
        _enemyHealthView.Init(healthModel);
        targetsProvider.Add(this);

        EnemySpikePatroolingState patroolingState = new(
            transform,
            speed,
            chaseDistance,
            patroolingTargets,
            targetToChase,
            _stateMachine);

        EnemySpikeChaseState chaseState = new(
            transform,
            targetToChase,
            _stateMachine,
            chaseDistance,
            chaseSpeed);

        _stateMachine.Register(patroolingState);
        _stateMachine.Register(chaseState);

        _stateMachine.Switch<EnemySpikePatroolingState>();
    }

    public void Heal(float value)
    {
        _enemyHealthView.Heal(value);
    }

    public void TakeDamage(float value)
    {
        _enemyHealthView.TakeDamage(value);
    }
}

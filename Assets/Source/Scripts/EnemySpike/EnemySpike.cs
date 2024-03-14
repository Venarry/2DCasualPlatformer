using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(EnemyHealthView))]
[RequireComponent(typeof(EnemyDeathView))]
public class EnemySpike : MonoBehaviour
{
    private StateMachine _stateMachine;
    private EnemyHealthView _enemyHealthView;
    private EnemyDeathView _enemyDeathView;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _enemyHealthView = GetComponent<EnemyHealthView>();
        _enemyDeathView = GetComponent<EnemyDeathView>();
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
        _enemyHealthView.Init(healthModel, teamIndex);
        _enemyDeathView.Init(healthModel, targetsProvider);
        targetsProvider.Add(transform);

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
}

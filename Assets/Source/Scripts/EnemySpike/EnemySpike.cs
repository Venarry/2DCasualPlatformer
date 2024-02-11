using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class EnemySpike : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
    }

    public void Init(
        float speed,
        float chaseSpeed,
        float chaseDistance,
        Transform[] patroolingTargets,
        Transform targetToChase)
    {
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

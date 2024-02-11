using UnityEngine;

public class EnemySpikeChaseState : IState
{
    private readonly Transform _character;
    private readonly Transform _targetToChase;
    private readonly IStateSwitcher _stateSwitcher;
    private readonly float _chaseDistance;
    private readonly float _chaseSpeed;

    public EnemySpikeChaseState(
        Transform character,
        Transform targetToChase,
        IStateSwitcher stateSwitcher,
        float chaseDistance,
        float chaseSpeed)
    {
        _character = character;
        _targetToChase = targetToChase;
        _stateSwitcher = stateSwitcher;
        _chaseDistance = chaseDistance;
        _chaseSpeed = chaseSpeed;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        if(Vector3.Distance(_character.position, _targetToChase.position) >
            _chaseDistance)
        {
            _stateSwitcher.Switch<EnemySpikePatroolingState>();
        }
        else
        {
            _character.position = Vector3
                .MoveTowards(
                _character.position,
                _targetToChase.position,
                _chaseSpeed * Time.deltaTime);
        }
    }
}

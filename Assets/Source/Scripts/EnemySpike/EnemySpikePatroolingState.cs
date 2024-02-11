using UnityEngine;

public class EnemySpikePatroolingState : IState
{
    private readonly float _speed = 6f;
    private readonly float _chaseDistance;
    private readonly Transform _character;
    private readonly Transform[] _targetPoints;
    private readonly Transform _targetToChase;
    private readonly IStateSwitcher _stateSwitcher;
    private int _currentPoint = 0;

    public EnemySpikePatroolingState(
        Transform character,
        float speed,
        float chaseDistance,
        Transform[] targetPoints,
        Transform targetToChase,
        IStateSwitcher stateSwitcher)
    {
        _character = character;
        _speed = speed;
        _chaseDistance = chaseDistance;
        _targetPoints = targetPoints;
        _targetToChase = targetToChase;
        _stateSwitcher = stateSwitcher;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        TranslateToTargetPoint();
        TryChangeTargetPoint();
        TryStartChase();
    }

    public void OnExit()
    {
    }

    private void TranslateToTargetPoint()
    {
        _character.position = Vector3.MoveTowards(
            _character.position,
            _targetPoints[_currentPoint].position,
            _speed * Time.deltaTime);
    }

    private void TryChangeTargetPoint()
    {
        float distance = 0.1f;

        if (Vector3.Distance(
            _character.position,
            _targetPoints[_currentPoint].position) < distance)
        {
            _currentPoint++;

            if(_currentPoint >= _targetPoints.Length)
                _currentPoint = 0;
        }
    }

    private void TryStartChase()
    {
        if (Vector3.Distance(_character.position, _targetToChase.position) <
            _chaseDistance)
        {
            _stateSwitcher.Switch<EnemySpikeChaseState>();
        }
    }
}

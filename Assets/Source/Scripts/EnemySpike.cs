using UnityEngine;

public class EnemySpike : Enemy
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private Transform[] _targetPoints;

    private int _currentPoint = 0;

    protected override void DoAction()
    {
        TranslateToTargetPoint();
        TryChangeTargetPoint();
    }

    private void TranslateToTargetPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoints[_currentPoint].position, _speed);
    }

    private void TryChangeTargetPoint()
    {
        float distance = 0.1f;

        if (Vector3.Distance(transform.position, _targetPoints[_currentPoint].position) < distance)
        {
            _currentPoint++;

            if(_currentPoint >= _targetPoints.Length)
                _currentPoint = 0;
        }
    }
}

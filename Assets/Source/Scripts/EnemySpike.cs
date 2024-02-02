using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private Transform[] _targetPoints;

    private int _currentPoint = 0;

    private void Update()
    {
        TranslateToTargetPoint();
        TryChangeTargetPoint();
    }

    private void TranslateToTargetPoint()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetPoints[_currentPoint].position,
            _speed * Time.deltaTime);
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

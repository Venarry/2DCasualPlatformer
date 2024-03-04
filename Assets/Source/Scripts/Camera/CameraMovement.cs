using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        if (_target == null)
            return;

        transform.position = Vector3.Lerp(
            transform.position,
            _target.position,
            _speed * Time.deltaTime) + _offset;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}

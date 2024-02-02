using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            _target.position,
            _speed * Time.deltaTime) + _offset;
    }
}

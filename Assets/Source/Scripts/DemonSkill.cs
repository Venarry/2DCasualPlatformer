using UnityEngine;

public class DemonSkill : MonoBehaviour
{
    [SerializeField] private float _coolDown = 1;
    [SerializeField] private float _radius = 2;
    [SerializeField] private Transform _skillCastPoint;

    private float _currentCoolDown;

    private bool _readyToCast => _currentCoolDown >= _coolDown;

    private void Update()
    {
        _currentCoolDown += Time.deltaTime;
        
        if(_readyToCast == true)
            TryCast();
    }

    public void TryCast()
    {
        if (_readyToCast == false)
            return;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            _skillCastPoint.position,
            new Vector2(_radius, _radius),
            0);

        foreach (Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out Player player))
            {
                player.Kill();
            }
        }

        _currentCoolDown = 0;
    }
}

using UnityEngine;

public class DemonSkill : MonoBehaviour, ISkill
{
    [SerializeField] private float _coolDown = 1;
    [SerializeField] private float _radius = 2;
    [SerializeField] private Transform _skillCastPoint;

    private float currentCoolDown;

    public bool Ready => currentCoolDown >= _coolDown;

    private void FixedUpdate()
    {
        currentCoolDown += Time.deltaTime;
    }

    public void TryCast()
    {
        if (Ready == false)
            return;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(_skillCastPoint.position, new Vector2(_radius, _radius), 0);

        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject.TryGetComponent(out Player player))
            {
                player.transform.position = Vector3.zero;
            }
        }

        currentCoolDown = 0;
    }
}

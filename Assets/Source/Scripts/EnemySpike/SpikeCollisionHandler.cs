using UnityEngine;

public class SpikeCollisionHandler : MonoBehaviour
{
    [SerializeField] private float _pushPower = 10f;
    [SerializeField] private int _damage = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IImpulsable impulsable))
        {
            Vector2 pushDirection = (
                collision.gameObject.transform.position - transform.position)
                .normalized;

            impulsable.AddImpulse(pushDirection * _pushPower);
        }

        if(collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamageWithOverflowValue(_damage);
        }
    }
}

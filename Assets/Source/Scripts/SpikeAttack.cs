using UnityEngine;

public class SpikeAttack : MonoBehaviour
{
    [SerializeField] private float _pushPower = 0.2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IImpulsable impulsable))
        {
            Vector2 pushDirection = (collision.gameObject.transform.position - transform.position).normalized;
            impulsable.AddImpulse(pushDirection * _pushPower);
        }
    }
}

using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private int _healCount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IHealable healable))
        {
            healable.Heal(_healCount);
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Picked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Picked?.Invoke(this);
            Destroy(gameObject);
        }
    }
}

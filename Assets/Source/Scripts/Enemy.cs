using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private void FixedUpdate()
    {
        DoAction();
    }

    protected abstract void DoAction();
}

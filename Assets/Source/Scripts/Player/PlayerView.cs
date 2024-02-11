using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerHealthView _healthView;
    [SerializeField] private CharacterMovement _characterMovement;

    public void Death()
    {
        transform.position = Vector3.zero;
    }
}

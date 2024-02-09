using UnityEngine;

public class Player : MonoBehaviour, IHealable, IDamageable, IImpulsable
{
    private HealthView _healthView;
    private CharacterMovement _characterMovement;

    public void Init(
        HealthView healthView,
        CharacterMovement characterMovement)
    {
        _healthView = healthView;
        _characterMovement = characterMovement;
    }

    public void Heal(int value)
    {
        _healthView.Add(value);
    }

    public void TakeDamage(int value)
    {
        _healthView.TakeDamage(value);
    }

    public void AddImpulse(Vector2 impulse)
    {
        _characterMovement.AddImpulse(impulse);
    }

    public void Kill()
    {
        transform.position = Vector3.zero;
    }
}

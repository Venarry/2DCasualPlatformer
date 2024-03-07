using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;
    [SerializeField] private PlayerHealthView _playerHealthView;
    [SerializeField] private CharacterMovement _characterMovement;

    public void Init(HealthPresenter healthPresenter, TMP_Text label)
    {
        _playerHealthView.SetHealthLabel(label);
        _healthView.Init(healthPresenter);
        healthPresenter.Init(_playerHealthView);
    }

    public void Death()
    {
        transform.position = Vector3.zero;
    }
}

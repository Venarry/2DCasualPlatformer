using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerHealthView))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(PlayerActivateSkillsHandler))]
public class PlayerView : BaseHero, IHealable, IDamageable
{
    [SerializeField] private PlayerHealthView _playerHealthView;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private PlayerActivateSkillsHandler _activateSkillsHandler;

    public void Init(
        TargetsProvider targetsProvider,
        HealthModel healthModel,
        IInputsProvider inputsHandler,
        SkillsProvider skillsProvider,
        int teamIndex,
        TMP_Text label)
    {
        InitBaseParamenters(teamIndex);
        _playerHealthView.Init(healthModel, label);
        _characterMovement.Init(inputsHandler);
        _activateSkillsHandler.Init(skillsProvider, inputsHandler);
        targetsProvider.Add(this);
    }

    public void Death()
    {
        transform.position = Vector3.zero;
    }

    public void Heal(float value)
    {
        _playerHealthView.Heal(value);
    }

    public void TakeDamage(float value)
    {
        _playerHealthView.TakeDamage(value);
    }
}

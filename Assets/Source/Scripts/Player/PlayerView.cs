using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : BaseHero, IHealable, IDamageable
{
    [SerializeField] private HealthView _healthView;
    [SerializeField] private PlayerHealthView _playerHealthView;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private PlayerActivateSkillsHandler _activateSkillsHandler;

    public void Init(
        TargetsProvider targetsProvider,
        HealthPresenter healthPresenter,
        IInputsHandler inputsHandler,
        List<ISkill> skills,
        int teamIndex,
        TMP_Text label)
    {
        InitBaseParamenters(teamIndex);
        _playerHealthView.Init(healthPresenter, label);
        _healthView.Init(healthPresenter);
        _characterMovement.Init(inputsHandler);
        _activateSkillsHandler.Init(skills, inputsHandler);
        targetsProvider.Add(this);
    }

    public void Death()
    {
        transform.position = Vector3.zero;
    }

    public void Heal(float value)
    {
        _healthView.Heal(value);
    }

    public void TakeDamage(float value)
    {
        _healthView.TakeDamage(value);
    }
}

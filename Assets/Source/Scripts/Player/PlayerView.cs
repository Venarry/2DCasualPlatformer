using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerHealthView))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(PlayerActivateSkillsHandler))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerHealthView _playerHealthView;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private PlayerActivateSkillsHandler _activateSkillsHandler;

    public void Init(
        TargetsProvider targetsProvider,
        HealthModel healthModel,
        IInputsProvider inputsHandler,
        SkillsHolder skillsProvider,
        int teamIndex,
        TMP_Text label)
    {
        _playerHealthView.Init(healthModel, teamIndex, label);
        _characterMovement.Init(inputsHandler);
        _activateSkillsHandler.Init(skillsProvider, inputsHandler);

        targetsProvider.Add(transform);
    }

    public void Death()
    {
        transform.position = Vector3.zero;
    }
}

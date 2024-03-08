using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFactory
{
    private readonly PlayerView _prefab = Resources.Load<PlayerView>(Paths.Player);

    public PlayerView Create(
        TargetsProvider targetsProvider,
        Vector3 position,
        IInputsHandler inputsHandler,
        TartgetsFinderForSkillCast tartgetsFinderForSkillCast,
        int teamIndex,
        TMP_Text healthLabel)
    {
        PlayerView player = Object.Instantiate(_prefab, position, Quaternion.identity);

        int maxHealth = 100;
        HealthModel healthModel = new(maxHealth);
        HealthPresenter healthPresenter = new(healthModel);

        PlayerLifestealSkill lifestealSkill = player.AddComponent<PlayerLifestealSkill>();
        lifestealSkill.Init(healthPresenter, tartgetsFinderForSkillCast, teamIndex);

        List<ISkill> skills = new() { lifestealSkill };

        player.Init(targetsProvider, healthPresenter, inputsHandler, skills, teamIndex, healthLabel);

        return player;
    }
}

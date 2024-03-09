using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFactory
{
    private readonly PlayerView _prefab = Resources.Load<PlayerView>(Paths.Player);

    public PlayerView Create(
        TargetsProvider targetsProvider,
        SkillsProvider skillsProvider,
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

        Sprite lifestealSkillSprite = Resources.Load<Sprite>("Images/LifestealSkillIcon");
        PlayerLifestealSkill lifestealSkill = player.AddComponent<PlayerLifestealSkill>();
        lifestealSkill.Init(lifestealSkillSprite, healthPresenter, tartgetsFinderForSkillCast, teamIndex);

        skillsProvider.Add(lifestealSkill);

        player.Init(targetsProvider, healthPresenter, inputsHandler, skillsProvider, teamIndex, healthLabel);

        return player;
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFactory
{
    private readonly PlayerView _prefab = Resources.Load<PlayerView>(Paths.Player);

    public PlayerView Create(
        TargetsProvider targetsProvider,
        SkillsHolder skillsProvider,
        Vector3 position,
        IInputsProvider inputsHandler,
        TartgetsFinderForSkillCast tartgetsFinderForSkillCast,
        int teamIndex,
        TMP_Text healthLabel)
    {
        PlayerView player = Object.Instantiate(_prefab, position, Quaternion.identity);

        int maxHealth = 100;
        HealthModel healthModel = new(maxHealth);

        Sprite lifestealSkillSprite = SkillsImageDataSource.LifestealSkill;
        PlayerLifestealSkill lifestealSkill = player.AddComponent<PlayerLifestealSkill>();
        lifestealSkill.Init(
            lifestealSkillSprite,
            healthModel,
            tartgetsFinderForSkillCast,
            teamIndex);

        skillsProvider.Add(lifestealSkill);

        player.Init(
            targetsProvider,
            healthModel,
            inputsHandler,
            skillsProvider,
            teamIndex,
            healthLabel);

        return player;
    }
}

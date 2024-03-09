using UnityEngine;

public class EnemySpikeFactory
{
    private readonly EnemySpike _prefab = Resources.Load<EnemySpike>(Paths.EnemySpike);

    public EnemySpike Create(
        TargetsProvider targetsProvider,
        Vector3 position,
        int teamIndex,
        float maxHealth,
        float speed,
        float chaseSpeed,
        float chaseDistance,
        Transform[] patroolingTargets,
        Transform targetToChase)
    {
        EnemySpike spike = Object.Instantiate(_prefab, position, Quaternion.identity);

        HealthModel healthModel = new(maxHealth);

        spike.Init(
            targetsProvider,
            healthModel,
            teamIndex,
            speed,
            chaseSpeed,
            chaseDistance,
            patroolingTargets,
            targetToChase);

        return spike;
    }
}

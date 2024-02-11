using UnityEngine;

public class EnemySpikeFactory
{
    private readonly EnemySpike _prefab = Resources.Load<EnemySpike>(Paths.EnemySpike);

    public EnemySpike Create(
        Vector3 position,
        float speed,
        float chaseSpeed,
        float chaseDistance,
        Transform[] patroolingTargets,
        Transform targetToChase)
    {
        EnemySpike spike = Object.Instantiate(_prefab, position, Quaternion.identity);

        spike.Init(speed, chaseSpeed, chaseDistance, patroolingTargets, targetToChase);
        return spike;
    }
}

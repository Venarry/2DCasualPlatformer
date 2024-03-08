using System;
using UnityEngine;

public class TartgetsFinderForSkillCast
{
    private readonly TargetsProvider _targetsProvider;

    public TartgetsFinderForSkillCast(TargetsProvider targetsProvider)
    {
        _targetsProvider = targetsProvider;
    }

    public void CastToRandomEnemyInRadius<T>(
        int teamIndex,
        Vector3 point,
        float radius,
        Action<T> castCallback) where T : ITarget
    {
        if(_targetsProvider.TryFindNearestEnemy<T>(
            point, teamIndex, radius, out T foundedTarget))
        {
            castCallback.Invoke(foundedTarget);
        }
    }
}

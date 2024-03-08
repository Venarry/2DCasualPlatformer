using System.Collections.Generic;
using UnityEngine;

public class TargetsProvider
{
    private readonly List<ITarget> _targets = new();

    public void Add(ITarget target)
    {
        _targets.Add(target);
    }

    public bool TryFindNearestEnemy<T>(Vector3 point, int teamIndex, float radius, out T foundedTarget) where T : ITarget
    {
        foundedTarget = default;

        if(_targets.Count == 0)
            return false;

        float nearestDistance = float.MaxValue;

        foreach (var target in _targets)
        {
            if(target is not T targetWithNeededType)
                continue;

            if (teamIndex == targetWithNeededType.TeamIndex)
                continue;

            float currentDistance = Vector3
                .Distance(point, targetWithNeededType.Position);

            if (currentDistance > radius)
                continue;

            if (foundedTarget == null || currentDistance < nearestDistance)
            {
                foundedTarget = targetWithNeededType;
                nearestDistance = currentDistance;
            }
        }

        return foundedTarget != null;
    }
}

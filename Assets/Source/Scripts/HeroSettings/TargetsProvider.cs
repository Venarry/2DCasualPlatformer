using System.Collections.Generic;
using UnityEngine;

public class TargetsProvider
{
    private readonly List<Transform> _targets = new();

    public void Add(Transform target)
    {
        if (_targets.Contains(target))
            return;

        _targets.Add(target);
    }

    public void Remove(Transform target)
    {
        if (_targets.Contains(target) == false)
            return;

        _targets.Remove(target);
    }

    public bool TryFindNearestEnemy<T>(
        Vector3 point,
        int teamIndex,
        float radius,
        out T foundedTarget) where T : ITarget
    {
        foundedTarget = default;

        if(_targets.Count == 0)
            return false;

        float nearestDistance = float.MaxValue;

        foreach (Transform target in _targets)
        {
            if (target.TryGetComponent(out T targetWithNeededType) == false)
                continue;

            if (teamIndex == targetWithNeededType.TeamIndex)
                continue;

            float currentDistance = Vector3
                .Distance(point, target.transform.position);

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

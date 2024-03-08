using UnityEngine;

public interface ITarget
{
    public int TeamIndex { get; }
    public Vector3 Position { get; }
}

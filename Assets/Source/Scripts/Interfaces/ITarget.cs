using UnityEngine;

public interface ITarget
{
    public Vector3 Position { get; }
    public int TeamIndex { get; }
}

using UnityEngine;

public interface IInputsProvider
{
    public Vector3 MoveDirection { get; }
    public bool IsPressedActivateFirstSkill { get; }
}

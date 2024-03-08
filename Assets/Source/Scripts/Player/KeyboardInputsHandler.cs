using UnityEngine;

public class KeyboardInputsHandler : IInputsHandler
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public Vector3 MoveDirection => new(Input.GetAxisRaw(Horizontal), 0, Input.GetAxisRaw(Vertical));
    public bool IsPressedActivateFirstSkill => Input.GetKeyDown(KeyCode.Q);
}

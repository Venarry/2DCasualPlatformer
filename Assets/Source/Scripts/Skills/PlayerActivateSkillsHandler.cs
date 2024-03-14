using UnityEngine;

public class PlayerActivateSkillsHandler : MonoBehaviour
{
    private IInputsProvider _inputsHandler;
    private SkillsHolder _skillsHolder;

    public void Init(SkillsHolder skillsHolder, IInputsProvider inputsHandler)
    {
        _inputsHandler = inputsHandler;
        _skillsHolder = skillsHolder;
    }

    private void Update()
    {
        if (_inputsHandler == null)
        {
            return;
        }

        if (_inputsHandler.IsPressedActivateFirstSkill)
        {
            _skillsHolder.TryCast(0);
        }
    }
}

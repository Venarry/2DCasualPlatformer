using UnityEngine;

public class PlayerActivateSkillsHandler : MonoBehaviour
{
    private IInputsProvider _inputsHandler;
    private SkillsProvider _skillsProvider;

    public void Init(SkillsProvider skillsProvider, IInputsProvider inputsHandler)
    {
        _inputsHandler = inputsHandler;
        _skillsProvider = skillsProvider;
    }

    private void Update()
    {
        if (_inputsHandler == null)
        {
            return;
        }

        if (_inputsHandler.IsPressedActivateFirstSkill)
        {
            _skillsProvider.TryCast(0);
        }
    }
}

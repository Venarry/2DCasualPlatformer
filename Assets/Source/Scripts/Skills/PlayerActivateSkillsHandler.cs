using UnityEngine;

public class PlayerActivateSkillsHandler : MonoBehaviour
{
    private IInputsHandler _inputsHandler;
    private SkillsProvider _skillsProvider;

    public void Init(SkillsProvider skillsProvider, IInputsHandler inputsHandler)
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

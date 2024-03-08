using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActivateSkillsHandler : MonoBehaviour
{
    private IInputsHandler _inputsHandler;
    private List<ISkill> _skills;

    public void Init(List<ISkill> skills, IInputsHandler inputsHandler)
    {
        _inputsHandler = inputsHandler;
        _skills = skills.ToList();
    }

    private void Update()
    {
        if (_inputsHandler == null)
        {
            return;
        }

        if (_inputsHandler.IsPressedActivateFirstSkill)
        {
            _skills[0].TryCast();
        }
    }

    public void AddSkill(ISkill skill)
    {
        _skills.Add(skill);
    }
}

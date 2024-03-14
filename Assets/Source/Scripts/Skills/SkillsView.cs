using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillsView : MonoBehaviour
{
    private readonly Dictionary<ISkill, SkillIcon> _skillsIcons = new();
    private Transform _skillsParent;
    private SkillsHolder _skillsHolder;
    private SkillsIconFactory _skillsIconFactory;

    public void Init(
        Transform skillsParent,
        SkillsHolder skillsHolder,
        SkillsIconFactory skillsIconFactory)
    {
        _skillsParent = skillsParent;
        _skillsHolder = skillsHolder;
        _skillsIconFactory = skillsIconFactory;
        Show(_skillsHolder.Skills);
    }

    private void Show(ISkill[] skills)
    {
        foreach (var skill in skills)
        {
            Show(skill);
        }
    }

    private void Show(ISkill skill)
    {
        SkillIcon icon = _skillsIconFactory.Create(_skillsParent, skill.Sprite);

        _skillsIcons.Add(skill, icon);
        skill.TimeLeftChanged += OnTimeChange;
        skill.SkillIsReady += OnSkillReady;
        OnTimeChange(skill);

        if(skill.TimeToReady == 0)
        {
            _skillsIcons[skill].DisableTimeToReadyLabel();
        }
    }

    private void OnSkillReady(ISkill skill)
    {
        _skillsIcons[skill].DisableTimeToReadyLabel();
    }

    private void OnTimeChange(ISkill skill)
    {
        SkillIcon currentIcon = _skillsIcons[skill];

        if (currentIcon.TimeToReadyLabelState == false)
        {
            currentIcon.EnableTimeToReadyLabel();
        }

        currentIcon.SetCooldownTime(skill.TimeToReadyNormalized, skill.TimeToReady);
    }
}

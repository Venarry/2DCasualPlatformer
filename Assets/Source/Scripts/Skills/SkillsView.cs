using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillsView : MonoBehaviour
{
    private readonly Dictionary<ISkill, SkillIcon> _skillsIcons = new();
    private Transform _skillsParent;
    private SkillsProvider _skillsProvider;

    public void Init(Transform skillsParent, SkillsProvider skillsProvider)
    {
        _skillsParent = skillsParent;
        _skillsProvider = skillsProvider;
        Show(_skillsProvider.Skills);
    }

    private void Show(ISkill skill)
    {
        SkillIcon icon = Instantiate(Resources.Load<SkillIcon>("Prefabs/SkillIcon"), _skillsParent);
        icon.Set(skill.Sprite);

        _skillsIcons.Add(skill, icon);
        skill.TimeLeftChanged += OnTimeChange;
        skill.SkillIsReady += OnSkillReady;
        OnTimeChange(skill);
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

        currentIcon.SetCooldown(skill.TimeToReadyNormalized, skill.TimeToReady);
    }

    private void Show(ISkill[] skills)
    {
        foreach (var skill in skills)
        {
            Show(skill);
        }
    }
}

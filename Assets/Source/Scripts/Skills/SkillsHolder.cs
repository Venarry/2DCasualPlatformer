using System.Collections.Generic;
using System.Linq;

public class SkillsHolder
{
    private readonly List<ISkill> _skills = new();

    public SkillsHolder(List<ISkill> skills)
    {
        _skills = skills.ToList();
    }

    public ISkill[] Skills => _skills.ToArray();

    public void Add(ISkill skill)
    {
        _skills.Add(skill);
    }

    public void TryCast(int index)
    {
        if(index < 0)
            return;

        if (index >= _skills.Count)
            return;

        _skills[index].TryCast();
    }
}

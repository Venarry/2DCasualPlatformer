using System.Collections.Generic;
using System.Linq;

public class SkillsProvider
{
    private readonly List<ISkill> _skills = new();

    public SkillsProvider(List<ISkill> skills)
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
        if (index >= _skills.Count)
            return;

        _skills[index].TryCast();
    }
}

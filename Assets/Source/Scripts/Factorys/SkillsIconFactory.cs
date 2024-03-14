using UnityEngine;

public class SkillsIconFactory
{
    private readonly SkillIcon _prefab = Resources.Load<SkillIcon>(Paths.SkillIcon);

    public SkillIcon Create(Transform parent, Sprite sprite)
    {
        SkillIcon skillIcon = Object.Instantiate(_prefab, parent);
        skillIcon.Set(sprite);

        return skillIcon;
    }
}

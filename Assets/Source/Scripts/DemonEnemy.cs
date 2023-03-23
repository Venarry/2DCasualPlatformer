public class DemonEnemy : Enemy
{
    private ISkill _skill;

    private void Awake()
    {
        _skill = GetComponent<ISkill>();
    }

    protected override void DoAction()
    {
        if(_skill.Ready)
            _skill.TryCast();
    }
}

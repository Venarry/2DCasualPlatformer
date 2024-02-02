using UnityEngine;

public class DemonEnemy : MonoBehaviour
{
    private ISkill _skill;

    private void Awake()
    {
        _skill = GetComponent<ISkill>();
    }

    private void Update()
    {
        if (_skill.Ready == true)
            _skill.TryCast();
    }
}

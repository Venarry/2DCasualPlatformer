using UnityEngine;

public abstract class BaseHero : MonoBehaviour, ITarget
{
    private int _teamIndex;
    public int TeamIndex => _teamIndex;

    public Vector3 Position => transform.position;

    protected void InitBaseParamenters(int teamIndex)
    {
        _teamIndex = teamIndex;
    }
}

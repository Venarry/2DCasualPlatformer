using System;
using UnityEngine;
using UnityEngine.UI;

public interface ISkill
{
    public Sprite Sprite { get; }
    public event Action<ISkill> TimeLeftChanged;
    public event Action<ISkill> SkillIsReady;
    public float TimeToReady { get; }
    public float TimeToReadyNormalized { get; }
    public float Cooldown { get; }
    public void TryCast();
}

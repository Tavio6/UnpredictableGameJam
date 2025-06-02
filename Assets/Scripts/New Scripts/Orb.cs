using UnityEngine;
using System;

public class Orb
{
    public string name;
    public string description;
    public Sprite icon;

    private Action effect;

    public Orb(string name, string description, Sprite icon, Action effect)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.effect = effect;
    }

    public virtual void ApplyEffect()
    {
        effect?.Invoke();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Bonus : IBonus
{
    public string Alias { get; set; }
    public BonusType BonusType { get; set; }
    public float RandomPercent { get; set; }
    public float HealthValue { get; set; }
    public float FireSpeedCoefficient { get; set; }
    public float ReloadSpeedCoefficient { get; set; }
    public float ActiveTime { get; set; }

    public GameObject GameObject { get; set; }
    public Collider2D Collider2D { get; set; }
}

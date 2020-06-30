using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonus : IEntity
{
    string Alias { get; set; }
    BonusType BonusType { get; set; }
    float RandomPercent { get; set; }
    float HealthValue { get; set; }
    float FireSpeedCoefficient { get; set; }
    float ReloadSpeedCoefficient { get; set; }
    float ActiveTime { get; set; }

    GameObject GameObject { get; set; }
    Collider2D Collider2D { get; set; }
}

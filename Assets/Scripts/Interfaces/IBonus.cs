using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonus
{
    string Alias { get; set; }
    BonusType BonusType { get; set; }
    float HealthValue { get; set; }
    float FireSpeedCoefficient { get; set; }
    float ReloadSpeedCoefficient { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    string Alias { get; set; } 
    GameObject UnitGameObject { get; set; }
    UnitType UnitType { get; set; }
    float BaseAttack { get; set; }
    int Health { get; set; }
    float Armor { get; set; }
    float Speed { get; set; }
    bool Ghost { get; set; }
    IDiapasonSpawnPosition DiapasonSpawnPosition { get; set; }
}

public enum UnitType
{
    Player,
    EnemyType1,
    EnemyType2,
    EnemyType3
}

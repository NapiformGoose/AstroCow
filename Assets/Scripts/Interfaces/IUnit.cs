using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;

public interface IUnit : IEntity
{
    string Alias { get; set; }
    UnitType UnitType { get; set; }
    float Health { get; set; }
    float MoveSpeed { get; set; }
    bool Ghost { get; set; }
    float InactiveTime { get; set; }
    float ExperienceValue { get; set; }
    float LootPercent { get; set; }
    int MagazineCapacity { get; set; }
    BonusType BonusType { get; set; }

    GameObject GameObject { get; set; }
    Collider2D Collider2D { get; set; }
    Rigidbody2D RigidBody2D { get; set; }
    IBehaviour Behaviour { get; set; }
    IDiapasonSpawnPosition DiapasonSpawnPosition { get; set; }
    IWeapon Weapon { get; set; }
    Team Team { get; set; }
    GameObject ShootPosition { get; set; }

    GameObject Text { get; set; }
}



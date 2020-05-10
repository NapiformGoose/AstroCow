using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;

public interface IUnit
{
    string Alias { get; set; }
    GameObject GameObject { get; set; }
    Collider2D Collider2D { get; set; }
    Rigidbody2D RigidBody2D { get; set; }
    IBehaviour Behaviour { get; set; }
    UnitType UnitType { get; set; }
    float Health { get; set; }
    float MoveSpeed { get; set; }
    bool Ghost { get; set; }
    IDiapasonSpawnPosition DiapasonSpawnPosition { get; set; }
    IWeapon Weapon { get; set; }
    GameObject ShootPosition { get; set; }
    Team Team { get; set; }

    GameObject Text { get; set; }
}



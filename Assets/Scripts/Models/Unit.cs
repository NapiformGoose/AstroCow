﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;

public class Unit : IUnit
{
    public string Alias { get; set; }
    public GameObject GameObject { get; set; }
    public Collider2D Collider2D { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }
    public UnitType UnitType { get; set; }
    public float Health { get; set; }
    public float MoveSpeed { get; set; }
    public bool Ghost { get; set; }
    public IDiapasonSpawnPosition DiapasonSpawnPosition { get; set; }
    public WeaponType WeaponType { get; set; }
    public IWeapon Weapon { get; set; }
    public IBehaviour Behaviour { get; set; }
    public GameObject ShootPosition { get; set; }
    public Team Team { get; set; }

    public GameObject Text { get; set; }
}
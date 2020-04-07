using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : IUnit
{
    public string Alias { get; set; }
    public GameObject UnitGameObject { get; set; }
    public Collider2D UnitCollider2D { get; set; }
    public Rigidbody2D UnitRigidBody2D { get; set; }
    public UnitType UnitType { get; set; }
    public float BaseAttack { get; set; }
    public int Health { get; set; }
    public float Armor { get; set; }
    public float Speed { get; set; }
    public bool Ghost { get; set; }
    public IDiapasonSpawnPosition DiapasonSpawnPosition { get; set; }
}

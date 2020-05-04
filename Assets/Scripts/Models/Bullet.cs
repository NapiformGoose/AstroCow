using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Bullet : IBullet
{
    public string Alias { get; set; }
    public GameObject BulletGameObject { get; set; }
    public Collider2D BulletCollider2D { get; set; }
    public Rigidbody2D BulletRigidBody2D { get; set; }
    public BulletType BulletType { get; set; }
    public Behaviour Behaviour { get; set; }
    public int MoveSpeed { get; set; }
    public Team Aim { get; set; }
    public int Damage { get; set; }
}

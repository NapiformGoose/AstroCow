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
    public BulletBehaviourType BulletBehaviourType { get; set; }
    public IBehaviour Behaviour { get; set; }
    public float MoveSpeed { get; set; }
    public Team Aim { get; set; }
    public float Damage { get; set; }
}

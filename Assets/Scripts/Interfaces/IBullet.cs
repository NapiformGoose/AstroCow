using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public interface IBullet
{
    string Alias { get; set; }
    GameObject BulletGameObject { get; set; }
    Collider2D BulletCollider2D { get; set; }
    Rigidbody2D BulletRigidBody2D { get; set; }
    BulletType BulletType { get; set; }
    Behaviour Behaviour { get; set; }
    int MoveSpeed { get; set; }
    Team Aim { get; set; }
    int Damage { get; set; }
}


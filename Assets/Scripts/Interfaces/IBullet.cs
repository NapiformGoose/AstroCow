using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public interface IBullet
{
    string Alias { get; set; }
    GameObject GameObject { get; set; }
    Collider2D Collider2D { get; set; }
    Rigidbody2D RigidBody2D { get; set; }
    BulletType BulletType { get; set; }
    BulletBehaviourType BulletBehaviourType { get; set; }
    IBehaviour Behaviour { get; set; }
    float MoveSpeed { get; set; }
    Team Aim { get; set; }
    float Damage { get; set; }
}


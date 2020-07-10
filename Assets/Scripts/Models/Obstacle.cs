using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Obstacle : IObstacle
{
    public string Alias { get; set; }
    public GameObject GameObject { get; set; }
    public Collider2D Collider2D { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }
    public ObstacleType ObstacleType { get; set; }
    //Behaviour
    public Vector3 SpawnPosition { get; set; }
    public float DamagePercent { get; set; }
    public float Health { get; set; }
}

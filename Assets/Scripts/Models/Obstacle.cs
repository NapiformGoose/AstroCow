using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Obstacle : IObstacle
{
    public string Alias { get; set; }
    public GameObject ObstacleGameObject { get; set; }
    public Collider2D ObstacleCollider2D { get; set; }
    public Rigidbody2D ObstacleRigidBody2D { get; set; }
    public ObstacleType ObstacleType { get; set; }
    public Vector3 SpawnPosition { get; set; }
}

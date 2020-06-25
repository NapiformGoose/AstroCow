using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public interface IObstacle : IEntity
{
    string Alias { get; set; }
    GameObject GameObject { get; set; }
    Collider2D Collider2D { get; set; }
    Rigidbody2D RigidBody2D { get; set; }
    ObstacleType ObstacleType { get; set; }
    Vector3 SpawnPosition { get; set; }
}
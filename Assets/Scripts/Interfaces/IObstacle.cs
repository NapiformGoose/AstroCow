using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public interface IObstacle : IEntity
{
    string Alias { get; set; }
    GameObject ObstacleGameObject { get; set; }
    Collider2D ObstacleCollider2D { get; set; }
    Rigidbody2D ObstacleRigidBody2D { get; set; }
    ObstacleType ObstacleType { get; set; }
    Vector3 SpawnPosition { get; set; }
}
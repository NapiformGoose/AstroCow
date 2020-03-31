using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : IObstacle
{
    public string Alias { get; set; }
    public GameObject ObstacleGameObject { get; set; }
    public ObstacleType ObstacleType { get; set; }
    public Vector3 SpawnPosition { get; set; }
}

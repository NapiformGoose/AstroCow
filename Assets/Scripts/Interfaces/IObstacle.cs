using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
    string Alias { get; set; }
    GameObject ObstacleGameObject { get; set; }
    ObstacleType ObstacleType { get; set; }
    Vector3 SpawnPosition { get; set; }
}

public enum ObstacleType
{
    SteelWall,
    EnergyWall
}
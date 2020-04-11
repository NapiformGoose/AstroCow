using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class ObjectCreateManager : IObjectCreateManager
{
    IDictionary<string, GameObject> _prefabs;
    public ObjectCreateManager()
    {

    }
    public void AddPrefabs(IDictionary<string, GameObject> prefabs)
    {
        _prefabs = prefabs;
    }
    //описать другие свойства
    public IObstacle CreateObstacle(IObstacle obstacle, Vector3 currentPos)
    {
        IObstacle newObstacle = new Obstacle
        {
            ObstacleGameObject = GameObject.Instantiate(_prefabs[obstacle.ObstacleType.ToString()])
        };
        newObstacle.ObstacleCollider2D = newObstacle.ObstacleGameObject.GetComponent<Collider2D>() as Collider2D;
        newObstacle.ObstacleRigidBody2D = newObstacle.ObstacleGameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;

        newObstacle.ObstacleGameObject.transform.position = currentPos + obstacle.SpawnPosition;
        return newObstacle;
    }

    public IUnit CreateUnit(IUnit unit, Vector3 spawnPos)
    {
        IUnit newUnit = new Unit
        {
            UnitGameObject = GameObject.Instantiate(_prefabs[unit.UnitType.ToString()]),
        };
        newUnit.UnitCollider2D = newUnit.UnitGameObject.GetComponent<Collider2D>() as Collider2D;
        newUnit.UnitRigidBody2D = newUnit.UnitGameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        newUnit.UnitGameObject.transform.position = spawnPos;
        return newUnit;
    }
    public Vector3 CalculateUnitSpawnPosition(IDiapasonSpawnPosition diapasonSpawnPosition, Vector3 currentCellPos)
    {
        int x = Random.Range(diapasonSpawnPosition.minXPos, diapasonSpawnPosition.maxXPos);
        int y = Random.Range(diapasonSpawnPosition.minYPos, diapasonSpawnPosition.maxYPos);

        Vector3 spawnPosition = new Vector3(currentCellPos.x + x, currentCellPos.y + y, diapasonSpawnPosition.ZPos);
        return spawnPosition;
    }
}




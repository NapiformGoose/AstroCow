using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectCreateManager
{
    void AddPrefabs(IDictionary<string, GameObject> prefabs);
    IObstacle CreateObstacle(IObstacle obstacle, Vector3 currentCellPos);
    IUnit CreateUnit(IUnit unit, Vector3 spawnPos);
    Vector3 CalculateUnitSpawnPosition(IDiapasonSpawnPosition diapasonSpawnPosition, Vector3 currentCellPos);
    void SetBehaviour(IUnit unit, Vector3 spawnPos);
    IBullet CreateBullet(GameObject bulletPrefab);
}

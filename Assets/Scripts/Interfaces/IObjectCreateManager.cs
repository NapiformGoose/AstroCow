using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectCreateManager
{
    void AddPrefabs(IDictionary<string, GameObject> prefabs);

    IObstacle CreateObstacle(IObstacle obstacle, Vector3 currentCellPos);
    IUnit CreateUnit(IUnit unit);
    IBullet CreateBullet(GameObject bulletPrefab);
    IBonus CreateBonus(IBonus bonusTemplate);

    Vector3 CalculateUnitSpawnPosition(IDiapasonSpawnPosition diapasonSpawnPosition, Vector3 currentCellPos);
    void SetBehaviour(IUnit unit, Vector3 spawnPos);

    void InstantiateUnit(IUnit unit, Transform transformCanvas);
    void InstantiateObstacle(IObstacle obstacle);
    void InstantiateBullet(IBullet bullet);
    void InstantiateCoin(ICoin coin);
}

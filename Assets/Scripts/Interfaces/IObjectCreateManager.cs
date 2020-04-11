using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectCreateManager
{
    void AddPrefabs(IDictionary<string, GameObject> prefabs);
    IObstacle CreateObstacle(IObstacle obstacle, Vector3 currentPos);
    IUnit CreateUnit(IUnit unit, Vector3 currentCellPos);
    Vector3 CalculateUnitSpawnPosition(IDiapasonSpawnPosition diapasonSpawnPosition, Vector3 currentCellPos);

}

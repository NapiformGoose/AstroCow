﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICell
{
    GameObject CellGameObject { get; set; }
    float Difficult { get; set; }
    int Id { get; set; }
    IDictionary<int, IDiapasonSpawnPosition> DiapasonSpawnPositions { get; set; }
    IList<IUnit> Units { get; set; }
    IList<IObstacle> ObstacleSet { get; set; }
}

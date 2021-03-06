﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : ICell
{
    public GameObject CellGameObject { get; set; }
    public float Difficult { get; set; }
    public int Id { get; set; }
    public IDictionary<int, IDiapasonSpawnPosition> DiapasonSpawnPositions { get; set; }
    public IList<IUnit> Units { get; set; }
    public IList<IObstacle> ObstacleSet { get; set; }
}

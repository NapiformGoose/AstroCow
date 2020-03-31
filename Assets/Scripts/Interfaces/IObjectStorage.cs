using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IObjectStorage
    {
        IDictionary<string, IUnit> UnitTemplates { get; set; }
        IDictionary<string, IObstacle> ObstacleTemplates { get; set; }
        IDictionary<int, IList<IObstacle>> ObstacleSet { get; set; }
        IDictionary<int, ICell> Cells { get; set; }
        IDictionary<int, IList<ICell>> CellSets { get; set; }
        IList<ILevel> Levels { get; set; }
        Collider2D LowerTrigger { get; set; }
        void Initialization(string playerName);
    }
}

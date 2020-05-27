using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class ObjectStorage : IObjectStorage
    {
        public IDictionary<string, GameObject> Prefabs { get; set; }
        public IDictionary<string, IUnit> UnitTemplates { get; set; }
        public IDictionary<string, IObstacle> ObstacleTemplates { get; set; }
        public IDictionary<string, IWeapon> WeaponTemplates { get; set; }
        public IDictionary<string, IBullet> BulletTemplates { get; set; }
        public IDictionary<int, IList<IObstacle>> ObstacleSet { get; set; }
        public IDictionary<int, ICell> Cells { get; set; }
        public IDictionary<int, IList<ICell>> CellSets { get; set; }
        public IList<ILevel> Levels { get; set; }
        public IDictionary<string, IList<IUnit>> Units { get; set; }
        public IDictionary<string, IList<IObstacle>> Obstacles { get; set; }
        public IDictionary<string, IList<IBullet>> Bullets { get; set; }

        public Collider2D ActivationTrigger { get; set; }
        public Collider2D TopDeactivationTrigger { get; set; }
        public Collider2D DownDeactivationTrigger { get; set; }
        public Collider2D LeftDeactivationTrigger { get; set; }
        public Collider2D RightDeactivationTrigger { get; set; }
        public Canvas Canvas { get; set; }

        public ObjectStorage()
        {
            Prefabs = new Dictionary<string, GameObject>();
            UnitTemplates = new Dictionary<string, IUnit>();
            ObstacleTemplates = new Dictionary<string, IObstacle>();
            WeaponTemplates = new Dictionary<string, IWeapon>();
            BulletTemplates = new Dictionary<string, IBullet>();
            ObstacleSet = new Dictionary<int, IList<IObstacle>>();
            Cells = new Dictionary<int, ICell>();
            CellSets = new Dictionary<int, IList<ICell>>();
            Levels = new List<ILevel>();
            Units = new Dictionary<string, IList<IUnit>>();
            Obstacles = new Dictionary<string, IList<IObstacle>>();
            Bullets = new Dictionary<string, IList<IBullet>>();
        }
    }
}

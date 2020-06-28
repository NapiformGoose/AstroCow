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
        public IDictionary<string, IBonus> BonusesTemplates { get; set; }

        public IDictionary<int, IList<IObstacle>> ObstacleSet { get; set; }
        public IDictionary<int, ICell> Cells { get; set; }
        public IDictionary<int, IList<ICell>> CellSets { get; set; }
        public IList<ILevel> Levels { get; set; }
        public IDictionary<string, IList<IUnit>> Units { get; set; }
        public IDictionary<string, IList<IObstacle>> Obstacles { get; set; }
        public IDictionary<string, IList<IBullet>> Bullets { get; set; }
        public IDictionary<string, IList<IBonus>> Bonuses { get; set; }
        public IList<IUpgrade> Upgrades { get; set; }

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
            BonusesTemplates = new Dictionary<string, IBonus>();

            ObstacleSet = new Dictionary<int, IList<IObstacle>>();
            Cells = new Dictionary<int, ICell>();
            CellSets = new Dictionary<int, IList<ICell>>();
            Levels = new List<ILevel>();

            Units = new Dictionary<string, IList<IUnit>>();
            Obstacles = new Dictionary<string, IList<IObstacle>>();
            Bullets = new Dictionary<string, IList<IBullet>>();
            Bonuses = new Dictionary<string, IList<IBonus>>();
            Upgrades = new List<IUpgrade>();
        }

        public void ClearLevelData()
        {
            foreach (string key in Units.Keys)
            {
                foreach (IUnit unit in Units[key])
                {
                    GameObject.Destroy(unit.GameObject);
                    GameObject.Destroy(unit.Text.gameObject);

                }
            }
            foreach (string key in Obstacles.Keys)
            {
                foreach (IObstacle obstacle in Obstacles[key])
                {
                    GameObject.Destroy(obstacle.GameObject);
                }
            }
            foreach (string key in Bullets.Keys)
            {
                foreach (IBullet bullet in Bullets[key])
                {
                    GameObject.Destroy(bullet.GameObject);
                }
            }
            foreach (string key in Bonuses.Keys)
            {
                foreach (IBonus bonus in Bonuses[key])
                {
                    GameObject.Destroy(bonus.GameObject);
                }
            }
            Units = new Dictionary<string, IList<IUnit>>();
            Obstacles = new Dictionary<string, IList<IObstacle>>();
            Bullets = new Dictionary<string, IList<IBullet>>();
            Bonuses = new Dictionary<string, IList<IBonus>>();
        }
    }
}

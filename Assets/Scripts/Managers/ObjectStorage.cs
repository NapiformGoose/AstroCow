using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class ObjectStorage : IObjectStorage
    {
        IObjectCreateManager _objectCreateManager;

        public IDictionary<string, GameObject> Prefabs { get; set; }
        public IDictionary<string, IUnit> UnitTemplates { get; set; }
        public IDictionary<string, IObstacle> ObstacleTemplates { get; set; }
        public IDictionary<int, IList<IObstacle>> ObstacleSet { get; set; }
        public IDictionary<int, ICell> Cells { get; set; }
        public IDictionary<int, IList<ICell>> CellSets { get; set; }
        public IList<ILevel> Levels { get; set; }
        public IDictionary<string, IList<IUnit>> Units { get; set; }
        public IDictionary<string, IList<IObstacle>> Obstacles { get; set; }
        public Collider2D LowerTrigger { get; set; }

        public ObjectStorage(IObjectCreateManager objectCreateManager)
        {
            _objectCreateManager = objectCreateManager;
            Prefabs = new Dictionary<string, GameObject>();
            UnitTemplates = new Dictionary<string, IUnit>();
            ObstacleTemplates = new Dictionary<string, IObstacle>();
            ObstacleSet = new Dictionary<int, IList<IObstacle>>();
            Cells = new Dictionary<int, ICell>();
            CellSets = new Dictionary<int, IList<ICell>>();
            Levels = new List<ILevel>();
            Units = new Dictionary<string, IList<IUnit>>();
            Obstacles = new Dictionary<string, IList<IObstacle>>();
        }

        public void Initialization(string playerName)
        {
            LoadPrefabs();
            _objectCreateManager.AddPrefabs(Prefabs);
            LoadLevel();
        }

        #region LoadData
        void LoadPrefabs()
        {
            Prefabs.Add(Constants.playerPrefabName, Resources.Load(Constants.prefabPath + Constants.playerPrefabName) as GameObject);
            Prefabs.Add(Constants.EnemyType1PrefabName, Resources.Load(Constants.prefabPath + Constants.EnemyType1PrefabName) as GameObject);
            Prefabs.Add(Constants.EnemyType2PrefabName, Resources.Load(Constants.prefabPath + Constants.EnemyType2PrefabName) as GameObject);
            Prefabs.Add(Constants.EnemyType3PrefabName, Resources.Load(Constants.prefabPath + Constants.EnemyType3PrefabName) as GameObject);
            Prefabs.Add(Constants.EnergyWallPrefabName, Resources.Load(Constants.prefabPath + Constants.EnergyWallPrefabName) as GameObject);
            Prefabs.Add(Constants.SteelWallPrefabName, Resources.Load(Constants.prefabPath + Constants.SteelWallPrefabName) as GameObject);

            Prefabs.Add(Constants.controllerPrefabName, Resources.Load(Constants.prefabPath + Constants.controllerPrefabName) as GameObject);
            Prefabs.Add(Constants.cellPrefabName, Resources.Load(Constants.prefabPath + Constants.cellPrefabName) as GameObject);
            Prefabs.Add(Constants.lowerTriggerName, Resources.Load(Constants.prefabPath + Constants.lowerTriggerName) as GameObject);
        }

        void LoadLevel()
        {
            IList<ICell> currentCellSet = CellSets[Levels[0].CellSet];
            Vector3 currentCellPos = Constants.startCellPosition;
            for (int i = 0; i < currentCellSet.Count; i++)
            {
                currentCellSet[i].CellGameObject = GameObject.Instantiate(Prefabs[Constants.cellPrefabName]);
                currentCellSet[i].CellCollider = currentCellSet[i].CellGameObject.GetComponent<Collider2D>(); //?
                currentCellSet[i].CellGameObject.transform.position = currentCellPos;

                foreach (IUnit unit in currentCellSet[i].Units)
                {
                    Vector3 spawnPos = _objectCreateManager.CalculateUnitSpawnPosition(unit.DiapasonSpawnPosition, currentCellPos);
                    if(Units.Keys.Contains(unit.UnitType.ToString()))
                    {
                        Units[unit.UnitType.ToString()].Add(_objectCreateManager.CreateUnit(unit, spawnPos));
                    }

                    Units[unit.UnitType.ToString()] = new List<IUnit>();
                    Units[unit.UnitType.ToString()].Add(_objectCreateManager.CreateUnit(unit, spawnPos));
                }
                foreach (IObstacle obstacle in currentCellSet[i].ObstacleSet)
                {
                    if(Obstacles.Keys.Contains(obstacle.ObstacleType.ToString()))
                    {
                        Obstacles[obstacle.ObstacleType.ToString()].Add(_objectCreateManager.CreateObstacle(obstacle, currentCellPos));
                    }

                    Obstacles[obstacle.ObstacleType.ToString()] = new List<IObstacle>();
                    Obstacles[obstacle.ObstacleType.ToString()].Add(_objectCreateManager.CreateObstacle(obstacle, currentCellPos));
                }
                currentCellPos += Constants.distanceToNextCell;
            }
        }
        #endregion
    }
}

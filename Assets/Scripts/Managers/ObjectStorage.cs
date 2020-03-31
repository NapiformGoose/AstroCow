using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class ObjectStorage : IObjectStorage
    {
        IDictionary<string, GameObject> _prefabs;
        public IDictionary<string, IUnit> UnitTemplates { get; set; }
        public IDictionary<string, IObstacle> ObstacleTemplates { get; set; }
        public IDictionary<int, IList<IObstacle>> ObstacleSet { get; set; }
        public IDictionary<int, ICell> Cells { get; set; }
        public IDictionary<int, IList<ICell>> CellSets { get; set; }
        public IList<ILevel> Levels { get; set; }
        public Collider2D LowerTrigger { get; set; }

        public ObjectStorage()
        {
            _prefabs = new Dictionary<string, GameObject>();
            UnitTemplates = new Dictionary<string, IUnit>();
            ObstacleTemplates = new Dictionary<string, IObstacle>();
            ObstacleSet = new Dictionary<int, IList<IObstacle>>();
            Cells = new Dictionary<int, ICell>();
            CellSets = new Dictionary<int, IList<ICell>>();
            Levels = new List<ILevel>();
        }

        public void Initialization(string playerName)
        {
            LoadPrefabs();
            LoadLevel();
        }

        #region LoadData
        void LoadPrefabs()
        {
            _prefabs.Add(Constants.playerPrefabName, Resources.Load(Constants.prefabPath + Constants.playerPrefabName) as GameObject);
            _prefabs.Add(Constants.EnemyType1PrefabName, Resources.Load(Constants.prefabPath + Constants.EnemyType1PrefabName) as GameObject);
            _prefabs.Add(Constants.EnemyType2PrefabName, Resources.Load(Constants.prefabPath + Constants.EnemyType2PrefabName) as GameObject);
            _prefabs.Add(Constants.EnemyType3PrefabName, Resources.Load(Constants.prefabPath + Constants.EnemyType3PrefabName) as GameObject);
            _prefabs.Add(Constants.EnergyWallPrefabName, Resources.Load(Constants.prefabPath + Constants.EnergyWallPrefabName) as GameObject);
            _prefabs.Add(Constants.SteelWallPrefabName, Resources.Load(Constants.prefabPath + Constants.SteelWallPrefabName) as GameObject);

            _prefabs.Add(Constants.controllerPrefabName, Resources.Load(Constants.prefabPath + Constants.controllerPrefabName) as GameObject);
            _prefabs.Add(Constants.cellPrefabName, Resources.Load(Constants.prefabPath + Constants.cellPrefabName) as GameObject);
            _prefabs.Add(Constants.lowerTriggerName, Resources.Load(Constants.prefabPath + Constants.lowerTriggerName) as GameObject);
        }

        void LoadLevel()
        {
            IList<ICell> currentCellSet = CellSets[Levels[0].CellSet];
            Vector3 currentPos = Constants.startCellPosition;
            for (int i = 0; i < currentCellSet.Count; i++)
            {
                currentCellSet[i].CellGameObject = GameObject.Instantiate(_prefabs[Constants.cellPrefabName]);
                currentCellSet[i].CellCollider = currentCellSet[i].CellGameObject.GetComponent<Collider2D>();
                currentCellSet[i].CellGameObject.transform.position = currentPos;

                CreateUnits(currentCellSet[i].Units, currentPos);
                CreateObstacles(currentCellSet[i].ObstacleSet, currentPos);
                currentPos += Constants.distanceToNextCell;
            }
        }
        void CreateObstacles(IList<IObstacle> obstacles, Vector3 currentPos)
        {
            foreach(IObstacle obstacle in obstacles)
            {
                obstacle.ObstacleGameObject = GameObject.Instantiate(_prefabs[obstacle.ObstacleType.ToString()]);
                Vector3 spawnPos = new Vector3(currentPos.x + obstacle.SpawnPosition.x, currentPos.y + obstacle.SpawnPosition.y, obstacle.SpawnPosition.z);
                obstacle.ObstacleGameObject.transform.position = spawnPos;
            }
        }
        void CreateUnits(IList<IUnit> units, Vector3 currentPos)
        {
            foreach(IUnit unit in units)
            {
                unit.UnitGameObject = GameObject.Instantiate(_prefabs[unit.UnitType.ToString()]);
                unit.UnitGameObject.transform.position = CalculateUnitSpawnPosition(unit.DiapasonSpawnPosition, currentPos);
            }
        }
        Vector3 CalculateUnitSpawnPosition(IDiapasonSpawnPosition diapasonSpawnPosition, Vector3 currentPos)
        {
            int x = Random.Range(diapasonSpawnPosition.minXPos, diapasonSpawnPosition.maxXPos);
            int y = Random.Range(diapasonSpawnPosition.minYPos, diapasonSpawnPosition.maxYPos);

            Vector3 spawnPosition = new Vector3(currentPos.x + x, currentPos.y + y, diapasonSpawnPosition.ZPos);
            return spawnPosition;
        }
        #endregion
    }
}

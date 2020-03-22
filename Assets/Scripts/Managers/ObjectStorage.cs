using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ObjectStorage : IObjectStorage
    {
        IDictionary<string, GameObject> _prefabs;
        public Player Player { get; set; }
        public GameObject Controller { get; set; }
        public IList<Cell> Cells { get; set; }

        //public int Score { get; set; }
        public Collider2D LowerTrigger { get; set; }
        public ObjectStorage()
        {
            _prefabs = new Dictionary<string, GameObject>();
            Cells = new List<Cell>(3);
        }

        public void Initialization(string playerName)
        {
            LoadPrefabs();
            Player = CreatePlayer(playerName);
            CreateCells();
        }

        #region LoadData
        void LoadPrefabs()
        {
            _prefabs.Add(Constants.playerPrefabName, Resources.Load(Constants.prefabPath + Constants.playerPrefabName) as GameObject);
            _prefabs.Add(Constants.controllerPrefabName, Resources.Load(Constants.prefabPath + Constants.controllerPrefabName) as GameObject);
            _prefabs.Add(Constants.cellPrefabName, Resources.Load(Constants.prefabPath + Constants.cellPrefabName) as GameObject);
            _prefabs.Add(Constants.lowerTriggerName, Resources.Load(Constants.prefabPath + Constants.lowerTriggerName) as GameObject);
        }
        Player CreatePlayer(string name)
        {
            Player player = new Player
            {
                Name = name,
                Speed = Constants.playerSpeed,
                UnitGameObject = GameObject.Instantiate(_prefabs[Constants.playerPrefabName])
            };
            Controller = GameObject.Instantiate(_prefabs[Constants.controllerPrefabName]);
            Controller.SetActive(false);

            LowerTrigger = GameObject.Find(Constants.lowerTriggerName).GetComponent<Collider2D>();

            //player.PlayerCollider2D = player.PlayerGameObject.GetComponent<Collider2D>();
            //player.UnitGameObject.SetActive(false);
            return player;
        }
        void CreateCells()
        {
            for(int i = 0; i < 3; i++)
            {
                Cell cell = new Cell
                {
                    Name = "Cell_" + i.ToString(),
                    CellGameObject = GameObject.Instantiate(_prefabs[Constants.cellPrefabName])
                };
                cell.CellCollider = cell.CellGameObject.GetComponent<Collider2D>();
                cell.CellGameObject.name = cell.Name;

                Cells.Add(cell);
            }
        }
        #endregion
    }
}

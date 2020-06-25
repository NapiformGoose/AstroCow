using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Bootstrapper : MonoBehaviour
    {
        IUpdateManager _updateManager;
        IPoolManager _poolManager;
        IObjectStorage _objectStorage;
        ILevelManager _levelManager;
        IDataLoadManager _dataLoadManager;
        IObjectCreateManager _objectCreateManager;
        IBehaviourManager _behaviourManager;
        UIManager _UIManager;

        void Start()
        {
            var updateManagerObject = new GameObject("UpdateManager");
            _updateManager = updateManagerObject.AddComponent<UpdateManager>();

            var coroutiner = new GameObject("Coroutiner").AddComponent<Coroutiner>();

            _objectCreateManager = new ObjectCreateManager();
            _objectStorage = new ObjectStorage();

            _dataLoadManager = new DataLoadManager(_objectStorage);

            _poolManager = new PoolManager(_objectStorage, _objectCreateManager);

            //_poolManager.LoadLevel();
            _dataLoadManager.ReadConfig();
            _dataLoadManager.LoadPrefabs();

            _UIManager = new UIManager(_updateManager, _objectStorage, _poolManager, _dataLoadManager);
            _UIManager.ShowMainMenu(); //start game here
            _behaviourManager = new BehaviourManager(_updateManager, _objectStorage);

        }

    }
}



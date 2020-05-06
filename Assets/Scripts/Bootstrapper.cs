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
        PoolManager _poolManager;
        IControlManager _controlManager;
        IObjectStorage _objectStorage;
        ILevelManager _levelManager;
        IDataLoadManager _dataLoadManager;
        IObjectCreateManager _objectCreateManager;
        BehaviourManager _behaviourManager;

        void Start()
        {
            var updateManagerObject = new GameObject("UpdateManager");
            _updateManager = updateManagerObject.AddComponent<UpdateManager>();

            var coroutiner = new GameObject("Coroutiner").AddComponent<Coroutiner>();
            BehaviourList.Coroutiner = coroutiner;

            _objectCreateManager = new ObjectCreateManager();
            _objectStorage = new ObjectStorage();

            BehaviourList.ObjectStorage = _objectStorage;

            _dataLoadManager = new DataLoadManager(_objectStorage);
            _dataLoadManager.ReadConfig();
            _dataLoadManager.LoadPrefabs();

            _poolManager = new PoolManager(_objectStorage, _objectCreateManager);
            _poolManager.LoadLevel();

            _controlManager = new ControlManager(_updateManager, _objectStorage);
            _behaviourManager = new BehaviourManager(_updateManager, _objectStorage);

            _updateManager.CustomStart();
        }

    }
}



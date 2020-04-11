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
        IControlManager _controlManager;
        IObjectStorage _objectStorage;
        ILevelManager _levelManager;
        IDataLoadManager _dataLoadManager;
        IObjectCreateManager _objectCreateManager;
        void Start()
        {
            var updateManagerObject = new GameObject("UpdateManager");
            _updateManager = updateManagerObject.AddComponent<UpdateManager>();
            _objectCreateManager = new ObjectCreateManager();
            _objectStorage = new ObjectStorage(_objectCreateManager);

            //_levelManager = new LevelManager(_updateManager, _objectStorage);
            //_levelManager.StartLevel();
            _dataLoadManager = new DataLoadManager(_objectStorage);
            _dataLoadManager.Read();
            _objectStorage.Initialization(Constants.playerPrefabName);
            _controlManager = new ControlManager(_updateManager, _objectStorage);

            _updateManager.CustomStart();
        }

    }
}



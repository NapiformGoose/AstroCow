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
        void Start()
        {
            var updateManagerObject = new GameObject("UpdateManager");
            _updateManager = updateManagerObject.AddComponent<UpdateManager>();

            _objectStorage = new ObjectStorage();
            _objectStorage.Initialization(Constants.playerPrefabName);
            _controlManager = new ControlManager(_updateManager, _objectStorage);
            _levelManager = new LevelManager(_updateManager, _objectStorage);
            _levelManager.StartLevel();

            _updateManager.CustomStart();
        }
        
    }
}



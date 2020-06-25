using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;
using TMPro;

namespace Assets.Scripts.Managers
{
    public class UIManager : IUpdatable
    {
        IUpdateManager _updateManager;
        IObjectStorage _objectStorage;
        IPoolManager _poolManager;
        IDataLoadManager _dataLoadManager;

        IDictionary<string, Button> _buttons { get; set; }

        GameObject _mainMenu { get; set; }

        public UIManager(IUpdateManager updateManager, IObjectStorage objectStorage, IPoolManager poolManager, IDataLoadManager dataLoadManager)
        {
            _updateManager = updateManager;
            _objectStorage = objectStorage;
            _poolManager = poolManager;
            _dataLoadManager = dataLoadManager;

            _updateManager.AddUpdatable(this);

            _buttons = new Dictionary<string, Button>();

            _buttons.Add(Constants.playBatton, GameObject.Find(Constants.playBatton).GetComponent<Button>());
            _buttons.Add(Constants.exitButton, GameObject.Find(Constants.exitButton).GetComponent<Button>());

            _buttons.Add(Constants.menuButton, GameObject.Find(Constants.menuButton).GetComponent<Button>());

            _buttons.Add(Constants.continueButton, GameObject.Find(Constants.continueButton).GetComponent<Button>());
            _buttons.Add(Constants.restartButton, GameObject.Find(Constants.restartButton).GetComponent<Button>());
            _buttons.Add(Constants.backButton, GameObject.Find(Constants.backButton).GetComponent<Button>());

            _buttons[Constants.playBatton].onClick.AddListener(delegate () { StartLevel(); });
            _buttons[Constants.exitButton].onClick.AddListener(delegate () { QuitApplication(); });

            _buttons[Constants.menuButton].onClick.AddListener(delegate () { OpenGameMenu(); });

            _buttons[Constants.continueButton].onClick.AddListener(delegate () { ContinueLevel(); });
            _buttons[Constants.restartButton].onClick.AddListener(delegate () { RestartLevel(); });
            _buttons[Constants.backButton].onClick.AddListener(delegate () { OpenMainMenu(); });
        }

        public void ShowMainMenu()
        {
            _buttons[Constants.playBatton].gameObject.SetActive(true);
            _buttons[Constants.exitButton].gameObject.SetActive(true);
            _buttons[Constants.menuButton].gameObject.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);
            _buttons[Constants.continueButton].gameObject.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);
        }
        void StartLevel()
        {
            _buttons[Constants.playBatton].gameObject.SetActive(false);
            _buttons[Constants.exitButton].gameObject.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);

            _buttons[Constants.menuButton].gameObject.SetActive(true);

            _poolManager.LoadLevel();
            _poolManager.InstantiateEntities();

            _updateManager.CustomStart();
        }

        void RestartLevel()
        {
            _buttons[Constants.playBatton].gameObject.SetActive(false);
            _buttons[Constants.exitButton].gameObject.SetActive(false);
            _buttons[Constants.continueButton].gameObject.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);

            _buttons[Constants.menuButton].gameObject.SetActive(true);

            Camera.main.transform.position = new Vector3(0, 0, -10);
            _objectStorage.ClearLevelData();
            _poolManager.LoadLevel();
            _poolManager.InstantiateEntities();

            _updateManager.CustomStart();
        }

        void QuitApplication()
        {
            Application.Quit();
        }

        void OpenGameMenu()
        {
            _buttons[Constants.continueButton].gameObject.SetActive(true);
            _buttons[Constants.restartButton].gameObject.SetActive(true);
            _buttons[Constants.backButton].gameObject.SetActive(true);
            _buttons[Constants.menuButton].gameObject.SetActive(false);
            _updateManager.Stop();

        }

        void OpenMainMenu()
        {
            _buttons[Constants.playBatton].gameObject.SetActive(true);
            _buttons[Constants.exitButton].gameObject.SetActive(true);

            _buttons[Constants.continueButton].gameObject.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);

            Camera.main.transform.position = new Vector3(0, 0, -10);

            _objectStorage.ClearLevelData();
            _updateManager.Stop();
        }

        void ContinueLevel()
        {
            _buttons[Constants.continueButton].gameObject.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);

            _buttons[Constants.menuButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
        }


        public void CustomUpdate()
        {
           
        }
        public void CustomFixedUpdate()
        {

        }
    }
}

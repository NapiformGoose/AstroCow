using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;
using TMPro;
using System.Linq;

namespace Assets.Scripts.Managers
{
    public class UIManager : IUpdatable
    {
        IUpdateManager _updateManager;
        IObjectStorage _objectStorage;
        IPoolManager _poolManager;
        IDataLoadManager _dataLoadManager;
        IBehaviourManager _behaviourManager;

        IDictionary<string, Button> _buttons { get; set; }
        IUnit _player;

        float minimumHealth;
        float maximumHealth;
        int lowHealth = 7;
        int highHealth = 14;

        Slider healthBar;
        Slider experiencebar;

        public Color highHealthColor = new Color(0.35f, 1f, 0.35f);
        public Color mediumHealthColor = new Color(0.9450285f, 1f, 0.4481132f);
        public Color lowHealthColor = new Color(1f, 0.259434f, 0.259434f);

        IList<IUpgrade> upgrades;
        GameObject _upgradeMenu;
        TextMeshProUGUI _firstTitle;
        TextMeshProUGUI _firstDescription;
        TextMeshProUGUI _secondTitle;
        TextMeshProUGUI _secondDescription;
        TextMeshProUGUI _thirdTitle;
        TextMeshProUGUI _thirdDescription;

        public UIManager(IUpdateManager updateManager, IObjectStorage objectStorage, IPoolManager poolManager, IDataLoadManager dataLoadManager, IBehaviourManager behaviourManager)
        {
            _updateManager = updateManager;
            _objectStorage = objectStorage;
            _poolManager = poolManager;
            _dataLoadManager = dataLoadManager;
            _behaviourManager = behaviourManager;

            _updateManager.AddUpdatable(this);

            _buttons = new Dictionary<string, Button>();

            _buttons.Add(Constants.playBatton, GameObject.Find(Constants.playBatton).GetComponent<Button>());
            _buttons.Add(Constants.exitButton, GameObject.Find(Constants.exitButton).GetComponent<Button>());

            _buttons.Add(Constants.menuButton, GameObject.Find(Constants.menuButton).GetComponent<Button>());

            _buttons.Add(Constants.continueButton, GameObject.Find(Constants.continueButton).GetComponent<Button>());
            _buttons.Add(Constants.restartButton, GameObject.Find(Constants.restartButton).GetComponent<Button>());
            _buttons.Add(Constants.backButton, GameObject.Find(Constants.backButton).GetComponent<Button>());

            _buttons.Add(Constants.firstUpgradeButton, GameObject.Find(Constants.firstUpgradeButton).GetComponent<Button>());
            _buttons.Add(Constants.secondUpgradeButton, GameObject.Find(Constants.secondUpgradeButton).GetComponent<Button>());
            _buttons.Add(Constants.thirdUpgradeButton, GameObject.Find(Constants.thirdUpgradeButton).GetComponent<Button>());

            _buttons[Constants.playBatton].onClick.AddListener(delegate () { StartLevel(); });
            _buttons[Constants.exitButton].onClick.AddListener(delegate () { QuitApplication(); });

            _buttons[Constants.menuButton].onClick.AddListener(delegate () { OpenGameMenu(); });

            _buttons[Constants.continueButton].onClick.AddListener(delegate () { ContinueLevel(); });
            _buttons[Constants.restartButton].onClick.AddListener(delegate () { RestartLevel(); });
            _buttons[Constants.backButton].onClick.AddListener(delegate () { OpenMainMenu(); });

            _buttons[Constants.firstUpgradeButton].onClick.AddListener(delegate () { ApplyFirstUpgrade(); });
            _buttons[Constants.secondUpgradeButton].onClick.AddListener(delegate () { ApplySecondUpgrade(); });
            _buttons[Constants.thirdUpgradeButton].onClick.AddListener(delegate () { ApplyThirdUpgrade(); });

            healthBar = GameObject.Find("Healthbar").GetComponent<Slider>();
            experiencebar = GameObject.Find("Experiencebar").GetComponent<Slider>();

            _upgradeMenu = GameObject.Find("UpgradeMenu");
            _firstTitle = GameObject.Find("FirstTitle").GetComponent<TextMeshProUGUI>();
            _firstDescription = GameObject.Find("FirstDescription").GetComponent<TextMeshProUGUI>();
            _secondTitle = GameObject.Find("SecondTitle").GetComponent<TextMeshProUGUI>();
            _secondDescription = GameObject.Find("SecondDescription").GetComponent<TextMeshProUGUI>();
            _thirdTitle = GameObject.Find("ThirdTitle").GetComponent<TextMeshProUGUI>();
            _thirdDescription = GameObject.Find("ThirdDescription").GetComponent<TextMeshProUGUI>();
        }

        public void ShowMainMenu()
        {
            _upgradeMenu.SetActive(false);
            _buttons[Constants.firstUpgradeButton].gameObject.SetActive(false);
            _buttons[Constants.secondUpgradeButton].gameObject.SetActive(false);
            _buttons[Constants.thirdUpgradeButton].gameObject.SetActive(false);

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

            _player = _objectStorage.Units[UnitType.Player.ToString()].First();

            healthBar.minValue = minimumHealth = 0;
            healthBar.maxValue = maximumHealth = _player.Health;

            experiencebar.minValue = 0;
            experiencebar.maxValue = 100;

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

        void UpdateHealthBar()
        {
            if (_player.Health < minimumHealth)
            {
                _player.Health = minimumHealth;
            }

            if (_player.Health > maximumHealth)
            {
                _player.Health = maximumHealth;
            }

            if (_player.Health >= minimumHealth && _player.Health < lowHealth)
            {
                healthBar.transform.Find("Bar").GetComponent<Image>().color = lowHealthColor;
            }
            else if (_player.Health > lowHealth && _player.Health < highHealth)
            {
                healthBar.transform.Find("Bar").GetComponent<Image>().color = mediumHealthColor;
            }
            else if (_player.Health > highHealth)
            {
                healthBar.transform.Find("Bar").GetComponent<Image>().color = highHealthColor;
            }

            healthBar.value = _player.Health;
        }

        void UpdateExperienceBar()
        {
            experiencebar.value = _player.Behaviour.CurrentExperience;
        }

        void ShowUpgradeView()
        {
            _buttons[Constants.menuButton].gameObject.SetActive(false);
            _updateManager.Stop();

            _upgradeMenu.SetActive(true);
            _buttons[Constants.firstUpgradeButton].gameObject.SetActive(true);
            _buttons[Constants.secondUpgradeButton].gameObject.SetActive(true);
            _buttons[Constants.thirdUpgradeButton].gameObject.SetActive(true);

            upgrades = _behaviourManager.GetUpgrades();
            _firstTitle.text = upgrades[0].Title;
            _firstDescription.text = upgrades[0].Description;
            _secondTitle.text = upgrades[1].Title;
            _secondDescription.text = upgrades[1].Description;
            _thirdTitle.text = upgrades[2].Title;
            _thirdDescription.text = upgrades[2].Description;
        }

        void ApplyFirstUpgrade()
        {
            _behaviourManager.ApplyUpgrade(upgrades[0].UpgradeType);
            _upgradeMenu.SetActive(false);
            _buttons[Constants.menuButton].gameObject.SetActive(true);

            _updateManager.CustomStart();

        }
        void ApplySecondUpgrade()
        {
            _behaviourManager.ApplyUpgrade(upgrades[1].UpgradeType);
            _upgradeMenu.SetActive(false);
            _buttons[Constants.menuButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
        }
        void ApplyThirdUpgrade()
        {
            _behaviourManager.ApplyUpgrade(upgrades[2].UpgradeType);
            _upgradeMenu.SetActive(false);
            _buttons[Constants.menuButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
        }

        public void CustomUpdate()
        {
            UpdateHealthBar();
            UpdateExperienceBar();

            if (_player.Behaviour.CurrentExperience >= 30)
            {
                ShowUpgradeView();
                _player.Behaviour.CurrentExperience = 0;
            }
        }
        public void CustomFixedUpdate()
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;
using TMPro;
using System.Linq;
using System;

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

        GameObject _mainMenu;
        GameObject _gameMenu;

        float minimumHealth;
        float maximumHealth;
        float lowHealth;
        float highHealth;

        Slider _healthBar;
        Slider _experiencebar;

        Color highHealthColor = new Color(0.35f, 1f, 0.35f);
        Color mediumHealthColor = new Color(0.9450285f, 1f, 0.4481132f);
        Color lowHealthColor = new Color(1f, 0.259434f, 0.259434f);

        IList<IUpgrade> upgrades;
        GameObject _upgradeMenu;
        TextMeshProUGUI _firstTitle;
        TextMeshProUGUI _firstDescription;
        TextMeshProUGUI _secondTitle;
        TextMeshProUGUI _secondDescription;
        TextMeshProUGUI _thirdTitle;
        TextMeshProUGUI _thirdDescription;

        GameObject _coinPanel;
        TextMeshProUGUI _coinValue;

        GameObject _magazinePanel;
        GameObject _bulletImagePrefab;
        IList<GameObject> _bulletImages;

        GameObject _machineMenu;
        TMP_Dropdown _machineDropdown;
        TextMeshProUGUI _weaponDescription;
        IWeapon _selectWeapon;

        public UIManager(IUpdateManager updateManager, IObjectStorage objectStorage, IPoolManager poolManager, IDataLoadManager dataLoadManager, IBehaviourManager behaviourManager)
        {
            _updateManager = updateManager;
            _objectStorage = objectStorage;
            _poolManager = poolManager;
            _dataLoadManager = dataLoadManager;
            _behaviourManager = behaviourManager;

            _updateManager.AddUpdatable(this);

            _buttons = new Dictionary<string, Button>();

            _mainMenu = GameObject.Find("MainMenu");
            _gameMenu = GameObject.Find("GameMenu");

            _buttons.Add(Constants.playBatton, GameObject.Find(Constants.playBatton).GetComponent<Button>());
            _buttons.Add(Constants.exitButton, GameObject.Find(Constants.exitButton).GetComponent<Button>());

            _buttons.Add(Constants.pauseButton, GameObject.Find(Constants.pauseButton).GetComponent<Button>());

            _buttons.Add(Constants.continueButton, GameObject.Find(Constants.continueButton).GetComponent<Button>());
            _buttons.Add(Constants.restartButton, GameObject.Find(Constants.restartButton).GetComponent<Button>());
            _buttons.Add(Constants.backButton, GameObject.Find(Constants.backButton).GetComponent<Button>());

            _buttons.Add(Constants.firstUpgradeButton, GameObject.Find(Constants.firstUpgradeButton).GetComponent<Button>());
            _buttons.Add(Constants.secondUpgradeButton, GameObject.Find(Constants.secondUpgradeButton).GetComponent<Button>());
            _buttons.Add(Constants.thirdUpgradeButton, GameObject.Find(Constants.thirdUpgradeButton).GetComponent<Button>());

            _buttons.Add(Constants.machineButton, GameObject.Find(Constants.machineButton).GetComponent<Button>());
            _buttons.Add(Constants.weaponApplyButton, GameObject.Find(Constants.weaponApplyButton).GetComponent<Button>());


            _buttons[Constants.playBatton].onClick.AddListener(delegate () { StartLevel(); });
            _buttons[Constants.exitButton].onClick.AddListener(delegate () { QuitApplication(); });

            _buttons[Constants.pauseButton].onClick.AddListener(delegate () { OpenGameMenu(); });

            _buttons[Constants.continueButton].onClick.AddListener(delegate () { ContinueLevel(); });
            _buttons[Constants.restartButton].onClick.AddListener(delegate () { RestartLevel(); });
            _buttons[Constants.backButton].onClick.AddListener(delegate () { OpenMainMenu(); });

            _buttons[Constants.firstUpgradeButton].onClick.AddListener(delegate () { ApplyFirstUpgrade(); });
            _buttons[Constants.secondUpgradeButton].onClick.AddListener(delegate () { ApplySecondUpgrade(); });
            _buttons[Constants.thirdUpgradeButton].onClick.AddListener(delegate () { ApplyThirdUpgrade(); });

            _buttons[Constants.machineButton].onClick.AddListener(delegate () { OpenMachineMenu(); });
            _buttons[Constants.weaponApplyButton].onClick.AddListener(delegate () { WeaponApplyMenu(); });

            _healthBar = GameObject.Find("Healthbar").GetComponent<Slider>();
            _experiencebar = GameObject.Find("Experiencebar").GetComponent<Slider>();

            _upgradeMenu = GameObject.Find("UpgradeMenu");
            _firstTitle = GameObject.Find("FirstTitle").GetComponent<TextMeshProUGUI>();
            _firstDescription = GameObject.Find("FirstDescription").GetComponent<TextMeshProUGUI>();
            _secondTitle = GameObject.Find("SecondTitle").GetComponent<TextMeshProUGUI>();
            _secondDescription = GameObject.Find("SecondDescription").GetComponent<TextMeshProUGUI>();
            _thirdTitle = GameObject.Find("ThirdTitle").GetComponent<TextMeshProUGUI>();
            _thirdDescription = GameObject.Find("ThirdDescription").GetComponent<TextMeshProUGUI>();

            _coinPanel = GameObject.Find("CoinPanel");
            _coinValue = GameObject.Find("CoinValue").GetComponent<TextMeshProUGUI>();

            _magazinePanel = GameObject.Find("MagazinePanel");
            _bulletImagePrefab = Resources.Load(Constants.prefabPath + "BulletImage") as GameObject;
            _bulletImages = new List<GameObject>();

            _machineMenu = GameObject.Find("MachineMenu");
            _machineDropdown = GameObject.Find("MachineDropdown").GetComponent<TMP_Dropdown>();
            _machineDropdown.options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData(WeaponType.PlayerWeaponType1.ToString()),
                new TMP_Dropdown.OptionData(WeaponType.PlayerWeaponType2.ToString()),

            };
            _machineDropdown.onValueChanged.AddListener(delegate { MachineDropdownValueChangedHandler(_machineDropdown); });
            _weaponDescription = GameObject.Find("WeaponDescription").GetComponent<TextMeshProUGUI>();
        }

        public void ShowMainMenu()
        {
            _mainMenu.SetActive(true);
            _gameMenu.SetActive(false);
            _upgradeMenu.SetActive(false);
            _healthBar.gameObject.SetActive(false);
            _experiencebar.gameObject.SetActive(false);
            _coinPanel.gameObject.SetActive(false);
            _machineMenu.gameObject.SetActive(false);
        }
        void StartLevel()
        {
            _mainMenu.SetActive(false);
            _healthBar.gameObject.SetActive(true);
            _experiencebar.gameObject.SetActive(true);
            _coinPanel.gameObject.SetActive(true);
            _buttons[Constants.pauseButton].gameObject.SetActive(true);

            _poolManager.LoadLevel();
            _poolManager.InstantiateEntities();

            _player = _objectStorage.Units[UnitType.Player.ToString()].First();

            _healthBar.minValue = minimumHealth = 0;
            _healthBar.maxValue = maximumHealth = _player.Health;
            lowHealth = _player.Behaviour.CurrentHealth * 0.33f;
            highHealth = _player.Behaviour.CurrentHealth * 0.66f;

            _experiencebar.minValue = Constants.experiencebarMinValue;
            _experiencebar.maxValue = Constants.experiencebarMaxValue;

            CreateBulletImage();
            _buttons[Constants.machineButton].gameObject.SetActive(_player.Behaviour.IsMachineAvailable);

            _updateManager.CustomStart();
        }

        void RestartLevel()
        {
            _gameMenu.SetActive(false);
            _buttons[Constants.pauseButton].gameObject.SetActive(true);

            Camera.main.transform.position = new Vector3(0, 0, -10);
            _objectStorage.ClearLevelData();
            _poolManager.LoadLevel();
            _poolManager.InstantiateEntities();
            _player = _objectStorage.Units[UnitType.Player.ToString()].First();

            _updateManager.CustomStart();
        }

        void QuitApplication()
        {
            Application.Quit();
        }

        void OpenGameMenu()
        {
            _gameMenu.SetActive(true);
            _buttons[Constants.pauseButton].gameObject.SetActive(false);
            _updateManager.Stop();
        }

        void OpenMainMenu()
        {
            _mainMenu.SetActive(true);
            _gameMenu.SetActive(false);
            _healthBar.gameObject.SetActive(false);
            _experiencebar.gameObject.SetActive(false);
            _coinPanel.gameObject.SetActive(false);

            Camera.main.transform.position = new Vector3(0, 0, -10);

            _objectStorage.ClearLevelData();
            _updateManager.Stop();
        }

        void ContinueLevel()
        {
            _gameMenu.SetActive(false);
            _buttons[Constants.pauseButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
        }

        void OpenGameOverMenu()
        {
            _updateManager.Stop();

            _gameMenu.SetActive(true);
            _buttons[Constants.continueButton].gameObject.SetActive(false);
            _buttons[Constants.pauseButton].gameObject.SetActive(false);
        }

        void OpenMachineMenu()
        {
            _updateManager.Stop();

            _buttons[Constants.pauseButton].gameObject.SetActive(false);
            _coinPanel.SetActive(false);
            _magazinePanel.SetActive(false);

            _machineMenu.gameObject.SetActive(true);
            MachineDropdownValueChangedHandler(_machineDropdown);
        }

        void CreateBulletImage()
        {
            GameObject image = GameObject.Instantiate(_bulletImagePrefab);
            image.transform.SetParent(_magazinePanel.transform);
            image.transform.localScale = new Vector3(0.002f, 0.09f, 1);
            image.transform.localPosition = new Vector3(-3.3f, 0, 0);
            _bulletImages.Add(image);
            for (int i = 1; i < Constants.maxMagazineCapacity; i++)
            {
                GameObject newImage = GameObject.Instantiate(_bulletImagePrefab);
                newImage.transform.SetParent(_magazinePanel.transform);
                newImage.transform.localPosition = new Vector3(_bulletImages[i - 1].gameObject.transform.localPosition.x + 0.4f, 0, 0);
                newImage.transform.localScale = new Vector3(0.002f, 0.09f, 1);

                _bulletImages.Add(newImage);

                if (i >= _player.MagazineCapacity)
                {
                    newImage.SetActive(false);
                }
            }

        }

        void UpdateHealthBar()
        {
            if (_player.Behaviour.CurrentHealth < minimumHealth)
            {
                _player.Behaviour.CurrentHealth = minimumHealth;
            }

            if (_player.Behaviour.CurrentHealth > maximumHealth)
            {
                _player.Behaviour.CurrentHealth = maximumHealth;
            }

            if (_player.Behaviour.CurrentHealth >= minimumHealth && _player.Behaviour.CurrentHealth < lowHealth)
            {
                _healthBar.transform.Find("Bar").GetComponent<Image>().color = lowHealthColor;
            }
            else if (_player.Behaviour.CurrentHealth > lowHealth && _player.Behaviour.CurrentHealth < highHealth)
            {
                _healthBar.transform.Find("Bar").GetComponent<Image>().color = mediumHealthColor;
            }
            else if (_player.Behaviour.CurrentHealth > highHealth)
            {
                _healthBar.transform.Find("Bar").GetComponent<Image>().color = highHealthColor;
            }

            _healthBar.value = _player.Behaviour.CurrentHealth;
        }

        void UpdateExperienceBar()
        {
            _experiencebar.value = _player.Behaviour.CurrentExperience;
        }

        void ShowUpgradeView()
        {
            _buttons[Constants.pauseButton].gameObject.SetActive(false);
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
            _buttons[Constants.pauseButton].gameObject.SetActive(true);

            _updateManager.CustomStart();

        }
        void ApplySecondUpgrade()
        {
            _behaviourManager.ApplyUpgrade(upgrades[1].UpgradeType);
            _upgradeMenu.SetActive(false);
            _buttons[Constants.pauseButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
        }
        void ApplyThirdUpgrade()
        {
            _behaviourManager.ApplyUpgrade(upgrades[2].UpgradeType);
            _upgradeMenu.SetActive(false);
            _buttons[Constants.pauseButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
        }

        void MachineDropdownValueChangedHandler(TMP_Dropdown target)
        {
            switch ((WeaponType)Enum.Parse(typeof(WeaponType), target.options[target.value].text))
            {
                case WeaponType.PlayerWeaponType1:
                    {
                        _selectWeapon = _objectStorage.WeaponTemplates[WeaponType.PlayerWeaponType1.ToString()];
                        _weaponDescription.text = $"Скорострельность: {_player.Behaviour.CurrentFireSpeed} | {_selectWeapon.FireSpeed}\n\n" +
                            $"Скорость перезарядки: {_player.Behaviour.CurrentReloadSpeed} | {_selectWeapon.ReloadSpeed}\n\n" +
                            $"Урон: {_player.Behaviour.CurrentBaseAttack} | {_selectWeapon.BaseAttack}";
                        break;
                    }
                case WeaponType.PlayerWeaponType2:
                    {
                        _selectWeapon = _objectStorage.WeaponTemplates[WeaponType.PlayerWeaponType2.ToString()];
                        _weaponDescription.text = $"Скорострельность: {_player.Behaviour.CurrentFireSpeed} | {_selectWeapon.FireSpeed}\n\n" +
                            $"Скорость перезарядки: {_player.Behaviour.CurrentReloadSpeed} | {_selectWeapon.ReloadSpeed}\n\n" +
                            $"Урон: {_player.Behaviour.CurrentBaseAttack} | {_selectWeapon.BaseAttack}";
                        break;
                    }
            }
            
        }

        void WeaponApplyMenu()
        {
            _player.Behaviour.IsMachineAvailable = false;
            _player.Weapon.WeaponType = WeaponType.PlayerWeaponType1;
            _player.Behaviour.CurrentReloadSpeed = _player.Behaviour.TimeBeforeReload = _selectWeapon.ReloadSpeed;
            _player.Behaviour.CurrentFireSpeed = _player.Behaviour.TimeBeforeShot = _selectWeapon.FireSpeed;
            _player.Behaviour.CurrentBaseAttack = _selectWeapon.BaseAttack;

            _machineMenu.SetActive(false);
            _buttons[Constants.machineButton].gameObject.SetActive(false);
            _coinPanel.SetActive(true);
            _magazinePanel.SetActive(true);

            _updateManager.CustomStart();
        }


        public void CustomUpdate()
        {
            UpdateHealthBar();
            UpdateExperienceBar();

            if (_player.GameObject.activeSelf && _player.Behaviour.CurrentExperience >= Constants.experiencebarNextLevelValue)
            {
                ShowUpgradeView();
                _player.Behaviour.CurrentExperience = 0;
            }
            if (_player.Behaviour.CurrentHealth <= 0)
            {
                if (_player.Behaviour.CurrentResurrectionValue > 0)
                {
                    _player.Behaviour.CurrentHealth = _player.Health;
                    _player.GameObject.SetActive(true);
                    _player.Text.SetActive(true);
                    ContinueLevel();
                }
                else
                {
                    OpenGameOverMenu();
                }
            }

            _coinValue.text = _player.Behaviour.CurrentCoinValue.ToString();

            int currentMagazineCapacity = _player.MagazineCapacity;
            for (int i = 0; i < currentMagazineCapacity; i++)
            {
                _bulletImages[i].SetActive(false);

                if (i < _player.Behaviour.CurrentBulletValue)
                {
                    _bulletImages[i].SetActive(true);
                }
            }

            if (_player.Behaviour.IsMachineAvailable)
            {
                _buttons[Constants.machineButton].gameObject.SetActive(true);
            }
        }
        public void CustomFixedUpdate()
        {

        }
    }
}

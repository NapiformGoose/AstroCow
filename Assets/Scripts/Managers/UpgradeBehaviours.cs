using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeBehaviours : IUpgradeBehaviours
{
    IObjectStorage _objectStorage;
    IUnit _player;
    IList<IUpgrade> _availableUpgrades;

    public UpgradeBehaviours(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
        _availableUpgrades = new List<IUpgrade>();
    }

    public IList<IUpgrade> GetUpgrades()
    {
        IList<IUpgrade> upgrades = new List<IUpgrade>(3);
        upgrades.Add(_objectStorage.Upgrades[UnityEngine.Random.Range(0, _objectStorage.Upgrades.Count)]);
        IUpgrade upgrade;

        do
        {
            upgrade = _objectStorage.Upgrades[UnityEngine.Random.Range(0, _objectStorage.Upgrades.Count)];
            for(int i = 0; i < upgrades.Count; i++)
            {
                if (upgrades[0] == upgrade)
                {
                    break;
                }
                else
                {
                    upgrades.Add(upgrade);
                    break;
                }
            }
        }
        while (upgrades.Count < 3);

        return upgrades;
    }
    public void UpgradeAct(UpgradeType upgradeType)
    {
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();

        switch (upgradeType)
        {
            case UpgradeType.FireSpeedUp:
                {
                    FireSpeedUpApply();
                    break;
                }
            case UpgradeType.BaseAttackUp:
                {
                    BaseAttackApply();
                    break;
                }
            case UpgradeType.HealthUp:
                {
                    HealthUpApply();
                    break;
                }
            case UpgradeType.BonusRandomUp:
                {
                    BonusRandomUpApply();
                    break;
                }
            case UpgradeType.Resurrection:
                {
                    ResurrectionApply();
                    break;
                }
            case UpgradeType.LootPercentUp:
                {
                    LootPercentUpApply();
                    break;
                }
            case UpgradeType.ReloadSpeedUp:
                {
                    ReloadSpeedUpApply();
                    break;
                }
            case UpgradeType.Bloodthirstiness:
                {
                    BloodthirstinessApply();
                    break;
                }
            case UpgradeType.MagazineCapacityUp:
                {
                    MagazineCapacityUpApply();
                    break;
                }
        }
    }

    void FireSpeedUpApply()
    {
        _player.Behaviour.CurrentFireSpeed -= _player.Weapon.FireSpeed * (Constants.fireSpeedUpPercent / 100);
    }

    void BaseAttackApply()
    {
        _player.Behaviour.CurrentBaseAttack += _player.Weapon.BaseAttack * (Constants.baseAttackUpPercent / 100);
    }

    void HealthUpApply()
    {
        _player.Health += _player.Health * (Constants.healthUpPercent / 100);
        _player.Behaviour.CurrentHealth += _player.Health * (Constants.healthUpPercent / 100);
    }

    void BonusRandomUpApply()
    {
        foreach (var key in _objectStorage.Bonuses.Keys)
        {
            foreach (IBonus bonus in _objectStorage.Bonuses[key])
            {
                bonus.RandomPercent += Constants.bonusRandomUpPercent;
            }
        }
    }

    void ResurrectionApply()
    {
        _player.Behaviour.CurrentResurrectionValue++;
    }

    void LootPercentUpApply()
    {
        foreach (var key in _objectStorage.Units.Keys)
        {
            foreach (IUnit unit in _objectStorage.Units[key])
            {
                unit.Behaviour.CurrentLootPercent += Constants.lootPercentUpPercent;
            }
        }
    }

    void ReloadSpeedUpApply()
    {
        _player.Behaviour.CurrentReloadSpeed -= _player.Weapon.ReloadSpeed * (Constants.reloadSpeedUpPercent / 100);
    }

    void BloodthirstinessApply()
    {
        _player.Behaviour.Bloodthirstiness += _player.Health * (Constants.bloodthirstinessPercent / 100);
    }

    void MagazineCapacityUpApply()
    {
        _player.MagazineCapacity++;
    }
}

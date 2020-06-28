﻿using Assets.Scripts;
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
        for(int i = 0; i < 3; i++)
        {
            upgrades.Add(_objectStorage.Upgrades[UnityEngine.Random.Range(0, _objectStorage.Upgrades.Count)]);
        }
        return _objectStorage.Upgrades;//upgrades;
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
        }
    }

    void FireSpeedUpApply()
    {
        _player.Weapon.FireSpeed -= _player.Weapon.FireSpeed * 0.1f;
    }

    void BaseAttackApply()
    {
        _player.Weapon.BaseAttack += _player.Weapon.BaseAttack * 0.1f;
    }

    void HealthUpApply()
    {
        _player.Health += _player.Health * 0.1f;
    }
}

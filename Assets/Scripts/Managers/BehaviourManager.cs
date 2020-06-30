using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourManager : IBehaviourManager, IUpdatable
{
    IUpdateManager _updateManager;
    IObjectStorage _objectStorage;
    UnitBehaviours _unitBehaviours;     //
    BulletBehaviours _bulletBehaviours; // название классов
    BonusBehaviour _bonusBehaviour;
    IUpgradeBehaviours _upgradeBehaviours;

    IUnit _player;

    public BehaviourManager(IUpdateManager updateManager, IObjectStorage objectStorage)
    {
        _updateManager = updateManager;
        _objectStorage = objectStorage;

        _unitBehaviours = new UnitBehaviours(_objectStorage);
        _bulletBehaviours = new BulletBehaviours(_objectStorage);
        _bonusBehaviour = new BonusBehaviour(_objectStorage);
        _upgradeBehaviours = new UpgradeBehaviours(_objectStorage);
        _updateManager.AddUpdatable(this);
    }

    private bool IsActive(Collider2D collider2D)
    {
        if (_objectStorage.ActivationTrigger.IsTouching(collider2D))
        {
            return true;
        }
        return false;
    }
    private bool IsTopDeactive(Collider2D collider2D)
    {
        if (_objectStorage.TopDeactivationTrigger.IsTouching(collider2D))
        {
            return true;
        }
        return false;
    }
    private bool IsDownDeactive(Collider2D collider2D)
    {
        if (_objectStorage.DownDeactivationTrigger.IsTouching(collider2D))
        {
            return true;
        }
        return false;
    }

    private float IsHit(IUnit unit)
    {
        foreach (var key in _objectStorage.Bullets.Keys)
        {
            foreach (IBullet bullet in _objectStorage.Bullets[key])
            {
                if (bullet.GameObject.activeSelf && bullet.Aim == unit.Team && bullet.Collider2D.IsTouching(unit.Collider2D))
                {
                    bullet.GameObject.SetActive(false);
                    return bullet.Damage;
                }
            }
        }
        return 0;
    }

    private float IsCollision(IEntity entity)
    {
        IUnit unit = entity as IUnit;
        if (unit != null)
        {
            if (unit.GameObject.activeSelf && unit.Collider2D.IsTouching(_player.Collider2D))
            {
                CalculateExperience(unit.ExperienceValue);
                return Constants.unitCollisionDamage;
            }
        }

        IObstacle obstacle = entity as IObstacle;
        if (obstacle != null)
        {
            if (obstacle.GameObject.activeSelf && obstacle.Collider2D.IsTouching(_player.Collider2D))
            {
                float damage = 0;
                switch (obstacle.ObstacleType)
                {
                    case ObstacleType.EnergyWall:
                        {
                            damage = Constants.energyWallDamage;
                            break;
                        }
                    case ObstacleType.SteelWall:
                        {
                            damage = Constants.steelWallDamage;
                            break;
                        }
                }
                return damage;
            }
        }

        return 0;
    }

    private void CalculateHealth(IUnit unit, float damage)
    {
        if (damage > 0)
        {
            unit.Behaviour.CurrentHealth -= damage;
            if (unit.Behaviour.CurrentHealth <= 0)
            {
                unit.GameObject.SetActive(false);
                unit.Text.SetActive(false);
                ShowBonus(unit);
                ShowCoin(unit);
                CalculateExperience(unit.ExperienceValue);
            }
        }
        else
        {
            unit.Behaviour.CurrentHealth -= damage;
        }
    }

    private void CalculateExperience(float experienceValue)
    {
        _player.Behaviour.CurrentExperience += experienceValue;
    }

    private void ShowBonus(IUnit unit)
    {
        if (unit.BonusType != BonusType.Empty)
        {
            foreach (IBonus bonus in _objectStorage.Bonuses[unit.BonusType.ToString()])
            {
                if (!bonus.GameObject.activeSelf)
                {
                    if (UnityEngine.Random.Range(0, 100) <= bonus.RandomPercent)
                    {
                        bonus.GameObject.transform.position = unit.GameObject.transform.position;
                        bonus.GameObject.SetActive(true);
                    }

                    return;
                }
            }
        }
    }
    private void ShowCoin(IUnit unit)
    {
        if (UnityEngine.Random.Range(0, 100) <= unit.Behaviour.CurrentLootPercent)
        {
            foreach (ICoin coin in _objectStorage.Coins)
            {
                if (!coin.GameObject.activeSelf)
                {
                    coin.GameObject.transform.position = new Vector3(unit.GameObject.transform.position.x, unit.GameObject.transform.position.y + 0.5f, unit.GameObject.transform.position.z);

                    coin.GameObject.SetActive(true);
                    return;
                }
            }
        }
    }

    public void ApplyUpgrade(UpgradeType upgradeType)
    {
        _upgradeBehaviours.UpgradeAct(upgradeType);
    }

    public IList<IUpgrade> GetUpgrades()
    {
        return _upgradeBehaviours.GetUpgrades();
    }

    public void CustomFixedUpdate()
    {
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();
        Camera.main.transform.position += new Vector3(0, Constants.cameraSpeed * Time.deltaTime, 0);

        foreach (var key in _objectStorage.Units.Keys)
        {
            foreach (IUnit unit in _objectStorage.Units[key])
            {
                float collisionDamage = IsCollision(unit);

                if (unit.UnitType != UnitType.Player && collisionDamage > 0)
                {
                    //destroy enemy after collision with player
                    CalculateHealth(unit, 1000);

                    CalculateHealth(_player, collisionDamage);
                    continue;
                }

                float damage = IsHit(unit);
                CalculateHealth(unit, damage);

                if (IsActive(unit.Collider2D))
                {
                    _unitBehaviours.UnitAct(unit);
                    unit.Behaviour.IsActive = true;
                    unit.Text.transform.position = unit.GameObject.transform.position + new Vector3(0.7f, 0.7f, 0);
                    unit.Text.GetComponent<Text>().text = unit.Behaviour.CurrentHealth.ToString();
                }
                if (IsDownDeactive(unit.Collider2D))
                {
                    //unit.GameObject.SetActive(false);
                }
            }
        }
        foreach (var key in _objectStorage.Obstacles.Keys)
        {
            foreach (IObstacle obstacle in _objectStorage.Obstacles[key])
            {
                float collisionDamage = IsCollision(obstacle);
                if (collisionDamage > 0)
                {
                    CalculateHealth(_player, collisionDamage);
                    continue;
                }

                if (IsDownDeactive(obstacle.Collider2D))
                {
                    //unit.GameObject.SetActive(false);
                }
            }
        }
        foreach (var key in _objectStorage.Bullets.Keys)
        {
            foreach (IBullet bullet in _objectStorage.Bullets[key])
            {
                if (IsActive(bullet.Collider2D))
                {
                    _bulletBehaviours.BulletAct(bullet);
                }
                if (IsTopDeactive(bullet.Collider2D) || IsDownDeactive(bullet.Collider2D))
                {
                    bullet.GameObject.SetActive(false);
                }
            }
        }
        foreach (var key in _objectStorage.Bonuses.Keys)
        {
            foreach (IBonus bonus in _objectStorage.Bonuses[key])
            {
                if (bonus.GameObject.activeSelf && bonus.Collider2D.IsTouching(_player.Collider2D))
                {
                    bonus.GameObject.SetActive(false);
                    _bonusBehaviour.BonusAct(bonus);
                }
            }
            _bonusBehaviour.ActiveBonusAct();
        }

        foreach (ICoin coin in _objectStorage.Coins)
        {
            if (coin.GameObject.activeSelf && coin.Collider2D.IsTouching(_player.Collider2D))
            {
                coin.GameObject.SetActive(false);
                _player.Behaviour.CurrentCoinValue++;
            }
        }
    }
    public void CustomUpdate()
    {

    }

}

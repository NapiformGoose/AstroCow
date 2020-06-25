using Assets.Scripts;
using Assets.Scripts.Interfaces;
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

    public BehaviourManager(IUpdateManager updateManager, IObjectStorage objectStorage)
    {
        _updateManager = updateManager;
        _objectStorage = objectStorage;

        _unitBehaviours = new UnitBehaviours(_objectStorage);
        _bulletBehaviours = new BulletBehaviours(_objectStorage);
        _bonusBehaviour = new BonusBehaviour(_objectStorage);
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
        IUnit player = _objectStorage.Units[UnitType.Player.ToString()].First();

        IUnit unit = entity as IUnit;
        if (unit != null)
        {
            if (unit.GameObject.activeSelf && unit.Collider2D.IsTouching(player.Collider2D))
            {
                return Constants.unitCollisionDamage;
            }
        }

        IObstacle obstacle = entity as IObstacle;
        if (obstacle != null)
        {
            if (obstacle.GameObject.activeSelf && obstacle.Collider2D.IsTouching(player.Collider2D))
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
            unit.Health -= damage;
            if (unit.Health <= 0)
            {
                unit.GameObject.SetActive(false);
                unit.Text.SetActive(false);
                showBonus(unit);
            }
        }
        else
        {
            unit.Health -= damage;
        }
    }

    private void showBonus(IUnit unit)
    {
        if (unit.BonusType != BonusType.Empty)
        {
            foreach (IBonus bonus in _objectStorage.Bonuses[unit.BonusType.ToString()])
            {
                if (!bonus.GameObject.activeSelf)
                {
                    if (UnityEngine.Random.Range(0, 100) <= bonus.RandomValue)
                    {
                        bonus.GameObject.transform.position = unit.GameObject.transform.position;
                        bonus.GameObject.SetActive(true);
                    }

                    return;
                }
            }
        }
    }
    public void CustomFixedUpdate()
    {
        Camera.main.transform.position += new Vector3(0, Constants.cameraSpeed * Time.deltaTime, 0);
        IUnit player = _objectStorage.Units["Player"].First();

        foreach (var key in _objectStorage.Units.Keys)
        {
            foreach (IUnit unit in _objectStorage.Units[key])
            {
                float collisionDamage = IsCollision(unit);
                if (unit.UnitType != UnitType.Player && collisionDamage > 0)
                {
                    CalculateHealth(unit, 1000);
                    CalculateHealth(player, collisionDamage);
                    continue;
                }

                float damage = IsHit(unit);
                CalculateHealth(unit, damage);

                if (IsActive(unit.Collider2D))
                {
                    _unitBehaviours.UnitAct(unit);
                    unit.Behaviour.IsActive = true;
                    unit.Text.transform.position = unit.GameObject.transform.position + new Vector3(0.7f, 0.7f, 0);
                    unit.Text.GetComponent<Text>().text = unit.Health.ToString();
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
                    CalculateHealth(player, collisionDamage);
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
                if (bonus.GameObject.activeSelf && bonus.Collider2D.IsTouching(player.Collider2D))
                {
                    bonus.GameObject.SetActive(false);
                    _bonusBehaviour.BonusAct(bonus);
                }
            }
            _bonusBehaviour.ActiveBonusAct();
        }
    }
    public void CustomUpdate()
    {

    }

}

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

    public BehaviourManager(IUpdateManager updateManager, IObjectStorage objectStorage)
    {
        _updateManager = updateManager;
        _objectStorage = objectStorage;

        _unitBehaviours = new UnitBehaviours(_objectStorage);
        _bulletBehaviours = new BulletBehaviours();

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
                if (bullet.BulletGameObject.activeSelf && bullet.Aim == unit.Team && bullet.BulletCollider2D.IsTouching(unit.Collider2D))
                {
                    bullet.BulletGameObject.SetActive(false);
                    return bullet.Damage;
                }
            }
        }
        return 0;
    }

    private float IsCollision(IEntity entity)
    {
        IUnit player = _objectStorage.Units["Player"].First();

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
            if (obstacle.ObstacleGameObject.activeSelf && obstacle.ObstacleCollider2D.IsTouching(player.Collider2D))
            {
                float damage = 0;
                switch(obstacle.ObstacleType)
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
            }
        }
        else
        {
            unit.Health -= damage;
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

                if (IsDownDeactive(obstacle.ObstacleCollider2D))
                {
                    //unit.GameObject.SetActive(false);
                }
            }
        }
        foreach (var key in _objectStorage.Bullets.Keys)
        {
            foreach (IBullet bullet in _objectStorage.Bullets[key])
            {
                if (IsActive(bullet.BulletCollider2D))
                {
                    _bulletBehaviours.BulletAct(bullet);
                }
                if (IsTopDeactive(bullet.BulletCollider2D) || IsDownDeactive(bullet.BulletCollider2D))
                {
                    bullet.BulletGameObject.SetActive(false);
                }
            }
        }
    }
    public void CustomUpdate()
    {

    }

}

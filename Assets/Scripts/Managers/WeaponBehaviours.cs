﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Linq;

public class WeaponBehaviours
{
    IObjectStorage _objectStorage;

    IUnit _player;

    public WeaponBehaviours(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();
    }

    public void WeaponAct(IUnit unit)
    {
        switch (unit.Weapon.WeaponType)
        {
            case WeaponType.WeaponType1:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType1Shoot(unit);
                    }
                    
                    break;
                }
            case WeaponType.WeaponType2:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType2Shoot(unit);
                    }
                    break;
                }
            case WeaponType.WeaponType3:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType3Shoot(unit);
                    }
                    break;
                }
            case WeaponType.WeaponType4:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType4Shoot(unit);
                    }
                    break;
                }
            case WeaponType.WeaponType5:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType5Shoot(unit);
                    }
                    break;
                }
            case WeaponType.PlayerWeaponType1:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType1Shoot(unit);
                    }
                    break;
                }
            case WeaponType.PlayerWeaponType2:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType2Shoot(unit);
                    }
                    break;
                }
        }
    }

    void WeaponType1Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Vertical, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        unit.Behaviour.TimeBeforeShot = unit.Weapon.FireSpeed;
    }

    void WeaponType2Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Vertical, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.LeftDiagonal, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.RightDiagonal, unit.ShootPosition, aim, unit.Weapon.BaseAttack);

        unit.Behaviour.TimeBeforeShot = unit.Weapon.FireSpeed;
    }

    void WeaponType3Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Directional, unit.ShootPosition, aim, unit.Weapon.BaseAttack);

        unit.Behaviour.TimeBeforeShot = unit.Weapon.FireSpeed;
    }

    void WeaponType4Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Homing, unit.ShootPosition, aim, unit.Weapon.BaseAttack);

        unit.Behaviour.TimeBeforeShot = unit.Weapon.FireSpeed;
    }

    void WeaponType5Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Directional, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.LeftDirectional, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.RightDirectional, unit.ShootPosition, aim, unit.Weapon.BaseAttack);


        unit.Behaviour.TimeBeforeShot = unit.Weapon.FireSpeed;
    }

    void CreateBullet(BulletType bulletType, BulletBehaviourType bulletBehaviourType, GameObject shootPosition, Team aim, float damage)
    {
        foreach (IBullet bullet in _objectStorage.Bullets[bulletType.ToString()])
        {
            if (!bullet.BulletGameObject.activeSelf)
            {
                bullet.BulletGameObject.SetActive(true);
                bullet.BulletGameObject.transform.position = shootPosition.transform.position;
                bullet.Aim = aim;
                bullet.Damage = damage;
                bullet.BulletBehaviourType = bulletBehaviourType;
                bullet.Behaviour.NextPos = _player.GameObject.transform.position;
                bullet.Behaviour.StartPos = shootPosition.transform.position;

                return;
            }
        }
    }
}

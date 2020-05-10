using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Interfaces;

public class WeaponBehaviours
{
    IObjectStorage _objectStorage;

    public WeaponBehaviours(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
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
        }
    }

    void WeaponType1Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;
        CreateBullet(BulletType.BulletType1, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        unit.Behaviour.TimeBeforeShot = unit.Weapon.FireSpeed;
    }

    void WeaponType2Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;

        CreateBullet(BulletType.BulletType1, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(BulletType.BulletType2, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(BulletType.BulletType3, unit.ShootPosition, aim, unit.Weapon.BaseAttack);

        unit.Behaviour.TimeBeforeShot = unit.Weapon.FireSpeed;
    }

    void CreateBullet(BulletType bulletType, GameObject shootPosition, Team aim, float damage)
    {
        foreach (IBullet bullet in _objectStorage.Bullets[bulletType.ToString()])
        {
            if (!bullet.BulletGameObject.activeSelf)
            {
                bullet.BulletGameObject.SetActive(true);
                bullet.BulletGameObject.transform.position = shootPosition.transform.position;
                bullet.Aim = aim;
                bullet.Damage = damage;
                return;
            }
        }
    }
}

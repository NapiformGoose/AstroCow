using System.Collections;
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
    }

    public void WeaponAct(IUnit unit)
    {
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();

        switch (unit.Weapon.WeaponType)
        {
            case WeaponType.WeaponType0:
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
            case WeaponType.WeaponType7:
                {
                    unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                    if (unit.Behaviour.TimeBeforeShot <= 0)
                    {
                        WeaponType7Shoot(unit);
                    }
                    break;
                }
            case WeaponType.PlayerBlaster:
                {
                    if(_player.Behaviour.CurrentBulletValue <= 0)
                    {
                        _player.Behaviour.TimeBeforeReload -= Time.fixedDeltaTime;
                        if(_player.Behaviour.TimeBeforeReload <= 0)
                        {
                            _player.Behaviour.CurrentBulletValue = _player.MagazineCapacity;
                            _player.Behaviour.TimeBeforeReload = _player.Weapon.ReloadSpeed;
                        }
                    }
                    else
                    {
                        unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                        if (unit.Behaviour.TimeBeforeShot <= 0)
                        {
                            WeaponType1Shoot(unit);
                        }
                    }
                   
                    break;
                }
            case WeaponType.PlayerShotgun:
                {
                    if (_player.Behaviour.CurrentBulletValue <= 0)
                    {
                        _player.Behaviour.TimeBeforeReload -= Time.fixedDeltaTime;
                        if (_player.Behaviour.TimeBeforeReload <= 0)
                        {
                            _player.Behaviour.CurrentBulletValue = _player.MagazineCapacity;
                            _player.Behaviour.TimeBeforeReload = _player.Weapon.ReloadSpeed;
                        }
                    }
                    else
                    {
                        unit.Behaviour.TimeBeforeShot -= Time.fixedDeltaTime;
                        if (unit.Behaviour.TimeBeforeShot <= 0)
                        {
                            WeaponType8Shoot(unit);
                        }
                    }
                    break;
                }
        }
    }

    void WeaponType1Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Vertical, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack);
        unit.Behaviour.TimeBeforeShot = unit.Behaviour.CurrentFireSpeed;
        unit.Behaviour.CurrentBulletValue--;
    }

    void WeaponType2Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Vertical, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.LeftDiagonal, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.RightDiagonal, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack);

        unit.Behaviour.TimeBeforeShot = unit.Behaviour.CurrentFireSpeed;
        unit.Behaviour.CurrentBulletValue--;
    }

    void WeaponType3Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Directional, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack, _player.GameObject.transform.position);

        unit.Behaviour.TimeBeforeShot = unit.Behaviour.CurrentFireSpeed;
    }

    void WeaponType4Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Homing, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack, _player.GameObject.transform.position);

        unit.Behaviour.TimeBeforeShot = unit.Behaviour.CurrentFireSpeed;
    }

    void WeaponType5Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Directional, unit.ShootPosition, aim, unit.Weapon.BaseAttack, _player.GameObject.transform.position);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.LeftDirectional, unit.ShootPosition, aim, unit.Weapon.BaseAttack, _player.GameObject.transform.position);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.RightDirectional, unit.ShootPosition, aim, unit.Weapon.BaseAttack, _player.GameObject.transform.position);

        unit.Behaviour.TimeBeforeShot = unit.Behaviour.CurrentFireSpeed;
    }
    void WeaponType7Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = Team.Player;
        IList<Vector3> directions = new List<Vector3>()
        {
            new Vector3(0, 1, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, -1, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0)
        };

        for(int i = 0; i < directions.Count; i++)
        {
            CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.AroundDirectional, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack, directions[i]);
        }

        unit.Behaviour.TimeBeforeShot = unit.Behaviour.CurrentFireSpeed;
    }

    void WeaponType8Shoot(IUnit unit)
    {
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;

        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.Vertical, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack);
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.LeftDirectional, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack, new Vector3(_player.GameObject.transform.position.x, _player.GameObject.transform.position.y + 5, 0));
        CreateBullet(unit.Weapon.BulletType, BulletBehaviourType.RightDirectional, unit.ShootPosition, aim, unit.Behaviour.CurrentBaseAttack, new Vector3(_player.GameObject.transform.position.x, _player.GameObject.transform.position.y + 5, 0));

        unit.Behaviour.TimeBeforeShot = unit.Behaviour.CurrentFireSpeed;
        unit.Behaviour.CurrentBulletValue--;
    }

    void CreateBullet(BulletType bulletType, BulletBehaviourType bulletBehaviourType, GameObject shootPosition, Team aim, float damage, Vector3 nextPos = new Vector3())
    {
        foreach (IBullet bullet in _objectStorage.Bullets[bulletType.ToString()])
        {
            if (!bullet.GameObject.activeSelf)
            {
                bullet.GameObject.SetActive(true);
                bullet.GameObject.transform.position = shootPosition.transform.position;
                bullet.Aim = aim;
                bullet.Damage = damage;
                bullet.BulletBehaviourType = bulletBehaviourType;
                bullet.Behaviour.NextPos = nextPos;
                bullet.Behaviour.StartPos = shootPosition.transform.position;

                return;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Interfaces;

public static class BehaviourList
{
    public static ICoroutiner Coroutiner { get; set; }
    public static IObjectStorage ObjectStorage { get; set; }

    public static void UnitAct(IUnit unit)
    {
        switch (unit.UnitType)
        {
            case UnitType.Player:
                {
                    SingleVerticalShooting(unit);
                    break;
                }
            case UnitType.EnemyType1:
                {
                    VerticalMoving(unit);
                    SingleVerticalShooting(unit);
                    break;
                }
            case UnitType.EnemyType2:
                {
                    HorizontalMoving(unit);
                    ShotgunShooting(unit);
                    break;
                }
            case UnitType.EnemyType3:
                {
                    break;
                }
        }
    }
    public static void BulletAct(IBullet bullet)
    {
        switch (bullet.BulletType)
        {
            case BulletType.BulletType1:
                {
                    VerticalBulletMoving(bullet);
                    break;
                }
            case BulletType.BulletType2:
                {
                    LeftDiagonalBulletMoving(bullet);
                    break;
                }
            case BulletType.BulletType3:
                {
                    RightDiagonalBulletMoving(bullet);
                    break;
                }
        }
    }
    private static void WeaponShoot(IUnit unit)
    {
        switch (unit.Weapon.WeaponType)
        {
            case WeaponType.WeaponType1:
                {
                    Coroutiner.StartCoroutine(WeaponType1Shoot(unit));
                    break;
                }
            case WeaponType.WeaponType2:
                {
                    Coroutiner.StartCoroutine(WeaponType2Shoot(unit));
                    break;
                }
        }
    }

    static void CreateBullet(BulletType bulletType, GameObject shootPosition, Team aim, float damage)
    {
        foreach (IBullet bullet in ObjectStorage.Bullets[bulletType.ToString()])
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
    static IEnumerator WeaponType1Shoot(IUnit unit)
    {
        yield return new WaitForSeconds(unit.Weapon.FireSpeed);
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;
        CreateBullet(BulletType.BulletType1, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
    }
    static IEnumerator WeaponType2Shoot(IUnit unit)
    {
        yield return new WaitForSeconds(unit.Weapon.FireSpeed);
        unit.Behaviour.IsAttack = true;
        Team aim = unit.Team == Team.Player ? Team.Enemy : Team.Player;

        CreateBullet(BulletType.BulletType1, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(BulletType.BulletType2, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
        CreateBullet(BulletType.BulletType3, unit.ShootPosition, aim, unit.Weapon.BaseAttack);
    }

    #region Units Moving Behaviour
    static void VerticalMoving(IUnit unit)
    {
        Vector3 distance = new Vector3(0, unit.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newUnitPosition = unit.GameObject.transform.position - distance;
        unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, newUnitPosition, 1f));
    }
    static void HorizontalMoving(IUnit unit)
    {
        if (unit.GameObject.transform.position.x <= unit.Behaviour.MaxLeftPos.x)
        {
            unit.Behaviour.Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0);
        }
        if (unit.GameObject.transform.position.x >= unit.Behaviour.MaxRightPos.x)
        {
            unit.Behaviour.Direction = new Vector3(-unit.MoveSpeed * Time.fixedDeltaTime, 0, 0);
        }

        Vector2 newUnitPosition = unit.GameObject.transform.position + unit.Behaviour.Direction;
        unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, newUnitPosition, 0.05f));
    }
    #endregion

    #region Units Shooting Behaviour
    static void SingleVerticalShooting(IUnit unit)
    {
        if (unit.Behaviour.IsAttack)
        {
            WeaponShoot(unit);
            unit.Behaviour.IsAttack = false;
        }
    }
    static void ShotgunShooting(IUnit unit)
    {
        if (unit.Behaviour.IsAttack)
        {
            WeaponShoot(unit);
            unit.Behaviour.IsAttack = false;
        }
    }
    #endregion

    #region Bullets Moving Behaviour

    public static void VerticalBulletMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(0, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(0, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.BulletGameObject.transform.position + distanceDown : bullet.BulletGameObject.transform.position - distanceUp;
        bullet.BulletRigidBody2D.MovePosition(Vector2.MoveTowards(bullet.BulletGameObject.transform.position, newBulletPosition, 1f));
    }
    public static void LeftDiagonalBulletMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(-bullet.MoveSpeed * Time.fixedDeltaTime, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(bullet.MoveSpeed * Time.fixedDeltaTime, -bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.BulletGameObject.transform.position + distanceUp : bullet.BulletGameObject.transform.position + distanceDown;

        bullet.BulletRigidBody2D.MovePosition(Vector2.MoveTowards(bullet.BulletGameObject.transform.position, newBulletPosition, 1f));
    }
    public static void RightDiagonalBulletMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(bullet.MoveSpeed * Time.fixedDeltaTime, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(-bullet.MoveSpeed * Time.fixedDeltaTime, -bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.BulletGameObject.transform.position + distanceUp : bullet.BulletGameObject.transform.position + distanceDown;

        bullet.BulletRigidBody2D.MovePosition(Vector2.MoveTowards(bullet.BulletGameObject.transform.position, newBulletPosition, 1f));
    }
    #endregion
}


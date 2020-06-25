using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Interfaces;

public class BulletBehaviours
{
    IObjectStorage _objectStorage;
    IUnit _player;

    public BulletBehaviours(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
    }

    public void BulletAct(IBullet bullet)
    {
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();

        switch (bullet.BulletBehaviourType)
        {
            case BulletBehaviourType.Vertical:
                {
                    VerticalMoving(bullet);
                    break;
                }
            case BulletBehaviourType.LeftDiagonal:
                {
                    LeftDiagonalMoving(bullet);
                    break;
                }
            case BulletBehaviourType.RightDiagonal:
                {
                    RightDiagonalMoving(bullet);
                    break;
                }
            case BulletBehaviourType.Directional:
                {
                    DirectionMoving(bullet);
                    break;
                }
            case BulletBehaviourType.Homing:
                {
                    HomingMoving(bullet);
                    break;
                }
            case BulletBehaviourType.LeftDirectional:
                {
                    LeftDirectionalMoving(bullet);
                    break;
                }
            case BulletBehaviourType.RightDirectional:
                {
                    RightDirectionalMoving(bullet);
                    break;
                }
            case BulletBehaviourType.AroundDirectional:
                {
                    AroundDirectionalMoving(bullet);
                    break;
                }
        }
    }

    void VerticalMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(0, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(0, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.GameObject.transform.position + distanceDown : bullet.GameObject.transform.position - distanceUp;
        bullet.RigidBody2D.MovePosition(Vector2.MoveTowards(bullet.GameObject.transform.position, newBulletPosition, 1f));
    }
    void LeftDiagonalMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(-bullet.MoveSpeed * Time.fixedDeltaTime, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(bullet.MoveSpeed * Time.fixedDeltaTime, -bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.GameObject.transform.position + distanceUp : bullet.GameObject.transform.position + distanceDown;

        bullet.RigidBody2D.MovePosition(Vector2.MoveTowards(bullet.GameObject.transform.position, newBulletPosition, 1f));
    }
    void RightDiagonalMoving(IBullet bullet)
    {
        Vector3 distanceUp = new Vector3(bullet.MoveSpeed * Time.fixedDeltaTime, bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector3 distanceDown = new Vector3(-bullet.MoveSpeed * Time.fixedDeltaTime, -bullet.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newBulletPosition = bullet.Aim == Team.Enemy ? bullet.GameObject.transform.position + distanceUp : bullet.GameObject.transform.position + distanceDown;

        bullet.RigidBody2D.MovePosition(Vector2.MoveTowards(bullet.GameObject.transform.position, newBulletPosition, 1f));
    }
    void DirectionMoving(IBullet bullet)
    {
        Vector3 distance = (bullet.Behaviour.NextPos - bullet.Behaviour.StartPos).normalized;
        bullet.Behaviour.Direction = new Vector3(distance.x, distance.y, 0).normalized;

        bullet.RigidBody2D.velocity = new Vector2(bullet.Behaviour.Direction.x * bullet.MoveSpeed, bullet.Behaviour.Direction.y * bullet.MoveSpeed);
    }

    void HomingMoving(IBullet bullet)
    {
        Vector3 distance = (_player.GameObject.transform.position - bullet.GameObject.transform.position).normalized;
        bullet.Behaviour.Direction = new Vector3(distance.x, distance.y, 0).normalized;

        bullet.RigidBody2D.velocity = new Vector2(bullet.Behaviour.Direction.x * bullet.MoveSpeed, bullet.Behaviour.Direction.y * bullet.MoveSpeed);
    }

    void LeftDirectionalMoving(IBullet bullet)
    {
        Vector3 distance = (bullet.Behaviour.NextPos - bullet.Behaviour.StartPos).normalized;

        float angle = (float)10/(float)90;
        bullet.Behaviour.Direction = Vector3.Lerp(distance, new Vector3(-1, 0, 0), angle).normalized;

        bullet.RigidBody2D.velocity = new Vector2(bullet.Behaviour.Direction.x * bullet.MoveSpeed, bullet.Behaviour.Direction.y * bullet.MoveSpeed);
    }

    void RightDirectionalMoving(IBullet bullet)
    {
        Vector3 distance = (bullet.Behaviour.NextPos - bullet.Behaviour.StartPos).normalized;

        float angle = (float)10 / (float)90;
        bullet.Behaviour.Direction = Vector3.Lerp(distance, new Vector3(1, 0, 0), angle).normalized;

        bullet.RigidBody2D.velocity = new Vector2(bullet.Behaviour.Direction.x * bullet.MoveSpeed, bullet.Behaviour.Direction.y * bullet.MoveSpeed);
    }

    void AroundDirectionalMoving(IBullet bullet)
    {
        bullet.Behaviour.Direction = bullet.Behaviour.NextPos;

        bullet.RigidBody2D.velocity = new Vector2(bullet.Behaviour.Direction.x * bullet.MoveSpeed, bullet.Behaviour.Direction.y * bullet.MoveSpeed);
    }
}

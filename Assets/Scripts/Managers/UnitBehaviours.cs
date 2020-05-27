using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using System.Linq;

public class UnitBehaviours
{
    IObjectStorage _objectStorage;
    WeaponBehaviours _weaponBehaviour;

    IUnit _player;
    Vector3 firstClickPos = Vector3.zero;
    Vector3 newPlayerPosition = Vector3.zero;
    Vector3 distToPlayer = Vector3.zero;

    public UnitBehaviours(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
        _weaponBehaviour = new WeaponBehaviours(_objectStorage);
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();
    }

    public void UnitAct(IUnit unit)
    {
        switch (unit.UnitType)
        {
            case UnitType.Player:
                {
                    PlayerMoving(unit);
                    //_weaponBehaviour.WeaponAct(unit);
                    break;
                }
            case UnitType.EnemyType1:
                {
                    VerticalMoving(unit);
                    //_weaponBehaviour.WeaponAct(unit);
                    break;
                }
            case UnitType.EnemyType2:
                {
                    HorizontalMoving(unit);
                    //_weaponBehaviour.WeaponAct(unit);
                    break;
                }
            case UnitType.EnemyType3:
                {
                    RandomHorizontalMoving(unit);
                    break;
                }
            case UnitType.EnemyType4:
                {
                    RandomDirectionMoving(unit);
                    break;
                }
            case UnitType.EnemyType5:
                {
                    RandomDirectionAndTeleportMoving(unit);
                    break;
                }
            case UnitType.EnemyType6:
                {
                    RandomTeleportAndStopMoving(unit);
                    break;
                }
            case UnitType.EnemyType7:
                {
                    RamAttackAndUpMiving(unit);
                    break;
                }
            case UnitType.EnemyType8:
                {
                    RamAttackAndStopMiving(unit);
                    break;
                }
        }
    }

    #region Unit Moving Methods

    void PlayerMoving(IUnit unit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                firstClickPos = hit.point;
                distToPlayer = new Vector3(unit.GameObject.transform.position.x - firstClickPos.x, unit.GameObject.transform.position.y - firstClickPos.y, 0);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                newPlayerPosition = new Vector3(hit.point.x + distToPlayer.x, hit.point.y + distToPlayer.y, 0);
            }
        }
        if (!Input.GetMouseButton(0))
        {
            unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, new Vector2(unit.GameObject.transform.position.x, unit.GameObject.transform.position.y) + new Vector2(0, Constants.cameraSpeed) * Time.fixedDeltaTime, 5f));
        }
        else
        {
            unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, newPlayerPosition, unit.MoveSpeed * Time.fixedDeltaTime));
        }
    }

    void VerticalMoving(IUnit unit)
    {
        Vector3 distance = new Vector3(0, unit.MoveSpeed * Time.fixedDeltaTime, 0);
        Vector2 newUnitPosition = unit.GameObject.transform.position - distance;
        unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, newUnitPosition, 1f));
    }

    void HorizontalMoving(IUnit unit)
    {
        unit.Behaviour.MaxDownPos = new Vector3(0, Camera.main.transform.localPosition.y + 3.2f, 0);

        if (unit.GameObject.transform.position.x <= unit.Behaviour.MaxLeftPos.x)
        {
            unit.Behaviour.Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0);
        }
        if (unit.GameObject.transform.position.x >= unit.Behaviour.MaxRightPos.x)
        {
            unit.Behaviour.Direction = new Vector3(-unit.MoveSpeed * Time.fixedDeltaTime, 0, 0);
        }
        if (unit.GameObject.transform.position.y <= unit.Behaviour.MaxDownPos.y)
        {
            unit.Behaviour.Direction = new Vector3(unit.Behaviour.Direction.x, Constants.cameraSpeed * Time.fixedDeltaTime, 0);
        }

        Vector2 newUnitPosition = unit.GameObject.transform.position + unit.Behaviour.Direction;

        unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, newUnitPosition, Constants.cameraSpeed));
    }

    void RandomHorizontalMoving(IUnit unit)
    {
        unit.Behaviour.MaxDownPos = new Vector3(0, Camera.main.transform.localPosition.y + 2f, 0);

        if (!unit.Behaviour.IsMoving)
        {
            if (unit.GameObject.transform.position.x <= 0)
            {
                unit.Behaviour.NextPos = new Vector3(UnityEngine.Random.Range(0, unit.Behaviour.MaxRightPos.x), 0, 0);
                unit.Behaviour.Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            if (unit.GameObject.transform.position.x >= 0)
            {
                unit.Behaviour.NextPos = new Vector3(UnityEngine.Random.Range(0, unit.Behaviour.MaxLeftPos.x), 0, 0);
                unit.Behaviour.Direction = new Vector3(-unit.MoveSpeed * Time.fixedDeltaTime, 0, 0);
            }

            unit.Behaviour.IsMoving = true;
        }
        if (unit.GameObject.transform.position.y <= unit.Behaviour.MaxDownPos.y)
        {
            unit.Behaviour.Direction = new Vector3(unit.Behaviour.Direction.x, Constants.cameraSpeed * Time.fixedDeltaTime, 0);
        }

        Vector2 newUnitPosition = unit.GameObject.transform.position + unit.Behaviour.Direction;
        unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, newUnitPosition, Constants.cameraSpeed));

        if (System.Math.Abs(unit.GameObject.transform.position.x - unit.Behaviour.NextPos.x) < 0.1f)
        {
            unit.Behaviour.IsMoving = false;
        }
    }

    void RandomDirectionMoving(IUnit unit)
    {
        if (!unit.Behaviour.IsMoving && unit.Behaviour.InactiveTime <= 0)
        {
            float newXPos = UnityEngine.Random.Range(unit.Behaviour.MaxLeftPos.x, unit.Behaviour.MaxRightPos.x);
            float newYPos = UnityEngine.Random.Range(Camera.main.transform.localPosition.y, _objectStorage.TopDeactivationTrigger.transform.position.y - 4.5f);
            unit.Behaviour.NextPos = new Vector3(newXPos, newYPos, 0);

            Vector3 distance = (unit.Behaviour.NextPos - unit.GameObject.transform.position).normalized;
            float newXDirection = distance.x * unit.MoveSpeed * Time.fixedDeltaTime;
            float newYDirection = distance.y * unit.MoveSpeed * Time.fixedDeltaTime;
            unit.Behaviour.Direction = new Vector3(newXDirection, newYDirection, 0).normalized;

            unit.Behaviour.IsMoving = true;
            unit.Behaviour.InactiveTime = unit.InactiveTime;
        }

        if (System.Math.Abs(unit.GameObject.transform.position.x - unit.Behaviour.NextPos.x) < 0.3f)
        {
            unit.Behaviour.Direction = new Vector3(0, Constants.cameraSpeed * Time.fixedDeltaTime, 0).normalized;
            unit.RigidBody2D.velocity = new Vector2(0, unit.Behaviour.Direction.y * Constants.cameraSpeed);

            unit.Behaviour.InactiveTime -= Time.fixedDeltaTime;
            unit.Behaviour.IsMoving = false;
        }
        else
        {
            unit.RigidBody2D.velocity = new Vector2(unit.Behaviour.Direction.x * unit.MoveSpeed, unit.Behaviour.Direction.y * unit.MoveSpeed);
        }
    }

    void RandomDirectionAndTeleportMoving(IUnit unit)
    {
        if (!unit.Behaviour.IsMoving)
        {
            float newXPos = UnityEngine.Random.Range(unit.Behaviour.MaxLeftPos.x, unit.Behaviour.MaxRightPos.x);
            float newYPos = UnityEngine.Random.Range(Camera.main.transform.localPosition.y, _objectStorage.TopDeactivationTrigger.transform.position.y - 4.5f);
            unit.GameObject.transform.position = new Vector3(newXPos, newYPos);

            newXPos = UnityEngine.Random.Range(unit.Behaviour.MaxLeftPos.x, unit.Behaviour.MaxRightPos.x);
            newYPos = UnityEngine.Random.Range(Camera.main.transform.localPosition.y, _objectStorage.TopDeactivationTrigger.transform.position.y - 4.5f);

            unit.Behaviour.NextPos = new Vector3(newXPos, newYPos, 0);

            Vector3 distance = (unit.Behaviour.NextPos - unit.GameObject.transform.position).normalized;
            float newXDirection = distance.x * unit.MoveSpeed * Time.fixedDeltaTime;
            float newYDirection = distance.y * unit.MoveSpeed * Time.fixedDeltaTime; 
            unit.Behaviour.Direction = new Vector3(newXDirection, newYDirection, 0).normalized;

            unit.Behaviour.IsMoving = true;
        }

        unit.RigidBody2D.velocity = new Vector2(unit.Behaviour.Direction.x * unit.MoveSpeed, unit.Behaviour.Direction.y * unit.MoveSpeed);

        if (System.Math.Abs(unit.GameObject.transform.position.x - unit.Behaviour.NextPos.x) < 0.1f)
        {
            unit.Behaviour.IsMoving = false;
        }
    }

    void RandomTeleportAndStopMoving(IUnit unit)
    {
        if (!unit.Behaviour.IsMoving)
        {
            float newXPos = UnityEngine.Random.Range(unit.Behaviour.MaxLeftPos.x, unit.Behaviour.MaxRightPos.x);
            float newYPos = UnityEngine.Random.Range(Camera.main.transform.localPosition.y, _objectStorage.TopDeactivationTrigger.transform.position.y - 4.5f);
            unit.GameObject.transform.position = new Vector3(newXPos, newYPos);

            unit.Behaviour.IsMoving = true;
            unit.Behaviour.InactiveTime = unit.InactiveTime;
        }

        unit.Behaviour.InactiveTime -= Time.fixedDeltaTime;
        unit.Behaviour.Direction = new Vector3(0, Constants.cameraSpeed * Time.fixedDeltaTime, 0).normalized;
        unit.RigidBody2D.velocity = new Vector2(0, unit.Behaviour.Direction.y * Constants.cameraSpeed);

        if (unit.Behaviour.InactiveTime <= 0)
        {
            unit.Behaviour.IsMoving = false;
        }
    }

    void RamAttackAndUpMiving(IUnit unit)
    {
        void CalcNewPos()
        {
            unit.Behaviour.NextPos = new Vector3(_player.GameObject.transform.position.x, _player.GameObject.transform.position.y + 0.7f, 0);

            Vector3 distance = (unit.Behaviour.NextPos - unit.GameObject.transform.position).normalized;
            float newXDirection = distance.x * unit.MoveSpeed * Time.fixedDeltaTime;
            float newYDirection = distance.y * unit.MoveSpeed * Time.fixedDeltaTime;
            unit.Behaviour.Direction = new Vector3(newXDirection, newYDirection, 0).normalized;
        }

        if (!unit.Behaviour.IsMoving)
        {
            if (unit.Behaviour.InactiveTime >= 0)
            {
                unit.Behaviour.InactiveTime -= Time.fixedDeltaTime;
                unit.Behaviour.Direction = new Vector3(0, Constants.cameraSpeed * Time.fixedDeltaTime, 0).normalized;
            }
            else
            {
                CalcNewPos();
                unit.Behaviour.IsMoving = true;
            }
        }
        else
        {
            unit.Behaviour.NextPos = new Vector3(unit.Behaviour.NextPos.x, unit.Behaviour.NextPos.y + (Constants.cameraSpeed * Time.fixedDeltaTime), 0);
            Vector3 distance = unit.Behaviour.NextPos - unit.GameObject.transform.position;
            unit.Behaviour.Direction = new Vector3(distance.x, distance.y, 0).normalized;
        }

        if(unit.Behaviour.Direction.y < 0)
        {
            unit.RigidBody2D.velocity = new Vector2(unit.Behaviour.Direction.x * unit.MoveSpeed, unit.Behaviour.Direction.y * unit.MoveSpeed);
        }
        else if (!unit.Behaviour.IsMoving)
        {
            unit.RigidBody2D.velocity = new Vector2(0, unit.Behaviour.Direction.y * Constants.cameraSpeed * 1.5f);
        }
        else
        {
            unit.RigidBody2D.velocity = new Vector2(unit.Behaviour.Direction.x * unit.MoveSpeed, unit.Behaviour.Direction.y * (unit.MoveSpeed + Constants.cameraSpeed));
        }

        if (unit.Behaviour.IsMoving && (System.Math.Abs(unit.GameObject.transform.position.y - unit.Behaviour.NextPos.y) < 0.1f || System.Math.Abs(unit.GameObject.transform.position.x - unit.Behaviour.NextPos.x) < 0.1f))
        {
            unit.Behaviour.IsMoving = false;
            unit.Behaviour.InactiveTime = unit.InactiveTime;
            unit.Behaviour.NextPos = Vector3.zero;
        }
    }

    void RamAttackAndStopMiving(IUnit unit)
    {
        void CalcPos()
        {
            unit.Behaviour.NextPos = new Vector3(_player.GameObject.transform.position.x, _player.GameObject.transform.position.y + 0.7f, 0);

            Vector3 distance = (unit.Behaviour.NextPos - unit.GameObject.transform.position).normalized;
            float newXDirection = distance.x * unit.MoveSpeed * Time.fixedDeltaTime;
            float newYDirection = distance.y * unit.MoveSpeed * Time.fixedDeltaTime;
            unit.Behaviour.Direction = new Vector3(newXDirection, newYDirection, 0).normalized;
        }

        if (!unit.Behaviour.IsMoving)
        {
            if (unit.Behaviour.InactiveTime >= 0)
            {
                unit.Behaviour.InactiveTime -= Time.fixedDeltaTime;
                unit.Behaviour.Direction = new Vector3(0, Constants.cameraSpeed * Time.fixedDeltaTime, 0).normalized;
            }
            else
            {
                CalcPos();
                unit.Behaviour.IsMoving = true;
            }
        }
        else
        {
            unit.Behaviour.NextPos = new Vector3(unit.Behaviour.NextPos.x, unit.Behaviour.NextPos.y + (Constants.cameraSpeed * Time.fixedDeltaTime), 0);
            Vector3 distance = unit.Behaviour.NextPos - unit.GameObject.transform.position;

            unit.Behaviour.Direction = new Vector3(distance.x, distance.y, 0).normalized;
        }

        if (unit.Behaviour.Direction.y < 0 || !unit.Behaviour.IsMoving)
        {
            unit.RigidBody2D.velocity = new Vector2(unit.Behaviour.Direction.x * unit.MoveSpeed, unit.Behaviour.Direction.y * unit.MoveSpeed);
        }
        else
        {
            unit.RigidBody2D.velocity = new Vector2(unit.Behaviour.Direction.x * unit.MoveSpeed, unit.Behaviour.Direction.y * (unit.MoveSpeed + Constants.cameraSpeed));
        }

        if (unit.Behaviour.IsMoving && (System.Math.Abs(unit.GameObject.transform.position.y - unit.Behaviour.NextPos.y) < 0.1f || System.Math.Abs(unit.GameObject.transform.position.x - unit.Behaviour.NextPos.x) < 0.1f))
        {
            unit.Behaviour.IsMoving = false;
            unit.Behaviour.InactiveTime = unit.InactiveTime;
            unit.Behaviour.NextPos = Vector3.zero;
        }
    }
    #endregion
}

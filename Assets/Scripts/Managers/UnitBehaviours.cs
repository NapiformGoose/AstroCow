using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Interfaces;

public class UnitBehaviours
{
    IObjectStorage _objectStorage;
    WeaponBehaviours _weaponBehaviour;

    Vector3 firstClickPos = Vector3.zero;
    Vector3 newPlayerPosition = Vector3.zero;
    Vector3 distToPlayer = Vector3.zero;

    public UnitBehaviours(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
        _weaponBehaviour = new WeaponBehaviours(_objectStorage);
	}

    public void UnitAct(IUnit unit)
    {
        switch (unit.UnitType)
        {
            case UnitType.Player:
                {
                    PlayerMoving(unit);
                    _weaponBehaviour.WeaponAct(unit);
                    break;
                }
            case UnitType.EnemyType1:
                {
                    VerticalMoving(unit);
                    _weaponBehaviour.WeaponAct(unit);
                    break;
                }
            case UnitType.EnemyType2:
                {
                    HorizontalMoving(unit);
                    _weaponBehaviour.WeaponAct(unit);
                    break;
                }
            case UnitType.EnemyType3:
                {
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
            unit.RigidBody2D.MovePosition(Vector2.MoveTowards(unit.GameObject.transform.position, newPlayerPosition, unit.MoveSpeed));
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
}

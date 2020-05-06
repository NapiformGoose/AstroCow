using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourManager : IUpdatable
{
	IUpdateManager _updateManager;
	IObjectStorage _objectStorage;

	public BehaviourManager(IUpdateManager updateManager, IObjectStorage objectStorage)
	{
		_updateManager = updateManager;
		_objectStorage = objectStorage;

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
                if(bullet.BulletGameObject.activeSelf && bullet.Aim == unit.Team && bullet.BulletCollider2D.IsTouching(unit.Collider2D))
                {
                    bullet.BulletGameObject.SetActive(false);
                    return bullet.Damage;
                }
            }
        }
        return 0;
    }

    public void CustomFixedUpdate()
    {
        foreach (var key in _objectStorage.Units.Keys)
        {
            foreach(IUnit unit in _objectStorage.Units[key])
            {
                float damage = IsHit(unit);
                if (damage > 0)
                {
                    unit.Health -= damage;
                    if(unit.Health <= 0)
                    {
                        unit.GameObject.SetActive(false);
                        unit.Text.SetActive(false);
                    }
                }
                else
                {
                    unit.Health -= damage;
                }
                if (IsActive(unit.Collider2D))
                {
                    BehaviourList.UnitAct(unit);
                    unit.Text.transform.position = unit.GameObject.transform.position + new Vector3(0.7f, 0.7f, 0);
                    unit.Text.GetComponent<Text>().text = unit.Health.ToString();
                }
                if(IsDownDeactive(unit.Collider2D))
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
                    BehaviourList.BulletAct(bullet);
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

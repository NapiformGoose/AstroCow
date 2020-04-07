using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class LevelManager : ILevelManager, IUpdatable
{
	IUpdateManager _updateManager;
	IObjectStorage _objectStorage;
	Cell _topCell;
	public LevelManager(IUpdateManager updateManager, IObjectStorage objectStorage)
	{
		_updateManager = updateManager;
		_objectStorage = objectStorage;

		_updateManager.AddUpdatable(this);
	}
	public void CustomFixedUpdate()
	{

	}
	public void CustomUpdate()
	{

	}

}

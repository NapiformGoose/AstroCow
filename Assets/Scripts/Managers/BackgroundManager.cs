using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager
{
	IObjectStorage _objectStorage;

	public BackgroundManager(IObjectStorage objectStorage)
	{
		_objectStorage = objectStorage;
	}

	public void CreateBackground()
	{
		Vector3 currentCellPos = Constants.startCellPosition;

		for (int i = 0; i < _objectStorage.CellSets[0].Count; i++)
		{

		}
	}
}

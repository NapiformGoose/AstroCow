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
	public void CustomUpdate()
	{
		for (int i = 0; i < _objectStorage.Cells.Count; i++)
		{
			if (_objectStorage.LowerTrigger.IsTouching(_objectStorage.Cells[i].CellCollider))
			{
				_objectStorage.Cells[i].CellGameObject.SetActive(false);
				GenerateCell(i);
			}
		}
	}
	public void StartLevel()
	{
		_objectStorage.Cells[0].CellGameObject.transform.position = Constants.startCellPosition;
		_objectStorage.Cells[1].CellGameObject.transform.position = _objectStorage.Cells[0].CellGameObject.transform.position + Constants.distanceToNextCell;
		_objectStorage.Cells[2].CellGameObject.transform.position = _objectStorage.Cells[1].CellGameObject.transform.position + Constants.distanceToNextCell; ;
		//_topCell = _objectStorage.Cells[2];
	}
	void GenerateCell(int i)
	{
		_objectStorage.Cells[i].CellGameObject.transform.position = _topCell.CellGameObject.transform.position + Constants.distanceToNextCell;
		//_topCell = _objectStorage.Cells[i];
		_objectStorage.Cells[i].CellGameObject.SetActive(true);

	}

}

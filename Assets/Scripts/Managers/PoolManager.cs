using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : IPoolManager
{
	IObjectStorage _objectStorage;
	IObjectCreateManager _objectCreateManager;

	public PoolManager(IObjectStorage objectStorage, IObjectCreateManager objectCreateManager)
	{
		_objectStorage = objectStorage;
		_objectCreateManager = objectCreateManager;
        _objectCreateManager.AddPrefabs(_objectStorage.Prefabs);
	}

    public void LoadLevel()
    {
        IList<ICell> currentCellSet = _objectStorage.CellSets[_objectStorage.Levels[0].CellSet];
        Vector3 currentCellPos = Constants.startCellPosition;
        for (int i = 0; i < currentCellSet.Count; i++)
        {
            currentCellSet[i].CellGameObject = GameObject.Instantiate(_objectStorage.Prefabs[Constants.cellPrefabName]);
            currentCellSet[i].CellGameObject.transform.position = currentCellPos;

            foreach (IUnit unit in currentCellSet[i].Units)
            {
                Vector3 spawnPos = _objectCreateManager.CalculateUnitSpawnPosition(unit.DiapasonSpawnPosition, currentCellPos); //использлвать в _objectCreateManager
                IUnit newUnit = _objectCreateManager.CreateUnit(unit, spawnPos);
                newUnit.GameObject.transform.position = spawnPos;
                var text = Resources.Load(Constants.prefabPath + "Text") as GameObject;
                newUnit.Text = GameObject.Instantiate<GameObject>(text);
                newUnit.Text.transform.position = newUnit.GameObject.transform.position + new Vector3(0.7f, 0.7f, 0);
                newUnit.Text.GetComponent<Text>().text = newUnit.Health.ToString();

                newUnit.Text.GetComponent<Text>().transform.SetParent(_objectStorage.Canvas.transform);
             
                _objectCreateManager.SetBehaviour(newUnit, spawnPos); //?

                if (_objectStorage.Units.Keys.Contains(unit.UnitType.ToString()))
                {
                    _objectStorage.Units[unit.UnitType.ToString()].Add(newUnit);
                }
                else
                {
                    _objectStorage.Units[unit.UnitType.ToString()] = new List<IUnit>();
                    _objectStorage.Units[unit.UnitType.ToString()].Add(newUnit);
                }
            }
            foreach (IObstacle obstacle in currentCellSet[i].ObstacleSet)
            {
                if (_objectStorage.Obstacles.Keys.Contains(obstacle.ObstacleType.ToString()))
                {
                    _objectStorage.Obstacles[obstacle.ObstacleType.ToString()].Add(_objectCreateManager.CreateObstacle(obstacle, currentCellPos));
                }
                else
                {
                    _objectStorage.Obstacles[obstacle.ObstacleType.ToString()] = new List<IObstacle>();
                    _objectStorage.Obstacles[obstacle.ObstacleType.ToString()].Add(_objectCreateManager.CreateObstacle(obstacle, currentCellPos));
                }
            }
            currentCellPos += Constants.distanceToNextCell;
        }
        CreateBullet();
    }

    void CreateBullet()
    {
        for (int i = 0; i < 20; i++)
        {
            IBullet bullet = _objectCreateManager.CreateBullet(_objectStorage.Prefabs[BulletType.BulletType1.ToString()]);
            bullet.BulletGameObject.SetActive(false);
            bullet.BulletType = BulletType.BulletType1;
            bullet.MoveSpeed = _objectStorage.BulletTemplates[bullet.BulletType.ToString()].MoveSpeed;
            if (_objectStorage.Bullets.Keys.Contains(BulletType.BulletType1.ToString()))
            {
                _objectStorage.Bullets[BulletType.BulletType1.ToString()].Add(bullet);

            }
            else
            {
                _objectStorage.Bullets[BulletType.BulletType1.ToString()] = new List<IBullet>();
                _objectStorage.Bullets[BulletType.BulletType1.ToString()].Add(bullet);
            }
        }
        for (int i = 0; i < 20; i++)
        {
            IBullet bullet = _objectCreateManager.CreateBullet(_objectStorage.Prefabs[BulletType.BulletType1.ToString()]);
            bullet.BulletGameObject.SetActive(false);
            bullet.BulletType = BulletType.BulletType2;
            bullet.MoveSpeed = _objectStorage.BulletTemplates[BulletType.BulletType1.ToString()].MoveSpeed;
            if (_objectStorage.Bullets.Keys.Contains(BulletType.BulletType2.ToString()))
            {
                _objectStorage.Bullets[BulletType.BulletType2.ToString()].Add(bullet);

            }
            else
            {
                _objectStorage.Bullets[BulletType.BulletType2.ToString()] = new List<IBullet>();
                _objectStorage.Bullets[BulletType.BulletType2.ToString()].Add(bullet);
            }
        }
        for (int i = 0; i < 20; i++)
        {
            IBullet bullet = _objectCreateManager.CreateBullet(_objectStorage.Prefabs[BulletType.BulletType1.ToString()]);
            bullet.BulletGameObject.SetActive(false);
            bullet.BulletType = BulletType.BulletType3;
            bullet.MoveSpeed = _objectStorage.BulletTemplates[BulletType.BulletType1.ToString()].MoveSpeed;

            if (_objectStorage.Bullets.Keys.Contains(BulletType.BulletType3.ToString()))
            {
                _objectStorage.Bullets[BulletType.BulletType3.ToString()].Add(bullet);

            }
            else
            {
                _objectStorage.Bullets[BulletType.BulletType3.ToString()] = new List<IBullet>();
                _objectStorage.Bullets[BulletType.BulletType3.ToString()].Add(bullet);
            }
        }
    }
}

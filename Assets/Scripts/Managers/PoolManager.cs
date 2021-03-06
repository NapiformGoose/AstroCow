﻿using Assets.Scripts;
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

    IList<ICell> CreateLinearLevel(IList<ICell> currentCellSet)
    {
        IList<ICell> cells = new List<ICell>();
        IList<ICell> temporaryCellList = new List<ICell>();

        int diapasonMinValue = 1;
        int diapasonMaxValue = 4;

        cells.Add(currentCellSet[0]); //playerCell

        while (currentCellSet.Count-1 != cells.Count)
        {
            for (int j = diapasonMinValue; j < diapasonMaxValue; j++)
            {
                temporaryCellList.Add(currentCellSet[j]);
            }
            for (int j = diapasonMinValue; j < diapasonMaxValue - 1; j++)
            {
                int index = UnityEngine.Random.Range(0, temporaryCellList.Count);
                cells.Add(temporaryCellList[index]);
                temporaryCellList.RemoveAt(index);
            }
            cells.Add(temporaryCellList[temporaryCellList.Count - 1]);
            diapasonMinValue = diapasonMaxValue;
            diapasonMaxValue += 3;

            temporaryCellList = new List<ICell>();
        }
        cells.Add(currentCellSet[currentCellSet.Count-1]);
        for (int i = 0; i < cells.Count; i++)
        {
            Debug.Log($"id - {cells[i].Id} dif - {cells[i].Difficult}");
        }
        return cells;
    }
    IList<ICell> CreateWaveLevel(IList<ICell> currentCellSet)
    {
        IList<ICell> cells = new List<ICell>();
        IList<ICell> temporaryCellList = new List<ICell>();

        int diapasonMinValue = 1;
        int diapasonMaxValue = 4;

        cells.Add(currentCellSet[0]); //playerCell

        while (currentCellSet.Count != cells.Count)
        {
            for (int j = diapasonMinValue; j < diapasonMaxValue; j++)
            {
                temporaryCellList.Add(currentCellSet[j]);
            }
            for (int j = diapasonMinValue; j < diapasonMaxValue - 1; j++)
            {
                int index = UnityEngine.Random.Range(0, temporaryCellList.Count);
                cells.Add(temporaryCellList[index]);
                temporaryCellList.RemoveAt(index);
            }
            cells.Add(temporaryCellList[temporaryCellList.Count - 1]);
            cells.Add(currentCellSet[diapasonMaxValue]);

            diapasonMinValue = diapasonMaxValue + 1;
            diapasonMaxValue += 4;

            temporaryCellList = new List<ICell>();
        }
        for (int i = 0; i < cells.Count; i++)
        {
            Debug.Log($"id - {cells[i].Id} dif - {cells[i].Difficult}");
        }

        return cells;
    }

    public void LoadLevel()
    {
        //CreateLinearLevel - Линеный
        //CreateWaveLevel - Волновой

        IList<ICell> currentCellSet = CreateLinearLevel(_objectStorage.CellSets[_objectStorage.Levels[0].CellSet]); //заменить название

        //IList<ICell> currentCellSet = _objectStorage.CellSets[_objectStorage.Levels[0].CellSet];
        Vector3 currentCellPos = Constants.startCellPosition;
        for (int i = 0; i < currentCellSet.Count; i++)
        {
            currentCellSet[i].CellGameObject = GameObject.Instantiate(_objectStorage.Prefabs[Constants.cellPrefabName]);
            currentCellSet[i].CellGameObject.transform.position = currentCellPos;

            foreach (IUnit unit in currentCellSet[i].Units)
            {
                Vector3 spawnPos = _objectCreateManager.CalculateUnitSpawnPosition(unit.DiapasonSpawnPosition, currentCellPos);
                IUnit newUnit = _objectCreateManager.CreateUnit(unit);

                _objectCreateManager.SetBehaviour(newUnit, spawnPos);

                if (_objectStorage.Units.Keys.Contains(unit.UnitType.ToString()))
                {
                    _objectStorage.Units[unit.UnitType.ToString()].Add(newUnit);
                }
                else
                {
                    _objectStorage.Units[unit.UnitType.ToString()] = new List<IUnit>();
                    _objectStorage.Units[unit.UnitType.ToString()].Add(newUnit);
                }
                IBonus bonus = unit.BonusType != BonusType.Empty ? _objectCreateManager.CreateBonus(_objectStorage.BonusesTemplates[unit.BonusType.ToString()]) : null;

                if (bonus == null)
                {
                    continue;
                }

                if (_objectStorage.Bonuses.Keys.Contains(bonus.BonusType.ToString()))
                {
                    _objectStorage.Bonuses[bonus.BonusType.ToString()].Add(bonus);
                }
                else
                {
                    _objectStorage.Bonuses[bonus.BonusType.ToString()] = new List<IBonus>();
                    _objectStorage.Bonuses[bonus.BonusType.ToString()].Add(bonus);
                }

                bonus.GameObject.SetActive(false);
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
        CreateBullets();
        CreateCoins();
    }

    public void InstantiateEntities()
    {
        foreach (string key in _objectStorage.Units.Keys)
        {
            foreach (IUnit unit in _objectStorage.Units[key])
            {
                _objectCreateManager.InstantiateUnit(unit, _objectStorage.Canvas.transform);
            }
        }
        foreach (string key in _objectStorage.Obstacles.Keys)
        {
            foreach (IObstacle obstacle in _objectStorage.Obstacles[key])
            {
                _objectCreateManager.InstantiateObstacle(obstacle);
            }
        }
        foreach (string key in _objectStorage.Bullets.Keys)
        {
            foreach (IBullet bullet in _objectStorage.Bullets[key])
            {
                _objectCreateManager.InstantiateBullet(bullet);
            }
        }

        foreach (ICoin coin in _objectStorage.Coins)
        {
            _objectCreateManager.InstantiateCoin(coin);
        }
    }

    void CreateBullets()
    {
        for (int i = 0; i < 50; i++)
        {
            IBullet bullet = _objectCreateManager.CreateBullet(_objectStorage.Prefabs[BulletType.BulletType1.ToString()]);
            bullet.BulletType = BulletType.BulletType1;
            bullet.MoveSpeed = _objectStorage.BulletTemplates[bullet.BulletType.ToString()].MoveSpeed;
            bullet.Behaviour = new Behaviour();
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
        for (int i = 0; i < 50; i++)
        {
            IBullet bullet = _objectCreateManager.CreateBullet(_objectStorage.Prefabs[BulletType.BulletType1.ToString()]);
            bullet.BulletType = BulletType.BulletType2;
            bullet.MoveSpeed = _objectStorage.BulletTemplates[BulletType.BulletType2.ToString()].MoveSpeed;
            bullet.Behaviour = new Behaviour();

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
        for (int i = 0; i < 50; i++)
        {
            IBullet bullet = _objectCreateManager.CreateBullet(_objectStorage.Prefabs[BulletType.BulletType4.ToString()]);
            bullet.BulletType = BulletType.BulletType2;
            bullet.MoveSpeed = _objectStorage.BulletTemplates[BulletType.BulletType3.ToString()].MoveSpeed;
            bullet.Behaviour = new Behaviour();

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
        for (int i = 0; i < 50; i++)
        {
            IBullet bullet = _objectCreateManager.CreateBullet(_objectStorage.Prefabs[BulletType.BulletType4.ToString()]);
            bullet.BulletType = BulletType.BulletType2;
            bullet.MoveSpeed = _objectStorage.BulletTemplates[BulletType.BulletType4.ToString()].MoveSpeed;
            bullet.Behaviour = new Behaviour();

            if (_objectStorage.Bullets.Keys.Contains(BulletType.BulletType4.ToString()))
            {
                _objectStorage.Bullets[BulletType.BulletType4.ToString()].Add(bullet);

            }
            else
            {
                _objectStorage.Bullets[BulletType.BulletType4.ToString()] = new List<IBullet>();
                _objectStorage.Bullets[BulletType.BulletType4.ToString()].Add(bullet);
            }
        }
    }
    void CreateCoins()
    {
        for (int i = 0; i < 7; i++)
        {
            _objectStorage.Coins.Add(new Coin());
        }
    }
}

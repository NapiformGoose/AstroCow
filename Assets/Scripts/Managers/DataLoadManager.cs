using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;
using System.Globalization;

public class DataLoadManager : IDataLoadManager
{
    IObjectStorage _objectStorage;

    public DataLoadManager(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
    }

    public void ReadConfig()
    {
        ReadTemplatesConfig();
        ReadObstacleSetsConfig();
        ReadCellsConfig();
        ReadObjectsConfig();
    }

    public void LoadPrefabs()
    {
        _objectStorage.ActivationTrigger = GameObject.Find("ActivationTrigger").GetComponent<Collider2D>() as Collider2D;
        _objectStorage.TopDeactivationTrigger = GameObject.Find("TopDeactivationTrigger").GetComponent<Collider2D>() as Collider2D;
        _objectStorage.DownDeactivationTrigger = GameObject.Find("DownDeactivationTrigger").GetComponent<Collider2D>() as Collider2D;
        _objectStorage.LeftDeactivationTrigger = GameObject.Find("LeftDeactivationTrigger").GetComponent<Collider2D>() as Collider2D;
        _objectStorage.RightDeactivationTrigger = GameObject.Find("RightDeactivationTrigger").GetComponent<Collider2D>() as Collider2D;
        _objectStorage.Canvas = GameObject.Find("Canvas").GetComponent<Canvas>() as Canvas;

        _objectStorage.Prefabs.Add(UnitType.Player.ToString(), Resources.Load(Constants.prefabPath + UnitType.Player.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType1.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType1.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType2.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType2.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType3.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType3.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType4.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType4.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType5.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType5.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType6.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType6.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType7.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType7.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(UnitType.EnemyType8.ToString(), Resources.Load(Constants.prefabPath + UnitType.EnemyType8.ToString()) as GameObject);

        _objectStorage.Prefabs.Add(ObstacleType.EnergyWall.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.EnergyWall.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(ObstacleType.SteelWall.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.SteelWall.ToString()) as GameObject);

        _objectStorage.Prefabs.Add(BulletType.BulletType1.ToString(), Resources.Load(Constants.prefabPath + BulletType.BulletType1.ToString()) as GameObject);

        _objectStorage.Prefabs.Add(Constants.controllerPrefabName, Resources.Load(Constants.prefabPath + Constants.controllerPrefabName) as GameObject);
        _objectStorage.Prefabs.Add(Constants.cellPrefabName, Resources.Load(Constants.prefabPath + Constants.cellPrefabName) as GameObject);
        _objectStorage.Prefabs.Add(Constants.lowerTriggerName, Resources.Load(Constants.prefabPath + Constants.lowerTriggerName) as GameObject);
    }

    #region Private read method
    void ReadTemplatesConfig()
    {
        string path = Constants.ConfigFolderPath + Constants.templatesConfigName + Constants.xmlFormat;
        XElement xEGameObject = XDocument.Parse(File.ReadAllText(path)).Element(Constants.templatesConfigName);

        try
        {
            foreach (XElement element in xEGameObject.Element("WeaponTemplates").Elements("WeaponTemplate"))
            {
                try
                {
                    ReadWeaponTemplate(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }
            foreach (XElement element in xEGameObject.Element("BulletTemplates").Elements("BulletTemplate"))
            {
                try
                {
                    ReadBulletTemplate(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }

            foreach (XElement element in xEGameObject.Element("UnitTemplates").Elements("UnitTemplate"))
            {
                try
                {
                    ReadUnitTemplate(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }
            foreach (XElement element in xEGameObject.Element("ObstacleTemplates").Elements("ObstacleTemplate"))
            {
                try
                {
                    ReadObstacleTemplate(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

    }

    void ReadCellsConfig()
    {
        string path = Constants.ConfigFolderPath + Constants.cellsConfigName + Constants.xmlFormat;
        XElement xEGameObject = XDocument.Parse(File.ReadAllText(path)).Element(Constants.cellsConfigName);
        try
        {
            foreach (XElement element in xEGameObject.Elements("Cell"))
            {
                try
                {
                    ReadCell(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void ReadObstacleSetsConfig()
    {
        string path = Constants.ConfigFolderPath + Constants.obstacleSetsConfigName + Constants.xmlFormat;
        XElement xEGameObject = XDocument.Parse(File.ReadAllText(path)).Element(Constants.obstacleSetsConfigName);

        try
        {
            foreach (XElement element in xEGameObject.Elements("ObstacleSet"))
            {
                try
                {
                    ReadObstacleSet(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void ReadObjectsConfig()
    {
        string path = Constants.ConfigFolderPath + Constants.objectsConfigName + Constants.xmlFormat;
        XElement xEGameObject = XDocument.Parse(File.ReadAllText(path)).Element(Constants.objectsConfigName);
        try
        {
            foreach (XElement element in xEGameObject.Element("CellSets").Elements("CellSet"))
            {
                try
                {
                    ReadCellSet(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }
            foreach (XElement element in xEGameObject.Element("Levels").Elements("Level"))
            {
                try
                {
                    ReadLevel(element);
                }
                catch
                {
                    throw new Exception($"Error reading config file.\nFile: {path}.\nNode: {element.Name}.\nNode value: {element}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    #endregion

    #region Templates methods

    void ReadUnitTemplate(XElement element)
    {
        IUnit unit = new Unit();

        unit.Alias = element.Attribute("alias").Value;
        unit.UnitType = (UnitType)Enum.Parse(typeof(UnitType), element.Attribute("type").Value);
        unit.Health = float.Parse(element.Attribute("health").Value, CultureInfo.InvariantCulture);
        unit.MoveSpeed = float.Parse(element.Attribute("moveSpeed").Value, CultureInfo.InvariantCulture);
        unit.Ghost = bool.Parse(element.Attribute("ghost").Value);
        unit.Weapon = _objectStorage.WeaponTemplates[element.Attribute("weaponType").Value];

        _objectStorage.UnitTemplates.Add(element.Attribute("type").Value, unit);
    }

    void ReadObstacleTemplate(XElement element)
    {
        IObstacle obstacle = new Obstacle();

        obstacle.Alias = element.Attribute("alias").Value;
        obstacle.ObstacleType = (ObstacleType)Enum.Parse(typeof(ObstacleType), element.Attribute("type").Value);

        _objectStorage.ObstacleTemplates.Add(element.Attribute("type").Value, obstacle);
    }

    void ReadWeaponTemplate(XElement element)
    {
        IWeapon weapon = new Weapon
        {
            Alias = element.Attribute("alias").Value,
            WeaponType = (WeaponType)Enum.Parse(typeof(WeaponType), element.Attribute("type").Value),
            FireSpeed = float.Parse(element.Attribute("fireSpeed").Value, CultureInfo.InvariantCulture),
            ReloadSpeed = float.Parse(element.Attribute("reloadSpeed").Value, CultureInfo.InvariantCulture),
            CritAttack = float.Parse(element.Attribute("critAttack").Value, CultureInfo.InvariantCulture),
            BaseAttack = float.Parse(element.Attribute("baseAttack").Value, CultureInfo.InvariantCulture),
            BulletType = (BulletType)Enum.Parse(typeof(BulletType), element.Attribute("bulletType").Value)
        };

        _objectStorage.WeaponTemplates.Add(weapon.WeaponType.ToString(), weapon);
    }

    void ReadBulletTemplate(XElement element)
    {
        IBullet bullet = new Bullet
        {
            Alias = element.Attribute("alias").Value,
            BulletType = (BulletType)Enum.Parse(typeof(BulletType), element.Attribute("type").Value),
            MoveSpeed = float.Parse(element.Attribute("moveSpeed").Value, CultureInfo.InvariantCulture),
        };

        _objectStorage.BulletTemplates.Add(bullet.BulletType.ToString(), bullet);
    }

    #endregion

    #region Cells method

    void ReadCell(XElement element)
    {
        ICell cell = new Cell();
        cell.DiapasonSpawnPositions = new Dictionary<int, IDiapasonSpawnPosition>();
        cell.Units = new List<IUnit>();
        //cell.Difficult = int.Parse(element.Attribute("difficult").Value);

        foreach (XElement subElement in element.Element("SpawnPositions").Elements("diapasonSpawnPosition"))
        {
            IDiapasonSpawnPosition diapasonSpawnPosition = new DiapasonSpawnPosition();

            diapasonSpawnPosition.minXPos = int.Parse(subElement.Attribute("minXPos").Value);
            diapasonSpawnPosition.maxXPos = int.Parse(subElement.Attribute("maxXPos").Value);

            diapasonSpawnPosition.minYPos = int.Parse(subElement.Attribute("minYPos").Value);
            diapasonSpawnPosition.maxYPos = int.Parse(subElement.Attribute("maxYPos").Value);

            diapasonSpawnPosition.ZPos = int.Parse(subElement.Attribute("ZPos").Value);

            cell.DiapasonSpawnPositions.Add(int.Parse(subElement.Attribute("id").Value), diapasonSpawnPosition);
        }

        foreach (XElement subElement in element.Element("Units").Elements("Unit"))
        {
            IUnit template = _objectStorage.UnitTemplates[subElement.Attribute("type").Value];
            IUnit unit = new Unit
            {
                Alias = template.UnitType.ToString(),
                UnitType = template.UnitType,
                Health = template.Health,
                MoveSpeed = template.MoveSpeed,
                Ghost = template.Ghost,
                DiapasonSpawnPosition = cell.DiapasonSpawnPositions[int.Parse(subElement.Attribute("diapasonSpawnPosition").Value)],
                Weapon = template.Weapon
            };
            cell.Units.Add(unit);
        }

        IList<IObstacle> obstacles = new List<IObstacle>(); //исправить для пулл менеджера!
        foreach (IObstacle obstacleTemplate in _objectStorage.ObstacleSet[int.Parse(element.Element("ObstacleSet").Attribute("id").Value)])
        {
            IObstacle obstacle = new Obstacle
            {
                Alias = obstacleTemplate.Alias,
                ObstacleType = obstacleTemplate.ObstacleType,
                SpawnPosition = obstacleTemplate.SpawnPosition
            };
            obstacles.Add(obstacle);
        }
        cell.ObstacleSet = obstacles;

        _objectStorage.Cells.Add(int.Parse(element.Attribute("id").Value), cell);
    }
    #endregion

    #region Sets methods

    void ReadObstacleSet(XElement element)
    {
        IList<IObstacle> obstacles = new List<IObstacle>();
        foreach (XElement subElement in element.Elements("Obstacle"))
        {
            IObstacle template = _objectStorage.ObstacleTemplates[subElement.Attribute("type").Value];

            int x = int.Parse(subElement.Attribute("x").Value);
            int y = int.Parse(subElement.Attribute("y").Value);
            int z = int.Parse(subElement.Attribute("z").Value);
            IObstacle obstacle = new Obstacle
            {
                Alias = template.ObstacleType.ToString(),
                ObstacleType = template.ObstacleType,
                SpawnPosition = new Vector3(x, y, z)

            };
            obstacles.Add(obstacle);
        }
        _objectStorage.ObstacleSet.Add(int.Parse(element.Attribute("id").Value), obstacles);
    }

    void ReadCellSet(XElement element)
    {
        IList<ICell> cells = new List<ICell>();
        foreach (XElement subElement in element.Elements("Cell"))
        {
            cells.Add(_objectStorage.Cells[int.Parse(subElement.Attribute("cellId").Value)]);
        }
        _objectStorage.CellSets.Add(int.Parse(element.Attribute("id").Value), cells);

    }

    void ReadLevel(XElement element)
    {
        ILevel level = new Level();
        level.Id = int.Parse(element.Attribute("id").Value);
        level.Stage = int.Parse(element.Attribute("stage").Value);
        level.CellSet = int.Parse(element.Attribute("cellSet").Value);

        _objectStorage.Levels.Add(level);
    }

    #endregion

}
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

        _objectStorage.Prefabs.Add(ObstacleType.WallType1.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.WallType1.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(ObstacleType.WallType2.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.WallType2.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(ObstacleType.WallType3.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.WallType1.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(ObstacleType.WallType4.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.WallType1.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(ObstacleType.WallType5.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.WallType1.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(ObstacleType.WallType6.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.WallType6.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(ObstacleType.WallType7.ToString(), Resources.Load(Constants.prefabPath + ObstacleType.WallType7.ToString()) as GameObject);

        _objectStorage.Prefabs.Add(BulletType.BulletType1.ToString(), Resources.Load(Constants.prefabPath + BulletType.BulletType1.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(BulletType.BulletType2.ToString(), Resources.Load(Constants.prefabPath + BulletType.BulletType2.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(BulletType.BulletType3.ToString(), Resources.Load(Constants.prefabPath + BulletType.BulletType3.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(BulletType.BulletType4.ToString(), Resources.Load(Constants.prefabPath + BulletType.BulletType4.ToString()) as GameObject);

        _objectStorage.Prefabs.Add(BonusType.BonusHealth.ToString(), Resources.Load(Constants.prefabPath + BonusType.BonusHealth.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(BonusType.BonusBoom.ToString(), Resources.Load(Constants.prefabPath + BonusType.BonusBoom.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(BonusType.BonusFastShoot.ToString(), Resources.Load(Constants.prefabPath + BonusType.BonusFastShoot.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(BonusType.СrystalEnemies.ToString(), Resources.Load(Constants.prefabPath + BonusType.СrystalEnemies.ToString()) as GameObject);
        _objectStorage.Prefabs.Add(BonusType.BonusMachine.ToString(), Resources.Load(Constants.prefabPath + BonusType.BonusMachine.ToString()) as GameObject);

        _objectStorage.Prefabs.Add(Constants.coin, Resources.Load(Constants.prefabPath + Constants.coin) as GameObject);

        _objectStorage.Prefabs.Add(Constants.controllerPrefabName, Resources.Load(Constants.prefabPath + Constants.controllerPrefabName) as GameObject);
        _objectStorage.Prefabs.Add(Constants.cellPrefabName, Resources.Load(Constants.prefabPath + Constants.cellPrefabName) as GameObject);
        _objectStorage.Prefabs.Add(Constants.lowerTriggerName, Resources.Load(Constants.prefabPath + Constants.lowerTriggerName) as GameObject);
    }

    public void CreateUpgrades()
    {
        IUpgrade fireSpeedUp = new Upgrade(Constants.FireSpeedUpTitle, Constants.FireSpeedUpDescription, "", UpgradeType.FireSpeedUp);
        _objectStorage.Upgrades.Add(fireSpeedUp);

        IUpgrade baseAttackUp = new Upgrade(Constants.BaseAttackUpTitle, Constants.BaseAttackUpDescription, "", UpgradeType.BaseAttackUp);
        _objectStorage.Upgrades.Add(baseAttackUp);

        IUpgrade healthUp = new Upgrade(Constants.HealthUpTitle, Constants.HealthUpDescription, "", UpgradeType.HealthUp);
        _objectStorage.Upgrades.Add(healthUp);

        IUpgrade reloadSpeedUp = new Upgrade(Constants.ReloadSpeedUpTitle, Constants.ReloadSpeedUpDescription, "", UpgradeType.ReloadSpeedUp);
        _objectStorage.Upgrades.Add(reloadSpeedUp);

        IUpgrade resurrection = new Upgrade(Constants.ResurrectionTitle, Constants.ResurrectionDescription, "", UpgradeType.Resurrection);
        _objectStorage.Upgrades.Add(resurrection);

        IUpgrade bloodthirstiness = new Upgrade(Constants.BloodthirstinessTitle, Constants.BloodthirstinessDescription, "", UpgradeType.Bloodthirstiness);
        _objectStorage.Upgrades.Add(bloodthirstiness);

        IUpgrade lootPercentUp = new Upgrade(Constants.LootPercentUpTitle, Constants.LootPercentUpDescription, "", UpgradeType.LootPercentUp);
        _objectStorage.Upgrades.Add(lootPercentUp);

        IUpgrade bonusRandomUp = new Upgrade(Constants.BonusRandomUpTitle, Constants.BonusRandomUpDescription, "", UpgradeType.BonusRandomUp);
        _objectStorage.Upgrades.Add(bonusRandomUp);

        IUpgrade magazineCapacityUp = new Upgrade(Constants.MagazineCapacityUpTitle, Constants.MagazineCapacityUpDescription, "", UpgradeType.MagazineCapacityUp);
        _objectStorage.Upgrades.Add(magazineCapacityUp);
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
            foreach (XElement element in xEGameObject.Element("BonusTemplates").Elements("BonusTemplate"))
            {
                try
                {
                    ReadBonusTemplate(element);
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
        unit.InactiveTime = float.Parse(element.Attribute("inactiveTime").Value, CultureInfo.InvariantCulture);
        unit.Weapon = _objectStorage.WeaponTemplates[element.Attribute("weaponType").Value];
        unit.ExperienceValue = float.Parse(element.Attribute("experienceValue").Value, CultureInfo.InvariantCulture);
        unit.LootPercent = float.Parse(element.Attribute("lootPercent").Value, CultureInfo.InvariantCulture);

        _objectStorage.UnitTemplates.Add(element.Attribute("type").Value, unit);
    }

    void ReadObstacleTemplate(XElement element)
    {
        IObstacle obstacle = new Obstacle();

        obstacle.Alias = element.Attribute("alias").Value;
        obstacle.ObstacleType = (ObstacleType)Enum.Parse(typeof(ObstacleType), element.Attribute("type").Value);
        obstacle.DamagePercent = float.Parse(element.Attribute("damagePercent").Value, CultureInfo.InvariantCulture);
        obstacle.Health = float.Parse(element.Attribute("health").Value, CultureInfo.InvariantCulture);

        _objectStorage.ObstacleTemplates.Add(element.Attribute("type").Value, obstacle);
    }

    void ReadBonusTemplate(XElement element)
    {
        IBonus bonus = new Bonus();

        bonus.Alias = element.Attribute("alias").Value;
        bonus.BonusType = (BonusType)Enum.Parse(typeof(BonusType), element.Attribute("type").Value);
        bonus.RandomPercent = float.Parse(element.Attribute("randomPercent").Value, CultureInfo.InvariantCulture);
        bonus.HealthValue = float.Parse(element.Attribute("healthValue").Value, CultureInfo.InvariantCulture);
        bonus.FireSpeedCoefficient = float.Parse(element.Attribute("fireSpeedCoefficient").Value, CultureInfo.InvariantCulture);
        bonus.ReloadSpeedCoefficient = float.Parse(element.Attribute("reloadSpeedCoefficient").Value, CultureInfo.InvariantCulture);
        bonus.ActiveTime = float.Parse(element.Attribute("activeTime").Value, CultureInfo.InvariantCulture);

        _objectStorage.BonusesTemplates.Add(element.Attribute("type").Value, bonus);
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
        cell.Difficult = float.Parse(element.Attribute("difficult").Value, CultureInfo.InvariantCulture);
        cell.Id = int.Parse(element.Attribute("id").Value, CultureInfo.InvariantCulture);

        foreach (XElement subElement in element.Element("SpawnPositions").Elements("diapasonSpawnPosition"))
        {
            IDiapasonSpawnPosition diapasonSpawnPosition = new DiapasonSpawnPosition();

            diapasonSpawnPosition.minXPos = float.Parse(subElement.Attribute("minXPos").Value, CultureInfo.InvariantCulture);
            diapasonSpawnPosition.maxXPos = float.Parse(subElement.Attribute("maxXPos").Value, CultureInfo.InvariantCulture);

            diapasonSpawnPosition.minYPos = float.Parse(subElement.Attribute("minYPos").Value, CultureInfo.InvariantCulture);
            diapasonSpawnPosition.maxYPos = float.Parse(subElement.Attribute("maxYPos").Value, CultureInfo.InvariantCulture);

            diapasonSpawnPosition.ZPos = float.Parse(subElement.Attribute("ZPos").Value);

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
                Weapon = template.Weapon,
                InactiveTime = template.InactiveTime,
                BonusType = (BonusType)Enum.Parse(typeof(BonusType), subElement.Attribute("bonusType").Value),
                ExperienceValue = template.ExperienceValue,
                LootPercent = template.LootPercent,
                MagazineCapacity = Constants.magazineCapacity
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
                SpawnPosition = obstacleTemplate.SpawnPosition,
                DamagePercent = obstacleTemplate.DamagePercent,
                Health = obstacleTemplate.Health
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

            float x = float.Parse(subElement.Attribute("x").Value);
            float y = float.Parse(subElement.Attribute("y").Value);
            float z = float.Parse(subElement.Attribute("z").Value);
            IObstacle obstacle = new Obstacle
            {
                Alias = template.ObstacleType.ToString(),
                ObstacleType = template.ObstacleType,
                SpawnPosition = new Vector3(x, y, z),
                DamagePercent = template.DamagePercent,
                Health = template.Health
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
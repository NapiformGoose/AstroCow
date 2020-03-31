using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class DataLoadManager : IDataLoadManager
{
    IObjectStorage _objectStorage;
    string _path;
    XElement _xEGameObject;
    public DataLoadManager(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
    }

    public void Read()
    {
        _path = Application.dataPath + "/Config/Objects.xml";
        _xEGameObject = XDocument.Parse(File.ReadAllText(_path)).Element("Objects");

        foreach (XElement element in _xEGameObject.Element("UnitTemplates").Elements("UnitTemplate"))
        {
            ReadUnitTemplate(element);
        }
        foreach (XElement element in _xEGameObject.Element("ObstacleTemplates").Elements("ObstacleTemplate"))
        {
            ReadObstacleTemplate(element);
        }
        foreach (XElement element in _xEGameObject.Element("ObstacleSets").Elements("ObstacleSet"))
        {
            ReadObstacleSet(element);
        }
        foreach (XElement element in _xEGameObject.Element("Cells").Elements("Cell"))
        {
            ReadCell(element);
        }
        foreach (XElement element in _xEGameObject.Element("CellSets").Elements("CellSet"))
        {
            ReadCellSet(element);
        }
        foreach (XElement element in _xEGameObject.Element("Levels").Elements("Level"))
        {
            ReadLevel(element);
        }   
    }
    void ReadUnitTemplate(XElement element)
    {
        IUnit unit = new Unit();

        unit.Alias = element.Attribute("alias").Value;
        unit.UnitType = (UnitType)Enum.Parse(typeof(UnitType), element.Attribute("type").Value);
        unit.BaseAttack = float.Parse(element.Attribute("baseAttack").Value);
        unit.Health = int.Parse(element.Attribute("health").Value);
        unit.Armor = float.Parse(element.Attribute("armor").Value);
        unit.Speed = float.Parse(element.Attribute("speed").Value);
        unit.Ghost = bool.Parse(element.Attribute("ghost").Value);

        _objectStorage.UnitTemplates.Add(element.Attribute("type").Value, unit);
    }

    void ReadObstacleTemplate(XElement element)
    {
        IObstacle obstacle = new Obstacle();

        obstacle.Alias = element.Attribute("alias").Value;
        obstacle.ObstacleType = (ObstacleType)Enum.Parse(typeof(ObstacleType), element.Attribute("type").Value);

        _objectStorage.ObstacleTemplates.Add(element.Attribute("type").Value, obstacle);
    }
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
                BaseAttack = template.BaseAttack,
                Health = template.Health,
                Armor = template.Armor,
                Speed = template.Speed,
                Ghost = template.Ghost,
                DiapasonSpawnPosition = cell.DiapasonSpawnPositions[int.Parse(subElement.Attribute("diapasonSpawnPosition").Value)]
            };
            cell.Units.Add(unit);
        }

        IList<IObstacle> obstacles = new List<IObstacle>(); //исправить для пулл менеджера!
        foreach(IObstacle obstacleTemplate in _objectStorage.ObstacleSet[int.Parse(element.Element("ObstacleSet").Attribute("id").Value)])
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
}

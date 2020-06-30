using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IObjectStorage
    {
        IDictionary<string, GameObject> Prefabs { get; set; }

        IDictionary<string, IUnit> UnitTemplates { get; set; }
        IDictionary<string, IObstacle> ObstacleTemplates { get; set; }
        IDictionary<string, IWeapon> WeaponTemplates { get; set; }
        IDictionary<string, IBullet> BulletTemplates { get; set; }
        IDictionary<string, IBonus> BonusesTemplates { get; set; }

        IDictionary<int, IList<IObstacle>> ObstacleSet { get; set; }
        IDictionary<int, ICell> Cells { get; set; }
        IDictionary<int, IList<ICell>> CellSets { get; set; }
        IList<ILevel> Levels { get; set; }
        IDictionary<string, IList<IUnit>> Units { get; set; }
        IDictionary<string, IList<IObstacle>> Obstacles { get; set; }
        IDictionary<string, IList<IBullet>> Bullets { get; set; }
        IDictionary<string, IList<IBonus>> Bonuses { get; set; }
        IList<IUpgrade> Upgrades { get; set; }
        IList<ICoin> Coins { get; set; }

        Collider2D ActivationTrigger { get; set; }
        Collider2D TopDeactivationTrigger { get; set; }
        Collider2D DownDeactivationTrigger { get; set; }
        Collider2D LeftDeactivationTrigger { get; set; }
        Collider2D RightDeactivationTrigger { get; set; }

        Canvas Canvas { get; set; }

        void ClearLevelData();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum UnitType
    {
        Player,
        EnemyType1,
        EnemyType2,
        EnemyType3
    }

    public enum ObstacleType
    {
        SteelWall,
        EnergyWall
    }

    public enum BulletType
    {
        BulletType1,
        BulletType2,
        BulletType3
    }

    public enum WeaponType
    {
        WeaponType1,
        WeaponType2
    }

    public enum Team
    {
        Player,
        Enemy
    }
    public static class Constants
    {
        //Config files
        public static string ConfigFolderPath = Application.dataPath + "/Config/";

        public static string templatesConfigName = "Templates";
        public static string cellsConfigName = "Cells";
        public static string setsConfigName = "Sets";
        public static string obstacleSetsConfigName = "ObstacleSets";
        public static string objectsConfigName = "Objects";
        public static string xmlFormat = ".xml";

        public static string controllerPrefabName = "Controller";
        public static string cellPrefabName = "Cell";
        public static string lowerTriggerName = "LowerTrigger";
        public static string prefabPath = "Prefabs/"; 

        //Positions
        public static Vector3 playerStartPosition = new Vector3(0, 0, 0);
        public static Vector3 startCellPosition = Vector3.zero;
        public static Vector3 distanceToNextCell = new Vector3(0, 12.8f, 0);

        public static float cameraSpeed = 1f;

        //Player Property Modifiers
        public static float baseAttackModifier = 1f;
        public static float critAttackModifier = 2f; //пока не работает
        public static float fireSpeedModifier = 1f;
        public static float reloadSpeedModifier = 2f; //пока не работает

        public static float unitCollisionDamage = 5f; //
        public static float energyWallDamage = 5f;    //если урон от столкновения 100%, то сюда вписывается количество хп коровы из конфига
        public static float steelWallDamage = 10f;    //
    }
}

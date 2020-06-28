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
        EnemyType3,
        EnemyType4,
        EnemyType5,
        EnemyType6,
        EnemyType7,
        EnemyType8,
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

    public enum BulletBehaviourType
    {
        Vertical,
        LeftDiagonal,
        RightDiagonal,
        Directional,
        Homing,
        LeftDirectional,
        RightDirectional,
        AroundDirectional
    }
    public enum WeaponType
    {
        WeaponType1,
        WeaponType2,
        WeaponType3,
        WeaponType4,
        WeaponType5,
        WeaponType7,
        PlayerWeaponType1,
        PlayerWeaponType2
    }

    public enum Team
    {
        Player,
        Enemy
    }

    public enum BonusType
    {
        Empty,
        BonusBoom,
        BonusFrost,
        BonusHealth,
        BonusMagnet,
        СrystalEnemies
    }

    public enum UpgradeType
    {
        FireSpeedUp,
        BaseAttackUp,
        HealthUp
    }

    public enum EffectType
    {
        FireSpeedUp
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

        public static float cameraSpeed = 2f;

        //Player Property Modifiers
        public static float baseAttackModifier = 1f;
        public static float critAttackModifier = 2f; //пока не работает
        public static float fireSpeedModifier = 1f;
        public static float reloadSpeedModifier = 2f; //пока не работает

        public static float unitCollisionDamage = 5f; //
        public static float energyWallDamage = 5f;    //если урон от столкновения 100%, то сюда вписывается количество хп коровы из конфига
        public static float steelWallDamage = 10f;    //

        public static Vector3 centerActiveField = Camera.main.transform.position;
        public static Vector3 topActiveField = Camera.main.transform.position + new Vector3(0, 6.4f, 0);

        //UI
        //Buttons
        public static string playBatton = "PlayButton";
        public static string restartButton = "RestartButton";
        public static string continueButton = "ContinueButton";
        public static string menuButton = "MenuButton";
        public static string exitButton = "ExitButton";
        public static string backButton = "BackButton";

        public static string firstUpgradeButton = "FirstUpgradeButton"; 
        public static string secondUpgradeButton = "SecondUpgradeButton";
        public static string thirdUpgradeButton = "ThirdUpgradeButton";

        //Upgrades
        //Titles
        public static string FireSpeedUpTitle = "Скорость атаки";
        public static string BaseAttackUpTitle = "Урон";
        public static string HealthUpTitle = "Здоровье";
        //Descriptions
        public static string FireSpeedUpDescription = "Увеличивает скорострельность на 10%";
        public static string BaseAttackUpDescription = "Увеличивает урон на 10%";
        public static string HealthUpDescription = "Увеличивает количество здоровья на 10%";

    }
}

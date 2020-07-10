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
        WallType1,
        WallType2,
        WallType3,
        WallType4,
        WallType5,
        WallType6,
        WallType7
    }

    public enum BulletType
    {
        BulletType1,
        BulletType2,
        BulletType3,
        BulletType4
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
        WeaponType0,
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
        BonusFastShoot,
        СrystalEnemies,
        BonusMachine
    }

    public enum UpgradeType
    {
        FireSpeedUp,
        BaseAttackUp,
        HealthUp,
        ReloadSpeedUp,
        Resurrection,
        Bloodthirstiness,
        LootPercentUp,
        BonusRandomUp,
        MagazineCapacityUp
    }

    public enum EffectType
    {
        FireSpeedUp,
        MoveSpeedReduce
    }

    public static class Constants
    {
        public static string coin = "Coin";


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
        //buttons
        public static string playBatton = "PlayButton";
        public static string restartButton = "RestartButton";
        public static string continueButton = "ContinueButton";
        public static string pauseButton = "PauseButton";
        public static string exitButton = "ExitButton";
        public static string backButton = "BackButton";

        public static string firstUpgradeButton = "FirstUpgradeButton"; 
        public static string secondUpgradeButton = "SecondUpgradeButton";
        public static string thirdUpgradeButton = "ThirdUpgradeButton";

        public static string machineButton = "MachineButton";
        public static string weaponApplyButton = "WeaponApplyButton";

        //Bars
        public static float experiencebarMinValue = 0;
        public static float experiencebarMaxValue = 100;
        public static float experiencebarNextLevelValue = 10;

        //Upgrades
        //values and percent
        public static int magazineCapacity = 10; //количество патронов на старте
        public static int maxMagazineCapacity = 17; //максимум патронов (если больше, то магазин вылезет за пределы экоана и сломается)

        public static float bloodthirstiness = 0; //кровожадность на старте

        public static float fireSpeedUpPercent = 10;     //прцоенты
        public static float baseAttackUpPercent = 10;
        public static float healthUpPercent = 10;
        public static float reloadSpeedUpPercent = 10;
        public static float bloodthirstinessPercent = 10;
        public static float lootPercentUpPercent = 10;
        public static float bonusRandomUpPercent = 10;
        public static int magazineCapacityUpValue = 1; //количество патронов которое добавится при выборе перка

        //titles
        public static string FireSpeedUpTitle = "Скорость атаки";
        public static string BaseAttackUpTitle = "Урон";
        public static string HealthUpTitle = "Здоровье";
        public static string ReloadSpeedUpTitle = "Скорость перезарядки";
        public static string ResurrectionTitle = "Возрождение";
        public static string BloodthirstinessTitle = "Кровожадность";
        public static string LootPercentUpTitle = "Нужно больше золота";
        public static string BonusRandomUpTitle = "Бонусный магнат";
        public static string MagazineCapacityUpTitle = "Больше патронов";

        //descriptions
        public static string FireSpeedUpDescription = $"Увеличивает скорострельность на {fireSpeedUpPercent}%";
        public static string BaseAttackUpDescription = $"Увеличивает урон на {baseAttackUpPercent}%";
        public static string HealthUpDescription = $"Увеличивает максимальное и текущее количество здоровья на {healthUpPercent}%";
        public static string ReloadSpeedUpDescription = $"Увеличивает скорость перезарядки на {reloadSpeedUpPercent}%";
        public static string ResurrectionDescription = "Возрождение после смерти с 100% здоровья";
        public static string BloodthirstinessDescription = $"Пополнение здоровья после каждого убитого врага на {bloodthirstinessPercent}%";
        public static string LootPercentUpDescription = $"Увеличивает шанс выпадения монет из врагов на {lootPercentUpPercent}%";
        public static string BonusRandomUpDescription = $"Увеличивает шанс выпадения бонусов из врагов на {bonusRandomUpPercent}%";
        public static string MagazineCapacityUpDescription = $"Увеличивает ёмкость магазина на {magazineCapacityUpValue} патрон";

    }
}

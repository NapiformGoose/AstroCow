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
        public static string controllerPrefabName = "Controller";
        public static string cellPrefabName = "Cell";
        public static string lowerTriggerName = "LowerTrigger";
        public static string prefabPath = "Prefabs/"; 


        //Game logic: player
        public static Vector3 playerStartPosition = new Vector3(0, 0, 0);

        public static Vector3 startCellPosition = Vector3.zero;
        public static Vector3 distanceToNextCell = new Vector3(0, 12.8f, 0);

        public static float cameraSpeed = 1f;
    }
}

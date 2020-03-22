using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Constants
    {
        public static string playerPrefabName = "Player";
        public static string controllerPrefabName = "Controller";
        public static string cellPrefabName = "Cell";
        public static string lowerTriggerName = "LowerTrigger";
        public static string prefabPath = "Prefabs/"; 


        //Game logic: player
        public static float playerSpeed = 1f;
        public static Vector3 playerStartPosition = new Vector3(0, 0, 0);
        public static float maxPlayerVelocity = 10f;

        public static Vector3 startCellPosition = Vector3.zero;
        public static Vector3 distanceToNextCell = new Vector3(0, 70, 0);

        public static float cameraSpeed = 20f;

    }
}

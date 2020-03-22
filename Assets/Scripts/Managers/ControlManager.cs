using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers
{
    public class ControlManager : IControlManager, IUpdatable
    {
        IUpdateManager _updateManager;
        IObjectStorage _objectStorage;

        Player _player;
        Vector3 clickPos = new Vector3();
        Vector3 newPlayerPosition = new Vector3();
        Vector3 distToPlayer = new Vector3();

        public ControlManager(IUpdateManager updateManager, IObjectStorage objectStorage)
        {
            _updateManager = updateManager;
            _objectStorage = objectStorage;

            _updateManager.AddUpdatable(this);
            _player = _objectStorage.Player;
        }
        public void Initialization()
        {
        }

        public void CustomUpdate()
        {
            PlayerControl();
        }

        void PlayerControl()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    clickPos = hit.point;
                    distToPlayer = new Vector3(_player.UnitGameObject.transform.position.x - clickPos.x, _player.UnitGameObject.transform.position.y - clickPos.y, 10);
                }
            }
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    newPlayerPosition = new Vector3(hit.point.x + distToPlayer.x, hit.point.y + distToPlayer.y, 0);
                }
            }
                MovePlayer();
        }

        void MovePlayer()
        {
            _player.UnitGameObject.transform.position = newPlayerPosition;
        }
    }
}

//контроллер следует за камерой
//resultVector = new Vector3(secondPos.x - firstPos.x, secondPos.y - firstPos.y, 0);
//_player.UnitGameObject.transform.Translate(resultVector * 22f * Time.deltaTime, Space.World);
//secondPos += new Vector3(0, 5f * Time.deltaTime, 0);
//_objectStorage.Controller.transform.position += new Vector3(0, 5f * Time.deltaTime, 0); 

// разворот юнита "головой" к позиции в 2д
//void LookAt2D(Transform me, Vector3 target, Vector3? eye = null)
//{
//    float signedAngle = Vector2.SignedAngle(eye ?? me.up, target - me.position); 

//    if (Mathf.Abs(signedAngle) >= 1e-3f)
//    {
//        var angles = me.eulerAngles;
//        angles.z += signedAngle;
//        me.eulerAngles = angles;
//    }
//}
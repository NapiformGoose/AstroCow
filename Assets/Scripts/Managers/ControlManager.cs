using UnityEngine;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;


namespace Assets.Scripts.Managers
{
    public class ControlManager : IControlManager, IUpdatable
    {
        IUpdateManager _updateManager;
        IObjectStorage _objectStorage;

        IUnit _player;
        Vector3 firstClickPos = new Vector3();
        Vector3 newPlayerPosition = Vector3.zero;
        Vector3 distToPlayer = new Vector3();
        Vector3 direction = new Vector3();

        bool isMoving = false;
        public ControlManager(IUpdateManager updateManager, IObjectStorage objectStorage)
        {
            _updateManager = updateManager;
            _objectStorage = objectStorage;

            _updateManager.AddUpdatable(this);
            _player = _objectStorage.Units[UnitType.Player.ToString()][0];
        }
        public void Initialization()
        {
        }

        public void CustomFixedUpdate()
        {
            MoveCamera();
           
            if (isMoving)
            {
                Moving();
            }
            else
            {
                FollowCamera();
            }
        }
        public void CustomUpdate()
        {
            CalculatePosition();
        }

        void CalculatePosition()
        {
            isMoving = true;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    firstClickPos = hit.point;
                    distToPlayer = new Vector3(_player.GameObject.transform.position.x - firstClickPos.x, _player.GameObject.transform.position.y - firstClickPos.y, 0);
                }
            }
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    direction = hit.point - firstClickPos;
                    newPlayerPosition = new Vector3(hit.point.x + distToPlayer.x, hit.point.y + distToPlayer.y, 0);
                }
            }
            if (!Input.GetMouseButton(0))
            {
                isMoving = false;
            }
        }
        void FollowCamera()
        {
            _player.RigidBody2D.MovePosition(Vector2.MoveTowards(_player.GameObject.transform.position, new Vector2(_player.GameObject.transform.position.x, _player.GameObject.transform.position.y) + new Vector2(0, Constants.cameraSpeed) * Time.fixedDeltaTime, 5f));
        }

        void Moving()
        {
            _player.RigidBody2D.MovePosition(Vector2.MoveTowards(_player.GameObject.transform.position, newPlayerPosition, _player.MoveSpeed));
        }
        void MoveCamera()
        {
            Camera.main.transform.position += new Vector3(0, Constants.cameraSpeed * Time.deltaTime, 0);
        }
    }
}

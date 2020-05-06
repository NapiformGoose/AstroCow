using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class ObjectCreateManager : IObjectCreateManager
    {
        IDictionary<string, GameObject> _prefabs;
        public ObjectCreateManager()
        {

        }
        public void AddPrefabs(IDictionary<string, GameObject> prefabs)
        {
            _prefabs = prefabs;
        }

        public IObstacle CreateObstacle(IObstacle obstacle, Vector3 currentCellPos)
        {
            IObstacle newObstacle = new Obstacle
            {
                ObstacleGameObject = GameObject.Instantiate(_prefabs[obstacle.ObstacleType.ToString()])
            };
            newObstacle.ObstacleCollider2D = newObstacle.ObstacleGameObject.GetComponent<Collider2D>() as Collider2D;
            newObstacle.ObstacleRigidBody2D = newObstacle.ObstacleGameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
            newObstacle.ObstacleGameObject.transform.position = Vector3.zero;

            newObstacle.ObstacleGameObject.transform.position = currentCellPos + obstacle.SpawnPosition;
            return newObstacle;
        }

        public IUnit CreateUnit(IUnit unit, Vector3 spawnPos)
        {
            IUnit newUnit = new Unit
            {
                GameObject = GameObject.Instantiate(_prefabs[unit.UnitType.ToString()]),
            };

            newUnit.UnitType = unit.UnitType;
            newUnit.Team = unit.UnitType == UnitType.Player ? Team.Player : Team.Enemy;

            newUnit.Collider2D = newUnit.GameObject.GetComponent<Collider2D>() as Collider2D;
            newUnit.RigidBody2D = newUnit.GameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;

            newUnit.ShootPosition = (GameObject)newUnit.GameObject.transform.Find("ShootPosition").gameObject;
            newUnit.Weapon = new Weapon
            {
                BaseAttack = unit.Weapon.BaseAttack,
                CritAttack = unit.Weapon.CritAttack,
                FireSpeed = unit.Weapon.FireSpeed,
                ReloadSpeed = unit.Weapon.ReloadSpeed,
            };

            if (newUnit.UnitType == UnitType.Player)
            {
                newUnit.Weapon.BaseAttack *= Constants.baseAttackModifier;
                newUnit.Weapon.CritAttack *= Constants.critAttackModifier;
                newUnit.Weapon.FireSpeed /= Constants.fireSpeedModifier;
                newUnit.Weapon.ReloadSpeed /= Constants.reloadSpeedModifier;
            }

            newUnit.Health = unit.Health;
            newUnit.MoveSpeed = unit.MoveSpeed;
            return newUnit;
        }
        public Vector3 CalculateUnitSpawnPosition(IDiapasonSpawnPosition diapasonSpawnPosition, Vector3 currentCellPos)
        {
            int x = Random.Range(diapasonSpawnPosition.minXPos, diapasonSpawnPosition.maxXPos);
            int y = Random.Range(diapasonSpawnPosition.minYPos, diapasonSpawnPosition.maxYPos);

            Vector3 spawnPosition = new Vector3(currentCellPos.x + x, currentCellPos.y + y, diapasonSpawnPosition.ZPos);
            return spawnPosition;
        }

        public void SetBehaviour(IUnit unit, Vector3 spawnPos)
        {
            switch (unit.UnitType)
            {
                case UnitType.Player:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                        };
                        break;
                    }
                case UnitType.EnemyType1:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                        };
                        break;
                    }
                case UnitType.EnemyType2:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-3.2f, 0, 0),
                            MaxRightPos = new Vector3(3.2f, 0, 0),
                            Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0),
                        };
                        break;
                    }
            }
        }

        public IBullet CreateBullet(GameObject bulletPrefab)
        {
            IBullet newBullet = new Bullet
            {
                BulletGameObject = GameObject.Instantiate(_prefabs[BulletType.BulletType1.ToString()])
            };
            newBullet.BulletCollider2D = newBullet.BulletGameObject.GetComponent<Collider2D>() as Collider2D;
            newBullet.BulletRigidBody2D = newBullet.BulletGameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
            return newBullet;
        }
    }
}




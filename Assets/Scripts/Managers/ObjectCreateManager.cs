﻿using System.Collections;
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
                WeaponType = unit.Weapon.WeaponType,
                BulletType = unit.Weapon.BulletType
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
            newUnit.InactiveTime = unit.InactiveTime;
            newUnit.BonusType = unit.BonusType;
            
            return newUnit;
        }
        public Vector3 CalculateUnitSpawnPosition(IDiapasonSpawnPosition diapasonSpawnPosition, Vector3 currentCellPos)
        {
            float x = Random.Range(diapasonSpawnPosition.minXPos, diapasonSpawnPosition.maxXPos);
            float y = Random.Range(diapasonSpawnPosition.minYPos, diapasonSpawnPosition.maxYPos);

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
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0),
                            InactiveTime = 0
                        };
                        break;
                    }
                case UnitType.EnemyType1:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            InactiveTime = 0
                        };
                        break;
                    }
                case UnitType.EnemyType2:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0),
                            IsMoving = false,
                            InactiveTime = 0
                        };
                        break;
                    }
                case UnitType.EnemyType3:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0),
                            IsMoving = false,
                            InactiveTime = 0
                        };
                        break;
                    }
                case UnitType.EnemyType4:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            InactiveTime = 0
                        };
                        break;
                    }
                case UnitType.EnemyType5:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            InactiveTime = 0
                        };
                        break;
                    }
                case UnitType.EnemyType6:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            InactiveTime = 0
                        };
                        break;
                    }
                case UnitType.EnemyType7:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            InactiveTime = -1
                        };
                        break;
                    }
                case UnitType.EnemyType8:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            InactiveTime = -1
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

        public IBonus CreateBonus(IBonus bonusTemplate)
        {

            IBonus newBonus = new Bonus
            {
                BonusGameObject = GameObject.Instantiate(_prefabs[bonusTemplate.BonusType.ToString()]),
                Alias = bonusTemplate.Alias,
                BonusType = bonusTemplate.BonusType,
                RandomValue = bonusTemplate.RandomValue,
                HealthValue = bonusTemplate.HealthValue,
                FireSpeedCoefficient = bonusTemplate.FireSpeedCoefficient,
                ReloadSpeedCoefficient = bonusTemplate.ReloadSpeedCoefficient,
                ActiveTime = bonusTemplate.ActiveTime
            };
            newBonus.BonusCollider2D = newBonus.BonusGameObject.GetComponent<Collider2D>() as Collider2D;
            return newBonus;
        }
    }
}




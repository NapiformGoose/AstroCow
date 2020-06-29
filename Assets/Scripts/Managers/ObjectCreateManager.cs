using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;

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
            IObstacle newObstacle = new Obstacle();

            newObstacle.GameObject.transform.position = currentCellPos + obstacle.SpawnPosition;
            return newObstacle;
        }

        public IUnit CreateUnit(IUnit unit)
        {
            IUnit newUnit = new Unit();

            newUnit.UnitType = unit.UnitType;
            newUnit.Team = unit.UnitType == UnitType.Player ? Team.Player : Team.Enemy;

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
            newUnit.ExperienceValue = unit.ExperienceValue;
            return newUnit;
        }

        public IBullet CreateBullet(GameObject bulletPrefab)
        {
            IBullet newBullet = new Bullet();
            return newBullet;
        }

        public IBonus CreateBonus(IBonus bonusTemplate)
        {

            IBonus newBonus = new Bonus
            {
                GameObject = GameObject.Instantiate(_prefabs[bonusTemplate.BonusType.ToString()]),
                Alias = bonusTemplate.Alias,
                BonusType = bonusTemplate.BonusType,
                RandomValue = bonusTemplate.RandomValue,
                HealthValue = bonusTemplate.HealthValue,
                FireSpeedCoefficient = bonusTemplate.FireSpeedCoefficient,
                ReloadSpeedCoefficient = bonusTemplate.ReloadSpeedCoefficient,
                ActiveTime = bonusTemplate.ActiveTime
            };
            newBonus.Collider2D = newBonus.GameObject.GetComponent<Collider2D>() as Collider2D;
            return newBonus;
        }

        public void InstantiateUnit(IUnit unit, Transform transformCanvas)
        {
            if (unit.GameObject == null)
            {
                unit.GameObject = GameObject.Instantiate(_prefabs[unit.UnitType.ToString()], unit.Behaviour.StartPos, Quaternion.identity);
                unit.Collider2D = unit.GameObject.GetComponent<Collider2D>() as Collider2D;
                unit.RigidBody2D = unit.GameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
                unit.ShootPosition = (GameObject)unit.GameObject.transform.Find("ShootPosition").gameObject;

                var text = Resources.Load(Constants.prefabPath + "Text") as GameObject;
                unit.Text = GameObject.Instantiate<GameObject>(text);
            }

            unit.GameObject.transform.position = unit.Behaviour.StartPos;
            unit.Text.transform.position = unit.GameObject.transform.position + new Vector3(0.7f, 0.7f, 0);
            unit.Text.GetComponent<Text>().text = unit.Health.ToString();

            unit.Text.GetComponent<Text>().transform.SetParent(transformCanvas);
        }
        public void InstantiateObstacle(IObstacle obstacle)
        {
            obstacle.GameObject = GameObject.Instantiate(_prefabs[obstacle.ObstacleType.ToString()]);
            obstacle.Collider2D = obstacle.GameObject.GetComponent<Collider2D>() as Collider2D;
            obstacle.RigidBody2D = obstacle.GameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
            obstacle.GameObject.SetActive(false);
        }
        public void InstantiateBullet(IBullet bullet)
        {
            bullet.GameObject = GameObject.Instantiate(_prefabs[bullet.BulletType.ToString()]);
            bullet.Collider2D = bullet.GameObject.GetComponent<Collider2D>() as Collider2D;
            bullet.RigidBody2D = bullet.GameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
            bullet.GameObject.SetActive(false);
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
                case UnitType.EnemyType1:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            InactiveTime = 0,
                            CurrentHealth = unit.Health,
                            CurrentMoveSpeed = unit.MoveSpeed,
                            CurrentFireSpeed = unit.Weapon.FireSpeed,
                            CurrentReloadSpeed = unit.Weapon.ReloadSpeed,
                            CurrentCritAttack = unit.Weapon.CritAttack,
                            CurrentBaseAttack = unit.Weapon.BaseAttack
                        };
                        break;
                    }
                case UnitType.Player:
                case UnitType.EnemyType2:
                case UnitType.EnemyType3:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            Direction = new Vector3(unit.MoveSpeed * Time.fixedDeltaTime, 0, 0),
                            IsMoving = false,
                            InactiveTime = 0,
                            CurrentHealth = unit.Health,
                            CurrentMoveSpeed = unit.MoveSpeed,
                            CurrentFireSpeed = unit.Weapon.FireSpeed,
                            CurrentReloadSpeed = unit.Weapon.ReloadSpeed,
                            CurrentCritAttack = unit.Weapon.CritAttack,
                            CurrentBaseAttack = unit.Weapon.BaseAttack
                        };
                        break;
                    }
                case UnitType.EnemyType4:
                case UnitType.EnemyType5:
                case UnitType.EnemyType6:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            InactiveTime = 0,
                            CurrentHealth = unit.Health,
                            CurrentMoveSpeed = unit.MoveSpeed,
                            CurrentFireSpeed = unit.Weapon.FireSpeed,
                            CurrentReloadSpeed = unit.Weapon.ReloadSpeed,
                            CurrentCritAttack = unit.Weapon.CritAttack,
                            CurrentBaseAttack = unit.Weapon.BaseAttack
                        };
                        break;
                    }
                case UnitType.EnemyType7:
                case UnitType.EnemyType8:
                    {
                        unit.Behaviour = new Behaviour
                        {
                            StartPos = spawnPos,
                            MaxLeftPos = new Vector3(-2.7f, 0, 0),
                            MaxRightPos = new Vector3(2.7f, 0, 0),
                            IsMoving = false,
                            InactiveTime = -1,
                            CurrentHealth = unit.Health,
                            CurrentMoveSpeed = unit.MoveSpeed,
                            CurrentFireSpeed = unit.Weapon.FireSpeed,
                            CurrentReloadSpeed = unit.Weapon.ReloadSpeed,
                            CurrentCritAttack = unit.Weapon.CritAttack,
                            CurrentBaseAttack = unit.Weapon.BaseAttack
                        };
                        break;
                    }
            }
        }
    }
}




using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleBehaviour
{
    protected class ActiveObstacleTemplate
    {
        public float Value { get; set; }
        public float ActiveTime { get; set; }
        public EffectType EffectType { get; set; }
        public float Coefficient { get; set; }

        public ActiveObstacleTemplate(float value, float coefficient, float activeTime, EffectType effectType)
        {
            Value = value;
            ActiveTime = activeTime;
            EffectType = effectType;
            Coefficient = coefficient;
        }
    }
    IList<ActiveObstacleTemplate> ActiveObstacles;
    IObjectStorage _objectStorage;

    IUnit _player;

    public ObstacleBehaviour(IObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
        ActiveObstacles = new List<ActiveObstacleTemplate>();
    }

    public void ActiveObstacleAct()
    {
        for (int i = 0; i < ActiveObstacles.Count; i++)
        {
            ActiveObstacles[i].ActiveTime -= Time.fixedDeltaTime;
            if (ActiveObstacles[i].ActiveTime < 0)
            {
                switch (ActiveObstacles[i].EffectType)
                {
                    case EffectType.MoveSpeedReduce:
                        _player.Behaviour.CurrentMoveSpeed /= ActiveObstacles[i].Coefficient;
                        ActiveObstacles.RemoveAt(i);
                        break;
                    default:
                        return;

                }
            }
        }
    }

    public void ObstacleAct(IObstacle obstacle)
    {
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();

        switch (obstacle.ObstacleType)
        {
            case ObstacleType.WallType1:
                {
                    _player.Behaviour.CurrentHealth -= _player.Health * (obstacle.DamagePercent / 100);
                    break;
                }
            case ObstacleType.WallType2:
                {
                    _player.Behaviour.CurrentHealth -= _player.Health * (obstacle.DamagePercent / 100);
                    obstacle.DamagePercent = 0;
                    obstacle.RigidBody2D.simulated = false;
                    break;
                }
            case ObstacleType.WallType3:
                {
                    _player.Behaviour.CurrentHealth -= _player.Health * (obstacle.DamagePercent / 100);
                    obstacle.DamagePercent = 0;
                    break;
                }
            case ObstacleType.WallType4:
            case ObstacleType.WallType6:
                {
                    break;
                }
            case ObstacleType.WallType5:
                {
                    _player.Behaviour.CurrentHealth -= _player.Health * (obstacle.DamagePercent / 100);
                    obstacle.GameObject.SetActive(false);
                    break;
                }
            case ObstacleType.WallType7:
                {
                    _player.Behaviour.CurrentMoveSpeed *= 0.5f;
                    ActiveObstacles.Add(new ActiveObstacleTemplate(_player.Behaviour.CurrentMoveSpeed, 0.5f, 2, EffectType.MoveSpeedReduce));
                    obstacle.RigidBody2D.simulated = false;
                    break;
                }
        }
    }

    void PlayerMoving(IUnit unit)
    {
       
    }
}

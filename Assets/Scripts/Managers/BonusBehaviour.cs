using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusBehaviour 
{
    protected class ActiveBonusTemplate
    {
        public float Value { get; set; }
        public float ActiveTime { get; set; }
        public EffectType EffectType { get; set; }
        public float Coefficient { get; set; }

        public ActiveBonusTemplate(float value, float coefficient, float activeTime, EffectType effectType)
        {
            Value = value;
            ActiveTime = activeTime;
            EffectType = effectType;
            Coefficient = coefficient;
        }
    }

    IObjectStorage _objectStorage;
    IUnit _player;
    IList<ActiveBonusTemplate> ActiveBonus;

    public BonusBehaviour(IObjectStorage objectStorage)
	{
        _objectStorage = objectStorage;
        ActiveBonus = new List<ActiveBonusTemplate>();
    }

    public void BonusAct(IBonus bonus)
    {
        _player = _objectStorage.Units[UnitType.Player.ToString()].First();

        switch (bonus.BonusType)
        {
            case BonusType.BonusHealth:
                {
                    BonusHealthEffect(bonus);
                    break;
                }
            case BonusType.BonusBoom:
                {
                    BonusBoomEffect();
                    break;
                }
            case BonusType.BonusMagnet: // скоростреьность
                {
                    BonusMagnetEffect(bonus);
                    break;
                }
            case BonusType.СrystalEnemies:
                {
                    BonusСrystalEnemiesEffect();
                    break;
                }
        }
    }

    public void ActiveBonusAct()
    {
        for(int i = 0; i < ActiveBonus.Count; i++)
        {
            ActiveBonus[i].ActiveTime -= Time.fixedDeltaTime;
            if(ActiveBonus[i].ActiveTime < 0)
            {
                switch(ActiveBonus[i].EffectType)
                {
                    case EffectType.FireSpeedUp:
                        _player.Weapon.FireSpeed /= ActiveBonus[i].Coefficient;
                        ActiveBonus.RemoveAt(i);
                        break;
                    default:
                        return;
                            
                }
            }
        }
    }

    void BonusHealthEffect(IBonus bonus)
    {
        _player.Health += bonus.HealthValue;
    }

    void BonusBoomEffect()
    {
        foreach (var key in _objectStorage.Units.Keys)
        {
            foreach (IUnit unit in _objectStorage.Units[key])
            {
                if(unit.Behaviour.IsActive && unit.Team == Team.Enemy)
                {
                    unit.GameObject.SetActive(false);
                    unit.Text.SetActive(false);
                }
            }
        }
    }

    void BonusMagnetEffect(IBonus bonus)
    {
        _player.Weapon.FireSpeed *= bonus.FireSpeedCoefficient;
        ActiveBonus.Add(new ActiveBonusTemplate(_player.Weapon.FireSpeed, bonus.FireSpeedCoefficient, bonus.ActiveTime, EffectType.FireSpeedUp));
    }

    void BonusСrystalEnemiesEffect()
    {
        foreach (var key in _objectStorage.Units.Keys)
        {
            foreach (IUnit unit in _objectStorage.Units[key])
            {
                if (unit.Behaviour.IsActive && unit.Team == Team.Enemy)
                {
                    unit.Health = 0.0001f;
                }
            }
        }
    }
}

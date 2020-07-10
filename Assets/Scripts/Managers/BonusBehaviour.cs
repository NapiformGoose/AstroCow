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
    IList<ActiveBonusTemplate> ActiveBonuses;

    public BonusBehaviour(IObjectStorage objectStorage)
	{
        _objectStorage = objectStorage;
        ActiveBonuses = new List<ActiveBonusTemplate>();
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
            case BonusType.BonusFastShoot:
                {
                    BonusFastShootEffect(bonus);
                    break;
                }
            case BonusType.СrystalEnemies:
                {
                    BonusСrystalEnemiesEffect();
                    break;
                }
            case BonusType.BonusMachine:
                {
                    MachineEffect();
                    break;
                }
        }
    }

    public void ActiveBonusAct()
    {
        for(int i = 0; i < ActiveBonuses.Count; i++)
        {
            ActiveBonuses[i].ActiveTime -= Time.fixedDeltaTime;
            if(ActiveBonuses[i].ActiveTime < 0)
            {
                switch(ActiveBonuses[i].EffectType)
                {
                    case EffectType.FireSpeedUp:
                        _player.Behaviour.CurrentFireSpeed /= ActiveBonuses[i].Coefficient;
                        ActiveBonuses.RemoveAt(i);
                        break;
                    default:
                        return;
                            
                }
            }
        }
    }

    void BonusHealthEffect(IBonus bonus)
    {
        _player.Behaviour.CurrentHealth += bonus.HealthValue;
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

    void BonusFastShootEffect(IBonus bonus)
    {
        _player.Behaviour.CurrentFireSpeed *= bonus.FireSpeedCoefficient;
        ActiveBonuses.Add(new ActiveBonusTemplate(_player.Behaviour.CurrentFireSpeed, bonus.FireSpeedCoefficient, bonus.ActiveTime, EffectType.FireSpeedUp));
    }

    void BonusСrystalEnemiesEffect()
    {
        foreach (var key in _objectStorage.Units.Keys)
        {
            foreach (IUnit unit in _objectStorage.Units[key])
            {
                if (unit.Behaviour.IsActive && unit.Team == Team.Enemy)
                {
                    unit.Behaviour.CurrentHealth = 0.0001f;
                }
            }
        }
    }

    void MachineEffect()
    {
        _player.Behaviour.IsMachineAvailable = true;
    }
}

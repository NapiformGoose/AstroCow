﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : IBehaviour
{
    public Vector3 StartPos { get; set; }
    public Vector3 NextPos { get; set; }
    public Vector3 MaxLeftPos { get; set; }
    public Vector3 MaxRightPos { get; set; }
    public Vector3 MaxTopPos { get; set; }
    public Vector3 MaxDownPos { get; set; }
    public Vector3 Direction { get; set; }

    public float CurrentExperience { get; set; }
    public float CurrentHealth { get; set; }
    public float CurrentMoveSpeed { get; set; }

    public float CurrentFireSpeed { get; set; }
    public float CurrentReloadSpeed { get; set; }
    public float CurrentCritAttack { get; set; }
    public float CurrentBaseAttack { get; set; }
    public int CurrentResurrectionValue { get; set; }
    public float CurrentLootPercent { get; set; }
    public int CurrentCoinValue { get; set; }
    public int CurrentBulletValue { get; set; }
    public float Bloodthirstiness { get; set; }
    public bool IsMoving { get; set; }
    public bool IsAttack { get; set; }
    public bool IsActive { get; set; }
    public bool IsMachineAvailable { get; set; }

    public float TimeBeforeShot { get; set; }
    public float TimeBeforeReload { get; set; }
    public float InactiveTime { get; set; }

    public float[] PhaseTimes { get; set; }
    public int CurrentPhase { get; set; }

    public Behaviour(Vector3 startPos = new Vector3(),
                     Vector3 nextPos = new Vector3(),
                     Vector3 maxLeftPos = new Vector3(),
                     Vector3 maxRightPos = new Vector3(),
                     Vector3 maxTopPos = new Vector3(),
                     Vector3 maxDownPos = new Vector3(),
                     Vector3 direction = new Vector3(),

                     float currentExperience = 0,
                     float currentHealth = 0,
                     float currentMoveSpeed = 0,
                     float currentFireSpeed = 0,
                     float currentReloadSpeed = 0,
                     float currentCritAttack = 0,
                     float currentBaseAttack = 0,
                     int currentResurrectionValue = 0,
                     float currentLootPercent = 0,
                     int currentCoinValue = 0,
                     int currentBulletValue = 0,
                     float bloodthirstiness = 0,

                     bool isMoving = true,
                     bool isAttack = true,
                     bool isActive = false,
                     bool isMachineAvailable = false,

                     float timeBeforeShot = 0,
                     float timeBeforeReload = 0,
                     float inactiveTime = 0,

                     int currentPhase = 0)
    {
        StartPos = startPos;
        NextPos = nextPos;
        MaxLeftPos = maxLeftPos;
        MaxRightPos = maxRightPos;
        MaxTopPos = maxTopPos;
        MaxDownPos = maxDownPos;
        Direction = direction;

        CurrentExperience = currentExperience;
        CurrentHealth = currentHealth;
        CurrentMoveSpeed = currentMoveSpeed;
        CurrentFireSpeed = currentFireSpeed;
        CurrentReloadSpeed = currentReloadSpeed;
        CurrentCritAttack = currentCritAttack;
        CurrentBaseAttack = currentBaseAttack;
        CurrentResurrectionValue = currentResurrectionValue;
        CurrentLootPercent = currentLootPercent;
        CurrentCoinValue = currentCoinValue;
        CurrentBulletValue = currentBulletValue;
        Bloodthirstiness = bloodthirstiness;

        IsMoving = isMoving;
        IsAttack = isAttack;
        IsActive = isActive;
        IsMachineAvailable = isMachineAvailable;

        TimeBeforeShot = timeBeforeShot;
        TimeBeforeReload = timeBeforeReload;
        InactiveTime = inactiveTime;

        CurrentPhase = currentPhase;
    }

}

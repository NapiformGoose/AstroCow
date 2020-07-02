using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviour
{
    Vector3 StartPos { get; set; }
    Vector3 NextPos { get; set; }
    Vector3 MaxLeftPos { get; set; }
    Vector3 MaxRightPos { get; set; }
    Vector3 MaxTopPos { get; set; }
    Vector3 MaxDownPos { get; set; }
    Vector3 Direction { get; set; }

    float CurrentExperience { get; set; }
    float CurrentHealth { get; set; }
    float CurrentMoveSpeed { get; set; }

    float CurrentFireSpeed { get; set; }
    float CurrentReloadSpeed { get; set; }
    float CurrentCritAttack { get; set; }
    float CurrentBaseAttack { get; set; }
    int CurrentResurrectionValue { get; set; }
    float CurrentLootPercent { get; set; }
    int CurrentCoinValue { get; set; }
    int CurrentBulletValue { get; set; }
    float Bloodthirstiness { get; set; }

    bool IsMoving { get; set; }
    bool IsAttack { get; set; }
    bool IsActive { get; set; }

    float TimeBeforeShot { get; set; }
    float TimeBeforeReload { get; set; }
    float InactiveTime { get; set; }
}

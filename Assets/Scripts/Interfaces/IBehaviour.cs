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
    bool IsMoving { get; set; }
    bool IsAttack { get; set; }
    Vector3 Direction { get; set; }
    bool IsActive { get; set; }

    float TimeBeforeShot { get; set; }
    float InactiveTime { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : IBehaviour
{
    public Vector3 StartPos { get; set; }
    public Vector3 NextPos { get; set; }
    public Vector3 MaxLeftPos { get; set; }
    public Vector3 MaxRightPos { get; set; }
    public bool IsMoving { get; set; }
    public bool IsAttack { get; set; }
    public Vector3 Direction { get; set; }

    public Behaviour(Vector3 startPos = new Vector3(),
                     Vector3 nextPos = new Vector3(),
                     Vector3 maxLeftPos = new Vector3(),
                     Vector3 maxRightPos = new Vector3(),
                     bool isMoving = true,
                     bool isAttack = true,
                     Vector3 direction = new Vector3())
    {
        StartPos = startPos;
        NextPos = nextPos;
        MaxLeftPos = maxLeftPos;
        MaxRightPos = maxRightPos;
        IsMoving = isMoving;
        IsAttack = isAttack;
        Direction = direction;
    }

}

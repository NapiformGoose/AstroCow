using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoin
{
    GameObject GameObject { get; set; }
    Collider2D Collider2D { get; set; }
    Rigidbody2D RigidBody2D { get; set; }
}

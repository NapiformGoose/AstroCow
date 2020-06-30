using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ICoin
{
    public GameObject GameObject { get; set; }
    public Collider2D Collider2D { get; set; }
    public Rigidbody2D RigidBody2D { get; set; }
}

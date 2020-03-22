using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Player : IUnit
    {
        public string Name { get; set; }
        public GameObject UnitGameObject { get; set; }
        public float Speed { get; set; }
        //public Rigidbody2D PlayerRigidbody2D { get; set; }
        public Collider2D PlayerCollider2D { get; set; }
    }
}

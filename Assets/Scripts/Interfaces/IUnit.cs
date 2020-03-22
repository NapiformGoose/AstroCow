using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    string Name { get; set; } //or Alias
    GameObject UnitGameObject { get; set; }
    float Speed { get; set; }

}

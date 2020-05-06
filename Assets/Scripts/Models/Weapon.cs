using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
	public string Alias { get; set; }
	public WeaponType WeaponType { get; set; }
	public float FireSpeed { get; set; }
	public float ReloadSpeed { get; set; }
	public float CritAttack { get; set; }
	public float BaseAttack { get; set; }
	public BulletType BulletType { get; set; }
}

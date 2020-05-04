using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
	public string Alias { get; set; }
	public WeaponType WeaponType { get; set; }
	public int FireSpeed { get; set; }
	public int ReloadSpeed { get; set; }
	public int CritAttack { get; set; }
	public int BaseAttack { get; set; }
	public BulletType BulletType { get; set; }
}

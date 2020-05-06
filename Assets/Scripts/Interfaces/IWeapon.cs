using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
	string Alias { get; set; }
	WeaponType WeaponType { get; set; }
	float FireSpeed { get; set; }
	float ReloadSpeed { get; set; }
	float CritAttack { get; set; }
	float BaseAttack { get; set; }
	BulletType BulletType { get; set; }
}

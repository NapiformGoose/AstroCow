using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
	string Alias { get; set; }
	WeaponType WeaponType { get; set; }
	int FireSpeed { get; set; }
	int ReloadSpeed { get; set; }
	int CritAttack { get; set; }
	int BaseAttack { get; set; }
	BulletType BulletType { get; set; }
}

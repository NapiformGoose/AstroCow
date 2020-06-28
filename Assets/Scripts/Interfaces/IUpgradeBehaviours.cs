using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeBehaviours
{
    IList<IUpgrade> GetUpgrades();
    void UpgradeAct(UpgradeType upgradeType);
}

﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourManager
{
    void ApplyUpgrade(UpgradeType upgradeType);
    IList<IUpgrade> GetUpgrades();
}

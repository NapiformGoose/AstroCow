using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : IUpgrade
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string IconName { get; set; }
    public UpgradeType UpgradeType { get; set; }
}

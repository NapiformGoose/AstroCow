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

    public Upgrade(string title, string description, string iconName, UpgradeType upgradeType)
    {
        Title = title;
        Description = description;
        IconName = iconName;
        UpgradeType = upgradeType;
    }
}

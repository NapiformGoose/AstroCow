using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrade
{
    string Title { get; set; }
    string Description { get; set; }
    string IconName { get; set; }
    UpgradeType UpgradeType { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataLoadManager
{
    void ReadConfig();
    void LoadPrefabs();
    void CreateUpgrades();
}

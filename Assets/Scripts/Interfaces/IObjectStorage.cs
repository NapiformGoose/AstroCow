using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

namespace Assets.Scripts.Interfaces
{
    public interface IObjectStorage
    {
        Player Player { get; set; }
        //int Score { get; set; }
        GameObject Controller { get; set; }
        IList<Cell> Cells { get; set; }
        Collider2D LowerTrigger { get; set; }
        void Initialization(string playerName);
    }
}

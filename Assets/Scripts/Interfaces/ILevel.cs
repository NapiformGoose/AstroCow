using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevel
{
    int Id { get; set; }
    int Stage { get; set; }
    int CellSet { get; set; }
}

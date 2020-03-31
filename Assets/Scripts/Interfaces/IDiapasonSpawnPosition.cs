using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDiapasonSpawnPosition
{
    int minXPos { get; set; }
    int maxXPos { get; set; }
    int minYPos { get; set; }
    int maxYPos { get; set; }
    int ZPos { get; set; }
}

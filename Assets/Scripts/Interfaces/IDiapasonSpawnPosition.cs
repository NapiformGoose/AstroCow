using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDiapasonSpawnPosition
{
    float minXPos { get; set; }
    float maxXPos { get; set; }
    float minYPos { get; set; }
    float maxYPos { get; set; }
    float ZPos { get; set; }
}

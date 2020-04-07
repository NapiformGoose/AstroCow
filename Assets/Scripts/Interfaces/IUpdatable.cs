﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IUpdatable
    {
        void CustomUpdate();
        void CustomFixedUpdate();
    }
}

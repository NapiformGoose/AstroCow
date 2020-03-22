using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IUpdateManager
    {
        void AddUpdatable(IUpdatable updatable);
        void RemoveUpdatable(IUpdatable updatable);
        void CustomStart();
        void Stop();
    }
}

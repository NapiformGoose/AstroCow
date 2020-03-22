using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class UpdateManager : MonoBehaviour, IUpdateManager
{
    readonly IList<IUpdatable> _customUpdatables = new List<IUpdatable>();
    bool IsOpenUpdate = false;

    public void AddUpdatable(IUpdatable updatable)
    {
        _customUpdatables.Add(updatable);
    }

    public void RemoveUpdatable(IUpdatable updatable)
    {
        _customUpdatables.Remove(updatable);
    }

    public void Update()
    {
        if (IsOpenUpdate == false)
            return;
        for (int i = 0; i < _customUpdatables.Count; i++)
        {
            _customUpdatables[i].CustomUpdate();
        }
    }

    public void CustomStart()
    {
        IsOpenUpdate = true;
        Time.timeScale = 1;
    }

    public void Stop()
    {
        IsOpenUpdate = false;
        Time.timeScale = 0;
    }
}

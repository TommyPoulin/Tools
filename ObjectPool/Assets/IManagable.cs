using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IManagable
{
    void Refresh();
    void FixedRefresh();
    bool IsAlive();
}

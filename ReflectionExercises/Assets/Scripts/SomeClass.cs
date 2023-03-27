using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SomeClass : MonoBehaviour, IPoolable
{
    [Resetable(4)] public int age;
    [Resetable(33)] public int IQ;
    [Resetable(2)] public int legs;

    public void Free() {
        throw new System.NotImplementedException();
    }

    public void New() {
        throw new System.NotImplementedException();
    }

    private void Start() {
        
    }
}

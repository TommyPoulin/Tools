using System;
using UnityEngine;

public class ResetableAttribute : PropertyAttribute
{
    int resetValue;

    public ResetableAttribute(int resetValue) {
        this.resetValue = resetValue;
    }
}

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class DropdownStringAttribute : PropertyAttribute
{
    public string[] options;

    public DropdownStringAttribute(params string[] options) {
        this.options = options;
    }
}
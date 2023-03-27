using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class GameOverCaller : MonoBehaviour
{
    Dictionary<int, MethodInComponent> methodInfos = new Dictionary<int, MethodInComponent>();
    private void Awake() {
        var objects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject o in objects) {
            foreach (var component in o.GetComponents(typeof(Component))) {
                foreach (MethodInfo method in component.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) {
                    var attributes = method.GetCustomAttributes(typeof(CallOnGameOverAttribute), true);
                    if (attributes.Length > 0) {
                        methodInfos.Add(methodInfos.Count, new MethodInComponent(component, method));
                    }
                }
            }
        }
    }

    public void GameOver() {
        Debug.Log("Game over called.");
        for (int i = 0; i < methodInfos.Count; i++) {
            methodInfos[i].methodInfo.Invoke(methodInfos[i].component, null);
        }
    }

    private class MethodInComponent
    {
        public Component component;
        public MethodInfo methodInfo;

        public MethodInComponent(Component component, MethodInfo methodInfo) {
            this.component = component;
            this.methodInfo = methodInfo;
        }
    }
}

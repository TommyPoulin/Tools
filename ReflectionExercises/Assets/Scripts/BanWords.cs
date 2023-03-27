using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class BanWords : MonoBehaviour
{
    BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    string[] banWords = { "Dog", "dog", "Loser", "loser", "Dumb", "dumb"};

    Type[] allTypes;

    private void Start() {
        allTypes = Assembly.GetExecutingAssembly().GetTypes();
        CheckArrayForBanWords<Type>(allTypes, "");
        foreach (var type in allTypes) {
            MethodInfo[] allMethods = type.GetMethods(bindingFlags);
            PropertyInfo[] allProperties = type.GetProperties(bindingFlags);
            FieldInfo[] allFields = type.GetFields(bindingFlags);

            CheckArrayForBanWords<MethodInfo>(allMethods, type.Name, "Method");
            CheckArrayForBanWords<PropertyInfo>(allProperties, type.Name, "Property");
            CheckArrayForBanWords<FieldInfo>(allFields, type.Name, "Field");
        }
    }

    void CheckArrayForBanWords<T>(T[] arr, string className, string type = "Class") where T : MemberInfo {
        foreach (var item in arr) {
            if (CheckItemForBanWords(item))
                if (!type.Equals("Class"))
                    Debug.LogWarning(type + " " + item.Name + " in the class " + className + " contains a ban word inside of it. Please rename it.");
                else
                    Debug.LogWarning(type + " " + item.Name + " contains a ban word inside of it. Please rename it.");
        }
    }

    bool CheckItemForBanWords<T>(T item) where T : MemberInfo {
        for (int i = 0; i < banWords.Length; i++) {
            if (item.Name.Equals(banWords[i]))
                return true;
        }
        return false;
    }

    [CallOnGameOver]
    void GameOver() {
        Debug.Log("Ban words is game over!");
    }
}

class Dog
{
    int age;
    int iq;
    bool dumb;

    public Dog(int age, int iq, bool dumb) {
        this.age = age;
        this.iq = iq;
        this.dumb = dumb;
    }

    bool isDumb() {
        return dumb;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    Dictionary<string, MethodInfo> myDict = new Dictionary<string, MethodInfo>();
    private void Start() {

        // Creation of dogs and binding flags
        BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;

        Dog jimDog = new Dog("Jim", 3, 23, false);
        Dog whiskerCat = new Dog("Bill", 8, 1000, true);



        // Get type
        Type dogType = typeof(Dog);
        dogType = jimDog.GetType();
        dogType = Type.GetType("Dog");



        FieldInfo dogNameFieldInfo = dogType.GetField("dogAge", bindingFlags);
        int dogAge = (int)dogNameFieldInfo.GetValue(jimDog);
        Debug.Log(dogAge);
        OutputField(jimDog, "dogName");



        MethodInfo methodInfo = dogType.GetMethod("SetDogIQ", bindingFlags);
        methodInfo.Invoke(jimDog, new object[] { 1999});
        ParameterInfo[] paramInfo = methodInfo.GetParameters();

        PropertyInfo pi = typeof(Dog).GetProperty("isADog", bindingFlags);
    }

    public void SomeFunction() {
        FieldInfo[] allFields = typeof(Dog).GetFields();
        foreach(var fi in allFields) {
            FindMeAttribute findMeAttr = fi.GetCustomAttribute<FindMeAttribute>();
            if (findMeAttr != null) {
                string secretFieldInside = findMeAttr.someCustomData;
            }
        }
    }

    public static void HowManyFloats() {
        int numberOfFloatTypes = 0;
        Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var t in allTypes) {
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var fi in finfos) {
                if (fi.FieldType == typeof(float)) {
                    Debug.Log("Float found: " + t + ", fieldname: " + fi.Name);
                    numberOfFloatTypes++;
                }
            }
        }
        Debug.Log(numberOfFloatTypes + " float types found!");
    }

    public static void OutputField(object target, string fieldName) {
        Type type = target.GetType();
        BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;

        FieldInfo fieldInfo = type.GetField(fieldName, bindingFlags);
        Debug.Log(fieldInfo.GetValue(target).ToString());
    }

    public static T GetValueInField<T>(object target, string fieldName) {
        BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
        Type type = target.GetType();
        return (T)type.GetField(fieldName, bindingFlags).GetValue(target);
    }
}

[FindMe("Doggo", 4)]
public class Dog
{
    public string dogName;
    int dogAge;
    [SerializeField] float IQ;
    [HideInInspector] bool isSecretlyACat;
    public bool isADog => !isSecretlyACat;

    public Dog(string dogName, int dogAge, float IQ, bool isSecretlyACat) {
        this.dogName = dogName;
        this.dogAge = dogAge;
        this.IQ = IQ;
        this.isSecretlyACat = isSecretlyACat;
    }

    public void OutputDogName() {
        Debug.Log("Name : " + this.dogName);
    }

    bool SecretCatFunction() {
        if (isSecretlyACat)
            Debug.Log("You found " + dogName + " secret cat function!");
        else
            Debug.Log("You found " + dogName + " secret cat function... but it's not a cat!");
        return isSecretlyACat;
    }

    void SetDogIQ(float newIQ) {
        this.IQ = newIQ;
        Debug.Log("New " + dogName + " IQ is " + newIQ);
    }

    public bool CheckDogName(string checkName) {
        bool goodName = dogName == checkName;
        if (goodName)
            Debug.Log(checkName + " is this dog's name");
        else
            Debug.Log(checkName + " is not this dog's name");
        return goodName;
    }
}

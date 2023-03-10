using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Awake() {
        Debug.Log(new Vector2(3, 10).RandomBetweenXAndY());

        GameObject go = new GameObject();
        go.AddComponent<Rigidbody>();
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(7, 7, 7);
        Debug.Log("Before: " + rb.velocity.magnitude);
        rb.SetSpeedThreshold(4);
        Debug.Log("After: " + rb.velocity.magnitude);

        string[] arr = { "aaa", "bbb", "ccc" };
        Debug.Log(arr.StringArrayToString("Letters"));

        int[] arr2 = { 1, 4, 65, 23, 17 };
        Debug.Log(arr2.ArrayToString("Int"));

        int[] arr3 = { 1, 4, 6, 3, 2 };
        Debug.Log(arr3.IsContaining(4));
        List<string> list = new List<string>();
        list.Add("aaa");
        list.Add("bbb");
        list.Add("ccc");
        Debug.Log(list.IsContaining("ddd"));

        int[] arr4 = { 1, 4, 6, 0, 2 };
        arr4.AreElementsTrue(arr4[0]);
        List<bool> list2 = new List<bool>();
        list2.Add(true);
        list2.Add(false);
        list2.Add(true);
        list2.Add(true);
        list2.Add(false);
        list2.AreElementsTrue(list2[0]);


    }
}

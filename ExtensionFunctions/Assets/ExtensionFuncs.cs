using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionFuncs              //static required
{
    //Student requirements
    /*
     * For these exercises. You are not allowed to use ChatGPT or use the ExtensionFunc sheet I gave you
     * My sheet contains the answers
     * ChatGPT will be able to answer these with no problem at all
     * Using stack overflow or other resources is allowed and heavily recommended
     * 
    
    
    
    Make an extension function that mimics how .Contains works  
    Make an extension function that runs a predicate on each element and returns if it is true for all elements
    Make an extension function that runs a delegate on a collection of type T, and returns a collection of type G
        Examples:
            Vector3[] velocitiesOfEachRb = arrayOfRigidbodiesIHave.CollectionFrom((rb)=>{ return rb.velocity;});
            Vector3[] positionsOfGameObjects = arrayOfGameObjects.CollectionFrom((go)=>{ return go.position;});
    */

    /*
     Examples shown in class
     An extension function that returns the average of a vector3
     An extension function that absolutes all values in an int array
     An extension function of rigidbody that given a float arguement, returns if speed is above that threshold
     Our own Contains function
     An extension function that runs a delegate on each element in an int array and replaces the int
     An extension function that extends type T array and randomizes the array

     */



    //Make an extension function on a Vector2 which returns a random value between x & y
    public static float RandomBetweenXAndY(this Vector2 v2) {
        return Random.Range(v2.x, v2.y);
    }

    //Make an extension function of rigidbody that given a float arguement, clamps speed at that threshold
    public static void SetSpeedThreshold(this Rigidbody rb, float max) {
        if (rb.velocity.magnitude > max) {
            rb.velocity = rb.velocity.normalized * max;
        }
    }

    //Make an extension function that given a string array & label, returns a single string formated like:
    //      label: elem1,elem2,elem3
    public static string StringArrayToString(this string[] arr, string label) {
        string output = label + ": ";
        foreach (string s in arr) {
            output = output + s + ",";
        }
        return output;
    }

    //Make an extension function same as above, but works on an array of any type
    public static string ArrayToString<T>(this T[] arr, string label) {
        string output = label + ": ";
        foreach (T elem in arr) {
            output = output + elem + ",";
        }
        return output;
    }

    //Make an extension function that mimics how .Contains works
    public static bool IsContaining<T,R>(this T collection, R itemToSearch) where T : ICollection {
        foreach (R item in collection)
            if (item.Equals(itemToSearch))
                return true;
        return false;
    }
}

using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[MyAttribute()]
public class Test : ScriptableObject
{
    [MenuItem("Duplicate objects test/Show object")]
    public static void Show()
    {
        Selection.activeObject = MySingleton.instance;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public sealed class MyAttribute : Attribute
{
    public MyAttribute()
    {
        // this creates a singleton object instance, but it's a broken instance
        // the m_Script field is never assigned in the native instance,
        // so Unity will keep trying to recreate it, over and over again
        var singleton = MySingleton.instance;
    }
}

public class MySingleton : ScriptableSingleton<MySingleton>
{
    protected MySingleton()
    {
        Debug.Log($"Running constructor {++Counter.counter} times");
    }

    // just a little static counter that doesn't get blown away by Unity recreating this singleton over and over again
    static class Counter
    {
        public static int counter;
    }

}


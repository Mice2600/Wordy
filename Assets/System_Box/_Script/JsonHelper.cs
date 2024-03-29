using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        if (string.IsNullOrEmpty(json)) return new T[0];
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        if(wrapper == null)return new T[0];
        if(wrapper.Items == null)return new T[0];
        return wrapper.Items.ToArray();
    }
    public static List<T> FromJsonList<T>(string json)
    {
        if (string.IsNullOrEmpty(json)) return new List<T>();
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(IEnumerable<T> array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = new List<T>(array);
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array.ToList();
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array.ToList();
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }

}
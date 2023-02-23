using Sirenix.OdinInspector;
using Study.aSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SystemBox;
using UnityEngine;

public class QuestRandom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, object> All = DictionaryFromType(ProjectSettings.ProjectSettings.Mine);
        List<string> Kays = new List<string>(All.Keys);
        TList<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < All.Count; i++)
            if (All[Kays[i]] != null && All[Kays[i]] is GameObject)
                if ((All[Kays[i]] as GameObject).TryGetComponent(out Quest D))
                    gameObjects.Add(All[Kays[i]] as GameObject);

        Quest quest = FindObjectOfType<Quest>();
        if (quest == null)
        {
            OnFinsh();
            return;
        }
        quest.OnFineshed += OnFinsh;
        void OnFinsh()
        {
            GameObject D = Instantiate(gameObjects.RandomItem);
            D.GetComponent<Quest>().OnFineshed += OnFinsh;
        }




    }
    public static Dictionary<string, object> DictionaryFromType(object atype)
    {
        if (atype == null) return new Dictionary<string, object>();
        Dictionary<string, object> dict = new Dictionary<string, object>();
        foreach (var prop in atype.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            object value = prop.GetValue(atype);
            dict.Add(prop.Name, value);
        }
        return dict;
    }
}

using Newtonsoft.Json.Linq;
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
    
    public static Dictionary<string, bool> Values 
    {
        get 
        {
            if (_Values == null)
            {
                _Values = new Dictionary<string, bool>();
                Dictionary<string, object> AllVV = DictionaryFromType(ProjectSettings.ProjectSettings.Mine);
                List<string> AllllKays = new List<string>(AllVV.Keys);
                TList<GameObject> AlllGGAm = new List<GameObject>();
                for (int i = 0; i < AllVV.Count; i++)
                    if (AllVV[AllllKays[i]] != null && AllVV[AllllKays[i]] is GameObject)
                        if ((AllVV[AllllKays[i]] as GameObject).TryGetComponent(out Quest D))
                            AlllGGAm.Add(AllVV[AllllKays[i]] as GameObject);

                for (int i = 0; i < AlllGGAm.Count; i++)
                    _Values.Add(AlllGGAm[i].GetComponent<Quest>().QuestName, true);
            }
            return _Values;

        }
        set => _Values = value;

    }
    public static Dictionary<string, bool> _Values;
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
            TList<GameObject> HH = (new TList<GameObject>(gameObjects)).Mix();

            for (int i = 0; i < HH.Count; i++)
            {
                if (Values[HH[i].GetComponent<Quest>().QuestName]) 
                {
                    Instantiate(HH[i]).GetComponent<Quest>().OnFineshed += OnFinsh; 
                    return;
                }
            }

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
    public GameObject FilterPrefab;
    public void OnOpenFillter() => Instantiate(FilterPrefab);
}

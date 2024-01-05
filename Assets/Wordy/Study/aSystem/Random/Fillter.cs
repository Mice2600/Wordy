using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fillter : MonoBehaviour
{
    public GameObject SceneTogelPrefab;
    public Transform TargetParrent;

    public Dictionary<string, bool> Values => QuestRandom.Values;

    private void Start()
    {
        TargetParrent.ClearChilds();
        new List<string>(Values.Keys).ForEach((a) => CreatOne(a, Values[a]));
    }

    public void CreatOne(string Name, bool Default) 
    {
        GameObject G = Instantiate(SceneTogelPrefab, TargetParrent);
        G.GetComponent<Toggle>().isOn = Default;
        G.GetComponent<Toggle>().onValueChanged.AddListener((a) => { OnValueChanged(Name, a); });
        G.GetComponentInChildren<TextMeshProUGUI>().text = Name;
    }
    void OnValueChanged(string Name, bool Value) 
    {
        Values[Name] = Value;
    }
    public void DestroyURSelf() => Destroy(gameObject);
}

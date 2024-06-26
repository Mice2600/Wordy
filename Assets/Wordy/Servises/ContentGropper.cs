using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using SystemBox;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine.UIElements;

public class ContentGropper : MonoBehaviour
{
    public Vector2 Spacing;
    [Range(.2f,1f)]
    public float ScreenSize = 1;
    private void Start()
    {
        gameObject.AddComponent<VerticalLayoutGroup>();
        GetComponent<VerticalLayoutGroup>().childControlHeight = false;
        GetComponent<VerticalLayoutGroup>().childControlWidth = false;
        GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
        GetComponent<VerticalLayoutGroup>().spacing = Spacing.y;
        //gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
    }
    private TList<RectTransform> Lines;

    public bool MaualUpdate;
    private float Pertime;
    public void Update()
    {
        if (MaualUpdate) return;
        Pertime += Time.deltaTime;
        if (Pertime > .2f) 
        {
            FixPos();
            Pertime = 0;
        }
    }
    public void FixPos() 
    {
        if (Lines == null) Lines = new TList<RectTransform>();
        //ortiqchalani chiqarvolish
        TList<Transform> Others = new List<Transform>();
        for (int i = 0; i < Lines.Count; i++)
        {
            Transform Parrent = Lines[i];
            Others.Add(Parrent.Childs());
            while (Parrent.childCount > 0)
            {
                TList<Transform> ds = Parrent.Childs();
                for (int s = 0; s < ds.Count; s++)ds[s].SetParent(null);
            }
        }
        Others.ForEach(AddNewContent);
    }





    private Transform ConcleateLast(float NextSize) 
    {
        if (Lines == null) Lines = new List<RectTransform>();

        for (int i = 0; i < Lines.Count; i++)
        {
            if (Lines[i].childCount > 0) continue;
            Lines[i].SetParent(null);
            Destroy(Lines[i].gameObject);
            Lines.RemoveAt(i);
            i--;
        }

        if (Lines.Count == 0) 
        {
            GameObject N = AddNewList();
            Lines.Add( N.GetComponent<RectTransform>());
        }
        
        Transform Founded = null;
        for (int i = 0; i < Lines.Count; i++)
        {
            float NSize = GetSize(Lines[i]);
            if (NSize + NextSize + Lines[i].GetComponent<HorizontalLayoutGroup>().spacing > (Screen.width * ScreenSize)) continue;
            Founded = Lines[i];
            break;
        }
        if (Founded == null)
        {
            Lines.Add(AddNewList().GetComponent<RectTransform>());
            Founded = Lines.Last;
        }
        return Founded;

        float GetSize(Transform Parrent) 
        {
            List<Transform> childes =  Parrent.Childs();
            float Ofset = Parrent.GetComponent<HorizontalLayoutGroup>().spacing;
            float size = 0;
            for (int i = 0; i < childes.Count; i++)
            {
                size += childes[i].GetComponent<RectTransform>().rect.width;
                if(i + 1 < childes.Count) size += Ofset;
            }
            return size;
        }

        GameObject AddNewList()
        {
            GameObject NLists = new GameObject("NewList", typeof(RectTransform), typeof(HorizontalLayoutGroup), typeof(ContentSizeFitter));
            NLists.transform.SetParent(transform);
            NLists.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.MinSize;
            NLists.GetComponent<HorizontalLayoutGroup>().childControlHeight = false;
            NLists.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            NLists.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            NLists.GetComponent<HorizontalLayoutGroup>().spacing = Spacing.x;
            return NLists;
        }
    }
    [Button]
    public void AddNewContent(Transform NObject) 
    {
        if (NObject == null) return;
        NObject.transform.SetParent(ConcleateLast(NObject.GetComponent<RectTransform>().rect.width));
    }
    
}

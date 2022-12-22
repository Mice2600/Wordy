using Servises;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Unity.Mathematics;
using UnityEngine;

namespace Base
{
    public abstract class DataList<T> : List<T> where T : IContent, new ()
    {
        public DataList()
        {

            T[] DaaaTaa = new List<T>(JsonHelper.FromJson<T>(PlayerPrefs.GetString(DataID))).ToArray();
            if (DaaaTaa.Length > 1) Array.Sort(DaaaTaa);
            for (int i = 0; i < DaaaTaa.Length; i++)
            {
                T dialog = DaaaTaa[i];
                if (string.IsNullOrEmpty(dialog.EnglishSource)) continue;
                base.Add(dialog);
            }
            if (!Application.isPlaying) return;
            GameObject s = GameObject.Find("Save " + DataID + " Engine Dont Touch");
            if (s == null)
            {
                s = new GameObject("Save " + DataID + " Engine Dont Touch", typeof(ListBaseEngine));
                GameObject.DontDestroyOnLoad(s);
            }
            s.GetComponent<ListBaseEngine>().OnSaveTime = Save;
        }

        protected abstract string DataID { get; }

        public void Save()
        {
            PlayerPrefs.SetString(DataID, JsonHelper.ToJson<T>((this as List<T>).ToArray()));
        }

        public new void Add(T Content)
        {
            if (string.IsNullOrEmpty(Content.EnglishSource)) return;
            if (Contains(Content)) return;
            base.Add(Content);
        }

        public List<T> GetContnetList(int ListCount)
        {
            TList<T> All = new List<T>(this);
            if (All.Count <= ListCount) return All.Mix();
            List<T> NewList = new List<T>();

            // int ListCount

            //5% 90-100    -5
            //10% 70-90    -15
            //30% 50-70    -45
            //40% 35-50    -85
            //10% 10-35    -95
            //5% 0-10      -10
            List<T> L90_100 = new List<T>();
            List<T> L70_90 = new List<T>();
            List<T> L50_70 = new List<T>();
            List<T> L35_50 = new List<T>();
            List<T> L10_35 = new List<T>();
            List<T> L0_10 = new List<T>();
            for (int i = 0; i < All.Count; i++)
            {
                float Score = All[i].Score;
                if (Score > 90) L90_100.Add(All[i]);
                else if (Score > 70 && Score <= 90) L70_90.Add(All[i]);
                else if (Score > 50 && Score <= 70) L50_70.Add(All[i]);
                else if (Score > 35 && Score <= 50) L35_50.Add(All[i]);
                else if (Score > 10 && Score <= 35) L10_35.Add(All[i]);
                else L0_10.Add(All[i]);
            }


            TList<T> rezultat = new List<T>();
            int need_count = ListCount;
            int[] pratcents = new int[] { 5, 10, 40, 30, 10, 5 };
            TList<T>[] lists = new TList<T>[] { L0_10, L10_35, L35_50, L50_70, L70_90, L90_100 };
            for (int LL = 0; LL < pratcents.Length; LL++)
            {
                for (int LL2 = 0; LL2 < pratcents[LL] * need_count / 100; LL2++)
                {
                    if (lists[LL].Count == 0) continue;
                    int RandomOne = UnityEngine.Random.Range(0, lists[LL].Count);
                    rezultat.Add(lists[LL][RandomOne]);
                    lists[LL].RemoveAt(RandomOne);
                }
            }

            int Breacer = 0;
            while (rezultat.Count < need_count)
            {
                int d = UnityEngine.Random.Range(0, lists.Length);
                Breacer++;
                if (Breacer > 800) break;
                if (lists[d].Count < 1) continue;
                rezultat.Add(lists[d].RemoveRandomItem());
            }


            return rezultat;
        }
        public T GetContnet() => this[UnityEngine.Random.Range(0, Count)];
        private IEnumerator FindContentsFromStringCoroutine(string Todiagnist, System.Action<T> Founded) 
        {
            int TCount = 0;
            for (int i = 0; i < Count; i++)
            {
                if (Todiagnist.Contains(this[i].EnglishSource, StringComparison.OrdinalIgnoreCase)) Founded?.Invoke(this[i]);
                if (TCount > 100) { yield return null; TCount = 0; }
            }
        }
        public void FindContentsFromString(string ToDiagnost, System.Action<T> OnFound) 
        {
            GameObject s = GameObject.Find("Save " + DataID + " Engine Dont Touch");
            s.GetComponent<ListBaseEngine>().StartCoroutine(FindContentsFromStringCoroutine(ToDiagnost, OnFound));
        }
    }
    public class ListBaseEngine : MonoBehaviour
    {
        public System.Action OnSaveTime;
        private void OnLevelWasLoaded(int level) => OnSaveTime?.Invoke();
        private void OnApplicationQuit() => OnSaveTime?.Invoke();
    }
}
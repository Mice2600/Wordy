using Base.Word;
using Servises;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using Unity.Mathematics;
using UnityEngine;

namespace Base
{
    public interface IDataListComands 
    {
        static IDataListComands() 
        {
            DataLists = new List<IDataListComands>();
        }
        public static List<IDataListComands> DataLists;
        public void SetUp_Commands() => DataLists.Add(this);
        public void Add(Content Content);
        public void Save();
        public int IndexOf(Content Content);
        public void Remove(Content Content);
        public bool Contains(Content Content);
        public bool Contains(string Content);
        public Content GetContent(int Index);
        public Content GetContent(string Index);
        public void SetContent(int Index, Content content);
        public void FindContentsFromString(string ToDiagnost, System.Action<Content> OnFound);
    }
    public abstract class DataList<T> : List<T>, IDataListComands where T : Content
    {
        public DataList()
        {
            if (JsonHelper.FromJson<T>(PlayerPrefs.GetString(DataID)) == null) return;
            T[] DaaaTaa = new List<T>(JsonHelper.FromJson<T>(PlayerPrefs.GetString(DataID))).ToArray();
            if (DaaaTaa.Length > 1) Array.Sort(DaaaTaa);
            for (int i = 0; i < DaaaTaa.Length; i++)
            {
                T dialog = DaaaTaa[i];
                if (string.IsNullOrEmpty(dialog.EnglishSource)) continue;
                base.Add(dialog);
            }

            if (Application.isPlaying) 
            {
                GameObject s = GameObject.Find("Save " + DataID + " Engine Dont Touch");
                if (s == null)
                {
                    s = new GameObject("Save " + DataID + " Engine Dont Touch");
                    s.AddComponent<ListBaseEngine>();
                    GameObject.DontDestroyOnLoad(s);
                }
                s.GetComponent<ListBaseEngine>().OnSaveTime = Save;
            }

            (this as IDataListComands).SetUp_Commands();
        }

        public void Avake() { }

        protected abstract string DataID { get; }
        public void Save()
        {
            for (int i = 0; i < Count; i++)if (this[i].ScoreConculeated < 0) this[i].ScoreConculeated = 0;
            PlayerPrefs.SetString(DataID, JsonHelper.ToJson<T>((this as List<T>).ToArray()));
        }

        public void Add(Content Content)
        {
            if (string.IsNullOrEmpty(Content.EnglishSource)) return;
            if (Contains(Content as T)) return;
            base.Add(Content as T);
        }
        public Content GetContent(int Index) => this[Index];
        public Content GetContent(string Index) => this[IndexOf(tryCreat(Index))];
        public void SetContent(int Index, Content content) => this[Index] = content as T;

        public void Remove(Content Content)  => base.Remove(Content as T);
        public int IndexOf(Content Content) => base.IndexOf(Content as T);
        public bool Contains(Content Content) => base.Contains(Content as T);
        public bool Contains(string Content) => base.Contains(tryCreat(Content));
        public abstract T tryCreat(string Id);
        public List<T> ActiveItems => new List<T>(this.Where(a => a.Active));
        public List<T> PassiveItems => new List<T>(this.Where(a => !a.Active));
        public List<T> GetContnetList(int ListCount)
        {
            TList<T> All = new List<T>(ActiveItems);
            if (All.Count < ListCount) 
            {
                TList<T> PassiveItems = this.PassiveItems;
                for (int i = All.Count; i < ListCount; i++)All.Add(PassiveItems.RemoveRandomItem());
                return All.Mix();
            }
            if (All.Count == ListCount) return All.Mix();
            if (true) 
            {

                int sasas = 0;
                List<T> dsa = new List<T>();
                while (dsa.Count < ListCount)
                {
                    int d = UnityEngine.Random.Range(0, All.Count);
                    sasas++;
                    if (sasas > 800) break;
                    if (All.Count < 1) continue;
                    dsa.Add(All.RemoveRandomItem());
                }

                return dsa;
            
            }
            /*
             List<T> L90_100 = new List<T>();
            List<T> L70_90 = new List<T>();
            List<T> L50_70 = new List<T>();
            List<T> L35_50 = new List<T>();
            List<T> L10_35 = new List<T>();
            List<T> L0_10 = new List<T>();
            for (int i = 0; i < All.Count; i++)
            {
                float Score = All[i].ScoreConculeated;
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
             */
            // int ListCount

            //5% 90-100    -5
            //10% 70-90    -15
            //30% 50-70    -45
            //40% 35-50    -85
            //10% 10-35    -95
            //5% 0-10      -10

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
        public void FindContentsFromString(string ToDiagnost, System.Action<Content> OnFound) 
        {
            GameObject s = GameObject.Find("Save " + DataID + " Engine Dont Touch");
            s.GetComponent<ListBaseEngine>().StartCoroutine(FindContentsFromStringCoroutine(ToDiagnost, OnFound));
        }
    }
    public class ListBaseEngine : MonoBehaviour
    {
        public System.Action OnSaveTime;
        private void Start()
        {
            StartCoroutine(enumerator());
            IEnumerator enumerator() 
            {
                while (true) 
                {
                    yield return new WaitForSeconds(10);
                    OnSaveTime.Invoke();
                }
            }

        }
        private void OnApplicationQuit() => OnSaveTime?.Invoke();
    }
}
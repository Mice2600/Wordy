using Servises;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Base
{
    public abstract class DataList<T> : List<T> where T : IContent, new()
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
    }
    public class ListBaseEngine : MonoBehaviour
    {
        public System.Action OnSaveTime;
        private void OnLevelWasLoaded(int level) => OnSaveTime?.Invoke();
        private void OnApplicationQuit() => OnSaveTime?.Invoke();
    }
}
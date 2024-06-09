using Base.Word;
using Sirenix.OdinInspector;
using Study.aSystem;
using Study.Crossword;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Study.aSystem 
{
    public class WinViwe : MonoBehaviour
    {

        public static void Creat(TList<Content> contents) 
        {
            Instantiate(ProjectSettings.ProjectSettings.Mine.StandartWinUI).
                GetComponent<WinViwe>().contents = contents;
        }

        [System.NonSerialized]
        public List<Content> contents;
        private Quest Quest => _Quest ??= GameObject.FindFirstObjectByType<Quest>();
        private Quest _Quest;
        public GameObject SingelScorePrefab;
        void Start()
        {

            Quest d = GameObject.FindFirstObjectByType<Quest>();
            contents.ForEach(ss => { if (ss is Word) d.OnWordWin?.Invoke(ss as Word); });
            d.OnGameWin?.Invoke();
            ContentGropper LLD = GetComponentInChildren<ContentGropper>();
            
            contents.ForEach(a =>
            {
                GameObject G = Instantiate(SingelScorePrefab);
                G.GetComponent<ScoreChanginInfo>().Set(a, (Quest.QuestData as IWordScorer).AddScoreWord);
                LLD.AddNewContent(G.transform);
            });
            LLD.MaualUpdate = true;
            StartCoroutine(dd());
            IEnumerator dd() 
            {
                LLD.FixPos();
                yield return new WaitForSeconds(1);
                LLD.FixPos();
            }


        }
        public void DestroyUrself()
        {
            Destroy(gameObject);
            Quest.OnFineshed.Invoke();
        }

    }

}
namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        [Required]
        public GameObject StandartWinUI;

    }
}


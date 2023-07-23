using Base.Word;
using Study.aSystem;
using Study.Crossword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Study.CoupleParticles
{
    public class WinViwe : MonoBehaviour
    {
        private Quest Quest => _Quest ??= FindObjectOfType<Quest>();
        private Quest _Quest;
        public GameObject SingelScorePrefab;
        void Start()
        {
            List<Content> contents = new List<Content>(FindObjectOfType<CoupleParticles>().CompleatedContents);
            ContentGropper LLD = GetComponentInChildren<ContentGropper>();
            contents.ForEach(a =>
            {
                GameObject G = Instantiate(SingelScorePrefab);
                G.GetComponent<ScoreChanginInfo>().Set(a, Quest.AddScoreWord);
                LLD.AddNewContent(G.transform);
            });
        }
        public void DestroyUrself()
        {
            Destroy(gameObject);
            Quest.OnFineshed.Invoke();
        }

    }
}
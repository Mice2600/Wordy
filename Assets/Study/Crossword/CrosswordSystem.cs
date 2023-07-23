using Base.Word;
using Sirenix.OdinInspector;
using Study.TwoWordSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Study.Crossword
{
    public class CrosswordSystem : MonoBehaviour
    {

        public GameObject WinPrefab;


        private QuestCrossword QuestCrosswor => _QuestCrosswor ??= GetComponent<QuestCrossword>();
        private QuestCrossword _QuestCrosswor;
        private Builder BuilderP => _Builder ??= GetComponentInChildren<Builder>();
        private Builder _Builder;
        bool isdone;
        public void OnWin()
        {
            if (isdone) return;
            isdone = true;

            List<Content> contents = new List<Content>();
            Builder.ToBuild.AllContentIDes.ForEach(a => QuestCrosswor.OnWordWin.Invoke(WordBase.Wordgs.GetContent(a) as Word));
            Instantiate(WinPrefab);
            QuestCrosswor.OnGameWin?.Invoke();

        }


    }
}
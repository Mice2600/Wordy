using Base;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Study.aSystem;
using Study.Crossword;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEditor;
using UnityEngine;
namespace Study.CoupleParticles
{
    public class CoupleParticles : MonoBehaviour
    {


        public System.Action<Content> OncontentFound;

        [Required]
        public GameObject SapatetedLatterPrefab;
        [Required]
        public Transform SapatetedLatterParrent;


        [Required]
        public GameObject ContentPrefab;

        [Required]
        public ContentGropper ContentParrent;

        [Required, SerializeField]
        private GameObject Win;

        public string CollectedString
        {
            get => _CollectedString;
            set
            {
                _CollectedString = value;


                Content C = contents.Find((a) => a.EnglishSource == _CollectedString);
                if (C != null && !CompleatedContents.Contains(C))
                {
                    _CollectedString = "";
                    CompleatedContents.Add(C);
                    OncontentFound.Invoke(C);

                    if (CompleatedContents.Count == contents.Count)
                    {
                        StartCoroutine(WW());
                        IEnumerator WW() 
                        {
                            yield return new WaitForSeconds(2f);
                            Instantiate(Win).GetComponent<WinViwe>().contents = CompleatedContents;
                            QuestCoupleParticles d = GetComponent<QuestCoupleParticles>();
                            CompleatedContents.ForEach(ss => d.OnWordWin?.Invoke(ss as Word));
                            d.OnGameWin?.Invoke();
                        }
                    }

                }
                


            }
        }
        private string _CollectedString;


        [System.NonSerialized]
        public List<Content> contents;
        public List<Content> CompleatedContents;

        private void Start()
        {

            List<Content> contents = new List<Content>(GetComponent<QuestCoupleParticles>().NeedWords.Mix());
            List<string> strings = contents.Select(x => x.EnglishSource).ToList();
            List<string> Latters = SeparateAllCantent(strings, out List<string> UsedContents).Mix();
            contents = new List<Content>();
            UsedContents.ForEach(c => contents.Add(WordBase.Wordgs[new Word(c, "", 0, false, "", "")]));

            SapatetedLatterParrent.ClearChilds();
            Latters.ForEach(o =>
            {
                Instantiate(SapatetedLatterPrefab, SapatetedLatterParrent).GetComponentInChildren<TextMeshProUGUI>().text = o;
            });
            ContentParrent.transform.ClearChilds();
            contents.ForEach(
                C =>
                {
                    GameObject d = Instantiate(ContentPrefab, transform);
                    d.GetComponentInChildren<ContentObject>().Content = C;
                    ContentParrent.AddNewContent(d.transform);
                }
                );
            this.contents = contents;
            CompleatedContents = new List<Content>();
        }
        public TList<string> SeparateAllCantent(List<string> Words, out List<string> ComplatedContents)
        {

            TList<string> Latters = new TList<string>();
            TList<string> UsedWord = new TList<string>();
            Words.ForEach(S =>
            {
                TList<string> NLi = new List<string>();
                SeparateWord(S).ForEach(a => NLi.AddIfDirty(a));
                if (Latters.Count + NLi.Count < 21)
                {
                    Latters.AddRange(NLi);
                    UsedWord.Add(S);
                }
            });
            ComplatedContents = new TList<string>(UsedWord);
            return Latters;
        }


        [Button]
        public TList<string> SeparateWord(string Word)
        {

            if (Word.Length == 3) return new TList<string>(Word);
            if (Word.Length == 4) return new TList<string>() { Word[0] + "" + Word[1], Word[2] + "" + Word[3] };
            if (Word.Length == 5) return new TList<string>() { Word[0] + "" + Word[1] + "" + Word[2], Word[3] + "" + Word[4] };


            List<string> LastOne = new List<string>();

            if ((Word.Length % 3) == 1)
            {
                LastOne.Add(Word[Word.Length - 4] + "" + Word[Word.Length - 3]);
                LastOne.Add(Word[Word.Length - 2] + "" + Word[Word.Length - 1]);
                Word = Word.Remove(Word.Length - 4, 4);
            }
            else if ((Word.Length % 3) == 2)
            {
                LastOne.Add(Word[Word.Length - 2] + "" + Word[Word.Length - 1]);
                Word = Word.Remove(Word.Length - 2, 2);
            }



            for (int i = 0; i < 100; i++)
            {
                if (Word.Length == 0) break;
                LastOne.Add(Word[0] + "" + Word[1] + "" + Word[2]);
                Word = Word.Remove(0, 3);
            }
            return LastOne;


        }


        public bool FindMyContent(string Letter, out Color contentColor)
        {
            contentColor = Color.white;
            List<Content> CC = contents.FindAll((a) => a.EnglishSource.Contains(Letter, StringComparison.OrdinalIgnoreCase)).Where((a) => !CompleatedContents.Contains(a)).ToList();
            if (CC == null || CC.Count == 0) return false;
            contentColor = GetColor(WordBase.Wordgs.IndexOf(CC[0]));
            return true;
            Color GetColor(int index)
            {
                index = Mathf.Abs(index);
                int levv = (index / 30);
                index -= (levv * 30);
                Color color = ProjectSettings.ProjectSettings.Mine.ContentLoopColors.Evaluate((float)index / 30f);
                return color;
            }
        }


    }
}
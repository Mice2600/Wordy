using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemBox;
using Base;
using Base.Word;
using Servises;
using Unity.Collections.LowLevel.Unsafe;
using Base.Dialog;

namespace Study.TwoWordSystem
{
    public class TwoWordSystem : MonoBehaviour
    {
        
        public Transform EnglishContentParrent;
        public Transform RussianContentParrent;
        private TList<ContentObject> EnglishContents;
        private TList<ContentObject> RussianContents;

        [SerializeField]
        private TList<Word> Words;
        public Gradient HelpColor;

        [Sirenix.OdinInspector.Button]
        public void Start()
        {
            //Words = WordBase.Wordgs;
            Words = WordBase.Wordgs.GetContnetList(20);
            ScoresResultat = new Dictionary<Word, int>();
            for (int i = 0; i < Words.Count; i++) ScoresResultat.Add(Words[i], 0);

            List<Transform> RuChilds = RussianContentParrent.Childs();
            RussianContents = new TList<ContentObject>();
            RuChilds.ForEach(a => RussianContents.Add(a.GetComponent<ContentObject>()));
            RuChilds = EnglishContentParrent.Childs();
            EnglishContents = new TList<ContentObject>();
            RuChilds.ForEach(a => EnglishContents.Add(a.GetComponent<ContentObject>()));

            TList<ContentObject> _EnglishContents = EnglishContents.Mix(true);
            TList<ContentObject> _RussianContents = RussianContents.Mix(true);

            for (int i = 0; i < _EnglishContents.Count; i++)
            {
                Word N = Words.RemoveRandomItem();
                _EnglishContents[i].Content = N;
                (_EnglishContents[i] as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((_EnglishContents[i].Content as Word?).Value)));
                _RussianContents[i].Content = N;
                (_RussianContents[i] as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((_RussianContents[i].Content as Word?).Value)));
            }
        }
        [Sirenix.OdinInspector.Button]
        public void TryChange(ContentObject EnglishOnes, ContentObject RussiaOnes)
        {
            string OldOne = EnglishOnes.Content.EnglishSource;
            ScoresResultat[(EnglishOnes.Content as Word?).Value] += 1;
            ScoresResultat[(RussiaOnes.Content as Word?).Value] += 1;

            TList<Word> EnglishWords = new TList<Word>();
            for (int i = 0; i < EnglishContents.Count; i++)
            {
                if (EnglishContents[i] == EnglishOnes) continue;
                if ((EnglishContents[i] as TwoWordSystemContent).Dead) continue;
                if (string.IsNullOrEmpty(EnglishContents[i].Content.EnglishSource)) continue;
                EnglishWords.Add((EnglishContents[i].Content as Word?).Value);
            }
            TList<Word> RussianWords = new TList<Word>();
            for (int i = 0; i < RussianContents.Count; i++)
            {
                if (RussianContents[i] == RussiaOnes) continue;
                if ((RussianContents[i] as TwoWordSystemContent).Dead) continue;
                if (string.IsNullOrEmpty(RussianContents[i].Content.EnglishSource)) continue;
                RussianWords.Add((RussianContents[i].Content as Word?).Value);
            }

            int IsThereSomeOneTrue = 0;

            for (int i = 0; i < EnglishWords.Count; i++)
            {
                if (RussianWords.Contains(EnglishWords[i]))
                {
                    IsThereSomeOneTrue++;
                }
            }
            if (Words.Count > 0 && (IsThereSomeOneTrue > 4 || (Random.Range(0, 100) > 50 && IsThereSomeOneTrue > 3))) TryRandom();
            else TryFind();

            void TryRandom()
            {

                Word NWord = new Word();

                for (int i = 0; i < 20; i++)
                {
                    NWord = Words.RandomItem;
                    if (!EnglishWords.Contains(NWord)) break;
                }
                Words.Remove(NWord);
                EnglishOnes.Content = NWord;
                (EnglishOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((EnglishOnes.Content as Word?).Value)));

                NWord = new Word();
                for (int i = 0; i < 20; i++)
                {
                    NWord = Words.RandomItem;
                    if (!RussianWords.Contains(NWord)) break;
                }
                Words.Remove(NWord);
                RussiaOnes.Content = NWord;
                (RussiaOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((RussiaOnes.Content as Word?).Value)));
            }

            void TryFind()
            {

                TList<Word> AllRu = RussianWords.Mix(true);
                TList<Word> AllEN = EnglishWords.Mix(true);
                for (int i = 0; i < AllRu.Count; i++)
                {
                    if (AllEN.Contains(AllRu[i])) continue;
                    EnglishOnes.Content = AllRu[i];
                    (EnglishOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((EnglishOnes.Content as Word?).Value)));
                    break;
                }
                for (int i = 0; i < AllEN.Count; i++)
                {
                    if (AllRu.Contains(AllEN[i])) continue;
                    RussiaOnes.Content = AllEN[i];
                    (RussiaOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((RussiaOnes.Content as Word?).Value)));
                    break;
                }

                
            }
            if (OldOne == EnglishOnes.Content.EnglishSource) 
            {
                if (!WinWindow.activeSelf) 
                {
                    bool IsThereActiveOnes = false;


                    for (int i = 0; i < EnglishContents.Count; i++)
                    {
                        if (!(EnglishContents[i] as TwoWordSystemContent).Dead) 
                        {
                            IsThereActiveOnes = true;
                            break;
                        }
                    }

                    if(!IsThereActiveOnes)ShowWinWindow();
                }
                
            }
        }
        public void WrongChose(IContent EnglishOnes, IContent RussiaOnes) 
        {
            ScoresResultat[(EnglishOnes as Word?).Value] -= 1;
            ScoresResultat[(RussiaOnes as Word?).Value] -= 1;
        }

        public GameObject WinWindow;
        public GameObject SingelScorePrefab;
        public ContentGropper WinGropeParrent;
        public ContentGropper LostGropeParrent;
        public Dictionary<Word, int> ScoresResultat;

        public void ShowWinWindow() 
        {
            WinWindow.SetActive(true);
            List<Word> words = new List<Word>(ScoresResultat.Keys);
            bool IsThereWinn = false;
            bool IsThereLost = false;
            for (int i = 0; i < words.Count; i++)
            {
                GameObject G = Instantiate(SingelScorePrefab);
                if (ScoresResultat[words[i]] > 1) 
                {
                    G.GetComponent<ScoreChanginInfo>().Set(words[i], 5);
                    WinGropeParrent.AddNewContent(G.transform);
                    IsThereWinn = true; 
                }
                else 
                {
                    G.GetComponent<ScoreChanginInfo>().Set(words[i], -5);
                    LostGropeParrent.AddNewContent(G.transform);
                    IsThereLost = true;
                }
            }
            if (!IsThereWinn) 
            {
                
                GameObject G = Instantiate(SingelScorePrefab);
                G.GetComponent<ScoreChanginInfo>().Set(new Word("Nothing :(", "",0,"",""), -99);
                WinGropeParrent.AddNewContent(G.transform);
            }
            if (!IsThereLost) 
            {
                GameObject G = Instantiate(SingelScorePrefab);
                G.GetComponent<ScoreChanginInfo>().Set(new Word("wow soch empty", "", 0, "", ""), 5);
                LostGropeParrent.AddNewContent(G.transform);
            }
        }


        public Color GetColor(int index)
        {
            index = Mathf.Abs(index);
            int levv = (index / 10);
            index -= (levv * 10);
            return HelpColor.Evaluate((float)index / 10f);
        }
    }
}
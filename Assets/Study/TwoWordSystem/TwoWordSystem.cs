using Base;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using SystemBox;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Study.TwoWordSystem
{
    public class TwoWordSystem : MonoBehaviour
    {
        [Required]
        public QuestTwoWord QuestTwoWord;

        public Transform EnglishParrent;


        [SerializeField]
        private TList<Word> Words;
        public Gradient HelpColor;

        public GameObject ContntPrefab;

        [Sirenix.OdinInspector.Button]
        public void Start()
        {
            EnglishParrent.ClearChilds();
            int CC = Random.Range(4, 9);
            Debug.Log(CC);
            for (int i = 0; i < CC; i++)
            {
                Instantiate(ContntPrefab, EnglishParrent);
                Instantiate(ContntPrefab, EnglishParrent);
            }

            //Words = WordBase.Wordgs;
            Words = QuestTwoWord.NeedWords;
            ScoresResultat = new Dictionary<Word, int>();
            for (int i = 0; i < Words.Count; i++) ScoresResultat.Add(Words[i], 0);





            TList<TwoWordSystemContent> EnglishContents = new TList<TwoWordSystemContent>();
            TList<TwoWordSystemContent> RussianContents = new TList<TwoWordSystemContent>();
            TList<TwoWordSystemContent> AllOthers = EnglishParrent.GetComponentsInChildren<TwoWordSystemContent>();

            AllOthers.Mix();
            for (int i = 0; i < AllOthers.Count; i++)
            {
                if (i < AllOthers.Count / 2) EnglishContents.Add(AllOthers[i]);
                else RussianContents.Add(AllOthers[i]);
            }

            EnglishContents.ForEach(a => a.IsEnglishSide = true);
            RussianContents.ForEach(a => a.IsEnglishSide = false);
            TList<TwoWordSystemContent> _EnglishContents = EnglishContents.Mix(true);
            TList<TwoWordSystemContent> _RussianContents = RussianContents.Mix(true);

            for (int i = 0; i < _EnglishContents.Count; i++)
            {
                Word N = Words.RemoveRandomItem();
                _EnglishContents[i].Content = N;
                _RussianContents[i].Content = N;
            }
        }
        [Sirenix.OdinInspector.Button]
        public void TryChange(TwoWordSystemContent EnglishOnes, TwoWordSystemContent RussiaOnes)
        {
            string OldOne = EnglishOnes.Content.EnglishSource;
            ScoresResultat[EnglishOnes.Content as Word] += 1;
            ScoresResultat[RussiaOnes.Content as Word] += 1;


            TList<TwoWordSystemContent> EnglishContents = new TList<TwoWordSystemContent>();
            TList<TwoWordSystemContent> RussianContents = new TList<TwoWordSystemContent>();

            TList<TwoWordSystemContent> AllOthers = EnglishParrent.GetComponentsInChildren<TwoWordSystemContent>();

            AllOthers.Remove(EnglishOnes);
            AllOthers.Remove(RussiaOnes);

            AllOthers.ForEach((a) => { EnglishContents.AddIf(a, a.IsEnglishSide); RussianContents.AddIf(a, !a.IsEnglishSide); });


            TList<Word> EnglishWords = new TList<Word>();
            for (int i = 0; i < EnglishContents.Count; i++)
            {
                if (EnglishContents[i] == EnglishOnes) continue;
                if ((EnglishContents[i] as TwoWordSystemContent).Dead) continue;
                if (string.IsNullOrEmpty(EnglishContents[i].Content.EnglishSource)) continue;
                EnglishWords.Add(EnglishContents[i].Content as Word);
            }
            TList<Word> RussianWords = new TList<Word>();
            for (int i = 0; i < RussianContents.Count; i++)
            {
                if (RussianContents[i] == RussiaOnes) continue;
                if ((RussianContents[i] as TwoWordSystemContent).Dead) continue;
                if (string.IsNullOrEmpty(RussianContents[i].Content.EnglishSource)) continue;
                RussianWords.Add(RussianContents[i].Content as Word);
            }

            int IsThereSomeOneTrue = 0;

            for (int i = 0; i < EnglishWords.Count; i++)
            {
                if (RussianWords.Contains(EnglishWords[i]))
                {
                    IsThereSomeOneTrue++;
                }
            }

            if (Random.Range(0, 10) > 5) 
            {
                TwoWordSystemContent d = EnglishOnes;
                EnglishOnes = RussiaOnes;
                RussiaOnes = d;
            }
            EnglishOnes.IsEnglishSide = true;
            RussiaOnes.IsEnglishSide = false;

            if (Words.Count > 0 && (IsThereSomeOneTrue > 4 || (Random.Range(0, 100) > 50 && IsThereSomeOneTrue > 3))) TryRandom();
            else TryFind();

            void TryRandom()
            {
                Word NWord = new Word("", "", 0, false, "", "");

                for (int i = 0; i < 20; i++)
                {
                    NWord = Words.RandomItem;
                    if (!EnglishWords.Contains(NWord)) break;
                }
                Words.Remove(NWord);
                EnglishOnes.Content = NWord;
                if (Words.Count == 0) Words.Add(NWord);//prosta keyin remov boladi

                NWord = new Word("", "", 0, false, "", "");
                for (int i = 0; i < 20; i++)
                {
                    NWord = Words.RandomItem;
                    if (!RussianWords.Contains(NWord)) break;
                }
                Words.Remove(NWord);
                RussiaOnes.Content = NWord;
            }

            void TryFind()
            {
                TList<Word> AllRu = RussianWords.Mix(true);
                TList<Word> AllEN = EnglishWords.Mix(true);
                for (int i = 0; i < AllRu.Count; i++)
                {
                    if (AllEN.Contains(AllRu[i])) continue;
                    EnglishOnes.Content = AllRu[i];
                    break;
                }
                for (int i = 0; i < AllEN.Count; i++)
                {
                    if (AllRu.Contains(AllEN[i])) continue;
                    RussiaOnes.Content = AllEN[i];
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

                    if (!IsThereActiveOnes) ShowWinWindow();
                }

            }
        }
        public void WrongChose(Content EnglishOnes, Content RussiaOnes)
        {
            ScoresResultat[EnglishOnes as Word] -= 1;
            ScoresResultat[RussiaOnes as Word] -= 1;
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
                    G.GetComponent<ScoreChanginInfo>().Set(words[i], QuestTwoWord.AddScoreWord);
                    WinGropeParrent.AddNewContent(G.transform);
                    IsThereWinn = true;
                    QuestTwoWord.OnWordWin.Invoke(words[i]);
                }
                else
                {
                    G.GetComponent<ScoreChanginInfo>().Set(words[i], QuestTwoWord.RemoveScoreWord);
                    LostGropeParrent.AddNewContent(G.transform);
                    IsThereLost = true;
                    QuestTwoWord.OnWordLost.Invoke(words[i]);
                }
            }
            if (!IsThereWinn)
            {

                GameObject G = Instantiate(SingelScorePrefab);
                G.GetComponent<ScoreChanginInfo>().Set(new Word("Nothing :(", "", 0, false, "", ""), -99);
                WinGropeParrent.AddNewContent(G.transform);
            }
            if (!IsThereLost)
            {
                GameObject G = Instantiate(SingelScorePrefab);
                G.GetComponent<ScoreChanginInfo>().Set(new Word("wow soch empty", "", 0, false, "", ""), 5);
                LostGropeParrent.AddNewContent(G.transform);
            }
            QuestTwoWord.OnGameWin?.Invoke();
        }
        public void OnFinsh()
        {
            QuestTwoWord.OnFineshed.Invoke();
        }

    }
}
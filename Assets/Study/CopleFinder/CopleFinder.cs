using Base.Word;
using Study.aSystem;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
namespace Study.CopleFinder
{
    public class CopleFinder : MonoBehaviour
    {

        public GameObject ContntPrefab;
        public Transform ContentParrent;
        public Gradient HelpColor;
        private protected virtual TList<Content> Contents { get => GetComponent<Quest>().NeedContentList; }
        private protected TList<Content> ContentsUse { get; set; }
        private TList<Content> ContentsUSED { get; set; }

        public void Start()
        {
            ContentsUse = Contents;
            ContentsUSED = new TList<Content>();
            ContentParrent.ClearChilds();
            int CC = Random.Range(2, 11);
            for (int i = 0; i < CC; i++)
            {
                Instantiate(ContntPrefab, ContentParrent);
                Instantiate(ContntPrefab, ContentParrent);
            }
            TList<CopleFinderContent> FirstContents = new TList<CopleFinderContent>();
            TList<CopleFinderContent> SecondContents = new TList<CopleFinderContent>();
            TList<CopleFinderContent> AllOthers = ContentParrent.GetComponentsInChildren<CopleFinderContent>();
            AllOthers.Mix();
            for (int i = 0; i < AllOthers.Count; i++)
            {
                if (i < AllOthers.Count / 2) FirstContents.Add(AllOthers[i]);
                else SecondContents.Add(AllOthers[i]);
            }

            FirstContents.ForEach(a => a.IsFirst = true);
            SecondContents.ForEach(a => a.IsFirst = false);
            TList<CopleFinderContent> _FirstContents = FirstContents.Mix(true);
            TList<CopleFinderContent> _SecondContents = SecondContents.Mix(true);

            for (int i = 0; i < _FirstContents.Count; i++)
            {
                Content content = ContentsUse.RemoveRandomItem();
                _FirstContents[i].Content = content;
                _SecondContents[i].Content = content;
            }
        }



        public void TryChange(CopleFinderContent FirstOnes, CopleFinderContent SecondOnes)
        {
            string OldOne = FirstOnes.Content.EnglishSource;

            ContentsUSED.Add(FirstOnes.Content);

            TList<CopleFinderContent> FirstContents = new TList<CopleFinderContent>();
            TList<CopleFinderContent> SecondContents = new TList<CopleFinderContent>();

            TList<CopleFinderContent> AllOthers = ContentParrent.GetComponentsInChildren<CopleFinderContent>();

            AllOthers.Remove(FirstOnes);
            AllOthers.Remove(SecondOnes);
            AllOthers.ForEach((a) => { FirstContents.AddIf(a, a.IsFirst); SecondContents.AddIf(a, !a.IsFirst); });


            TList<Content> FirstWords = new TList<Content>();
            for (int i = 0; i < FirstContents.Count; i++)
            {
                if (FirstContents[i] == FirstOnes) continue;
                if ((FirstContents[i]).Dead) continue;
                if (string.IsNullOrEmpty(FirstContents[i].Content.EnglishSource)) continue;
                FirstWords.Add(FirstContents[i].Content);
            }
            TList<Content> SecondWords = new TList<Content>();
            for (int i = 0; i < SecondContents.Count; i++)
            {
                if (SecondContents[i] == SecondOnes) continue;
                if ((SecondContents[i]).Dead) continue;
                if (string.IsNullOrEmpty(SecondContents[i].Content.EnglishSource)) continue;
                SecondWords.Add(SecondContents[i].Content);
            }

            

            if (Random.Range(0, 10) > 5)
            {
                CopleFinderContent d = FirstOnes;
                FirstOnes = SecondOnes;
                SecondOnes = d;
            }
            FirstOnes.IsFirst = true;
            SecondOnes.IsFirst = false;


            if (GiveNewContent(FirstWords, SecondWords, out Content _FirstOne, out Content _SecondOne))
            {
                FirstOnes.Content = _FirstOne;
                SecondOnes.Content = _SecondOne;
            }
            else 
            {
                FindInList(FirstWords, SecondWords, out Content _1FirstOne, out Content _1SecondOne);
                FirstOnes.Content = _1FirstOne;
                SecondOnes.Content = _1SecondOne;

            }

            
            if (OldOne == FirstOnes.Content.EnglishSource)
            {
                if (!Done)
                {
                    bool IsThereActiveOnes = false;
                    for (int i = 0; i < FirstContents.Count; i++)
                    {
                        if (!(FirstContents[i]).Dead)
                        {
                            IsThereActiveOnes = true;
                            break;
                        }
                    }
                    if (!IsThereActiveOnes) ShowWinWindow();
                }

            }
        }


        public virtual bool GiveNewContent(
            TList<Content> FirstWords, 
            TList<Content> SecondWords,
            out Content FirstOne, out Content SecondOne)
        {
            FirstOne = null;
            SecondOne = null;
            int IsThereSomeOneTrue = 0;

            for (int i = 0; i < FirstWords.Count; i++)
            {
                if (SecondWords.Contains(FirstWords[i]))
                {
                    IsThereSomeOneTrue++;
                }
            }
            if (ContentsUse.Count > 0 && (IsThereSomeOneTrue > 4 ||
                (Random.Range(0, 100) > 50 && IsThereSomeOneTrue > 3))) 
            {
                Content NWord = null;

                for (int i = 0; i < 20; i++)
                {
                    NWord = ContentsUse.RandomItem;
                    if (!FirstWords.Contains(NWord)) break;
                }
                ContentsUse.Remove(NWord);
                FirstOne = NWord;
                if (ContentsUse.Count == 0) ContentsUse.Add(NWord);//prosta keyin remov boladi

                NWord = null;
                for (int i = 0; i < 20; i++)
                {
                    NWord = ContentsUse.RandomItem;
                    if (!SecondWords.Contains(NWord)) break;
                }
                ContentsUse.Remove(NWord);
                SecondOne = NWord;
                return true;
            }else return false;
        }

        public virtual void FindInList(
            TList<Content> FirstWords,
            TList<Content> SecondWords,
            out Content FirstOne, out Content SecondOne) 
        {
            FirstOne = null;
            SecondOne = null;
            TList<Content> AllRu = SecondWords.Mix(true);
            TList<Content> AllEN = FirstWords.Mix(true);
            for (int i = 0; i < AllRu.Count; i++)
            {
                if (AllEN.Contains(AllRu[i])) continue;
                FirstOne = AllRu[i];
                break;
            }
            for (int i = 0; i < AllEN.Count; i++)
            {
                if (AllRu.Contains(AllEN[i])) continue;
                SecondOne = AllEN[i];
                break;
            }
        }

        public void WrongChose(Content FirstOnes, Content SecondOnes)
        {

        }
        bool Done = false;
        public void ShowWinWindow()
        {
            Done = true;
            aSystem.WinViwe.Creat(ContentsUSED);
        }
    }
}
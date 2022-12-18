using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemBox;
using Base;
using Base.Word;
using Servises;
namespace Study.TwoWordSystem
{
    public class TwoWordSystem : MonoBehaviour
    {
        public Transform EnglishContentParrent;
        public Transform RussianContentParrent;
        private TList<ContentObject> EnglishContents;
        private TList<ContentObject> RussianContents;


        private TList<Word> Words;
        public Gradient HelpColor;

        [Sirenix.OdinInspector.Button]
        public void Start()
        {
            Words = WordBase.Wordgs;

            List<Transform> RuChilds = RussianContentParrent.Childs();
            RussianContents = new TList<ContentObject>();
            RuChilds.ForEach(a => RussianContents.Add(a.GetComponent<ContentObject>()));
            RuChilds = EnglishContentParrent.Childs();
            EnglishContents = new TList<ContentObject>();
            RuChilds.ForEach(a => EnglishContents.Add(a.GetComponent<ContentObject>()));

            for (int i = 0; i < EnglishContents.Count; i++)
            {

                EnglishContents[i].Content = Words.RandomItem;
                (EnglishContents[i] as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((EnglishContents[i].Content as Word?).Value)));
                RussianContents[i].Content = Words.RandomItem;
                (RussianContents[i] as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((RussianContents[i].Content as Word?).Value)));
            }
            TryChange(EnglishContents.RandomItem, RussianContents.RandomItem);
            TryChange(EnglishContents.RandomItem, RussianContents.RandomItem);
            TryChange(EnglishContents.RandomItem, RussianContents.RandomItem);
        }
        [Sirenix.OdinInspector.Button]
        public void TryChange(ContentObject EnglishOnes, ContentObject RussiaOnes)
        {

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
            if (IsThereSomeOneTrue > 4 || (Random.Range(0, 100) > 50 && IsThereSomeOneTrue > 3)) TryRandom();
            else TryFind();

            void TryRandom()
            {

                Word NWord = new Word();

                for (int i = 0; i < 20; i++)
                {
                    NWord = Words.RandomItem;
                    if (!EnglishWords.Contains(NWord)) break;
                }
                EnglishOnes.Content = NWord;
                (EnglishOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((EnglishOnes.Content as Word?).Value)));

                NWord = new Word();
                for (int i = 0; i < 20; i++)
                {
                    NWord = Words.RandomItem;
                    if (!RussianWords.Contains(NWord)) break;
                }
                RussiaOnes.Content = NWord;
                (RussiaOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((RussiaOnes.Content as Word?).Value)));
            }

            void TryFind()
            {

                TList<Word> AllRu = RussianWords.Mix(true);
                TList<Word> AllEN = EnglishWords.Mix(true);
                Word FF = AllRu[0];
                for (int i = 0; i < AllRu.Count; i++)
                {
                    FF = AllRu[i];
                    if (AllEN.Contains(FF)) continue;
                    break;
                }
                EnglishOnes.Content = FF;
                (EnglishOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((EnglishOnes.Content as Word?).Value)));


                FF = AllEN[0];
                for (int i = 0; i < AllEN.Count; i++)
                {
                    FF = AllEN[i];
                    if (AllRu.Contains(FF)) continue;
                    break;
                }

                RussiaOnes.Content = FF;
                (RussiaOnes as MonoBehaviour).GetComponent<ColorChanger>().SetColor(GetColor(WordBase.Wordgs.IndexOf((RussiaOnes.Content as Word?).Value)));
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
using Base.Antonym;
using Base.Synonym;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
namespace Study.CopleFinder.AntonymMatch
{
    public class AntonymMatch : CopleFinder
    {

        public override bool GiveNewContent(
            TList<Content> FirstWords,
            TList<Content> SecondWords,
            out Content FirstOne, out Content SecondOne)
        {
            FirstOne = null;
            SecondOne = null;
            int IsThereSomeOneTrue = 0;
            for (int i = 0; i < FirstWords.Count; i++)
            {

                if (SecondWords.Find(d => d.EnglishSource == FirstWords[i].EnglishSource) != null)
                {
                    IsThereSomeOneTrue++;
                }
                else
                {

                    List<string> strings = AntonymBase.Antonyms[FirstWords[i]].attachments;
                    if (SecondWords.Find(d => strings.Contains(d.EnglishSource)) != null) IsThereSomeOneTrue++;
                }
            }
            if (ContentsUse.Count > 1)
            {

                bool CountExeption = false;
                if (FirstWords.Count == 1 && IsThereSomeOneTrue > 0) CountExeption = true;
                if (FirstWords.Count == 3 && IsThereSomeOneTrue > 1) CountExeption = true;
                else if (FirstWords.Count == 2 && IsThereSomeOneTrue > 0) { CountExeption = true; }
                else if (Random.Range(0, 100) > 50 && IsThereSomeOneTrue > 2) CountExeption = true;
                if (CountExeption || IsThereSomeOneTrue > 4)
                {
                    FirstOne = ContentsUse.RemoveRandomItem();
                    SecondOne = ContentsUse.RemoveRandomItem();
                    return true;
                }
                else return false;
            }
            else return false;

        }


        public override bool FindInList(TList<Content> FirstWords, TList<Content> SecondWords, out Content FirstOne, out Content SecondOne)
        {
            FirstOne = null;
            SecondOne = null;

            TList<Content> FirstCopleless = new TList<Content>();
            TList<Content> SecondCopleless = new TList<Content>();

            for (int i = 0; i < FirstWords.Count; i++)
                for (int DI = 0; DI < SecondWords.Count; DI++)
                    if (!(SecondWords[DI] as Antonym).attachments.Contains(FirstWords[i].EnglishSource))
                        FirstCopleless.Add(SecondWords[DI]);


            for (int i = 0; i < SecondWords.Count; i++)
                for (int DI = 0; DI < FirstWords.Count; DI++)
                    if (!(FirstWords[DI] as Antonym).attachments.Contains(SecondWords[i].EnglishSource))
                        SecondCopleless.Add(FirstWords[DI]);
            if (FirstCopleless.Count == 0)
                FirstOne = SecondWords.RandomItem;
            else FirstOne = FirstCopleless.RandomItem;

            if (SecondCopleless.Count == 0)
                SecondOne = FirstWords.RandomItem;
            else SecondOne = SecondCopleless.RandomItem;
            return FirstOne != null && SecondOne != null;

        }
    }
}
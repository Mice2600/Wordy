using Base.Synonym;
using Study.CopleFinder;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
using UnityEngine.UIElements;

namespace Study.CopleFinder.SynonymMatch
{
    public class SynonymMatch : CopleFinder
    {

        public override bool GiveNewContent(
            TList<Content> FirstWords, 
            TList<Content> SecondWords, 
            out Content FirstOne, out Content SecondOne)
        {
            FirstOne = null;
            SecondOne = null;


            TList<Content> FirstCopls = new TList<Content>();
            TList<Content> SecondCopls = new TList<Content>();

            for (int i = 0; i < FirstWords.Count; i++)
                for (int DI = 0; DI < SecondWords.Count; DI++)
                    if ((SecondWords[DI] as Synonym).attachments.Contains(FirstWords[i].EnglishSource))
                        FirstCopls.Add(SecondWords[DI]);


            for (int i = 0; i < SecondWords.Count; i++)
                for (int DI = 0; DI < FirstWords.Count; DI++)
                    if ((FirstWords[DI] as Synonym).attachments.Contains(SecondWords[i].EnglishSource))
                        SecondCopls.Add(FirstWords[DI]);
            int IsThereSomeOneTrue = (int)System.MathF.Min(FirstCopls.Count, SecondCopls.Count);

            if (ContentsUse.Count > 0 && (IsThereSomeOneTrue > 4 ||
                (Random.Range(0, 100) > 50 && IsThereSomeOneTrue > 3)))
            {
                TList<Content> FirstCopleless = new TList<Content>();
                TList<Content> SecondCopleless = new TList<Content>();

                for (int i = 0; i < FirstWords.Count; i++)
                    for (int DI = 0; DI < SecondWords.Count; DI++)
                        if (!(SecondWords[DI] as Synonym).attachments.Contains(FirstWords[i].EnglishSource))
                            FirstCopleless.Add(SecondWords[DI]);


                for (int i = 0; i < ContentsUse.Count; i++) 
                {
                    if (!(ContentsUse[i] as Synonym).attachments.Contains(FirstCopls[i].EnglishSource))
                        FirstCopleless.Add(ContentsUse[i]);
                    
                    if (!(ContentsUse[i] as Synonym).attachments.Contains(SecondCopls[i].EnglishSource))
                        SecondCopleless.Add(ContentsUse[i]);
                }
                if (FirstCopleless.Count > 0) FirstOne = FirstCopleless.RandomItem;
                else FirstOne = ContentsUse.RemoveRandomItem();
                if (SecondCopleless.Count > 0) SecondOne = SecondCopleless.RandomItem;
                else SecondOne = ContentsUse.RemoveRandomItem();
                return true;
            }
            else return false;






        }


        public override void FindInList(TList<Content> FirstWords, TList<Content> SecondWords, out Content FirstOne, out Content SecondOne)
        {
            FirstOne = null;
            SecondOne = null;

            TList<Content> FirstCopleless = new TList<Content>();
            TList<Content> SecondCopleless = new TList<Content>();

            for (int i = 0; i < FirstWords.Count; i++)
                for (int DI = 0; DI < SecondWords.Count; DI++)
                    if (!(SecondWords[DI] as Synonym).attachments.Contains(FirstWords[i].EnglishSource)) 
                        FirstCopleless.Add(SecondWords[DI]);


            for (int i = 0; i < SecondWords.Count; i++)
                for (int DI = 0; DI < FirstWords.Count; DI++)
                    if (!(FirstWords[DI] as Synonym).attachments.Contains(SecondWords[i].EnglishSource))
                        SecondCopleless.Add(FirstWords[DI]);
            if (FirstCopleless.Count == 0) 
                FirstOne = SecondWords.RandomItem;
            else FirstOne = FirstCopleless.RandomItem;

            if (SecondCopleless.Count == 0)
                SecondOne = FirstWords.RandomItem;
            else SecondOne = SecondCopleless.RandomItem;

        }
    }
}
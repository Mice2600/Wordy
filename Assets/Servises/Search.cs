using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
namespace Servises 
{
    public static class Search
    {
        public static TList<T> SearchAll<T>(TList<T> AllContents, string SearchString) where T : Content
        {
            return new TList<T>(AllContents.Where(stringToCheck => stringToCheck.EnglishSource.Contains(SearchString, StringComparison.OrdinalIgnoreCase)));
        }
        public static IEnumerator SearchAllEnumerator<T>(TList<T> AllContents, string SearchString, System.Action<TList<T>> ResaltOne) where T : Content 
        {

            int UpdateTime = 0;
            bool isChanged = false;
            TList<T> Resolt = new TList<T>();
            for (int i = 0; i < AllContents.Count; i++)
            {
                if(AllContents[i].EnglishSource.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                {
                    if (!Resolt.Contains(AllContents[i])) 
                    {
                        Resolt.Add(AllContents[i]);
                        isChanged = true;
                    }
                }
                UpdateTime++;
                if (UpdateTime > 100) 
                {
                    UpdateTime = 0;
                    yield return new WaitForEndOfFrame();
                    if (isChanged) 
                    {
                        ResaltOne?.Invoke(Resolt);
                        isChanged = false;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
            if (isChanged)
            {
                ResaltOne?.Invoke(Resolt);
                isChanged = false;
            }


        }

        public static TList<T> SearchIrregularAll<T>(TList<T> AllContents, string SearchString) where T : Content
        {
            return new TList<T>(AllContents.Where((stringToCheck) => 
            {
                if (stringToCheck is not IIrregular) return stringToCheck.EnglishSource.Contains(SearchString, StringComparison.OrdinalIgnoreCase);
                var I = (stringToCheck as IIrregular);
                if (I.BaseForm.Contains(SearchString, StringComparison.OrdinalIgnoreCase)) return true;
                if (I.SimplePast.Contains(SearchString, StringComparison.OrdinalIgnoreCase)) return true;
                if (I.PastParticiple.Contains(SearchString, StringComparison.OrdinalIgnoreCase)) return true;
                return false;
            }));
        }
        



    }
}


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


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
        public static TList<T> SearchAll<T>(TList<T> AllContents, string SearchString) where T : IContent
        {
            return new TList<T>(AllContents.Where(stringToCheck => stringToCheck.EnglishSource.Contains(SearchString, StringComparison.OrdinalIgnoreCase)));
            TList<T> Sorted = new TList<T>();
            while (AllContents.Count > 0)
            {
                T match = AllContents.FirstOrDefault(stringToCheck => stringToCheck.EnglishSource.Contains(SearchString, System.StringComparison.OrdinalIgnoreCase));
                if (string.IsNullOrEmpty(match.EnglishSource)) return Sorted;
                Sorted.Add(match);
                AllContents.Remove(match);
            } 
            return Sorted;
        }
    }
}


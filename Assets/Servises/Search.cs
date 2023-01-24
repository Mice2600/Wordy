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
    }
}


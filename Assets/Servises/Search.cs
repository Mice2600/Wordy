using Base;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
using UnityEngine.Windows;
using FuzzySharp;
using Unity.Jobs;
using Base.Word;
using UnityEditor.Search;

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


        public static bool IsThereKays(string Order)
        {
            if (Order.Contains(">")) 
            {
                if (Order.Contains("<SIZE:")) return true;
                if (Order.Contains("<HES:")) return true;
                if (Order.Contains("<STARTS:")) return true;
                if (Order.Contains("<ENDS:")) return true;
            }
            
            return false;
        }

        public static IEnumerator SmartSearch<T>(TList<T> AllContents, string Order, MonoBehaviour Engine, System.Action<TList<T>> OnFinsh) where T : Content
        {
            Order = Order.ToUpper();

            if (Order.Contains("<SIZE:")) 
            {
                bool WaitForSize = false;

                string KeyResult = $"<SIZE:{Order.Split(new string[] { "<SIZE:" }, StringSplitOptions.None)[1].Split('>')[0]}";
                Order = Order.Replace(KeyResult, "");
                Engine.StartCoroutine(SearchSize(AllContents, KeyResult, (a) => { }, (R) => {
                    AllContents = R;
                    WaitForSize = true;
                }));
                yield return new WaitUntil(() => WaitForSize);
            }


            if (Order.Contains("<HES:"))
            {
                bool WaitForSize = false;

                string KeyResult = $"<HES:{Order.Split(new string[] { "<HES:" }, StringSplitOptions.None)[1].Split('>')[0]}";
                Order = Order.Replace(KeyResult, "");
                Engine.StartCoroutine(SearchHas(AllContents, KeyResult, (a) => { }, (R) => {
                    AllContents = R;
                    WaitForSize = true;
                }));
                yield return new WaitUntil(() => WaitForSize);
            }


            if (Order.Contains("<STARTS:"))
            {
                bool WaitForSize = false;

                string KeyResult = $"<STARTS:{Order.Split(new string[] { "<STARTS:" }, StringSplitOptions.None)[1].Split('>')[0]}";
                Order = Order.Replace(KeyResult, "");
                Engine.StartCoroutine(SearchStarts(AllContents, KeyResult, (a) => { }, (R) => {
                    AllContents = R;
                    WaitForSize = true;
                }));
                yield return new WaitUntil(() => WaitForSize);
            }
            
            if (Order.Contains("<ENDS:"))
            {
                bool WaitForSize = false;

                string KeyResult = $"<ENDS:{Order.Split(new string[] { "<ENDS:" }, StringSplitOptions.None)[1].Split('>')[0]}";
                Order = Order.Replace(KeyResult, "");
                Engine.StartCoroutine(SearchEnds(AllContents, KeyResult, (a) => { }, (R) => {
                    AllContents = R;
                    WaitForSize = true;
                }));
                yield return new WaitUntil(() => WaitForSize);
            }
            OnFinsh.Invoke(AllContents);

        }

        public static IEnumerator SearchSize<T>(TList<T> AllContents, string Order, System.Action<TList<T>> ResaltOne, System.Action<TList<T>> OnFinsh) where T : Content //  <SIZE:5>  <SIZE:3,7>
        {

            int UpdateTime = 0;
            bool isChanged = false;
            TList<T> Resolt = new TList<T>();

            List<int> Lengts= new List<int>();
            Order = Order.ToUpper().Replace("<SIZE:", "").Replace(">", "");
            Order.Split(',').ToList().ForEach(s => { if(int.TryParse(s, out int D)) Lengts.Add(D); });


            if (Lengts.Count == 0) 
            {
                OnFinsh.Invoke(AllContents);
                yield break;
            }
            for (int i = 0; i < AllContents.Count; i++)
            {
                for (int SA = 0; SA < Lengts.Count; SA++)
                {
                    if (AllContents[i].EnglishSource.Length == Lengts[SA])
                    {
                        if (!Resolt.Contains(AllContents[i]))
                        {
                            Resolt.Add(AllContents[i]);
                            isChanged = true;
                            break;
                        }
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
            OnFinsh.Invoke(Resolt);
        }
        public static IEnumerator SearchHas<T>(TList<T> AllContents, string Order, System.Action<TList<T>> ResaltOne, System.Action<TList<T>> OnFinsh) where T : Content //  <HES:GH>  <HES:GH,ER>
        {

            int UpdateTime = 0;
            bool isChanged = false;
            TList<T> Resolt = new TList<T>();

            List<string> HasOrders = new List<string>();
            Order = Order.ToUpper().Replace("<HES:", "").Replace(">", "");
            Order.Split(',').ToList().ForEach(s => HasOrders.Add(s.ToUpper()));

            for (int i = 0; i < AllContents.Count; i++)
            {
                bool IsCorrect = false;
                for (int SA = 0; SA < HasOrders.Count; SA++) 
                {
                    if (AllContents[i].EnglishSource.Contains(HasOrders[SA], StringComparison.OrdinalIgnoreCase))
                        IsCorrect = true;
                    else 
                    {
                        IsCorrect = false;
                        break;
                    }
                }
                    
                if (IsCorrect && !Resolt.Contains(AllContents[i])) 
                {
                    Resolt.Add(AllContents[i]);
                    isChanged = true;
                }
                UpdateTime++;
                if (UpdateTime > 100)
                {
                    UpdateTime = 0;
                    yield return null;
                    if (isChanged)
                    {
                        ResaltOne?.Invoke(Resolt);
                        isChanged = false;
                    }
                    yield return null;
                }
            }
            if (isChanged)
            {
                ResaltOne?.Invoke(Resolt);
                isChanged = false;
            }
            OnFinsh.Invoke(Resolt);
        }
        public static IEnumerator SearchStarts<T>(TList<T> AllContents, string Order, System.Action<TList<T>> ResaltOne, System.Action<TList<T>> OnFinsh) where T : Content //  <STARTS:A> <STARTS:Un> <STARTS:Dis>
        {

            int UpdateTime = 0;
            bool isChanged = false;
            TList<T> Resolt = new TList<T>();

            string HasOrders = Order.ToUpper().Replace("<STARTS:", "").Replace(">", "");

            for (int i = 0; i < AllContents.Count; i++)
            {

                if (AllContents[i].EnglishSource.StartsWith(HasOrders, StringComparison.OrdinalIgnoreCase))
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
                    yield return null;
                    if (isChanged)
                    {
                        ResaltOne?.Invoke(Resolt);
                        isChanged = false;
                    }
                    yield return null;
                }
            }
            if (isChanged)
            {
                ResaltOne?.Invoke(Resolt);
                isChanged = false;
            }
            OnFinsh.Invoke(Resolt);
        }
        public static IEnumerator SearchEnds<T>(TList<T> AllContents, string Order, System.Action<TList<T>> ResaltOne, System.Action<TList<T>> OnFinsh) where T : Content //  <ENDS:A> <ENDS:Un> <ENDS:Dis>
        {

            int UpdateTime = 0;
            bool isChanged = false;
            TList<T> Resolt = new TList<T>();
            
            string HasOrders = Order.ToUpper().Replace("<ENDS:", "").Replace(">", "");

            for (int i = 0; i < AllContents.Count; i++)
            {

                if (AllContents[i].EnglishSource.EndsWith(HasOrders, StringComparison.OrdinalIgnoreCase))
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
                    yield return null;
                    if (isChanged)
                    {
                        ResaltOne?.Invoke(Resolt);
                        isChanged = false;
                    }
                    yield return null;
                }
            }
            if (isChanged)
            {
                ResaltOne?.Invoke(Resolt);
                isChanged = false;
            }
            OnFinsh.Invoke(Resolt);
        }
        
        
        
        
        
        public static void TryFuzzy(string query = "acquire")
        {

            var jobs = new List<WordDefoult>(WordBase.DefaultBase);
            

            

            var processedQuery = query;

            // Iterate over the job data and calculate the fuzzy score
            var fuzzyScores = new List<(WordDefoult, int score)>();
            foreach (var job in jobs)
            {
                var titleScore = Fuzz.PartialRatio(processedQuery, job.EnglishSource);
                fuzzyScores.Add((job, titleScore));
            }

            // Filter the jobs that have a fuzzy score above a certain threshold
            var threshold = 60;
            var filteredJobs = fuzzyScores.Where(x => x.score >= threshold).Select(x => x.Item1);

            // Sort the filtered jobs by their fuzzy score in descending order
            var sortedJobs = filteredJobs.OrderByDescending(x => fuzzyScores.FirstOrDefault(y => y.Item1 == x).score);

            // Return the top N jobs as search results
            var topN = 100;
            var searchResults = sortedJobs.Take(topN);

            Debug.Log(searchResults.Count());
            foreach (var job in searchResults) 
            {
                Debug.Log(job.EnglishSource);
            }






        }

    }
}


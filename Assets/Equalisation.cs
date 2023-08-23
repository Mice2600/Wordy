using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemBox;
using Traonsletor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Equalisation : MonoBehaviour
{

    [Button]
    public void HarryWorker_S() 
    {
        StartCoroutine(HarryWorker());
    }
    public IEnumerator HarryWorker()
    {

        yield return null;
        Func<string, bool> IsAddble =
            (W) =>
            {
                if (W.Length < 3) return false;
                if (W.Contains(" ")) return false;
                List<string> NoNo = new List<string>() { " ", "'", "’", "‘", ">", "<", ":", "”", "“" };
                List<string> NonnNeededWords = new List<string>() { 
                    "The","was", "and", "his", "you", "for", "have", "had", "that",
                    "this", "all", "one", "like", "off", "were", "from", "she", "get", "got", "him", "his", "far",
                    "harry","said","but","ron","out","hagrid","what","back","into","about","not","been","potter","could","when",
                    "her","know","down","over","looked","just","professor","see","who","are","around","going","through","snape",
                    "dumbledore","now","think","time","never","more","which","something","how","dudley","look","would","only",
                    "eyes","door","even","two","head","malfoy","vernon","thought", "again", "did", "still", "than",
                    "next",
                    "too",
                    "right",
                    "where",
                    "yeh",
                    "way",
                    "here",
                    "first",
                    "with",
                    "they",
                    "them",
                    "hermione",
                    "there",
                    "their",
                    "very",
                    "then",
                    "your",
                    "uncle",
                    "before",
                    "other",
                    "looking",
                    "face",
                    "told",
                    "people",
                    "neville",
                    "mcgonagall",
                    "will",
                    "because",
                    "quirrell",
                    "its",
                    "left",
                    "though",
                    "last",
                    "room",
                    "come",
                    "behind",
                    "boy",
                    "gryffindor",
                    "ter",
                    "some",
                    "came",
                    "turned",
                    "stone",
                    "heard",
                    "much",
                    "house",
                    "seemed",
                    "any",
                    "towards",
                    "long",
                    "can",
                    "little",
                    "want",
                    "went",
                    "suddenly",
                    "once",
                    "away",
                    "say",
                    "great",
                    "tell",
                    "found",
                    "found",
                    "after",
                    "really",
                    "really",
                    "bit",
                    "made",
                    "aunt",
                    "aunt",
                    "hand",
                    "hogwarts",
                    "tried",
                    "trying",
                    "saw",
                    "knew",
                    "anything",
                    "dark",
                    "while",
                    "himself",
                    "quidditch",
                    "quidditch",
                    "voice",
                    "three",
                    "good",
                    "good",
                    "took",
                    "take",
                    "put",
                    "school",
                    "ever",
                    "asked",
                    "let",
                    "petunia",
                    "well",
                    "well",
                    "old",
                    "yer",
                    "few",
                    "few",
                    "inside",
                    "must",
                    "open",
                    "find",
                    "day",
                    "dursley",
                    "might",
                    "seen",
                    "seen",
                    "things",
                };

                for (int i = 0; i < NoNo.Count; i++) if (W.Contains(NoNo[i], StringComparison.CurrentCultureIgnoreCase)) return false;
                //for (int i = 0; i < NonnNeededWords.Count; i++)if (W.Equals(NonnNeededWords[i], StringComparison.CurrentCultureIgnoreCase)) return false;
                
                if (W.Contains("1") || W.Contains("2") || W.Contains("3") || W.Contains("4") || W.Contains("5") || W.Contains("6") || W.Contains("7") || W.Contains("8") || W.Contains("9") || W.Contains("0"))
                    return false;
                return true;

            };


        



        string AllText = System.IO.File.ReadAllText(@"D:\Harry_1.txt");
        StreamWriter Writter = new(@"D:\Hurry_Resolt.txt");

        AllText = AllText.Replace(",", "");
        AllText = AllText.Replace("?", "");
        AllText = AllText.Replace(".", "");
        AllText = AllText.Replace("(", "");
        AllText = AllText.Replace(")", "");
        AllText = AllText.Replace(";", "");
        AllText = AllText.Replace(":", "");
        AllText = AllText.Replace("!", "");


        Dictionary<string, int> ResoldtDic = new Dictionary<string, int>();

        List<string> Words = new List<string>();
        AllText.Split(new[] { "\r\n", " ", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach((w) =>
        {
            w = w.ToLower();
            if (ResoldtDic.ContainsKey(w)) ResoldtDic[w]++;
            else if (IsAddble.Invoke(w)) ResoldtDic.Add(w, 1);
        });



        TList<KeyValuePair<string, int>> myList = ResoldtDic.ToList();

        myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        ResoldtDic = new Dictionary<string, int>();
        string ResoltString = "";
        myList.Rotate();
        for (int i = 0; i < myList.Count; i++)
        {

            //if (myList[i].Value < 3)  continue;

            string TransLetRUU = "";
            string TransLetUzz = "";

            StartCoroutine(Translator.Process("en", "ru", myList[i].Key, (a) => TransLetRUU = a));
            StartCoroutine(Translator.Process("en", "uz", myList[i].Key, (a) => TransLetUzz = a));

            while(string.IsNullOrEmpty(TransLetRUU) || string.IsNullOrEmpty(TransLetUzz))yield return null;
                
            string ReplaceKey = myList[i].Key;
            for (int D = myList[i].Key.Length; D < 20; D++) ReplaceKey += " ";


            string ReplaceKeyRuu = TransLetRUU;
            for (int D = TransLetRUU.Length; D < 20; D++) ReplaceKeyRuu += " ";
            ReplaceKey += ReplaceKeyRuu;

            string ReplaceKeyUzz = TransLetUzz;
            for (int D = TransLetUzz.Length; D < 20; D++) ReplaceKeyUzz += " ";
            ReplaceKey += ReplaceKeyUzz;

            ReplaceKey += " : Count = " + myList[i].Value + "\n\n\n";
            ResoltString += ReplaceKey;
        }
        Writter.WriteLine(ResoltString);
        Writter.Close();

        Debug.Log("Done");


    }


    [ShowInInspector]
    public static int UnnecessarySpaceCounter(string Orginal, string Other)
    {
        if (Orginal.Length == Other.Length) return 10;

        int d = Other.Length - Orginal.Length;
        if (Orginal.Length > Other.Length) d = Orginal.Length - Other.Length;
        if (d == 1) return -2;
        if (d == 2) return -4;
        return -(d * 3);
    }

    public static int LettersOrderCounter(string Orginal, string Other)
    {

        return 0;
    }
    public static int LettersEqualisation(string Orginal, string Other)
    {
        List<char> OrginalChars = Orginal.ToList();
        OrginalChars.Sort();
        List<char> OtherChars = Other.ToList();
        OtherChars.Sort();

        int Score = 0;

        for (int i = 0; i < OrginalChars.Count; i++)
        {
            if (OtherChars.Contains(OrginalChars[i]))
            {
                OtherChars.Remove(OrginalChars[i]);
            }
            Score += 5;
        }
        Score -= OtherChars.Count * 5;
        return Score;
    }






    public static void KK()
    {
        int Number = 30;
        string Bolinadigan = "";
        string Bolinmidigan = "";
        for (int i = 1; i < Number; i++)
        {
            if (i % 2 == 0) Bolinadigan += i + ", ";
            else Bolinmidigan += i + ", ";

        }
        Bolinadigan += "\n";
        Bolinmidigan += "\n";
        Debug.Log(Bolinadigan);
        Debug.Log(Bolinmidigan);



    }



}

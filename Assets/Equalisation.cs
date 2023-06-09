using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Equalisation : MonoBehaviour
{




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
            if(i % 2 == 0)Bolinadigan += i + ", ";
            else Bolinmidigan += i + ", ";

        }
        Bolinadigan += "\n";
        Bolinmidigan += "\n";
        Debug.Log(Bolinadigan);
        Debug.Log(Bolinmidigan);



    }



}

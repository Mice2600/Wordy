using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WordScrollBar : MonoBehaviour
{

    private void Update()
    {
        LetterObject.SetActive(scrollBar.IsDraging);
    }






    public GameObject LetterObject;
    public TMP_Text M_text;
    public TMP_Text Letter;

    public ScrollBar scrollBar;
    Dictionary<char, List<TMP_WordInfo>> Words;
    
    public void RefreshScrollbar()
    {

        Words = new Dictionary<char, List<TMP_WordInfo>>();

        for (int i = 0; i < M_text.textInfo.wordCount; i++) 
        {
            char C = char.ToUpper(M_text.textInfo.wordInfo[i].GetWord()[0]);
            if (!Words.ContainsKey(C)) Words.Add(C, new List<TMP_WordInfo>());
            Words[C].Add(M_text.textInfo.wordInfo[i]);
        }
        
        scrollBar.size = 1f / Words.Count;
        
    }

    public void on_ValueChanged(float Value) 
    {
        
        float IneLetterValue = 1f / Words.Count;
        int Index =(int)( Value / IneLetterValue);
        char NChar = Words.Keys.ToList()[Index];

        Letter.text = NChar.ToString();


        if (MoveCoroutine == null)
        {
            MoveChar = NChar;
            MoveCoroutine  = StartCoroutine(Move(NChar));
        } else if (MoveChar != NChar) 
        {
            StopCoroutine(MoveCoroutine);
            MoveChar = NChar;
            MoveCoroutine = StartCoroutine(Move(NChar));
        }

    }

    Coroutine MoveCoroutine = null;
    public char  MoveChar = ' ';
    IEnumerator Move(char NChar) 
    {

        int Algaritm = 0;
        while (true)
        {

            yield return null;


            var firstCharInfo = M_text.textInfo.characterInfo[Words[NChar][0].firstCharacterIndex];
            Vector2 Top = new Vector3(0, firstCharInfo.topLeft.y);
            float MyY = M_text.transform.TransformPoint(Top).y;




            int height = Screen.height / 2;

#if UNITY_EDITOR

            height = (int)UnityEditor.Handles.GetMainGameViewSize().y / 2;
#endif

            if(MathF.Abs(MyY - height) < 20) break;
            if (MyY < height)
            {
                if (Algaritm == 2) break;
                Algaritm = 1;
                scroll.verticalNormalizedPosition = TMath.MoveTowards(scroll.verticalNormalizedPosition, 0, Time.deltaTime);
                if (scroll.verticalNormalizedPosition < 0.1) break;
            }
            else 
            {
                if (Algaritm == 1) break;
                Algaritm = 2;
                scroll.verticalNormalizedPosition = TMath.MoveTowards(scroll.verticalNormalizedPosition, 1, Time.deltaTime);
                if (scroll.verticalNormalizedPosition > .95f) break;
            }
        }
        MoveCoroutine = null;
    }

    public ScrollRect scroll;
   
}

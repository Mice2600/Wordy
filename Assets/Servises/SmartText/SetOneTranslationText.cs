using Base.Word;
using Servises.SmartText;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class SetOneTranslationText : ContentText
{
    public int ChosenOne = -1;
    public override string GetValue(Content Object)
    {
        if(Object is IMultiTranslation)
            if(ChosenOne == -1) ChosenOne = Random.Range(0, (Object as IMultiTranslation).TranslationCount);
        if (Object == null) return "";
        else 
        {
            if (Object is IMultiTranslation)
                return (Object as IMultiTranslation).Translations[ChosenOne, ListGetType.Infinity];
            return Object.RussianSource; 
        }
    }
}
using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;

public class WI_Conttent : ContentObject
{
    [Required]
    public TMP_InputField Base_InputField;
    [Required]
    public GameObject Base_Text;

    [Required]
    public TMP_InputField Past_InputField;
    [Required]
    public GameObject Past_Text;

    [Required]
    public TMP_InputField Participle_InputField;
    [Required]
    public GameObject Participle_Text;

    public bool IsBaseCurrect => Base_InputField.text.ToUpper() == (Content as IIrregular).BaseForm.ToUpper();
    public bool IsPastCurrect => Past_InputField.text.ToUpper() == (Content as IIrregular).SimplePast.ToUpper();
    public bool IsParticipleCurrect => Participle_InputField.text.ToUpper() == (Content as IIrregular).PastParticiple.ToUpper();
    private void Start()
    {
        TList<(TMP_InputField InT, GameObject Te)> das = new List<(TMP_InputField InT, GameObject Te)>() { (Base_InputField, Base_Text), (Past_InputField, Past_Text), (Participle_InputField, Participle_Text) };
        das.ForEach(a => { a.InT.gameObject.SetActive(false); a.Te.SetActive(true); });
    }
}

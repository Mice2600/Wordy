using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
    
    public bool IsPastCurrect 
    {
        get 
        {
            string[] Sp = (Content as IIrregular).SimplePast.ToUpper().Split("/");
            for (int i = 0; i < Sp.Length; i++)
                if (Sp[i] == Past_InputField.text.ToUpper()) return true;
            return false;
        }
    } 
    public bool IsParticipleCurrect 
    {
        get 
        {
            string[] Sp = (Content as IIrregular).PastParticiple.ToUpper().Split("/");
            for (int i = 0; i<Sp.Length; i++)
                if (Sp[i] == Participle_InputField.text.ToUpper()) return true;
            return false;
        }
    } 
    public bool IsEvrethingCurrect => IsBaseCurrect && IsPastCurrect && IsParticipleCurrect;
    public bool IsEvrethingWroten => ((string.IsNullOrEmpty(Base_InputField.text)) || (string.IsNullOrEmpty(Past_InputField.text) || string.IsNullOrEmpty(Participle_InputField.text))) == false;
    public string WrotenText => Base_InputField.text+ " " + Past_InputField.text + " " + Participle_InputField.text;
    private void Start()
    {
        if (Content == null) return;
        IIrregular sd = (Content as IIrregular);
        Base_InputField.text = sd.BaseForm;
        Past_InputField.text = sd.SimplePast;
        Participle_InputField.text = sd.PastParticiple;

        TList<(TMP_InputField InT, GameObject Te)> List = new List<(TMP_InputField InT, GameObject Te)>() { (Base_InputField, Base_Text), (Past_InputField, Past_Text), (Participle_InputField, Participle_Text) };
        List.ForEach(a => { a.InT.gameObject.SetActive(false); a.Te.SetActive(true); });
        
        PasOne();
        if (Content.ScoreConculeated > 40) PasOne();  
        
        void PasOne()
        {
            (TMP_InputField InT, GameObject Te) sd = List.RemoveRandomItem();
            sd.InT.text = "";
            sd.InT.gameObject.SetActive(true);
            sd.Te.SetActive(false);
        }
    }
}

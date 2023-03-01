using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BI_Content : ContentObject
{

    public GameObject BoxPrefab;
    public GameObject BoxPrefab_Shadow;



    [Required]
    public GameObject Base_Text;
    [Required]
    public BI_Place Base_Place;
    [Required]
    public GameObject Past_Text;
    [Required]
    public BI_Place Past_Place;
    [Required]
    public GameObject Participle_Text;
    [Required]
    public BI_Place Participle_Place;


    public bool IsBaseCurrect => Base_Place.isActiveAndEnabled && Base_Place.contentBox != null && Base_Place.contentBox.Content.EnglishSource.ToUpper() == (Content as IIrregular).BaseForm.ToUpper() || !Base_Place.isActiveAndEnabled;
    public bool IsPastCurrect => Past_Place.isActiveAndEnabled && Past_Place.contentBox != null && (Past_Place.contentBox.Content as IIrregular).SimplePast.ToUpper() == (Content as IIrregular).SimplePast.ToUpper() || !Past_Place.isActiveAndEnabled;
    public bool IsParticipleCurrect => Participle_Place.isActiveAndEnabled && Participle_Place.contentBox != null && (Participle_Place.contentBox.Content as IIrregular).PastParticiple.ToUpper() == (Content as IIrregular).PastParticiple.ToUpper() || !Participle_Place.isActiveAndEnabled;
    public bool IsEvrethingCurrect => IsBaseCurrect && IsPastCurrect && IsParticipleCurrect;
    public bool IsEvrethingWroten => (
        (!Base_Place.isActiveAndEnabled || Base_Place.contentBox != null) || 
        ((!Past_Place.isActiveAndEnabled || Past_Place.contentBox != null) ||
        (!Participle_Place.isActiveAndEnabled || Participle_Place.contentBox != null)));
    public string WrotenText => ((Base_Place.isActiveAndEnabled && Base_Place.contentBox != null) ? Base_Place.contentBox.Content.EnglishSource.ToUpper() : " ") +
        ((Past_Place.isActiveAndEnabled && Past_Place.contentBox != null) ? Past_Place.contentBox.Content.EnglishSource.ToUpper() : " ") +
        ((Participle_Place.isActiveAndEnabled && Participle_Place.contentBox != null) ? Participle_Place.contentBox.Content.EnglishSource.ToUpper() : " ");

    private void Start()
    {
        if (Content == null) return;
        IIrregular sd = (Content as IIrregular);
        BuildIlrregularSystem SS = GetComponentInParent<BuildIlrregularSystem>();

        TList<(BI_Place InT, GameObject Te, int Type)> List = new List<(BI_Place InT, GameObject Te, int Type)>() { (Base_Place, Base_Text, 0), (Past_Place, Past_Text, 0), (Participle_Place, Participle_Text, 0) };
        List.ForEach(a => { a.InT.gameObject.SetActive(false); a.Te.SetActive(true); });

        PasOne();
        if (Content.ScoreConculeated > 25) PasOne();


        void PasOne()
        {
            (BI_Place InT, GameObject Te, int Type) sd = List.RemoveRandomItem();
            sd.InT.gameObject.SetActive(true);
            sd.Te.SetActive(false);
            BI_ContentBox NCOn = Instantiate(BoxPrefab, transform.root).GetComponent<BI_ContentBox>();
            NCOn.transform.position = new Vector3(5555, 5555, 0);
            GameObject Shadow = Instantiate(BoxPrefab_Shadow, transform.root);
            SS.DownContentsParrent.AddNewContent(Shadow.transform);
            NCOn.Content = Content;
            NCOn.IRRType = sd.Type;
            StartCoroutine(Afterr());
            IEnumerator Afterr() 
            {
                yield return new WaitForSeconds(.5f);
                NCOn.transform.position = Shadow.transform.position;
            }
            
        }



    }
}

using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public interface IBaseNameUser 
{
    public Sprite BaseImage { get; }
    public string BaseName { get; }
}
public class BaseName : MonoBehaviour, IBaseToolItem
{
    
    [SerializeField, Required]
    private Image BaseImage;
    [SerializeField, Required]
    private TMP_Text NameText;
    public Sprite DefaultImage;
    public void OnNewViweOpend(GameObject baseList)
    {
        var r = baseList.GetComponent<IBaseNameUser>();

        if (r == null)
        {
            BaseImage.sprite = DefaultImage;
            NameText.text = "BASE";
        }
        else 
        {
            BaseImage.sprite = (r).BaseImage;
            NameText.text = (r).BaseName;
        }
    }
}

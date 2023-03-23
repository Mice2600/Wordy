using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;
using SystemBox.Engine;
using UnityEngine.SceneManagement;
using Base.Word;
using Servises;
using Servises.BaseList;
using Base;

public class IrregularBaseViwe : BaseListWithFillter, IBaseNameUser
{
    public override List<Content> AllContents => new List<Content>(IrregularBase.Irregulars);

    Sprite IBaseNameUser.BaseImage => BaseImage;
    [SerializeField, Required]
    private Sprite BaseImage;
    string IBaseNameUser.BaseName => BaseName;
    [SerializeField]
    private string BaseName;
    protected override TList<Content> SearchComand(TList<Content> AllContents, string SearchString) => Search.SearchIrregularAll<Content>(AllContents, SearchString);
    private float CCSize;
    public override float GetSizeOfContent(Content content)
    {
        if (CCSize == 0f) CCSize = content.ContentObject.GetComponent<RectTransform>().rect.height;
        return CCSize;
    }
}

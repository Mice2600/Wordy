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

public class IrregularBaseViwe : BaseListWithFillter<Irregular>, IRemoveButtonUser
{
    public override List<Irregular> AllContents => IrregularBase.Irregulars;
    private protected override int IndexOf(Content content) => base.Contents.IndexOf(content as Irregular);
    public void OnRemoveButton(Content content)
    {
        IrregularBase.Irregulars.Remove(content as Irregular);
        FindObjectOfType<DiscretionViwe>()?.DestroyUrself();
        Refresh();
    }
    protected override TList<Irregular> SearchComand(TList<Irregular> AllContents, string SearchString) => Search.SearchIrregularAll<Irregular>(AllContents, SearchingString);
}
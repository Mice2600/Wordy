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

public class IrregularBaseViwe : BaseListWithFillter
{
    public override List<Content> AllContents => new List<Content>(IrregularBase.Irregulars);
    protected override TList<Content> SearchComand(TList<Content> AllContents, string SearchString) => Search.SearchIrregularAll<Content>(AllContents, SearchString);
}

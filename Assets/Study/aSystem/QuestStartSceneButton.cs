using Base.Dialog;
using Base.Word;
using Servises;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStartSceneButton : SceneLodeButton
{
    
    public override void OpenScene()
    {
        if (WordBase.Wordgs.Count < 20) return;
        if (DialogBase.Dialogs.Count < 3) return;
        base.OpenScene();

    }
}

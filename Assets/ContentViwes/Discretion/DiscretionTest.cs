using Base.Dialog;
using Base.Word;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class DiscretionTest : DiscretionViwe
{
    public bool isDialog = false;
    private void Start()
    {
        if (isDialog) Content = DialogBase.Dialogs.RandomItem();
        else Content = WordBase.Wordgs.RandomItem();
    }
}

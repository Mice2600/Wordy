using Base.Dialog;
using Servises.SmartText;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrregularBaseActiveCountText : SmartText
{
    public override string MyText => SevedString;
    private protected override int PerUpdateTime => 10;
    string SevedString;
    protected override void PerUpdate() => SevedString =  IrregularBase.Irregulars.ActiveItems.Count.ToString();

}

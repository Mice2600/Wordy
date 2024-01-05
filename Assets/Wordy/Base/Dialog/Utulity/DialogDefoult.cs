using Base.Word;
using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Dialog;
using Sirenix.OdinInspector;

[System.Serializable]
public partial class DialogDefoult : Content, ISpeeker // Utulity
{
    string ISpeeker.SpeekText => EnglishSource;
    public override IDataListComands BaseCommander => DialogBase.Dialogs;
    public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionDialogDefoult;
    public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectDialogDefoult;

    public DialogDefoult(string EnglishSource, string RussianSource) : base(EnglishSource, RussianSource)
    {
        EnglishSource.ToUpper();
    }
    public override bool Equals(object obj) => base.Equals(obj) && (obj is Dialog || obj is DialogDefoult);
    public override int GetHashCode() => base.GetHashCode();

}

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [FoldoutGroup("Discretion"), Required]
        public GameObject DiscretionDialogDefoult;
        [FoldoutGroup("ContentObject"), Required]
        public GameObject ContentObjectDialogDefoult;
    }
}
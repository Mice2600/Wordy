using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Base.Dialog
{
    public partial class Dialog : Content, IComparable, ISpeeker  // Utulity
    {
        public override IDataListComands BaseCommander => DialogBase.Dialogs;
        string ISpeeker.SpeekText => EnglishSource;
        public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionDialog;
        public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectDialog;
        public Dialog(string EnglishSource, string RussianSource, float Score, bool Active) : base(EnglishSource, RussianSource, Score, Active)
        {

        }
        public override bool Equals(object obj) => base.Equals(obj) && obj is Dialog;
        public override int GetHashCode() => base.GetHashCode();


    }
}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [FoldoutGroup("Discretion"), Required]
        public GameObject DiscretionDialog;
        [FoldoutGroup("ContentObject"), Required]
        public GameObject ContentObjectDialog;
    }
}
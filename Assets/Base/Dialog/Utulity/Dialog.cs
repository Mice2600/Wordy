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
        public Dialog(string EnglishSource, string RussianSource, float Score, bool Active) : base(EnglishSource, RussianSource, Score, Active)
        {

        }

    }
}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [FoldoutGroup("Discretion")]
        public GameObject DiscretionDialog;
    }
}
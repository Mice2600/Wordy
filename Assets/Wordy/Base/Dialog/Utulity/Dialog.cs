using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Base.Dialog
{
    public partial class Dialog : Content, IComparable, ISpeeker , IPersanalData // Utulity
    {
        public override IDataListComands BaseCommander => DialogBase.Dialogs;
        string ISpeeker.SpeekText => EnglishSource;
        float IPersanalData.Score { get => this.Score; set => this.Score = value; }
        bool IPersanalData.Active { get => this.Active; set => this.Active = value; }
        public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionDialog;
        public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectDialog;

        
        public Dialog(string EnglishSource, string RussianSource, float Score, bool Active) : base(EnglishSource, RussianSource)
        {
            this.Score = Score;
            this.Active = Active;
        }
        public override bool Equals(object obj) => base.Equals(obj) && (obj is Dialog || obj is DialogDefoult);
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
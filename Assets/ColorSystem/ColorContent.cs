using Base;
using Base.Dialog;
using Base.Word;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        public Gradient ContentLoopColors;
    }
}
public class ColorContent : OptimizedBehaver, IQueueUpdate
{
    public List<MaskableGraphic> Arts;
    private ContentObject ContentObject;
    public Gradient ContentLoopColors;
    private protected override void Start()
    {
        base.Start();
        ContentObject = GetComponent<ContentObject>();
        ContentLoopColors = ProjectSettings.ProjectSettings.Mine.ContentLoopColors;
    }

    public void TurnUpdate()
    {
        if (ContentObject == null) return;
        if (ContentObject.Content == null) return;
        
        int Index = -1;
        if (ContentObject.Content is Word)
            Index = WordBase.Wordgs.IndexOf(ContentObject.Content as Word);
        else if (ContentObject.Content is Dialog) 
            Index = DialogBase.Dialogs.IndexOf(ContentObject.Content as Dialog);
        else return;
        Color NC = GetColor(Index, ContentObject.Content.Active);
        for (int i = 0; i < Arts.Count; i++) 
        {
            if (Arts[i] == null) continue;
            Arts[i].color = NC;
        }
    }

    public Color GetColor(int index, bool IsActive)
    {
        index = Mathf.Abs(index);
        int levv = (index / 30);
        index -= (levv * 30);
        Color color = ContentLoopColors.Evaluate((float)index / 30f);
        if (IsActive) return color;
        return Color.Lerp(color, Color.black, .3f);
    }
}

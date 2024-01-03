using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorQuestContent : ColorContent
{
    private protected override Color GetColor(int index, bool IsActive)
    {
        if (ContentObject.Content is IPersanalData)
        {
            if ((ContentObject.Content as IPersanalData).ScoreConculeated > 40)
                return Color.white;
        }
        return base.GetColor(index, true);

    }
}

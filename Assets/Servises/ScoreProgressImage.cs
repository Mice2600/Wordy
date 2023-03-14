using Base;
using UnityEngine.UI;

public class ScoreProgressImage : OptimizedBehaver, IQueueUpdate
{
    private ContentObject ContentObject => _ContentObject ??= GetComponentInParent<ContentObject>();
    private ContentObject _ContentObject;
    private Image Image => _Image ??= GetComponent<Image>();
    private Image _Image;
    public void TurnUpdate()
    {
        if (ContentObject.Content != null)
            Image.fillAmount = ContentObject.Content.ScoreConculeated / 100f;
        else Image.fillAmount = 0;
    }
}

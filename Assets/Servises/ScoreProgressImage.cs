using Base;
using UnityEngine.UI;

public class ScoreProgressImage : OptimizedBehaver, IQueueUpdate
{
    public void TurnUpdate()
    {
        if (ContentObject.Content != null)
            Image.fillAmount = ContentObject.Content.ScoreConculeated / 100f;
        else Image.fillAmount = 0;


    }

    private ContentObject ContentObject;
    private Image Image;

    private protected override void Start()
    {
        base.Start();
        Image = GetComponent<Image>();
        ContentObject = GetComponentInParent<ContentObject>();
    }
}

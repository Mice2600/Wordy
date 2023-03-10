using Base;
using UnityEngine.UI;

public class ScoreProgressImage : OptimizedBehaver, IQueueUpdate
{
    public void TurnUpdate()
    {
        Image.fillAmount =   ContentObject.Content.ScoreConculeated / 100f;
        
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

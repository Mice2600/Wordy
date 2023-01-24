using Base;
using UnityEngine;
using UnityEngine.UI;
namespace Servises
{
    public class SoundButton : Button
    {
        protected override void Start()
        {
            base.Start();
            ContentObject wordContent = transform.GetComponentInParent<ContentObject>();
            if (wordContent == null) return;
            onClick.AddListener(() =>
            {
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                    EasyTTSUtil.SpeechAdd(wordContent.Content.EnglishSource);
                else Debug.Log(wordContent.Content.EnglishSource + " Speeking");
            });

            
        }
    }
}
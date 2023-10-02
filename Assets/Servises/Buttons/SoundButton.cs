using Base;
using UnityEngine;
using UnityEngine.UI;
namespace Servises
{
    public class SoundButton : Button
    {
        protected override void Start()
        {
            EasyTTSUtil.SpeechAdd("");
            base.Start();
            ContentObject wordContent = transform.GetComponentInParent<ContentObject>();
            if (wordContent == null) return;
            
            onClick.AddListener(() =>
            {
                if (wordContent.Content == null) return;
                if (wordContent.Content is not ISpeeker) return;
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                    EasyTTSUtil.SpeechAdd((wordContent.Content as ISpeeker).SpeekText,1,.5f,1);
                else Debug.Log(wordContent.Content.EnglishSource + " Speeking");

                

            });
        }
    }
}
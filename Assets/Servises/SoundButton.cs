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
            Transform ToTest = transform;
            for (int i = 0; i < 20; i++)
            {
                if (ToTest.TryGetComponent<ContentObject>(out ContentObject wordContent))
                {
                    onClick.AddListener(() =>
                    {
                        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                            EasyTTSUtil.SpeechAdd(wordContent.Content.EnglishSource);
                        else Debug.Log(wordContent.Content.EnglishSource + " Speeking");
                    });
                    break;
                }
                ToTest = ToTest.parent;
                if (ToTest == null) break;
            }
        }
    }
}
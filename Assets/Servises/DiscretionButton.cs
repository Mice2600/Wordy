using Base;
using Base.Dialog;
using Base.Word;
using UnityEngine;
using UnityEngine.UI;
namespace Servises
{
    public class DiscretionButton : Button
    {

        protected override void Start()
        {
            base.Start();
            Transform ToTest = transform;
            for (int i = 0; i < 20; i++)
            {
                if (ToTest.TryGetComponent<ContentObject>(out ContentObject wordContent))
                {

                    if (wordContent.Content is Word) onClick.AddListener(() => { DiscretionViwe.ShowWord((wordContent.Content as Word?).Value, InspectorButtons.SoundButton & InspectorButtons.Score); });
                    else if (wordContent.Content is Dialog) onClick.AddListener(() => { DiscretionViwe.ShowDialog((wordContent.Content as Dialog?).Value, InspectorButtons.SoundButton & InspectorButtons.Score); });
                    break;
                }
                ToTest = ToTest.parent;
                if (ToTest == null) break;
            }
        }
    }
}
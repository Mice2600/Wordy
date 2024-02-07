using Study.TwoWordSystem;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Study.CopleFinder
{
    public class CorrectContent : MonoBehaviour
    {
        public List<(string text, Content content)> values = null;


        [SerializeField]
        private List<TMP_FontAsset> Fonts;
        [SerializeField]
        private TList<Sprite> Sprites;
        private void Start()
        {
            if (values == null) return;
            GetComponentsInChildren<TextMeshProUGUI>().ForEach((a, I) => 
            {
                if (!Regex.IsMatch(values[I].text, "^[a-zA-Z0-9]*$"))
                    a.font = Fonts[1];
                else a.font = Fonts[0];

                a.text = values[I].text; 
            
            });
            GetComponentsInChildren<Button>().ForEach((a, I) =>
            {
                int index = values[I].content.BaseCommander.IndexOf(values[I].content);
                a.GetComponent<Image>().sprite = Sprites[index, ListGetType.Loop];

                Content C = values[I].content;
                a.onClick.AddListener(() => DiscretionObject.Show(C));
            });


            

        }

    }
}
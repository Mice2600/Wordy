using System.Collections.Generic;
using System.Text.RegularExpressions;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Study.CopleFinder
{
    public class VisualThings : MonoBehaviour
    {
        private CopleFinderContent twoWordSystemContent => _twoWordSystemContent ??= GetComponent<CopleFinderContent>();
        private CopleFinderContent _twoWordSystemContent;
        private Animator animator => _animator ??= GetComponent<Animator>();
        private Animator _animator;

        [SerializeField]
        private List<TMP_FontAsset> Fonts;
        [SerializeField]
        private TextMeshProUGUI textMeshProUGUI;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TList<Sprite> Sprites;



        private void Start()
        {
            twoWordSystemContent.OnRefreshed += Refresh;
            Refresh();
        }
        void Refresh()
        {
            int NIndex = 0;
            if (twoWordSystemContent.Content != null)
            {
                NIndex = twoWordSystemContent.Content.BaseCommander.IndexOf(twoWordSystemContent.Content);

                if (!Regex.IsMatch(textMeshProUGUI.text, "^[a-zA-Z0-9]*$"))
                    textMeshProUGUI.font = Fonts[1];
                else textMeshProUGUI.font = Fonts[0];
            }
            _image.sprite = Sprites[NIndex, ListGetType.Loop];
        }
        void Update()
        {
            if (twoWordSystemContent.Dead)
            {
                float DeadDegre = TMath.MoveTowards(animator.GetFloat("Sellected"), -1, Time.deltaTime * 10);
                animator.SetFloat("Sellected", DeadDegre);
                return;
            }


            bool Issellected = (CopleFinderContent.FirstSellected == twoWordSystemContent || CopleFinderContent.SecondSellected == twoWordSystemContent);

            float Degre = Issellected ? 1f : 0f;
            if (!Issellected)
                if (twoWordSystemContent.IsFirst && CopleFinderContent.FirstSellected != null ||
                    !twoWordSystemContent.IsFirst && CopleFinderContent.SecondSellected != null)
                    Degre -= .5f;
            Degre = TMath.MoveTowards(animator.GetFloat("Sellected"), Degre, Time.deltaTime * 10);
            animator.SetFloat("Sellected", Degre);
        }

    }
}
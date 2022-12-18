using Base;
using System.Collections;
using SystemBox;
using UnityEngine;
namespace Study.TwoWordSystem
{
    public class TwoWordSystemContent : ContentObject
    {
        public bool IsEnglishSide;
        public static TwoWordSystemContent EnglishSellected;
        public static TwoWordSystemContent RussianSellected;
        [System.NonSerialized]
        public bool Dead;
        private void Start()
        {
            Refresh();
        }
        public void Refresh()
        {
            TList<Transform> da = transform.Childs();
            da.ForEach(t => t.gameObject.SetActive(false));
            da.RandomItem.transform.gameObject.SetActive(true);
        }

        private static TwoWordSystem TwoWordSystem => _TwoWordSystem ??= FindObjectOfType<TwoWordSystem>();
        private static TwoWordSystem _TwoWordSystem;
        public void TrySellect()
        {
            if (Dead) return;
            if (IsEnglishSide)
            {
                EnglishSellected = this;
            }
            else
            {
                RussianSellected = this;
            }
            if (EnglishSellected != null && RussianSellected != null)
            {
                if (EnglishSellected.Content.Equals(RussianSellected.Content))
                {
                    TwoWordSystemContent Ones = EnglishSellected;
                    EnglishSellected = null;
                    Ones.Dead = true;

                    TwoWordSystemContent Twos = RussianSellected;
                    RussianSellected = null;
                    Twos.Dead = true;

                    StartCoroutine(DeadTime());

                    IEnumerator DeadTime()
                    {
                        yield return new WaitForSeconds(1);
                        TwoWordSystem.TryChange(Ones, Twos);
                        Refresh();
                        Twos.Dead = false;
                        Ones.Dead = false;
                    }
                }
                else
                {
                    EnglishSellected = null;
                    RussianSellected = null;
                }
            }
        }
    }
}
using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Study.FindOns
{
    public class FindonesContent : ContentObject
    {
        private static FindOnsSystem FindOnsSystem => _FindOnsSystem ??= FindObjectOfType<FindOnsSystem>();
        private static FindOnsSystem _FindOnsSystem;
        private static bool Done;

        public static FindonesContent SellectedObject;
        private void Start()
        {
            SellectedObject = null;
        }
        public void OnSellected()
        {
            SellectedObject = this;
        }
    }
}
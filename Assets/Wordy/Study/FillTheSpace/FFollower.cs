using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SystemBox;
using UnityEngine;
namespace FillTheSpace
{


    public class FFollower : MonoBehaviour
    {
        static FillTheSpace fillTheSpace;
        static Dictionary<GameObject, FFollower> Places
        {
            get
            {
                if (_Places == null || fillTheSpace == null)
                {
                    fillTheSpace = FindObjectOfType<FillTheSpace>();
                    _Places = new Dictionary<GameObject, FFollower>();
                    fillTheSpace.PlaceBoxs.ForEach(place => Places.Add(place.gameObject, null));
                }
                return _Places;
            }
        }
        static Dictionary<GameObject, FFollower> _Places;

        void Start()
        {
            Sell = DownPos;
        }
        GameObject DownPos => transform.parent.gameObject;
        GameObject Sell;

        void Update()
        {
            transform.MoveTowards(Sell.transform.position, 100f);
        }

        public void OnButton()
        {

            bool siInList = false;
            Places.Values.ToList().ForEach(v =>
            {
                if (v == this) siInList = true;
            });
            if (siInList)
            {
                Places[Sell] = null;
                Sell.GetComponent<TMPro.TMP_Text>().text = "_";
                Sell = DownPos;
                return;
            }

            List<GameObject> KL = Places.Keys.ToList();
            for (int i = 0; i < KL.Count; i++)
            {
                if (Places[KL[i]] == null)
                {
                    Places[KL[i]] = this;
                    Sell = KL[i];
                    KL[i].GetComponent<TMPro.TMP_Text>().text = GetComponentInChildren<TMPro.TMP_Text>().text;
                    break;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Study.Ring
{
    public class Sellecter : MonoBehaviour 
    {



        public Vector3 F;
        public Vector3 S;

        private void Start()
        {
            GetComponent<RectTransform>().offsetMax = new Vector2(1, 100);
            GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        }

        private void Update()
        {

            float dis = Vector2.Distance(F, S);
            transform.localScale = new Vector3(dis, 0.2f, 0f);
            transform.position = (S + F) / 2f;
            transform.right = F - transform.position;

        }
        

    }
}
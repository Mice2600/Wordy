using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using SystemBox.Simpls;
using UnityEngine;
namespace Study.BuildTheBox
{
    public class PosFixer : OptimizedBehaver
    {


        private protected override bool UesPerUpdate => true;
        private protected override int PerUpdateTime => 10;

        [Button]
        protected override void PerUpdate()
        {
            float MaxUp = 0;
            transform.GetComponentsInChildren<BackBlock>().ForEach(d => { if (d.transform.position.y > MaxUp) MaxUp = d.transform.position.y; });
            MaxUp = MaxUp - transform.transform.position.y;

            float MaxHorizontal = 0;
            transform.GetComponentsInChildren<BackBlock>().ForEach(d => { if (TMath.Distance(transform.position.x, d.transform.position.x) > MaxHorizontal) MaxHorizontal = TMath.Distance(transform.position.x, d.transform.position.x); });


            float NeedY = 4 - MaxUp;
            transform.position = new Vector3(-(MaxHorizontal / 2), NeedY, transform.position.z);

        }


        private protected override void Start()
        {

            base.Start();
            PerUpdate();




        }

    }
}
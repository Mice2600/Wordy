using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using SystemBox.Simpls;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PosFixer : OptimizedBehaver
{


    private protected override bool UesPerUpdate => true;
    private protected override int PerUpdateTime => 10;

    protected override void PerUpdate()
    {
        Start();
    }

    [Button]
    private protected override void Start()
    {

        base.Start();

        float MaxUp = 0;
        transform.Childs().ForEach(d => { if (d.transform.position.y > MaxUp) MaxUp = d.transform.position.y; });
        MaxUp = MaxUp - transform.transform.position.y;

        float MaxHorizontal = 0;
        transform.Childs().ForEach(d => { if (TMath.Distance(transform.position.x, d.position.x) > MaxHorizontal) MaxHorizontal = TMath.Distance(transform.position.x, d.position.x); });



        float NeedY = 4 - MaxUp;
        transform.position = new Vector3(-(MaxHorizontal / 2), NeedY, transform.position.z);


    }

}

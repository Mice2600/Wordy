using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersanalData
{


    [Obsolete("Dont use Score use ScoreConculeated")]
    public float Score { get; set; }
    public bool Active { get; set; }

#pragma warning disable 612, 618
    public float ScoreConculeated
    {
        get => Score;
        set
        {

            if (value > 100) value = 100;
            else if (value < 0) value = 0;
            Score = value;

        }
    }
#pragma warning restore 612, 618

}

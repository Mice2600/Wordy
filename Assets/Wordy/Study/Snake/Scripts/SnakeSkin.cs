using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSkin : MonoBehaviour
{

    public GameObject Backgraund => SkinsChosen.Backgraund;
    public GameObject Way => SkinsChosen.Way;
    public GameObject OutSide => SkinsChosen.OutSide;
    public GameObject Player => SkinsChosen.Player;


    private int SS = -1;
    public Skin SkinsChosen 
    {
        get {

            if (SS == -1) SS = Random.Range(0, Skins.Count);    
            return Skins[SS]; 
        }
    }
    public List<Skin> Skins;

    [System.Serializable]
    public class Skin 
    {
        public GameObject Backgraund;
        public GameObject Way;
        public GameObject OutSide;
        public GameObject Player;
    }

}

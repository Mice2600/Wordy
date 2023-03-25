using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaseViwe : MonoBehaviour, IBaseNameUser
{
    Sprite IBaseNameUser.BaseImage => BaseImage;
    [SerializeField, Required]
    private Sprite BaseImage;
    string IBaseNameUser.BaseName => BaseName;
    [SerializeField]
    private string BaseName;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemBox;
using Sirenix.OdinInspector;
namespace ProjectSettings
{
    public partial class ProjectSettings : TabelScriptableObject
    {
        public static ProjectSettings Mine => _Mine ??= ProjectSettingsTabelResurse.Items[0];
        private static ProjectSettings _Mine;
    }
}
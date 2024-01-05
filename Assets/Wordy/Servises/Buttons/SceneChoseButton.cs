using Servises;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChoseButton : SceneLodeButton
{
    private CanvasGroup CanvasGroup => _CanvasGroup ??= GetComponent<CanvasGroup>();
    private CanvasGroup _CanvasGroup;
    private void Update()
    {
        CanvasGroup.alpha = (SceneManager.GetActiveScene().name != SceneName) ? .48f : 1f;
    }
    public override void OpenScene()
    {
        if (SceneManager.GetActiveScene().name == SceneName) return;
        base.OpenScene();
    }
}

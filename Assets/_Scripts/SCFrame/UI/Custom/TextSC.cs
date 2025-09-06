using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("UI/TextSC", 10)]
public class TextSC : Text
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if (Application.isPlaying)
            text = "need to translate";
    }
}
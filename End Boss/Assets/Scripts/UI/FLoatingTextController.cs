using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLoatingTextController : MonoBehaviour
{

    private static DamageTextScript popupTextPrefab;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupTextPrefab)
            popupTextPrefab = Resources.Load<DamageTextScript>("Prefabs/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        DamageTextScript instance = Instantiate(popupTextPrefab);
        instance.transform.SetParent(canvas.transform, false);
        instance.SetText(text);
    }
}

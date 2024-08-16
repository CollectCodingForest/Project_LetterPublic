using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스티커기능을 부여하고싶은 오브젝트에 Sticker컴포넌트를 추가하면 자동으로 에디터에 Btn게임오브젝트가 생성됩니다. 
/// Sticker컴포넌트를 추가하는 오브젝트는 반드시 MainObjectEvent가 있어야 합니다.
/// </summary>
[CustomEditor(typeof(Sticker))]
public class StickerEditor : Editor
{
    Type[] components = new Type[]
    {
            typeof(RectTransform),
            typeof(Image),
            typeof(Button)
    };

    void OnEnable()
    {
        Sticker sticker = (Sticker)target;
        
        if (sticker != null && sticker.GetCreatedGameObject() == null)
        {
            CreateTargetGameObject(sticker);
        }
    }

    private void CreateTargetGameObject(Sticker _sticker)
    {
        GameObject BtnSticker = new GameObject("BtnSticker", components);

        BtnSticker.transform.SetParent(_sticker.transform);
        BtnSticker.transform.localPosition = new Vector3(-305.9f, 358.3f, 0f);
        BtnSticker.GetComponent<RectTransform>().sizeDelta = new Vector2(30f, 30f);
        BtnSticker.GetComponent <Image>().color = Color.red;
        BtnSticker.GetComponent<Image>().enabled = false;
        _sticker.SetCreatedGameObject(BtnSticker);
    }
}
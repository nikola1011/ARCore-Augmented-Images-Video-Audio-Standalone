using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChange : MonoBehaviour {

    public Sprite OffSprite;
    public Sprite OnSprite;

    public void ChageSprite()
    {
        Image image = GetComponent<Image>();
        if (image.sprite == OnSprite)
            image.sprite = OffSprite;
        else
        {
            image.sprite = OnSprite;
        }
    }
}


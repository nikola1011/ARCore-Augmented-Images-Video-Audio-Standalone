using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChange : MonoBehaviour {

    public Sprite OffSprite;
    public Sprite OnSprite;

    private Color onColor = new Color(22, 77, 137);
    private Color offColor = Color.gray;

    public void ChangeSpriteColor ()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            if (image.color == onColor)
                image.color = offColor;
            else
                image.color = onColor;
        }
    }

    public void ChageSprite()
    {
        Image image = GetComponent<Image>();
        if (image.sprite == OnSprite)
        {
            image.sprite = OffSprite;
        }
        else
        {
            image.sprite = OnSprite;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        Image image = GetComponent<Image>();

        if (image != null)
            image.sprite = sprite;
        else
            Debug.Log("Image component not set in ButtonSpriteChange script");
    }
}


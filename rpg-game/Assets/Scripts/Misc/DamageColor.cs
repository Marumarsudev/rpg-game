using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColor : MonoBehaviour
{
    private Color origColor;

    private LTDescr colorTween;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        origColor = GetComponent<SpriteRenderer>().color;
        //Debug.Log(this.gameObject.name + origColor);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageAnim()
    {

        if(colorTween != null)
            LeanTween.cancel(this.gameObject, colorTween.id);
        colorTween = LeanTween.color(spriteRenderer.gameObject, Color.red, 0.2f)
            .setOnComplete(() => 
            { 
                LeanTween.color(spriteRenderer.gameObject, origColor, 0.1f);
            });
    }
}

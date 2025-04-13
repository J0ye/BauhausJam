using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    protected Tween releaseAnimation;
    protected Vector3 clickIntensity = new Vector3(0.2f, 0.2f, 0.2f);
    // Start is called before the first frame update
    private void OnMouseDrag()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        if (releaseAnimation != null)
        {
            releaseAnimation.Complete();
        }
        releaseAnimation = transform.DOPunchScale(transform.localScale - clickIntensity, 0.2f);
    }
}

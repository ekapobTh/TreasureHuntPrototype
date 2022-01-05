using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureHint : MonoBehaviour
{
    private string treasureName;
    public string TreasureName
    {
        get { return treasureName; }
        set { treasureName = value; }
    }

    private Image treasureImage;
    public Image TreasureImage
    {
        get { return treasureImage; }
        set { treasureImage = value; }
    }
    public void Initiate()
    {
        TreasureImage = GetComponent<Image>();
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }
    public void Correct()
    {
        StartCoroutine(DoFade());
    }

    private IEnumerator DoFade()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }
}

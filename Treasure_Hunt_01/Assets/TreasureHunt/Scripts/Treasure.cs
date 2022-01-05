using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Treasure : MonoBehaviour
    , IPointerClickHandler
{
    private float xMin;
    private float yMin;
    private float xMax;
    private float yMax;
    RectTransform objRect;
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
    private GameObject hintObj;
    public GameObject HintObj
    {
        get { return hintObj; }
        set { hintObj = value; }
    }
    public void Initiate()
    {
        TreasureImage = GetComponent<Image>();
        objRect = gameObject.GetComponent<RectTransform>();
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        objRect.offsetMax = Vector2.zero;
        objRect.offsetMin = Vector2.zero;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        hintObj.GetComponent<TreasureHint>().Correct();
        StartCoroutine(FadeAndDestroy(0.75f)) ;
    }
    private IEnumerator FadeAndDestroy(float waitTime)
    {
        TreasureImage.CrossFadeAlpha(0f, waitTime, false);
        yield return new WaitForSeconds(waitTime);
        if(transform.parent.childCount-1 <= 0)
        {
            GameObject.Find("Game Controller").GetComponent<TreasureHunter>().EndContent();
        }
        Destroy(gameObject);
    }
    public void SetSyncRect(float newMinX, float newMinY, float newMaxX, float newMaxY)
    {
        xMin = newMinX;
        yMin = newMinY;
        xMax = newMaxX;
        yMax = newMaxY;
    }
    public void UpdateRect()
    {
        var anchorMin = new Vector2(xMin, yMin);
        var anchorMax = new Vector2(xMax, yMax);
        objRect.anchorMin = anchorMin;
        objRect.anchorMax = anchorMax;
    }
}

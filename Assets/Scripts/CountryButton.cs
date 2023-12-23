using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CountryButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] string thisCountry;
    [SerializeField] Color hoverButtonColor;
    private Color originalButtonColor;
    [SerializeField] Color textColor;
    private Color originalTextColor;

    private void Start()
    {
        //originalButtonColor = sr.color;
        originalTextColor = nameText.color;
    }

    private void OnMouseEnter()
    {
        nameText.color = textColor;
    }
    private void OnMouseDown()
    {
        if(Time.timeScale == 1)
            GM.instance.Answer(thisCountry);
    }
    private void OnMouseExit()
    {
        nameText.color = originalTextColor;
    }
}

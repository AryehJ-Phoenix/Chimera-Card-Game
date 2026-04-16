using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Card : MonoBehaviour
{
    public Card_data data;

    public string card_name;
    public string description;
    public bool disjointed;
    public int aoeType;
    public int damage;
    public int range;
    public Sprite sprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI aoeTypeText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI disjointedText;
    public Image spriteImage;
    public Image back;
        

    // Start is called before the first frame update
    void Start()
    {
        card_name = data.card_name;
        description = data.description;
        disjointed = data.disjointed;
        aoeType = data.aoeType;
        damage = data.damage;
        sprite = data.sprite;
        nameText.text = card_name;
        descriptionText.text = description;
        damageText.text = damage.ToString();
        rangeText.text = range.ToString();
        if (disjointed == true) {disjointedText.text = "D";} else {disjointedText.text = "C";}
        spriteImage.sprite = sprite;
        if (aoeType == 1) {aoeTypeText.text = "P";}
        else if (aoeType == 2) {aoeTypeText.text = "L";}
        else if (aoeType == 3) {aoeTypeText.text = "C";}

        transform.Rotate(0,180,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.rotation.y > 0) {transform.Rotate(0,-2,0);}

        if (transform.rotation.y < 0.75) {back.color = new(1,0,0,0);}
    }
}

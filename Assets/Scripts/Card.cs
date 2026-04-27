using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Net.NetworkInformation;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
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
    public Vector3 goal = new(0,0,0);
    float speed = 200;
    GameManager GM;
    Player_CardsManager CM;
    public Vector3 offset;
    public Button myButton;
    public bool isFollowing = false;
    public Slots slot = null;
    RectTransform rectTransform;
        

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
        GM = FindAnyObjectByType<GameManager>();
        CM = FindAnyObjectByType<Player_CardsManager>();

        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.rotation.y > 0) {transform.Rotate(0,-5,0);}

        if (transform.rotation.y < 0.75) {back.color = new(1,0,0,0);}

        if (Vector3.Distance(transform.position,goal) > 1 && !isFollowing)
        {
            transform.position = Vector3.MoveTowards(transform.position,goal,speed*Time.deltaTime);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Starting to drag " + gameObject.name);
        isFollowing = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GM.canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("Ending dragging " + gameObject.name);
        isFollowing = false;
        Discard();
    }

    void Discard()
    {
        print("Discarding " + name);
        slot.open = true;
        CM.discard.Add(data);
        CM.hand.Remove(data);
        Destroy(gameObject);
    }
}

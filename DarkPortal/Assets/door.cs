using System;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Canvas canvasForTavernaBtn;
    public Button goToTaverna;
    public GameObject positionInTaverna;
    public Player player;
    public GameObject ShopAssistent;
    public Transform localShopXYZ;
    public Transform localShopXYZInTawerna;
    private bool locateShop;


    private void Start()
    {
        goToTaverna.onClick.AddListener(GoToTaverna);
        canvasForTavernaBtn.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = GameObject.FindWithTag("Enemy");
        if (other.CompareTag("Player") 
            && !other.gameObject.GetComponent<Player>().fight)
        {
            canvasForTavernaBtn.enabled = true;
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasForTavernaBtn.enabled = false;
        }
    }

    public void GoToTaverna()
    {
        player.transform.position = positionInTaverna.transform.position;
        
        if (!locateShop)
        {
            ShopAssistent.transform.position = localShopXYZInTawerna.position;
            locateShop = false;
            ShopAssistent.GetComponent<SpriteRenderer>().flipX = !ShopAssistent.GetComponent<SpriteRenderer>().flipX;
        }
        else
        {
            ShopAssistent.transform.position = localShopXYZ.position;
            ShopAssistent.GetComponent<SpriteRenderer>().flipX = !ShopAssistent.GetComponent<SpriteRenderer>().flipX;
            locateShop = true;
        }
    }
}

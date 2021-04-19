using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ArrayList AuctionItems = new ArrayList();
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToCart(string name, string price)
    {
        string[] item = new string[] { name, price };
        AuctionItems.Add(item);
    }
}

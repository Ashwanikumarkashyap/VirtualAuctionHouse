using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCheckoutBoard : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ItemPrefab;
    private GameObject[] ItemSelection;
    private int index = 0;
    void Start()
    {
        ShoppingCartManager cart = GameObject.Find("ShoppingCartManager").GetComponent<ShoppingCartManager>();
        ArrayList AuctionItems = cart.AuctionItems;
        int total = 0;

        for (int i=0;i<AuctionItems.Count;i++)
        {
            
            var newItem = Instantiate(ItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newItem.transform.parent = GameObject.Find("ItemList").transform;
            newItem.transform.localPosition = new Vector3(0, 0, 0);
            newItem.transform.localRotation = Quaternion.identity;

            string[] item = (string[])AuctionItems[i];
            TMPro.TextMeshProUGUI itemName = newItem.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>();
            itemName.SetText(item[0]);

            TMPro.TextMeshProUGUI itemPrice = newItem.transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>();
            itemPrice.SetText("$" + item[1]);
            total += int.Parse(item[1]);

            TMPro.TextMeshProUGUI totalPrice = GameObject.Find("Total").GetComponent<TMPro.TextMeshProUGUI>();
            totalPrice.SetText("Total Amount: $" + total.ToString());
        }


        ItemSelection = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            ItemSelection[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in ItemSelection)
        {
            go.SetActive(false);
        }

        if (ItemSelection[0])
        {
            ItemSelection[0].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        ItemSelection[index].SetActive(false);

        index--;
        if (index < 0)
            index = ItemSelection.Length - 1;

        ItemSelection[index].SetActive(true);
    }

    public void ToggleRight()
    {
        ItemSelection[index].SetActive(false);

        index++;
        if (index == ItemSelection.Length)
            index = 0;

        ItemSelection[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

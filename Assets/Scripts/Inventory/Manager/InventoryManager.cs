using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InventoryManager : MonoBehaviour
{
    [BoxGroup("UI")]
    public GameObject inventoryGO;
    [BoxGroup("UI")]
    public GameObject itemUIPrefab;
    [BoxGroup("UI")]
    public Transform itemsUIParent;

    private List<Item> items;

    public static InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        items = new List<Item>();
    }

    public void SetInventory()
    {
        ClearInventory();
        foreach(Item item in items)
        {
            GameObject tempGO = Instantiate(itemUIPrefab, itemsUIParent);
            tempGO.GetComponent<ItemButton>().SetItem(item);
            UI_Initialiser temp = tempGO.GetComponent<UI_Initialiser>();
            temp.SetText(item.name);
            temp.SetImage(item.sprite);
        }
    }

    public void ClearInventory(bool removeItemsFromListAsWell = false)
    {
        for(int i = 0; i < itemsUIParent.childCount; i++)
        {
            Destroy(itemsUIParent.GetChild(i).gameObject);
        }

        if(!removeItemsFromListAsWell)
            return;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        item.gameObject.GetComponent<Renderer>().enabled = false;
        item.gameObject.GetComponent<Collider>().enabled = false;
        item.enabled = false;
        SoundManager.PlaySound(SoundType.SurgicalToolCollect);

        SetInventory();
    }
    
    public void RemoveItem(Item item, bool isUsed = false, bool showInventory = true)
    {
        items.Remove(item);
        item.gameObject.GetComponent<Renderer>().enabled = !isUsed;
        item.gameObject.GetComponent<Collider>().enabled = !isUsed;
        item.enabled = !isUsed;

        SetInventory();
    }

    public Item FindItem(int id)
    {
        Item temp = items.Find(x => x.id == id);
        return temp;
    }
}

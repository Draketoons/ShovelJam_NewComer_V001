using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<ItemProfile> itemList;
    private UIManager uiManager;
    bool openInventory;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            openInventory = !openInventory;
        }
        if (openInventory)
        {
            uiManager.DisplayInventory(itemList);
        }
        if (!openInventory)
        {
            uiManager.CloseInventoryUI();
        }
    }

    public void FindUIManager()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
    }

    public void AddItemToInventory(ItemProfile itemToAdd)
    {
        Debug.Log($"You have recieved: {itemToAdd}");
        uiManager.OpenUpdateUI();
        uiManager.DisplayUpdateText($"You got 1 {itemToAdd.itemName}!");
        itemList.Add(itemToAdd);
    }

    public void GiveItem(CharacterProfile character, ItemProfile itemToGive)
    {
        Debug.Log($"Gave {character.characterName} 1 {itemToGive}");
        uiManager.OpenUpdateUI();
        uiManager.DisplayUpdateText($"You gave {character.characterName} 1 {itemToGive.itemName}");
        RemoveItemFromInventory(itemToGive);
    }

    public bool FindItem(ItemProfile itemToFind)
    {
        bool found = false;
        foreach (ItemProfile item in itemList)
        {
            if (item == itemToFind) found = true;
            else
            {
                found = false;
            }
        }
        return found;
    }

    public void RemoveItemFromInventory(ItemProfile itemToRemove)
    {
        itemList.Remove(itemToRemove);
    }
}

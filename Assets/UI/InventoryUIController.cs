using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    public Texture2D itemImage;
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();

    private VisualElement m_Root;
    private VisualElement m_SlotContainer;

    private void Awake()
    {
        //Store the root from the UI Document component
        m_Root = GetComponent<UIDocument>().rootVisualElement;

        //Search the root for the SlotContainer Visual Element
        m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");

        // //Create InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < 4; i++)
        {
            InventorySlot item = new InventorySlot();
        
            InventoryItems.Add(item);
        
            m_SlotContainer.Add(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddToInventory();
        }
    }

    private void AddToInventory()
    {
        var slot = m_SlotContainer.Query(className: "slotIcon").First();
        slot.style.backgroundImage = new StyleBackground(itemImage); // Set background image
        // // Create a new InventorySlot
        // InventorySlot newItem = new InventorySlot
        // {
        //     Image = itemImage
        // };
        //
        // // Add the new item to the inventory list
        // InventoryItems.Add(newItem);
        //
        // // Create a new VisualElement to represent the inventory slot in the UI
        // VisualElement newSlot = new VisualElement();
        // newSlot.style.width = 64;  // Set width of the slot
        // newSlot.style.height = 64; // Set height of the slot
        // newSlot.style.backgroundImage = new StyleBackground(itemImage); // Set background image
        //
        // // Add the new slot to the SlotContainer in the UI
        // m_SlotContainer.Add(newSlot);
    }
}

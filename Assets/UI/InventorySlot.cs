using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public VisualElement Icon;
    public string ItemGuid = "";
    public bool occupied;
    public Texture2D Image;

    public InventorySlot()
    {
        // //Create a new Image element and add it to the root
        Icon = new VisualElement();
        Add(Icon);

        //Add USS style properties to the elements
        Icon.AddToClassList("slotIcon");
        AddToClassList("inventorySlot");
    }
    
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
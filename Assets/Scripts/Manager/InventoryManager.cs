using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Data")]
    private Dictionary<CatType, int> inventory = new();

    [Header("UI References")]
    public GameObject inventoryPanel;
    public Transform contentParent;
    public GameObject inventoryItemPrefab;

    private bool isOpen = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ToggleInventory();
    }

    public void AddToInventory(CatType gem)
    {
        if (!inventory.ContainsKey(gem))
            inventory[gem] = 0;

        inventory[gem]++;
    }

    private void ToggleInventory()
    {
        AudioManager.Instance.PlayInventoryOpen();
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        Time.timeScale = isOpen ? 0 : 1;

        if (isOpen)
            RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var kvp in inventory)
        {
            GameObject uiItem = Instantiate(inventoryItemPrefab, contentParent);
            Image icon = uiItem.GetComponentInChildren<Image>();
            TextMeshProUGUI qtyText = uiItem.GetComponentInChildren<TextMeshProUGUI>();

            Sprite sprite = kvp.Key.visualPrefab.GetComponent<SpriteRenderer>().sprite;
            if (icon != null && sprite != null)
                icon.sprite = sprite;

            if (qtyText != null)
                qtyText.text = $"x{kvp.Value}";
        }
    }
    
    public Dictionary<CatType, int> GetInventory()
    {
        return inventory;
    }
}
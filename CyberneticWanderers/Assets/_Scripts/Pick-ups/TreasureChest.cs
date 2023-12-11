using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    InventoryManager inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            OpenTreasureCheast();
            Destroy(gameObject);
        }
    }

    public void OpenTreasureCheast()
    {
        if(inventory.GetPossibleEvolutions().Count <= 0)
        {
            Debug.LogWarning("NO AVAILABLE EVOLVE");
            return;
        }
        WeaponEvolutionBlueprint toEvolve = inventory.GetPossibleEvolutions()[Random.Range(0, inventory.GetPossibleEvolutions().Count)];
        inventory.EvolveWeapon(toEvolve);
    }
}

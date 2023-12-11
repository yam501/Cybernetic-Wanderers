using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> PassiveItemUISlots = new List<Image>(6);

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public int passiveItemUpgradeIndex;
        public GameObject initialPassiveItem;
        public PassiveItemScriptableObject passiveItemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI upgradeNameDisplay;
        public TextMeshProUGUI upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponsUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    public List<WeaponEvolutionBlueprint> weaponEvolutions = new List<WeaponEvolutionBlueprint>();  

    PlayerStats player;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;

        if (GameManager.instance != null && GameManager.instance.choosingUpgrade) 
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        PassiveItemUISlots[slotIndex].enabled = true;
        PassiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;

        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if(weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            if(!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LVL FOR" + weapon.name);
                return;
            }

            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<WeaponController>().weaponData.Level;

            weaponsUpgradeOptions[upgradeIndex].weaponData = upgradedWeapon.GetComponent<WeaponController>().weaponData;

            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItem(int slotIndex, int upgradeIndex)
    {
        if(passiveItemSlots.Count > slotIndex)
        {

            PassiveItem passiveItem = passiveItemSlots[slotIndex];
            if(!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LVL FOR" + passiveItem.name);
                return;
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;

            passiveItemUpgradeOptions[upgradeIndex].passiveItemData = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData;

            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> avaliableWeaponUpgrades = new List<WeaponUpgrade>(weaponsUpgradeOptions);
        List<PassiveItemUpgrade> avaliablePassiveItemUpgrades = new List<PassiveItemUpgrade>(passiveItemUpgradeOptions);

        foreach (var upgradeOption in upgradeUIOptions)
        {
            if (avaliablePassiveItemUpgrades.Count == 0 && avaliableWeaponUpgrades.Count == 0)
            {
                return;
            }

            int upgradeType;

            if(avaliableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }
            else if (avaliablePassiveItemUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);
            }


            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = avaliableWeaponUpgrades[Random.Range(0, avaliableWeaponUpgrades.Count)];

                avaliableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                if (chosenWeaponUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);

                    bool newWeapon = false;
                    for (int i = 0; i< weaponSlots.Count; i++)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;

                            if (!newWeapon)
                            {
                                if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab) //No more upgrades for weapon
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }

                                upgradeOption.upgradeButton.onClick.AddListener(()=> LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex));
                                Debug.Log(chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description);
                                upgradeOption.upgradeDescriptionDisplay.text=chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                                upgradeOption.upgradeNameDisplay.text=chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon) //Spawn new weapon
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));

                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                }
            }
            else if (upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade = avaliablePassiveItemUpgrades[Random.Range(0, avaliablePassiveItemUpgrades.Count)];

                avaliablePassiveItemUpgrades.Remove(chosenPassiveItemUpgrade);

                if (passiveItemUpgradeOptions != null)
                {
                    EnableUpgradeUI(upgradeOption);

                    bool newPassiveItem = false;
                    for (int i = 0; i < passiveItemSlots.Count; i++)
                    {
                        if (passiveItemSlots[i] != null && passiveItemSlots[i].passiveItemData == chosenPassiveItemUpgrade.passiveItemData)
                        {
                            newPassiveItem = false;

                            if (!newPassiveItem)
                            {
                                if (!chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab) //No more upgrades for passive item
                                {
                                    DisableUpgradeUI(upgradeOption);

                                    break;
                                }

                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i, chosenPassiveItemUpgrade.passiveItemUpgradeIndex));

                                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newPassiveItem = true;
                        }
                    }
                    if (newPassiveItem) //Spawn new weapon
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemUpgrade.initialPassiveItem));

                        upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenPassiveItemUpgrade.passiveItemData.Icon;
                }
            }
        }
    }

    void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption);
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }

    void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }

    public List<WeaponEvolutionBlueprint> GetPossibleEvolutions()
    {
        List<WeaponEvolutionBlueprint> possibleEvolutions = new List<WeaponEvolutionBlueprint>();

        foreach(WeaponController weapon in weaponSlots)
        {
            if(weapon != null)
            {
                foreach (PassiveItem catalyst in passiveItemSlots)
                {
                    if (catalyst != null)
                    {
                        foreach (WeaponEvolutionBlueprint evolution in weaponEvolutions)
                        {
                            if (weapon.weaponData.Level >= evolution.baseWeaponData.Level && catalyst.passiveItemData.Level >= evolution.catalystPassiveItemData.Level)
                            {
                                possibleEvolutions.Add(evolution);
                            }
                        }

                    }
                }

            }
        }
        return possibleEvolutions;
    }

    public void EvolveWeapon(WeaponEvolutionBlueprint evolution)
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            WeaponController weapon = weaponSlots[i];

            if (!weapon) continue;

            for (int j = 0; j < passiveItemSlots.Count; j++)
            {
                PassiveItem catalist = passiveItemSlots[j];

                if (!catalist) continue;

                if (weapon && catalist && weapon.weaponData.Level >= evolution.baseWeaponData.Level && catalist.passiveItemData.Level >= evolution.catalystPassiveItemData.Level) 
                {
                    GameObject evolvedWeapon = Instantiate(evolution.evolvedWeapon, transform.position, Quaternion.identity);
                    WeaponController evolvedWeaponController = evolvedWeapon.GetComponent<WeaponController>(); 

                    evolvedWeapon.transform.SetParent(transform);
                    AddWeapon(i, evolvedWeaponController);
                    Destroy(weapon.gameObject);

                    weaponLevels[i] = evolvedWeaponController.weaponData.Level;
                    weaponUISlots[i].sprite = evolvedWeaponController.weaponData.Icon;

                    weaponsUpgradeOptions.RemoveAt(evolvedWeaponController.weaponData.EvolvedUpgradeToRemove);

                    Debug.LogWarning("EVOLVED!");

                    return;
                }
            }
        }
    }
}

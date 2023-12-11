using UnityEngine;

[CreateAssetMenu (fileName = "WeaponEvolutionBlueprint", menuName = "ScriptableObject/WeaponEvolutionBlueprint")]

public class WeaponEvolutionBlueprint : ScriptableObject
{
    public WeaponScriptableObject baseWeaponData;
    public WeaponScriptableObject evolvedWeaponData;
    public PassiveItemScriptableObject catalystPassiveItemData;
    public GameObject evolvedWeapon;
}

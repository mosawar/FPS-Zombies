using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject currentWeapon;   // The weapon the player is currently holding
    public Transform weaponHolderM1911;
    public Transform weaponHolderUzi;
    public Transform weaponHolderM4;
    public Transform weaponHolderAK74;

    public void EquipWeapon(GameObject newWeaponPrefab, string weaponName)
    {
        Transform targetHolder = null;
        Quaternion adjustedRotation = Quaternion.identity;

        switch (weaponName)
        {
            case "M1911":
                targetHolder = weaponHolderM1911;
                break;
            case "Uzi":
                targetHolder = weaponHolderUzi;
                adjustedRotation = Quaternion.Euler(0, 179.599f, 0); // Correct Uzi rotation
                break;
            case "M4":
                targetHolder = weaponHolderM4;
                adjustedRotation = Quaternion.Euler(0, 173f, 0); // Correct Uzi rotation
                break;
            case "AK74":
                targetHolder = weaponHolderAK74;
                adjustedRotation = Quaternion.Euler(0, -182f, 0); // Correct Uzi rotation
                break;
        }

        // Check if the player already has the weapon equipped
        if (currentWeapon != null && currentWeapon.name == weaponName)
        {
            Debug.Log("Already holding this weapon!");
            return; // Exit if the weapon is already equipped
        }

        // Destroy the current weapon
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Instantiate the weapon
        currentWeapon = Instantiate(newWeaponPrefab);

        // Set position, parent, and rotation
        currentWeapon.transform.SetParent(targetHolder);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = adjustedRotation;

        Debug.Log($"Equipped: {weaponName}");
    }
    private void Awake()
    {
        // Set M1911 as the starting weapon
        currentWeapon = weaponHolderM1911.GetChild(0).gameObject; // Assumes M1911 is the first child of WeaponHolderM1911
    }
}

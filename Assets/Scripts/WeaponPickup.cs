using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; // The weapon prefab to equip
    public string weaponName;       // The name of the weapon (e.g., "Uzi", "M4")
    public float holdTime = 1f;     // How long to hold E to pick up
    public Transform playerCamera; // Assign the player's camera here
    private float holdProgress = 0f; // Tracks how long E is held
    private bool isLookingAt = false;

    private void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Check if the player is looking at this object
        if (Physics.Raycast(ray, out hit, 5f)) // Adjust the distance as needed
        {
            if (hit.collider.gameObject == gameObject)
            {
                isLookingAt = true;

                // Check if the player is holding E
                if (Input.GetKey(KeyCode.E))
                {
                    holdProgress += Time.deltaTime;

                    if (holdProgress >= holdTime)
                    {
                        PickUpWeapon();
                    }
                }
                else
                {
                    holdProgress = 0f; // Reset progress if E is released
                }
            }
            else
            {
                isLookingAt = false;
                holdProgress = 0f;
            }
        }
        else
        {
            isLookingAt = false;
            holdProgress = 0f;
        }
    }

    private void PickUpWeapon()
    {
        // Find the player's WeaponManager
        WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
        if (weaponManager != null)
        {
            weaponManager.EquipWeapon(weaponPrefab, weaponName);
        }

        // Destroy the pickup object
        Destroy(gameObject);
    }
}


using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public List<Weapon> weaponSlots = new List<Weapon>();
    public int activeWeaponIndex ;  // Default to the first weapon slot

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
     }




    public Weapon GetActiveWeapon()
    {
        Debug.Log($"Active Weapon Index: {activeWeaponIndex}");
       
            // Ensure the weaponSlots list is not empty and that the activeWeaponIndex is within valid range
            if (weaponSlots.Count > 0 && activeWeaponIndex >= 0 && activeWeaponIndex < weaponSlots.Count)
            {
                return weaponSlots[activeWeaponIndex];


            }
            else
            {
                // Log more detailed info to help debug
                Debug.LogWarning($"Active weapon index ({activeWeaponIndex}) is out of range or weaponSlots is empty.");
                return null;  // Return null if there's an issue
            }
        
    }


    public void SwitchWeapon(int newWeaponIndex)
    {
        Debug.Log($"Attempting to switch to weapon index {newWeaponIndex}");
        if (newWeaponIndex >= 0 && newWeaponIndex < weaponSlots.Count)
        {
            activeWeaponIndex = newWeaponIndex;
            Debug.Log($"Switched to weapon index {activeWeaponIndex}");

            // Call the method to update the ammo bar UI
            AmmoBarManager.Instance.Update();
        }
        else
        {
            Debug.LogWarning("Attempted to switch to a weapon index out of range.");
        }
    }



}

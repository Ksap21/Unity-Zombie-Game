
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    // Public variable to store selected weapon index
    public int selectedWeapon = 0;
    public Transform[] Weapons;


    void Start()
    {
        ChangeWeapon(selectedWeapon);  // Ensure the correct weapon is selected at the start
    }
    
    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput>0)
        {
            ChangeWeapon(0);
        }
       else if (scrollInput < 0)
        {
            ChangeWeapon(1);
        }
    }

    // Select weapon based on the index and set visibility accordingly
    void ChangeWeapon(int num)
    {
        selectedWeapon = num;
        WeaponManager.Instance.SwitchWeapon(selectedWeapon);
        for (int i = 0; i < Weapons.Length; i++)
        {
            bool isActive = (i == selectedWeapon);
            Weapons[i].gameObject.SetActive(isActive);
        }

     
    }

}

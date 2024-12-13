using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBarManager : MonoBehaviour
{
    public static AmmoBarManager Instance { get; set; }

    [Header("UI Elements")]
    public Image weaponImage;
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    public Sprite emptySlot;

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

    public void Update()
    {
        Debug.Log($"Active Weapon Index in AmmoBar: {WeaponManager.Instance.activeWeaponIndex}");

        Weapon activeWeapon = WeaponManager.Instance.weaponSlots[WeaponManager.Instance.activeWeaponIndex];
        Debug.Log($"Active Weapon Index in AmmoBar: {WeaponManager.Instance.activeWeaponIndex}");

        Weapon unActiveWeapon = GetUnactiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon)
        {
            
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft }";
            totalAmmoUI.text = $"{activeWeapon.magazineSize }";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);
            activeWeaponUI.sprite = GetWeaponSprite(activeWeapon.thisWeaponModel);

            
        }
        if (unActiveWeapon)
        {


            unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
        }
        else
        {
            magazineAmmoUI.text = " ";
            totalAmmoUI.text = " ";
        }


        
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {

        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                Debug.Log("Loading Pistol Sprite");
                return Resources.Load<GameObject>("Pistol_Weapon").GetComponent<SpriteRenderer>().sprite;
                
            case Weapon.WeaponModel.Rifle:
                Debug.Log("Loading Rifle Sprite");
                return Resources.Load<GameObject>("Rifle_Weapon").GetComponent<SpriteRenderer>().sprite;
                
            default:
                return null;
                Debug.Log("Default Weapon Loaded");
        }
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                return Instantiate(Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite;
            case Weapon.WeaponModel.Rifle:
                return Instantiate(Resources.Load<GameObject>("Rifle_Ammo")).GetComponent<SpriteRenderer>().sprite;
            default:
                return emptySlot;
        }
    }

    private GameObject GetUnactiveWeaponSlot()
    {
        foreach (Weapon weapon in WeaponManager.Instance.weaponSlots)
        {
            // Skip the active weapon and return the unactive weapon's GameObject
            if (weapon != WeaponManager.Instance.weaponSlots[WeaponManager.Instance.activeWeaponIndex])
            {
                return weapon.gameObject;  // Return the GameObject of the unactive weapon
            }
        }
        return null;  // If no unactive weapon is found, return null
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour /*, IDataPersistance*/
{
    public static GunManager Instance { get; private set; }
    public enum gunType { pistol = 0, rifle = 1, shotgun = 2, assaultRifle = 3 };
    //Variables
    [SerializeField] GameObject[] prefabs;   //All the gun prefabs that should be instanciated
    List<GameObject> instanciatedWeapons = new();//To save all instanciated prefabs in a list
    List<GameObject> unlockedWeapons = new();//To save all the weapons that are Unlocked
    GameObject currentActiveWeapon;         //Saves the current active weapon
    Gun currentGunScript;                   //Saves the current gun script
    Camera cam;                             //Gets the camera script
    public LayerMask mask;                  //Gets the enemy layer mask
    gunType currentGun;                     //Makes a gunType
    private bool reloading = false;         //bool to in shure reload is only called once at a time
    private bool shooting = false;
    private PlayerUI playerUI;              //Gets the curren player UI
    private InputManager inputManager;
    private RaycastHit gunHit;
    private void Awake()
    {
        if (Instance != null || Instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        inputManager = InputManager.Instance;
        playerUI = PlayerUI.Instance;
        cam = Camera.main;
        InstanciateWeapons();
        playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
    }
    private void Update() => Physics.Raycast(cam.transform.position, cam.transform.forward, out gunHit, currentGunScript.range, mask);
    //Instanciating Weapons
    /// <summary>
    /// Instantiates each weapon prefab in the prefabs list and adds them to the instanciatedWeapons list. If a weapon is active and unlocked,
    /// it is also added to the unlockedWeapons list and set as the current active weapon with its Gun component stored in currentGunScript.
    /// Otherwise, the weapon is added to the unlockedWeapons list but remains inactive.
    /// </summary>
    private void InstanciateWeapons()
    {
        foreach (var weapon in prefabs)
        {
            Vector3 position = transform.position;
            var obj = Instantiate(weapon, position, weapon.transform.rotation, transform);
            instanciatedWeapons.Add(obj);
            var script = obj.GetComponent<Gun>();
            if (script.weaponActive == true && script.weaponUnlocked == true)
            {
                unlockedWeapons.Add(obj); //Adds a weapon to the active weapon list
                currentActiveWeapon = unlockedWeapons[0];
                currentGunScript = script;
            }else if (script.weaponUnlocked == true)
            {
                unlockedWeapons.Add(obj);
                obj.SetActive(false);
            }     
        }
    }
    //Reloading
    /// <summary>
    /// Triggers reloading the current gun if it is not already reloading and there is not a full clip.
    /// </summary>
    public void Reload()
    {
        //Debug.Log("Reloading Gun");
        if (reloading) return;
        if (currentGunScript.ammunition == currentGunScript.ammunitionMax) return;
        StartCoroutine(ReloadCorutine());
    }
    /// <summary>
    /// A coroutine that represents reloading the current gun. Sets the reloading and shooting flags, waits for the current gun's
    /// reload time, refills the current gun's ammunition, updates the player's UI, and resets the reloading and shooting flags.
    /// </summary>
    /// <returns>A coroutine that can be yielded in another method.</returns>
    public IEnumerator ReloadCorutine()
    {
        reloading = true;
        shooting = true;
        yield return new WaitForSeconds(currentGunScript.realoadTime);
        currentGunScript.ammunition = currentGunScript.ammunitionMax;
        playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
        reloading = false;
        shooting = false;
    }
    //Shooting
    /// <summary>
    /// Triggers shooting the current gun if the player is not already shooting and there is ammunition left.
    /// </summary>
    public void Shoot()
    {
        //Debug.Log("Shooting");
        if (shooting) return;
        if (currentGunScript.ammunition <= 0) return;
        StartCoroutine(ShootCorutine());
        //Debug.Log("Shooting End");
    }
    /// <summary>
    /// A coroutine that represents shooting a gun. Decrements the current gun's ammunition, updates the player's UI,
    /// waits for the current gun's shoot cooldown, and hits an enemy if one is hit by the gun's raycast.
    /// </summary>
    /// <returns>A coroutine that can be yielded in another method.</returns>
    public IEnumerator ShootCorutine()
    {
        shooting = true;
        currentGunScript.ammunition--;
        playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
        //Debug.Log("Shoot Gun");
        var enemy = gunHit.collider?.gameObject.GetComponent<Enemy>();
        yield return new WaitForSeconds(currentGunScript.shootCooldown);
        if (enemy != null)
        {
            enemy.Hit(currentGunScript.damage);
        }
        shooting = false;
        //Debug.Log("Shoot End");
    }
    /// <summary>
    /// Switches the active weapon to the next or previous one in the list of active weapons, based on the value of change.
    /// </summary>
    /// <param name="change">A positive or negative integer indicating the direction of the weapon switch.</param>
    /// <remarks>
    /// If the new active weapon is unlocked, sets the weaponActive property of the previous and new weapons accordingly.
    /// </remarks>
    public void SwitchWeapon(int change)
    {
        //Debug.Log("Switch Weapon");
        var nextGun = ((int)currentGun + change + unlockedWeapons.Count) % unlockedWeapons.Count;
        currentGunScript = currentActiveWeapon.GetComponent<Gun>();
        var nextGunScript = unlockedWeapons[nextGun].GetComponent<Gun>();
        if (nextGunScript.weaponUnlocked)
        {
            currentGun += change;
            currentGunScript.weaponActive = false;
            currentActiveWeapon.SetActive(false);
            nextGunScript.weaponActive = true;
            currentActiveWeapon = unlockedWeapons[nextGun];
            currentGunScript = currentActiveWeapon.GetComponent<Gun>();
            currentActiveWeapon.SetActive(true);
            playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
            //Debug.Log("Switching weapon success");
        }
    }
    //Save and Load the weapons
    //public void SaveData(ref SaveData data)
    //{
    //    data.unlockedWeapons = this.unlockedWeapons ;
    //    data.currentActiveWeapon = this.currentActiveWeapon;
    //}
    //public void LoadData(SaveData data)
    //{
    //    this.unlockedWeapons = data.unlockedWeapons;
    //    this.currentActiveWeapon = data.currentActiveWeapon ;
    //}
}

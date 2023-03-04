using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager Instance { get; private set; }
    public enum gunType { pistol = 0, rifle = 1, shotgun = 2, assaultRifle = 3 };

    [SerializeField] GameObject[] prefabs;   //All the gun prefabs that should be instanciated
    List<GameObject> instanciatedWeapons = new();   //To save all instanciated prefabs in a list
    List<GameObject> unlockedWeapons = new();         //To save all the weapons that are Unlocked
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
    private void Update()
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out gunHit, currentGunScript.range, mask);
    }
    private void InstanciateWeapons()
    {
        foreach (var weapon in prefabs)
        {
            Vector3 position = transform.position;
            var obj = Instantiate(weapon, position, Quaternion.identity,transform);
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
            }     
        }
    }
    //Reloading
    public void Reload()
    {
        Debug.Log("Reloading Gun");
        if (reloading) return;
        StartCoroutine(ReloadCorutine());
    }
    public IEnumerator ReloadCorutine()
    {
        reloading = true;
        yield return new WaitForSeconds(currentGunScript.realoadTime);
        currentGunScript.ammunition = currentGunScript.ammunitionMax;
        playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
        reloading = false;
    }
    //Shooting
    public void Shoot()
    {
        if (shooting) return;
        if (currentGunScript.ammunition <= 0) return;
        StartCoroutine(ShootCorutine());
    }
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
        Debug.Log("Switch Weapon");
        var nextGun = ((int)currentGun + change + unlockedWeapons.Count) % unlockedWeapons.Count;
        currentGunScript = currentActiveWeapon.GetComponent<Gun>();
        var nextGunScript = unlockedWeapons[nextGun].GetComponent<Gun>();
        if (nextGunScript.weaponUnlocked)
        {
            currentGunScript.weaponActive = false;
            nextGunScript.weaponActive = true;
            currentActiveWeapon = unlockedWeapons[nextGun];
            currentGunScript = currentActiveWeapon.GetComponent<Gun>();
        }
    }
    //TODO: Implement the Aim feature when enogth Time left
    public void Aim()
    {

    }
}

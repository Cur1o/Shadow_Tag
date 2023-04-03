using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GunManager : MonoBehaviour , IDataPersistance
{
    public static GunManager Instance { get; private set; }
    public enum gunType { pistol = 0, rifle = 1, shotgun = 2, assaultRifle = 3 };
    //Variables
    [SerializeField] GameObject[] prefabs;   //All the gun prefabs that should be instanciated
    public List<GameObject> instanciatedWeapons = new();//To save all instanciated prefabs in a list
    [SerializeField] List<bool> weaponUnlockSave = new();//To save all unlocked Weapons to save them 
    List<GameObject> unlockedWeapons = new();//To save all the weapons that are Unlocked
    GameObject currentActiveWeapon;         //Saves the current active weapon
    Gun currentGunScript;                   //Saves the current gun script
    Animator currentAnimator;               //The current Animater from the current gun
    AudioSource currentAudioSource;
    Camera cam;                             //Gets the camera script
    public LayerMask mask;                  //Gets the enemy layer mask
    gunType currentGun;                     //Makes a gunType
    private bool reloading = false;         //bool to in shure reload is only called once at a time
    private bool shooting = false;
    private PlayerUI playerUI;              //Gets the curren player UI
    private RaycastHit gunHit;
    private bool noWeapon;
    [SerializeField] GameObject gunHitVFX;
    [SerializeField] float rotationAngle;
    private void Awake()
    {
        if (Instance != null || Instance == this) Destroy(gameObject);
        else Instance = this;
    }
    private void Start()
    {
        //LoadData
        SaveManager.Instance.dataPersistenceObjects.Add(this);
        LoadData(SaveManager.Instance.gameData);
        playerUI = PlayerUI.Instance;
        cam = Camera.main;
        InstanciateWeapons();
        noWeapon = unlockedWeapons.Count == 0;
        if (!noWeapon)
            playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
    }
    private void Update()
    {
   
    }
    //Instanciating Weapons
    /// <summary>
    /// Instantiates each weapon prefab in the prefabs list and adds them to the instanciatedWeapons list. If a weapon is active and unlocked,
    /// it is also added to the unlockedWeapons list and set as the current active weapon with its Gun component stored in currentGunScript.
    /// Otherwise, the weapon is added to the unlockedWeapons list but remains inactive.
    /// </summary>
    private void InstanciateWeapons()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject weapon = prefabs[i];
            Vector3 position = transform.position;
            var obj = Instantiate(weapon, position, weapon.transform.rotation, transform);
            instanciatedWeapons.Add(obj);
            var script = obj.GetComponent<Gun>();
            Animator animator = script.animator;
            AudioSource audioSouce = script.audioSource;
            script.weaponUnlocked = weaponUnlockSave[i];
            if (script?.weaponActive == true && script?.weaponUnlocked == true)
            {
                unlockedWeapons.Add(obj); //Adds a weapon to the active weapon list
                weaponUnlockSave[i] = true;
                if (unlockedWeapons[0] != null)
                {
                    currentActiveWeapon = unlockedWeapons[0];
                    currentGunScript = script;
                    currentAnimator = animator;
                    currentAudioSource = audioSouce;
                } 
            }else if (script?.weaponUnlocked == true)
            {
                unlockedWeapons.Add(obj);
                weaponUnlockSave[i] = true;
                if (unlockedWeapons[0] == obj) //in case the admin forgets to set one weapon active or it activates the first weapon in the List
                { 
                    script.weaponActive = true;
                    currentActiveWeapon = unlockedWeapons[0];
                    currentGunScript = script;
                    currentAnimator = animator;
                    currentAudioSource = audioSouce;
                }else
                {
                    obj.SetActive(false);
                }
            }else if (script.weaponUnlocked == false)
            {
                obj.SetActive(false);
                weaponUnlockSave[i] = false;
            }    
        }
    }
    public void FindObjectToUnlock(gunType gunTypeToCompare)
    {
        for (int i = 0; i < Instance.instanciatedWeapons.Count; i++)
        {
            GameObject gun = GunManager.Instance.instanciatedWeapons[i];
            var scriptInstanciatedWeapon = gun.GetComponent<Gun>();
            if (gunTypeToCompare == scriptInstanciatedWeapon.gunVariant && 
                !unlockedWeapons.Find(weapon => weapon.GetComponent<Gun>() == scriptInstanciatedWeapon))
            {
                weaponUnlockSave[i] = true;
                UnlockWeapon(gun);
            }        
        }
    }
    //Order Weapons
    public void UnlockWeapon(GameObject newWeapon)
    {
        var script = newWeapon.GetComponent<Gun>();
        Animator animator = script.animator;
        AudioSource audioSouce = script.audioSource;
        unlockedWeapons.Add(newWeapon);
        script.weaponUnlocked = true;
        if (newWeapon == unlockedWeapons[0])
        {
            noWeapon = false;
            newWeapon.SetActive(true);
            currentActiveWeapon = newWeapon;
            currentGunScript = script;
            currentGunScript.weaponActive = true;
            currentAnimator = animator;
            currentAudioSource = audioSouce;
            playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
        }
        else
        { 
            newWeapon.SetActive(false);
            noWeapon = false;
        }   
    }
    //Reloading
    /// <summary>
    /// Triggers reloading the current gun if it is not already reloading and there is not a full clip.
    /// </summary>
    public void Reload()
    {
        if (GameManager.Instance.inMenu == true || GameManager.Instance.isPaused == true) return;
        if (noWeapon) return;
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
        currentAnimator.SetTrigger("reloading");
        AudioClip reloadAudio = currentGunScript.reloading;
        currentAudioSource.PlayOneShot(reloadAudio);
        yield return new WaitForSeconds(currentGunScript.realoadTime);
        currentGunScript.ammunition = currentGunScript.ammunitionMax;
        playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
        currentGunScript.ChangeColor();
        reloading = false;
        shooting = false;
        currentAnimator.ResetTrigger("reloading");
        currentAnimator.ResetTrigger("emptyWeapon");
    }
    //Shooting
    /// <summary>
    /// Triggers shooting the current gun if the player is not already shooting and there is ammunition left.
    /// </summary>
    public void Shoot()
    {
        if (GameManager.Instance.inMenu == true || GameManager.Instance.isPaused == true) return;
        if (noWeapon) return;
        if (shooting) return;
        if (currentGunScript.ammunition <= 0)
        {
            currentAnimator.SetTrigger("emptyWeapon");
            currentAnimator.SetTrigger("shooting");
            StartCoroutine(PlayEmptyAnimation());
            return;
        }
        StartCoroutine(ShootCorutine());
    }
    /// <summary>
    /// A coroutine that represents shooting a gun. Decrements the current gun's ammunition, updates the player's UI,
    /// waits for the current gun's shoot cooldown, and hits an enemy if one is hit by the gun's raycast.
    /// </summary>
    /// <returns>A coroutine that can be yielded in another method.</returns>
    private IEnumerator ShootCorutine()
    {
        shooting = true;
        currentAnimator.SetTrigger("shooting");
        currentGunScript.effect.Play();

        AudioClip shootingAudio = currentGunScript.shooting;
        currentAudioSource.PlayOneShot(shootingAudio);
        currentGunScript.ammunition--;
        if(currentGunScript.ammunition == 0) currentAnimator.SetTrigger("emptyWeapon");
        currentGunScript.ChangeColor();
        playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
        Enemy enemy = null;
        Vector3 hitPosition = Vector3.zero;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out gunHit, currentGunScript.range, mask))
        {
            enemy = gunHit.collider?.gameObject.GetComponent<Enemy>();
            hitPosition = gunHit.point;
        }
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out gunHit, currentGunScript.range))
        {
            hitPosition = gunHit.point;
        }
        Debug.Log(gunHit.normal);
        if (gunHit.normal == Vector3.up || gunHit.normal == Vector3.down)
            Instantiate(gunHitVFX, hitPosition, Quaternion.identity);
        else if(gunHit.normal == Vector3.left || gunHit.normal == Vector3.right)
            Instantiate(gunHitVFX, hitPosition, Quaternion.Euler(new Vector3(0,0,-90)));
        else
            Instantiate(gunHitVFX, hitPosition, Quaternion.Euler(new Vector3(-90, 0, 0)));
        yield return new WaitForSeconds(currentGunScript.shootCooldown);
        if (enemy != null) enemy.Hit(currentGunScript.damage);    
        shooting = false;
        currentAnimator.ResetTrigger("shooting");
        currentAnimator.ResetTrigger("emptyWeapon");
    }
    private IEnumerator PlayEmptyAnimation()
    {
        AudioClip emptyAudio = currentGunScript.empty;
        currentAudioSource.PlayOneShot(emptyAudio);
        yield return new WaitForSeconds(1.0f);
        currentAnimator.ResetTrigger("shooting");
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
        if (noWeapon) return;
        if (unlockedWeapons.Count == 1)return ;
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
            currentAnimator = currentActiveWeapon.GetComponent<Animator>(); ;
            currentAudioSource = currentActiveWeapon.GetComponent<AudioSource>();
            currentActiveWeapon.SetActive(true);
            playerUI.UpdateAmmunition(currentGunScript.ammunition, currentGunScript.ammunitionMax);
            currentGunScript.ChangeColor();
        }
    }
    //Save and Load the weapons
    public void SaveData(ref SaveData data) => data.unlockedWeapons = this.weaponUnlockSave;
    public void LoadData(SaveData data) => this.weaponUnlockSave = data.unlockedWeapons;
}

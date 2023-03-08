using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInput playerInput;                //The Player Input
    public PlayerInput.PlayerWalkActions onWalk;    //The Player Action while Walking
    public PlayerInput.WeaponActions onWeapon;      //The Player Action while holding a gun
    private PlayerMovement movement;                //The Player Movement
    private PlayerLook look;                        //The Player Look
    private GunManager gunManager;                                
                                                        
    void Awake()
    {
        if (Instance != null || Instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        playerInput = new PlayerInput();            //the Player input element gets assigned
        onWalk = playerInput.PlayerWalk;            //the player action onWalk gets assigned
        onWeapon = playerInput.Weapon;
        movement = GetComponent<PlayerMovement>();  //the Script PlayerMovement is searched and assigned
        look = GetComponent<PlayerLook>();          //the Script PlayerLook is searched and assigne
        onWalk.Jump.performed += ctx => movement.Jump();        //if the player jumps the jump function in PlayerMovement is called
        onWalk.Crouch.performed += ctx => movement.Crouch();    //if the player crouches the crouch function in PlayerMovement is called
        onWalk.Sprint.performed += ctx => movement.Sprint();    //if the player sprints the sprint funtion in PlayerMovement is caslled

    }
    private void Start()
    {
        
        gunManager = GunManager.Instance;                //the script GunManager
        onWeapon.Reload.performed += ctx => gunManager.Reload();//if the player reloads the Reload funtion in GunManager is called
        onWeapon.Shoot.performed += ctx => gunManager.Shoot();  //if the player shoots the Shoot funtion in GunManager is called    
        onWeapon.SwitchWeapon.performed += ctx =>
        {
            float weaponValue = onWeapon.SwitchWeapon.ReadValue<float>();
            float floatValue = weaponValue; // oder y, abhängig von dem konkreten Control
            int intValue = Mathf.Clamp((int)floatValue,-1,1); // Konvertiere den float-Wert in einen int-Wert
            
            gunManager.SwitchWeapon(intValue);

        };
    }
    void FixedUpdate()
    {
        movement.ManageMove(onWalk.Move.ReadValue<Vector2>());  //sends the movement information to PlayerMovement to move the player
    }
    private void LateUpdate()
    {
        look.ManageLook(onWalk.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onWalk.Enable();    //The action is enabled
        onWeapon.Enable();  //The action is enabled
    }
    private void OnDisable()
    {
        onWalk.Disable();   //The action is disabled
        onWeapon.Disable(); //The action is Disabled
    }
}


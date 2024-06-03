# Shadow Tag Unity Project

## ReadMe Documentation

---

### Table of Contents
1. [Manager Components](#manager-components)
    1. [Game Manager](#game-manager)
    2. [Audio Manager](#audio-manager)
    3. [Scenes Manager](#scenes-manager)
    4. [UI Control and Settings](#ui-control-and-settings)
2. [Player Components](#player-components)
    1. [Player Look](#player-look)
    2. [Player Movement](#player-movement)
    3. [Player UI](#player-ui)
    4. [Input Manager](#input-manager)
    5. [Player Interact](#player-interact)
3. [Interactable Components](#interactable-components)
    1. [Interactable Objects](#interactable-objects)
4. [Enemy](#enemy)
5. [Save System](#save-system)
    1. [SaveManager](#savemanager)
    2. [SaveData](#savedata)
    3. [FileSaveDataHandler](#filesavedatahandler)
    4. [IDataPersistence Interface](#idatapersistence-interface)
6. [Gun and Gun Manager](#gun-and-gun-manager)
    1. [Gun Manager](#gun-manager)
    2. [Gun](#gun)
7. [Spawner](#spawner)
8. [Portal](#portal)
    1. [Hub Portals](#hub-portals)
    2. [Labyrinth Portals](#labyrinth-portals)

---

### Manager Components

### 1.1 Game Manager
The Game Manager is responsible for managing player settings such as mouse and controller sensitivity, FOV (Field of View), and gamma settings. It also controls the in-game menu for skipping cutscenes.

- **Implementation**: Create a central GameManager component in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Manage sensitivity and video settings.
  - Provide a menu system to pause the game and control the cursor.
  - Apply gamma correction to the volume object.
  - Skip the intro video and activate the player and UI.

#### 1.2 Audio Manager
The Audio Manager controls the game's audio settings, including volume and audio effects.

- **Implementation**: Create a central AudioManager component in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Control the game’s volume settings.
  - Reference the AudioMixer object to mix audio effects.
  - Apply volume settings from the Settings script to the AudioMixer object.

#### 1.3 Scenes Manager
The Scenes Manager handles scene transitions and management within the game.

- **Implementation**: Create a central ScenesManager component in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Load new games and scenes.
  - Provide a loading screen UI with a progress bar.
  - Update audio and video settings upon scene load.
  - Change the player UI accordingly when a new scene is loaded.

#### 1.4 UI Control and Settings

##### 1.4.1 Settings
The Settings component provides various game options such as sensitivity, FOV, gamma correction, and audio settings.

- **Implementation**: Create a Settings component in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Include sliders and buttons to control sensitivity, FOV, gamma correction, and audio settings.
  - Provide a back button to close the menu and return to the game.
  - Implement functions to deactivate the menu window when closed and pause the game when the menu is open.

##### 1.4.2 Credits
The Credits component displays the credits and includes functionality to close the credits window.

- **Implementation**: Create a Credits component in Unity.
- **Functions**:
  - Provide a method to deactivate the credits window when the close button is pressed.
  - Inform the GameManager that the game is no longer in menu mode.

##### 1.4.3 UIMainMenu
The UIMainMenu script provides the main menu for starting, loading, and exiting the game, changing settings, and viewing credits.

- **Implementation**: Create a UIMainMenu script in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Include buttons to start the game, load the game, change settings, exit the game, and view credits.
  - Manage audio settings and game loading.

##### 1.4.4 UIGameMenu
The UIGameMenu script provides the in-game menu, allowing players to resume the game, load the main menu, save the game, and change settings.

- **Implementation**: Create a UIGameMenu script in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Include buttons to resume the game, load the main menu, save the game, and change settings.
  - Link UI elements like buttons and text fields to control the menu functions.

---

### Player Components

#### 2.1 Player Look
The PlayerLook component allows the player to look around in the game world.

- **Implementation**: Create a PlayerLook component in Unity.
- **Functions**:
  - Reference the player’s camera.
  - Control mouse sensitivity horizontally and vertically.
  - Change the player’s field of view.
  - Ensure the cursor is locked for an uninterrupted experience.

#### 2.2 Player Movement
The PlayerMovement component manages the player's movement within the game.

- **Implementation**: Create a PlayerMovement component in Unity.
- **Functions**:
  - Use the CharacterController to control player movement.
  - Process player inputs from the InputManager.
  - Allow the player to run, jump, crouch, and sprint.
  - Provide functionality to save and load the player’s position.

#### 2.3 Player UI
The PlayerUI class manages the player’s UI elements such as score, level, and messages.

- **Implementation**: Create a PlayerUI class in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Manage score, level, and ammo display using TextMeshProUGUI objects.
  - Ensure only one instance of the class exists.
  - Add the object to the SaveManager’s list and load data on start.
  - Provide methods to update score, ammo, and text display.
  - Handle saving and loading of data.
  - Remove the object from the SaveManager’s list on disable.

#### 2.4 Input Manager
The InputManager class handles all player input and maps it to appropriate actions.

- **Implementation**: Create an InputManager class in Unity.
- **Pattern**: Singleton.
- **Functions**:
  - Provide a static instance.
  - Reference a PlayerInput object and define player actions (e.g., onWalk, onWeapon).
  - Link player actions to appropriate functions in other scripts (e.g., PlayerMovement, PlayerLook, GunManager).
  - Implement a function to turn on the flashlight.

#### 2.5 Player Interact
The PlayerInteract class allows the player to interact with objects in the game world.

- **Implementation**: Create a PlayerInteract class in Unity.
- **Functions**:
  - Reference the player’s camera.
  - Control interaction distance and layer mask.
  - Reference the PlayerUI and InputManager classes.
  - Perform a Raycast in the Update() method to detect interactive objects.
  - Update the UI with interaction prompts.
  - Call the BaseInteract() method on the Interactable object when interaction is triggered.

---

### Interactable Components

#### 3.1 Interactable Objects
Interactive objects in the game inherit from the Interactable class, providing specific interaction logic.

- **Implementation**: Create an abstract Interactable class in Unity.
- **Functions**:
  - Provide a promptMessage property for interaction messages.
  - Implement a BaseInteract function to call the abstract Interact method, which must be overridden in derived classes.

##### Example: Door
A Door class inheriting from Interactable to open and close doors.

- **Implementation**: Create a Door class in Unity.
- **Variables**:
  - GameObject handle, bool isLeft, bool isRight, GameObject house, Animator currentAnimator, AudioTrigger audioTrigger.
- **Functions**:
  - Assign the current Animator in the Start method.
  - Override the Interact method to implement door-specific interaction logic (e.g., opening and closing the door based on its rotation and side).
  - Trigger audio effects during interaction.

---

### Enemy
The Enemy class handles enemy behaviors and interactions.

- **Implementation**: Create an Enemy class inheriting from Interactable.
- **Functions**:
  - Variables for health, points, lifespan, slime variants, textures, and audio.
  - Methods to add damage, manage lifespan with a coroutine, update player points, and play audio clips for different situations (e.g., scare, hit, death).
  - Special handling for slime enemies, including random variant selection and texture setting.

---

### Save System

#### 5.1 SaveManager
The SaveManager class manages saving and loading game states.

- **Implementation**: Create a SaveManager class in Unity.
- **Functions**:
  - Maintain a list of IDataPersistence objects to save relevant data.
  - Start a new game, load a game, and save the current game state.
  - Automatically save the game when exiting.

#### 5.2 SaveData
The SaveData class represents the serialized game data.

- **Implementation**: Create a SaveData class in Unity.
- **Attributes**:
  - currentPoints, currentLabyrinthLevel, playerPosition, unlockedWeapons, unlockedLevels.
- **Functions**:
  - Provide a default constructor to initialize fields with default values.
  - Use [System.Serializable] attribute for serialization.

#### 5.3 File

SaveDataHandler
The FileSaveDataHandler class handles file operations for saving and loading data.

- **Implementation**: Create a FileSaveDataHandler class in Unity.
- **Functions**:
  - Variables for save directory and file name.
  - Methods to load data, save data, and delete save files.
  - Ensure the save directory exists.

#### 5.4 IDataPersistence Interface
The IDataPersistence interface defines methods for saving and loading data.

- **Implementation**: Create an IDataPersistence interface in Unity.
- **Methods**:
  - LoadData(SaveData data)
  - SaveData(SaveData data)

---

### Gun and Gun Manager

#### 6.1 Gun Manager
The GunManager class manages the player's weapons.

- **Implementation**: Create a GunManager class in Unity.
- **Functions**:
  - Reference the gun and animation objects.
  - Handle input for weapon interactions (e.g., reload, shoot).
  - Update animations and UI elements.

#### 6.2 Gun
The Gun class represents individual weapons.

- **Implementation**: Create a Gun class in Unity.
- **Functions**:
  - Variables for damage, ammo, range, and fire rate.
  - Methods for shooting, reloading, and weapon-specific interactions.

---

### Spawner
The Spawner class handles spawning of enemies and items.

- **Implementation**: Create a Spawner class in Unity.
- **Functions**:
  - Reference prefab objects and spawn points.
  - Methods to spawn enemies and items at specified intervals.

---

### Portal

#### 8.1 Hub Portals
Hub portals allow the player to travel between the hub and other areas.

- **Implementation**: Create a HubPortal class in Unity.
- **Functions**:
  - Handle player interaction to teleport between the hub and other levels.

#### 8.2 Labyrinth Portals
Labyrinth portals manage transitions within the labyrinth levels.

- **Implementation**: Create a LabyrinthPortal class in Unity.
- **Functions**:
  - Control player navigation within the labyrinth.
  - Ensure smooth transition between labyrinth sections.

---

### Contribution
To contribute to this project, please follow the guidelines below:

1. **Fork the Repository**: Create a fork of this repository.
2. **Create a Branch**: Create a new branch for your feature or bug fix.
3. **Commit Changes**: Commit your changes with clear and descriptive messages.
4. **Push to Branch**: Push your changes to the branch.
5. **Create a Pull Request**: Open a pull request to merge your changes into the main repository.

---

### License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

### Contact
For questions or support, please open an issue in the repository or contact the project maintainer.

---

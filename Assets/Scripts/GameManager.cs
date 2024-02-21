using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject ingameUI;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    private NPCController[] npcs;
    private DoorController[] doors;

    private int points = 0;
    private int keyItemsCollected = 0;

    public bool gameRunning = false;
    public bool gamePaused = false;

    void Awake() {

    }

    void Start()
    {

        // Zentriert den Cursor in der Mitte und blendet ihn aus
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Startet das Spiel
        //Time.timeScale = 1.0f;
        //gameRunning = true;

        npcs = FindObjectsOfType<NPCController>();
        doors = FindObjectsOfType<DoorController>();

        UnlockDoors("Flur 1");
        UnlockDoors("Empfang");


        string[] npc1text = new string[] {"Oh, du bist der neue. Richtig?", "Komm, ich zeig dir mal was.", "Das wird auch ganz lustig!"};
        ActivateNPC("Test NPC", npc1text);
    }

    void Update()
    {

    }

    private void ActivateNPC(string npcName, string[] textLines) {

        // Durchlaeuft alle NPC und aktiviert sie, wenn der Name uebereinstimmt
        foreach (NPCController npc in npcs) {
            if (npc.npcName.Equals(npcName)) {
                npc.SetupNPC(textLines);
            }
        }

    }

    private void UnlockDoors(string roomName) {

        // Durchlaeuft alle Tueren und oeffnet sie, wenn der Raumname uebereinstimmt
        foreach (DoorController door in doors) {
            if (door.GetRoomName().Equals(roomName)) {
                door.locked = false;
            }
        }
    }

    public void AddPoint(bool keyItem) {
        points++;

        if (keyItem) {
            keyItemsCollected += 1;
        }

    }

    void OnEnable() {
        // Bei Ausloesen des Events, rufe die GameLoop-Methode auf (Abonnieren)
        ItemController.OnKeyItemCollected += GameLoop;
    }

    void OnDisable() {  
        // Bei Zerstoerung des GameObjects mit dem Event, rufe nicht mehr die GameLoop-Methode auf (Abbestellen)
        ItemController.OnKeyItemCollected -= GameLoop;
    }

    private void GameLoop() {


        
    }

}

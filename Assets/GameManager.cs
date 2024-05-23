using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public GameObject[] newGameCanvases;
    public GameObject[] loadSaveCanvases;

    public GameObject player;
    public string savePath = "/Saves/";
    public GameObject deletePopUpCanvas;
    public GameObject MiniMapCanvas;
    public HealthBar HealthBar;
    public PlayerInventory PlayerInventory;

    private string screenshotName = "Screenshot";
    private string saveDataFileName = "GameData.json";

    private DateTime startTime;
    public TimeSpan totalPlaytime;

    private void Start()
    {
        Directory.CreateDirectory(Application.persistentDataPath + savePath);

        for (int i = 0; i < loadSaveCanvases.Length; i++)
        {
            UpdatePlaytimeText(i);
            DisplayScreenshot(i);
        }
        
    }

    public void Update()
    {
        CheckAndShowCanvases();
    }

    public void LoadGameData(int slotIndex)
    {
        string filePath = Application.persistentDataPath + savePath + "GameData_" + slotIndex + ".json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            SceneManager.LoadScene("PlayScene");
            
            totalPlaytime = data.totalPlaytime;
            startTime = DateTime.Now - data.elapsedTime;
            player.transform.position = data.playerPosition;
            HealthBar.health = data.health;
            HealthBar.mana = data.mana;
            HealthBar.maxHealth = data.maxHealth;
            HealthBar.maxMana = data.maxMana;
            PlayerInventory.keysCount = data.RuneKeyAmount;

            // Update the saved data with the modified collectedRuneKeys list
            string updatedJsonData = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, updatedJsonData);

            Debug.Log("Game data loaded successfully for Slot Index: " + slotIndex);
            Debug.Log("GameDataID: " + data.GameDataID);
            Debug.Log("Player position: " + data.playerPosition);
            Debug.Log("Total playtime: " + data.totalPlaytime);
            Debug.Log("Elapsed time: " + data.elapsedTime);

            ShowCanvas(slotIndex, CheckFileByID(slotIndex));
        }
        else
        {
            SceneManager.LoadScene("PlayScene");
            Debug.LogWarning("Save file not found for Slot Index: " + slotIndex);
            // Handle case when save file is not found for the given slot index
        }
    }

    public void SaveGame(int slotIndex)
    {
        if (PlayerInventory != null && HealthBar != null && player != null)
        {
            GameData data = new GameData();
            PlayerPrefs.SetInt("SlotIndex", slotIndex);
            data.GameDataID = slotIndex.ToString(); // Set the ID
            data.health = HealthBar.health;
            data.mana = HealthBar.mana;
            data.maxHealth = HealthBar.maxHealth;
            data.maxMana = HealthBar.maxMana;

            // Check if PlayerInventory and keysCount are not null before accessing
            if (PlayerInventory != null)
            {
                data.RuneKeyAmount = PlayerInventory.keysCount;
            }
            else
            {
                data.RuneKeyAmount = 0; // or some default value
            }

            data.elapsedTime = (DateTime.Now - startTime);
            data.totalPlaytime = totalPlaytime;
            data.playerPosition = player.transform.position;

            string jsonData = JsonUtility.ToJson(data);
            string fileName = "GameData_" + slotIndex + ".json"; // Use ID in the file name
            File.WriteAllText(Application.persistentDataPath + savePath + fileName, jsonData);

            CaptureScreenshot(slotIndex);
            UIUpdate(slotIndex);

            Debug.Log("Game saved successfully with Slot Index: " + slotIndex);
            Debug.Log("Save Path: " + Application.persistentDataPath);
            Debug.Log("Game saved with slot index: " + slotIndex);
        }
        else
        {
            Debug.LogWarning("PlayerInventory, HealthBar, or player is not properly initialized.");
            // Handle this case as needed
        }
    }


    public void DeleteSave(int slotIndex)
    {
        string saveFilePath = Application.persistentDataPath + savePath + "GameData_" + slotIndex + ".json";
        string screenshotFilePath = Application.persistentDataPath + savePath + "Screenshot_" + slotIndex + ".png";

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }

        if (File.Exists(screenshotFilePath))
        {
            File.Delete(screenshotFilePath);
            foreach (GameObject canvas in loadSaveCanvases)
            {
                canvas.GetComponent<Image>().sprite = null;
            }
        }

        startTime = DateTime.MinValue;
        totalPlaytime = TimeSpan.Zero;
        UpdatePlaytimeText(slotIndex);
        

        Debug.Log("Save deleted successfully for Slot Index: " + slotIndex);
    }

    public void UIUpdate(int slotIndex)
    {
        UpdatePlaytimeText(slotIndex);
        DisplayScreenshot(slotIndex);
    }

    private void InitializeCanvases()
    {
        foreach (GameObject canvas in newGameCanvases)
        {
            canvas.SetActive(false);
        }

        foreach (GameObject canvas in loadSaveCanvases)
        {
            canvas.SetActive(false);
        }
    }

    private void CheckAndShowCanvases()
    {
        for (int i = 0; i < newGameCanvases.Length; i++)
        {
            bool hasSavedData = CheckFileByID(i);
            ShowCanvas(i, hasSavedData);
        }
    }

    public void MinimapToggle()
    {
        MiniMapCanvas.SetActive(!MiniMapCanvas.activeSelf);
    }

    private void CaptureScreenshot(int slotIndex)
    {
        string screenshotName = "Screenshot_" + slotIndex + ".png";
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + savePath + screenshotName);
    }

    private void DisplayScreenshot(int slotIndex)
    {
        string screenshotPath = Application.persistentDataPath + savePath + "Screenshot_" + slotIndex + ".png";
        if (File.Exists(screenshotPath))
        {
            byte[] bytes = File.ReadAllBytes(screenshotPath);
            Texture2D savedScreenshot = new Texture2D(1, 1);
            savedScreenshot.LoadImage(bytes);

            // Assuming each loadSaveCanvas has an Image component where you want to display the screenshot
            if (slotIndex >= 0 && slotIndex < loadSaveCanvases.Length)
            {
                Image imageComponent = loadSaveCanvases[slotIndex].GetComponentInChildren<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = Sprite.Create(savedScreenshot, new Rect(0, 0, savedScreenshot.width, savedScreenshot.height), Vector2.zero);
                }
                else
                {
                    Debug.LogWarning("Image component not found in loadSaveCanvas[" + slotIndex + "].");
                }
            }
            else
            {
                Debug.LogWarning("Invalid slot index for displaying screenshot.");
            }
        }
        else
        {
            Debug.LogWarning("Screenshot file not found for Slot Index: " + slotIndex);
        }
    }

    private bool playtimeInitialized = false;

    private void UpdatePlaytimeText(int slotIndex)
    {
        TimeSpan elapsedTime = DateTime.Now - startTime;
        TimeSpan playtime = totalPlaytime + elapsedTime;

        if (!playtimeInitialized)
        {
            playtime = TimeSpan.Zero;
            playtimeInitialized = true;
        }

        if (slotIndex >= 0 && slotIndex < loadSaveCanvases.Length)
        {
            GameObject canvas = loadSaveCanvases[slotIndex];
            TextMeshProUGUI playtimeText = canvas.GetComponentInChildren<TextMeshProUGUI>();

            if (playtimeText != null)
            {
                playtimeText.text = "Playtime: " + playtime.ToString(@"hh\:mm\:ss");
            }
        }
        else
        {
            Debug.LogWarning("Invalid slot index for updating playtime text.");
        }
    }



    public bool CheckFileByID(int slotIndex)
    {
        string filePath = Application.persistentDataPath + savePath + "GameData_" + slotIndex + ".json";
        if (File.Exists(filePath))
        {
            //Debug.Log(filePath);
            return File.Exists(filePath);
        }
        else
        {
            //Debug.Log("False");
            return false;
        }
    }

    public void ShowCanvas(int slotIndex, bool hasSavedData)
    {
        if (slotIndex < 0 || slotIndex >= newGameCanvases.Length)
        {
            Debug.LogWarning("Invalid slot index.");
            return;
        }

        if (hasSavedData)
        {
            loadSaveCanvases[slotIndex].SetActive(true);
            newGameCanvases[slotIndex].SetActive(false);
        }
        else
        {
            newGameCanvases[slotIndex].SetActive(true);
            loadSaveCanvases[slotIndex].SetActive(false);
        }
    }

    public void CheckBtn(int slotIndex)
    {
        string filePath = Application.persistentDataPath + savePath + "GameData_" + slotIndex + ".json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);

            Debug.Log(filePath);

            Debug.Log("Game data loaded successfully for Slot Index: " + slotIndex);
            Debug.Log("GameDataID: " + data.GameDataID);
            Debug.Log("Player position: " + data.playerPosition);
            Debug.Log("Total playtime: " + data.totalPlaytime);
            Debug.Log("Elapsed time: " + data.elapsedTime);
        }
        else
        {
            Debug.LogWarning("Save file not found for Slot Index: " + slotIndex);
        }
    }

    public string GetGameDataID(int saveIndex)
    {
        string filePath = Application.persistentDataPath + savePath + "GameData_" + saveIndex + ".json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            Debug.Log(data.GameDataID);
            return data.GameDataID;
        }
        else
        {
            Debug.LogWarning("Save file not found for Slot Index: " + saveIndex);
            return null; // or return a default value or handle this case accordingly
        }
    }

}

[System.Serializable]
public class GameData
{
    public string GameDataID;
    public Vector3 playerPosition;
    public int health;
    public int maxHealth;
    public int mana;
    public int maxMana;
    public bool DashActivated;
    public int RuneKeyAmount;
    public TimeSpan totalPlaytime;
    public TimeSpan elapsedTime;

    public Dictionary<string, bool> collectibleItem;
}
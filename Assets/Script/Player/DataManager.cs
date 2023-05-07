using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    [SerializeField] private CatsManager catsManager;
    private static fsSerializer Serializer = new fsSerializer();
    [SerializeField] private Button continueButton;
    [SerializeField] private List<Button> saveButtons = new List<Button>();
    [SerializeField] private List<Button> newGameButtons = new List<Button>();
    [SerializeField] private Button deleteOldGameConfirmationButton;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private GameObject newGamePanel;
    [SerializeField] private GameObject loadGamePanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject newGameCreationPanel;
    [SerializeField] private GameObject errorPanel;
    private static readonly string CatPath = Application.streamingAssetsPath;

    [SerializeField] private TextMeshProUGUI GameNameField;
    
    public static List<string> CatFiles => Directory.GetFiles(CatPath, "*" + Properties.File.CatExt).Select(Path.GetFileName).ToList();

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.S)) {
            SaveCatsManager(catsManager);
        }*/
        
        continueButton.interactable = CatFiles.Count > 0;
    }
    
    public void DisplayAllFoundFilesForLoad() {
        loadGamePanel.SetActive(true);
        int saveCount = 0;
        for (int i = 0; i < CatFiles.Count; i++) {
                string variableFile = CatFiles[i];
                TextMeshProUGUI tmp = saveButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                tmp.text = variableFile.Remove(variableFile.Length - 5, 5).Replace('_', ' ');
                saveButtons[i].onClick.AddListener(delegate {
                    Instantiate(loadingPanel, saveButtons[i].transform.parent.parent);
                    PlayerPrefs.SetString(Properties.Pref.LoadedGame, variableFile);
                    Debug.Log(variableFile + " has been loaded.");
                    loadGamePanel.SetActive(false);
                    Load();
                });
                saveCount++;
        }

        if (saveCount >= 3) return;
        for (int i = saveCount; i < saveButtons.Count; i++) { 
            saveButtons[i].interactable = false;
        }

        
        /*Button tempButton = Instantiate(buttonPrefab, loadPanel.transform).GetComponent<Button>();
        TextMeshProUGUI textMeshPro = tempButton.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = variableFile.Remove(variableFile.Length - 5, 5);
        tempButton.onClick.AddListener(delegate {
            PlayerPrefs.SetString(Properties.Pref.LoadedGame, variableFile);
            Debug.Log(variableFile + " has been loaded.");
            loadPanel.SetActive(false);
            DestroyAllGame();
            Load();
        });*/
    }

    public void DisplayAllFoundFileForNew() {
        newGamePanel.SetActive(true);
        for (int i = 0; i < CatFiles.Count; i++) {
            string variableFile = CatFiles[i];
            TextMeshProUGUI tmp = newGameButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = variableFile.Remove(variableFile.Length - 5, 5).Replace('_', ' ');
            newGameButtons[i].onClick.AddListener(delegate {
                confirmationPanel.SetActive(true);
                deleteOldGameConfirmationButton.onClick.AddListener(delegate {
                    DeleteOldGame(variableFile);
                    newGameCreationPanel.SetActive(true);
                    confirmationPanel.SetActive(false);
                });
            });
        }
        
        if(CatFiles.Count < 4){
            foreach (Button button in newGameButtons) {
                TextMeshProUGUI tmp = button.GetComponentInChildren<TextMeshProUGUI>();
                if (!CatFiles.Contains(tmp.text + Properties.File.CatExt)) {
                    button.onClick.AddListener(delegate {
                       newGameCreationPanel.SetActive(true);
                    });
                }
            }
        }
    }

    public void SaveCatsManager(CatsManager catsMan) {
        string path = CatPath + Path.DirectorySeparatorChar + catsMan.Name + Properties.File.CatExt;
        if (!File.Exists(path)) {
            File.Create(path).Close();
        }
        else {
            File.Delete(path);
            File.Create(path).Close();
        }
        File.WriteAllText(path, Serialize(typeof(CatsManager), catsMan));
        Debug.Log("Team saved at " + path);
    }

    public CatsManager LoadGame(string catManagerNameWithExtension) {
        string path = CatPath + Path.DirectorySeparatorChar + catManagerNameWithExtension;
        if(File.Exists(path)) File.OpenRead(path).Close();
        else Debug.Log("Pas de sauvegarde enregistrée.");
        string fileJson = File.ReadAllText(path);
        CatsManager temp = Deserialize(typeof(CatsManager), fileJson) as CatsManager;
        temp.Name = catManagerNameWithExtension.Remove(catManagerNameWithExtension.Length - 5, 5)
            .Replace('_', ' ');
        
        return temp;
    }

    private string Serialize(Type type, object value) {
        fsData data;
        Serializer.TrySerialize(typeof(CatsManager), value, out data).AssertSuccess();
        return fsJsonPrinter.PrettyJson(data);
        //return fsJsonPrinter.CompressedJson(data);
    }

    private static object Deserialize(Type type, string serializedState)
    {
        // step 1: parse the JSON data
        fsData data = fsJsonParser.Parse(serializedState);
        
        // step 2: deserialize the data
        object deserialized = null;
        Serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

        return deserialized;
    }

    public static bool CheckGame(string storyNameWithExtension) {
        string path = CatPath + Path.DirectorySeparatorChar + storyNameWithExtension;
        return File.Exists(path);
    }

    public void Load() {
        if (PlayerPrefs.HasKey(Properties.Pref.LoadedGame) && CheckGame(PlayerPrefs.GetString(Properties.Pref.LoadedGame))) {
            catsManager.SetGame(LoadGame(PlayerPrefs.GetString(Properties.Pref.LoadedGame)));
            PlayerPrefs.DeleteKey(Properties.Pref.LoadedGame);
            Debug.Log(catsManager.Name + " has been loaded");
        }
    }

    
    
    public void DeleteOldGame(string nameFile) {
        File.Delete(CatPath + Path.DirectorySeparatorChar + nameFile);
        newGameCreationPanel.SetActive(true);
        DisplayAllFoundFileForNew();
    }

    public void ValidateName() {
        if (CatFiles.Contains(GameNameField.text + Properties.File.CatExt)) {
            errorPanel.SetActive(true);
            return;
        }
        catsManager.Name = GameNameField.text;
    }

    public void CreateSave() {
        SaveCatsManager(catsManager);
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    [SerializeField] private CatsManager catsManager;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject loadPanel;
    private static fsSerializer serializer = new fsSerializer();

    private static readonly string CatPath = Application.streamingAssetsPath;
    public static List<string> CatFiles => Directory.GetFiles(CatPath, "*" + Properties.File.CatExt).Select(Path.GetFileName).ToList();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            SaveCatsManager(catsManager);
        }
    }
    
    public void DisplayAllFoundFiles() {
        foreach (string variableFile in CatFiles) {
            Button tempButton = Instantiate(buttonPrefab, loadPanel.transform).GetComponent<Button>();
            TextMeshProUGUI textMeshPro = tempButton.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = variableFile.Remove(variableFile.Length - 5, 5);
            tempButton.onClick.AddListener(delegate {
                PlayerPrefs.SetString(Properties.Pref.LoadedGame, variableFile);
                Debug.Log(variableFile + " has been loaded.");
                loadPanel.SetActive(false);
                DestroyAllGame();
                Load();
            });
        }
    }

    private void DestroyAllGame() {
        foreach (Transform child in loadPanel.GetComponent<Transform>()) {
            Destroy(child.gameObject);
        }
    }

    public void SaveCatsManager(CatsManager catsMan) {
        string path = CatPath + Path.DirectorySeparatorChar + catsMan.name + Properties.File.CatExt;
        if (!File.Exists(path)) {
            FileStream fileStream = File.Create(path);
            fileStream.Close();
        }
        File.WriteAllText(path, Serialize(typeof(CatsManager), catsMan));
        Debug.Log("Team saved at " + path);
    }
    
    public CatsManager LoadGame(string catManagerNameWithExtension) {
        string path = CatPath + Path.DirectorySeparatorChar + catManagerNameWithExtension;
        foreach (var files in CatFiles) {
            if(File.Exists(path)) File.OpenRead(path);
            else Debug.Log("Pas de sauvegarde enregistrée.");
        }
        string fileJson = File.ReadAllText(path);
        return Deserialize(typeof(CatsManager), fileJson) as CatsManager;
    }

    private string Serialize(Type type, object value) {
        fsData data;
        serializer.TrySerialize(typeof(CatsManager), value, out data).AssertSuccess();
        return fsJsonPrinter.PrettyJson(data);
        //return fsJsonPrinter.CompressedJson(data);

    }

    private static object Deserialize(Type type, string serializedState)
    {
        // step 1: parse the JSON data
        fsData data = fsJsonParser.Parse(serializedState);
        
        // step 2: deserialize the data
        object deserialized = null;
        serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

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
            foreach (var VARIABLE in catsManager.team)
            {
                Debug.Log(VARIABLE.Name);
            }
        }
    }
}
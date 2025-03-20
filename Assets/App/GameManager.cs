using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private SaveManager saveManager; //menteshez
    public List<SaveData> Saves { get; private set; } 
    public bool IsSaveAvailable { get; private set; }

    public event Action SceneChanged;

    public GameModel _gameModel { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        _gameModel = new GameModel();

        saveManager = new SaveManager(); //mentes
        Saves = saveManager.GetSaveList();
        IsSaveAvailable = Saves.Count > 0;

        Debug.Log("gameManager awake");
    }

    private void Start() => SceneChanged?.Invoke(); //azt hogy van mentes vagy nincs is itt kell ellenorizni majd

    public void NewGame()
    {
        Animal a1 = new Lion(new Vector3(0f, 0f, 0f), 5); // csak teszt hogy mukodik-e a simulation
        Animal a2 = new Lion(new Vector3(0f, 0f, 0f), 5); // csak teszt hogy mukodik-e a simulation
        _gameModel.AddAnimal(a1);
        _gameModel.AddAnimal(a2);

        TerrainObject t = TerrainObject.createLake(new Vector3(0, 0, 0));
        TerrainObject t2 = TerrainObject.createLake(new Vector3(5, 5, 0));
        _gameModel.AddTerrainObjects(t);
        _gameModel.AddTerrainObjects(t2);

        Jeep j = new Jeep(new Vector3(0f, 0f, 0f));
        _gameModel.AddJeep(j);
        //ez itt csak teszthez kell 

        SceneManager.LoadScene("GameScene");
    }

    public void LoadGame(GameModel gameModel)
    {
        _gameModel = gameModel;
        _gameModel.SetNextIDAfterLoad();
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() => Application.Quit();

    public void SaveGame(string name)
    {
        saveManager.SaveGame(_gameModel, name);
        IsSaveAvailable = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => SceneChanged?.Invoke();
}

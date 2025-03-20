using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private GameManager gameManager;

    GameObject homeUI;
    GameObject savedGamesUI;

    [SerializeField]
    private GameObject saveDataButtonPrefab;

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
            return;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.SceneChanged += CheckScene;
        CheckScene();
    }

    private void CheckScene()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetUpMainMenu();
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            SetUpGameUI();
        }
    }

    private void SetUpMainMenu()
    {
        homeUI = GameObject.Find("Home");
        savedGamesUI = GameObject.Find("SavedGames");

        //Home
        Button newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        Button loadGameButton = GameObject.Find("LoadGameButton").GetComponent<Button>();
        Button QuitGameButton = GameObject.Find("QuitGameButton").GetComponent<Button>();

        newGameButton.onClick.AddListener(() => gameManager.NewGame());
        loadGameButton.onClick.AddListener(SetUpSavedGames);
        QuitGameButton.onClick.AddListener(() => gameManager.QuitGame());
        
        //Saved games
        Button backButton = GameObject.Find("BackButton").GetComponent<Button>();

        backButton.onClick.AddListener(() =>
        {
            homeUI.SetActive(true);
            savedGamesUI.SetActive(false);
        });

        homeUI.SetActive(true);
        savedGamesUI.SetActive(false);

        if (!gameManager.IsSaveAvailable)
        {
            loadGameButton.interactable = false;
            loadGameButton.GetComponentInChildren<Text>().color = Color.gray;
        }
    }

    private void SetUpGameUI()
    {
        //toDo  befejezni majd ha van gameUI
        Button saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
        saveButton.onClick.AddListener(() => gameManager.SaveGame("qwe"));
    }

    private void SetUpSavedGames()
    {
        homeUI.SetActive(false);
        savedGamesUI.SetActive(true);

        Transform contentPanel = GameObject.Find("Content").transform;

        foreach(SaveData saveData in gameManager.Saves)
        {
            GameObject newButton = Instantiate(saveDataButtonPrefab, contentPanel, false);
            string date = DateTime.Parse(saveData.saveDate).ToString();
            newButton.GetComponentInChildren<TMP_Text>().text = "Name: " + saveData.saveName + " Date: " + date;
            newButton.GetComponent<Button>().onClick.AddListener(() => gameManager.LoadGame(saveData.gameModel));
        }
    }

    //private void OnDestroy() => gameManager.SceneChanged -= CheckScene;
}

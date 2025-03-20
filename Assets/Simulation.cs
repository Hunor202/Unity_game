using System;
using System.Collections;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    GameModel gameModel;

    [Header("Animal prefabs")]
    [SerializeField] GameObject lionPrefab;
    [SerializeField] GameObject zebraPrefab;
    [SerializeField] GameObject giraffePrefab;
    [SerializeField] GameObject bearPrefab;
    [SerializeField] GameObject tigerPrefab;
    [SerializeField] GameObject hippoPrefab;
    [SerializeField] GameObject rhinoPrefab;
    [SerializeField] GameObject elephantPrefab;
    [SerializeField] GameObject deerPrefab;

    [Header("People prefabs")]
    [SerializeField] GameObject visitorPrefab;
    [SerializeField] GameObject pocherPrefab;
    [SerializeField] GameObject caretakerPrefab;

    [Header("Jeep prefab")]
    [SerializeField] GameObject jeepPrefab;

    [Header("TerrainObject prefabs")]
    [SerializeField] GameObject bushPrefab;
    [SerializeField] GameObject grassPrefab;
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject lakePrefab;
    [SerializeField] GameObject roadPrefab;

    private float timer = 0f;
    [SerializeField]
    private float updateTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameModel = GameManager.Instance._gameModel;
        SpawnAnimals();
        //SpawnPersons(); //nincsenek kesz a prefabek
        //SpawnJeeps(); //nincsen kesz a jeep prefab
        SpawnTerrainObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > updateTime)
        {
            timer = 0f;
            AnimalEffect();
        }else
        {
            timer += Time.deltaTime * gameModel.GameSpeed;
        }
    }

    private void SpawnAnimals()
    {
        foreach (Animal a in gameModel.Animals)
        {
            GameObject go = null;
            switch (a.Type) 
            {
                case AnimalType.LION: go = Instantiate(lionPrefab, a.Position, Quaternion.identity); break;
                case AnimalType.GIRAFFE: go = Instantiate(giraffePrefab, a.Position, Quaternion.identity); break;
                case AnimalType.BEAR: go = Instantiate(bearPrefab, a.Position, Quaternion.identity); break;
                case AnimalType.TIGER: go = Instantiate(tigerPrefab, a.Position, Quaternion.identity); break;
                case AnimalType.ZEBRA: go = Instantiate(zebraPrefab, a.Position, Quaternion.identity); break;
                case AnimalType.HIPPO: go = Instantiate(hippoPrefab, a.Position, Quaternion.identity); break;
                case AnimalType.RHINO: go = Instantiate(rhinoPrefab, a.Position, Quaternion.identity); break;
                case AnimalType.DEER: go = Instantiate(deerPrefab, a.Position, Quaternion.identity); break;
                case AnimalType.ELEPHANT: go = Instantiate(elephantPrefab, a.Position, Quaternion.identity); break;
                default: Debug.LogWarning($"Unknown animal type: {a.Type}"); break;
            }
            if (go != null)
            {
                go.name = "Animal_" + a.ID;
                AnimalView animalView = go.GetComponent<AnimalView>(); 
                animalView.Initialize(a); //szerintem a view es a model kozott nem lehet kozvetlen kapcsolat
            }
        }
    }

    private void SpawnPersons()
    {
        foreach(Person p in gameModel.Persons)
        {
            GameObject go = null;
            switch (p.Type)
            {
                case PersonType.VISITOR: go = Instantiate(visitorPrefab, p.Position, Quaternion.identity); break;
                case PersonType.POCHER: go = Instantiate(pocherPrefab, p.Position, Quaternion.identity); break;
                case PersonType.CARETAKER: go = Instantiate(caretakerPrefab, p.Position, Quaternion.identity); break;
                default: Debug.LogWarning($"Unknown animal type: {p.Type}"); break;
            }

            if (go != null)
            {
                go.name = "Person_" + p.ID;
                // PersonView personView = go.GetComponent<PersonView>();
                // personView.Initialize(p); //szerintem a view es a model kozott nem lehet kozvetlen kapcsolat
            }
        }
    }

    private void SpawnJeeps()
    {
        foreach (Jeep j in gameModel.Jeeps)
        {
            GameObject go = Instantiate(jeepPrefab, j.position, Quaternion.identity);
            go.name = "Jeep_" + j.ID;
            // JeepView jeepView = go.GetComponent<JeepView>();
            // jeepView.Initialize(j); //szerintem a view es a model kozott nem lehet kozvetlen kapcsolat
        }
    }

    private void SpawnTerrainObjects()
    {
        foreach(TerrainObject t in gameModel.TerrainObjects)
        {
            GameObject go = null;

            switch (t.type)
            {
                case TerrainObjectType.BUSH: go = Instantiate(bushPrefab, t.position, Quaternion.identity); break;
                case TerrainObjectType.GRASS: go = Instantiate(grassPrefab, t.position, Quaternion.identity); break;
                case TerrainObjectType.TREE: go = Instantiate(treePrefab, t.position, Quaternion.identity); break;
                case TerrainObjectType.LAKE: go = Instantiate(lakePrefab, t.position, Quaternion.identity); break;
                case TerrainObjectType.ROAD: go = Instantiate(roadPrefab, t.position, Quaternion.identity); break;
                default: Debug.LogWarning($"Unknown terrain object type: {t.type}"); break;
            }
            if (go != null)
            {
                go.name = "TerrainObject_" + t.ID;
                TerrainObjectView terrainView = go.GetComponent<TerrainObjectView>();
                terrainView.Initialize(t);
            }
        }
    }

    private void AnimalEffect()
    {
        foreach (Animal animal in gameModel.Animals)
        {
            animal.IncreaseHungerAndThirst();
            animal.IncreaseAge();
            if(!animal.IsAlive) 
            {
                animal.Die();
                gameModel.RemoveAnimal(animal);
                GameObject animalGO = GameObject.Find("Animal_" + animal.ID);
                Destroy(animalGO);
            }

            if (animal.isThirsty())
            {
                animal.findWater();
            }
            else if (animal.isHungry())
            {
                animal.findFood();
            }
        }
    }
}

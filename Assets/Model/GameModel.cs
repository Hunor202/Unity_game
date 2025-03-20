using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameModel
{
    public float GameSpeed { get; private set; } = 1f;
    public int Money {  get; private set; }
    public List<Animal> Animals {  get; private set; } = new List<Animal>();
    public List<Group> Groups { get; private set; } = new List<Group>();
    public List<Person> Persons { get; private set; } = new List<Person>();
    public List<Jeep> Jeeps { get; private set; } = new List<Jeep>();
    public List<TerrainObject> TerrainObjects { get; private set; } = new List<TerrainObject>();

    public GameModel() 
    {
        //GameSpeed = 1f;
        //Animals = new List<Animal>();
        //Groups = new List<Group>();
        //Persons = new List<Person>();
        //Jeeps = new List<Jeep>();
        //TerrainObjects = new List<TerrainObject>();
    }

    public GameModel(int money, List<Animal> animals, List<Group> groups, 
        List<Person> persons, List<Jeep> jeeps, List<TerrainObject> terrainObjects)
    {
        GameSpeed = 1f;
        this.Money = money;
        this.Animals = animals;
        groups = new List<Group>();
        this.Persons = persons;
        this.Jeeps = jeeps;
        this.TerrainObjects = terrainObjects;

        foreach (Animal a in animals)
        {
            if (!groups.Contains(a.Group)) groups.Add(a.Group);
        }
    }

    public void ChangeGameSpeed() //mindig ez a 3 speed kozul lehet valtani ilyen modon
    {                               // 1 -> 2 -> 4 -> 1 -> ...
        if(GameSpeed == 1f)
        {
            GameSpeed = 2f;
            return;
        }
        if(GameSpeed == 2f)
        {
            GameSpeed = 4f;
            return;
        }
        if(GameSpeed == 4f)
        {
            GameSpeed = 1f;
        }
    }

    public void AddMoney(int amount) => Money += amount;
    public void RemoveMoney(int amount) => Money -= amount;   

    public void AddAnimal(Animal animal)
    {
        Animals.Add(animal);
        if(!Groups.Contains(animal.Group)) Groups.Add(animal.Group);
    }
    public void RemoveAnimal(Animal animal)
    {
        Group group = animal.Group;
        Animals.Remove(animal);
        group.RemoveAnimal(animal);
        if(group.Animals.Count == 0) Groups.Remove(group); 
    }

    public void AddPerson(Person person) => Persons.Add(person);
    public void RemovePerson(Person person) => Persons.Remove(person);

    public void AddJeep(Jeep jepp) => Jeeps.Add(jepp);
    public void RemoveJeep(Jeep Jeep) => Jeeps.Remove(Jeep);

    public void AddTerrainObjects(TerrainObject terrainObject) => TerrainObjects.Add(terrainObject);
    public void RemoveTerrainObjects(TerrainObject terrainObject) => TerrainObjects.Remove(terrainObject);

    public void SetNextIDAfterLoad()
    {
        if (Animals.Count > 0) Animal._nextID = Animals.Max(a => a.ID) + 1;
        if (Persons.Count > 0) Person._nextID = Persons.Max(p => p.ID) + 1;
        if (Jeeps.Count > 0) Jeep._nextID = Jeeps.Max(j => j.ID) + 1;
        if (TerrainObjects.Count > 0) TerrainObject._nextID = TerrainObjects.Max(t => t.ID) + 1;
    }
}

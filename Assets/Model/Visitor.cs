using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Visitor : Person
{
    public int Id { get; private set; } 
    private int animalSeenCount = 0;
    private int differentAnimalTypeSeenCount = 0; //rossz nevek tudom, szabadon lehet atnevezni
    private List<Animal> animalsSeen;
    //public Jeep Jeep { get; private set; } vedd ki a kommentet ha kesz van a Jeep osztaly

    [JsonConstructor]
    public Visitor(int id, Vector3 position, PersonType type, int animalSeenCount, int differentAnimalTypeSeenCount, List<Animal> animalsSeen)
        : base(id, position, type)
    {
        this.Id = id;
        this.animalSeenCount = animalSeenCount;
        this.differentAnimalTypeSeenCount = differentAnimalTypeSeenCount;
        this.animalsSeen = animalsSeen ?? new List<Animal>();
    }

    public Visitor(Vector3 position, int id) : base(position)
    {
        Id = id;
        animalsSeen = new List<Animal>();
    }

    public int HappinesLevel()
    {
        return 1;
        //ide kell majd egy szamitas, az animalSeenCount es a differentAnimalTypeSeenCount alapjan
    }

    public void AnimalSeen(Animal animal)
    {
        bool newType = true;
        foreach (Animal a in animalsSeen)
        {
            if (a.Type == animal.Type) newType = false;
        }
        ++animalSeenCount;
        if (newType) ++differentAnimalTypeSeenCount;
    }

    //public void AddJeep(Jeep jeep) //vedd ki a kommentet ha kesz van a Jeep osztaly
    //{
    //    Jeep = jeep;
    //}
}

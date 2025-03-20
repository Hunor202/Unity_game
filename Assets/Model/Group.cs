using Newtonsoft.Json;
using System.Collections.Generic;

public class Group
{
    public List<Animal> Animals { get; private set; }
    public AnimalType Type { get; private set; } //nem biztos kell de jol johet
    public bool IsAbleToReproduce { get; private set; }

    [JsonConstructor]
    public Group(List<Animal> animals, AnimalType type, bool isAbleToReproduce)
    {
        Animals = animals ?? new List<Animal>();
        Type = type;
        IsAbleToReproduce = isAbleToReproduce;
        CheckAdults();
    }

    public Group(Animal animal)
    {
        Type = animal.Type;
        Animals = new List<Animal>();
        AddAnimal(animal);
    }

    public void AddAnimal(Animal animal)
    {
        if(animal.Type != Type) return;
        Animals.Add(animal);
        animal.AgeChanged += CheckAdults;
        CheckAdults();
    }
    public void RemoveAnimal(Animal animal)
    {
        Animals.Remove(animal);
        animal.AgeChanged -= CheckAdults;
        CheckAdults();
    }
    private void CheckAdults() //ha hozzaadunk egy allatot, vagy eltavolitunk egyet, vagy ha valtozik az eltkora egy allatnak
    {
        int adultCount = 0;
        foreach (var animal in Animals)
        {
            if (animal.IsAdult()) ++adultCount;
        }
        if (adultCount > 2) IsAbleToReproduce = true;
    }
}

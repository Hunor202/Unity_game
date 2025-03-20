using Newtonsoft.Json;
using UnityEngine;

public class Lion : Animal
{
    [JsonConstructor]
    public Lion(int id, Vector3 position, float age, int hunger, int thirst, bool isAlive, Group group)
        : base(id, position, age, hunger, thirst, isAlive, group)
    {
        this.Type = AnimalType.LION;
    }
    public Lion(Vector3 position, float age)
        : base(position, age)
    {
        this.Type = AnimalType.LION;
    }
    public override void Drink()
    {
        throw new System.NotImplementedException();
    }

    public override void Eat()
    {
        throw new System.NotImplementedException();
    }
}

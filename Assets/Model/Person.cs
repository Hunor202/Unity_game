using Newtonsoft.Json;
using UnityEngine;

public abstract class Person
{
    public static int _nextID = 1;
    public int ID { get; private set; }
    public Vector3 Position { get; private set; }
    public PersonType Type { get; protected set; }

    [JsonConstructor]
    public Person(int id, Vector3 position, PersonType type)
    {
        ID = id;
        Position = position;
        Type = type;
    }

    public Person(Vector3 position)
    {
        ID = _nextID++;
        Position = position;
    }
    //atneztem az UML diagramon a t�bbbi dolgot de nem vagyak biztos benne,
    //hogy az alap personnek enn�l t�bb kell vagy ha igen akkor lehet modos�tani kell az UML-t
}

public enum PersonType
{
    VISITOR,
    POCHER,
    CARETAKER
}
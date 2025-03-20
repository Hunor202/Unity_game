using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal
{
    public static int _nextID = 1;

    public static int _maxThirst = 10;           // ha eleri szomjanhal :(
    public static int _maxHunger = 10;           // same

    public static int _needWater = 5;            // ha eleri, elmegy vizet keresni
    public static int _needFood = 5;             // same

    public static int _adult_age = 5;
    public static int _old_age = 10;


    public int ID { get; private set; }
    public event Action AgeChanged;
    //a lathatosagot majd modositjuk ha megvan a vegleges verzio
    public Vector3 Position { get; private set; }
    public int Price { get; private set; } //nem biztos hogy ide kell
    public AnimalType Type { get; protected set; }
    public Group Group { get; private set; }
    public int Hunger { get; private set; }
    public int Thirst { get; private set; }
    public float Age { get; private set; }


    public List<TerrainObject> discoveredLakes { get; private set; }
    public List<TerrainObject> discoveredFoodSources { get; private set; }

    [JsonConstructor]
    public Animal(int id, Vector3 position, float age, int hunger, int thirst, bool isAlive, Group group)
    {
        ID = id;
        Position = position;
        Age = age;
        Hunger = hunger;
        Thirst = thirst;
        IsAlive = isAlive;
        Group = group;
    }

    public Animal(Vector3 position, float age) //age azert kell mert szuletni is tud de venni is lehet
    {
        ID = _nextID++;
        Position = position;
        Age = age;
        Group = new Group(this);
    }

    public void SetGroup(Group group) => Group = group;
    public virtual bool IsAdult() => Age > _adult_age; //leeht maskepp oldjuk meg, az 5-os szam is lehet mas
    public bool IsAlive { get; private set; } = true; //nem biztos hogy kell

    public virtual void IncreaseAge()
    {
        Age += 0.2f;
        if (Age > _adult_age) AgeChanged?.Invoke(); //lehetne szebb is de jo lesz
        if (Age > _old_age) this.Die();
    }
    public abstract void Eat(); //nem biztos abstract lesz attol fugg, hogy minden allatfajtanal ugyan az az ertek lesz-e majd
    public abstract void Drink(); //same
    public virtual void IncreaseHungerAndThirst()
    {
        ++Thirst;
        ++Hunger;
        if(Hunger >= _maxHunger || Thirst >= _maxThirst) this.Die();
    }
    public virtual bool isThirsty()
    {
        return (Thirst > _needWater);
    }
    public virtual bool isHungry()
    {
        return (Hunger > _needFood);
    }
    public virtual void findFood()
    {
        // etel kereses, vagy ha van mar felfedezett pont akkor oda megy
        /// TODO
    }
    public virtual void findWater()
    {
        /// TODO
    }
    public void Die() => IsAlive = false; //nem biztos hogy kell
}

public enum AnimalType  //lehet jobb lesy ha a konkret typeot taroljuk
{
    LION,
    BEAR,
    ZEBRA,
    GIRAFFE,
    TIGER,
    ELEPHANT,
    DEER,
    HIPPO,
    RHINO
}
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Jeep
{
    public static int _nextID = 1;
    public int ID { get; private set; }
    public Vector3 position { get; private set; }
    public List<Visitor> passengers;
    public bool IsMoving { get; private set; } = false;

    private int maxPassengerCount = 4;
    private int CurrentPassengerCount = 0;

    [JsonConstructor]
    public Jeep(int id, Vector3 position, List<Visitor> passengers, bool isMoving)
    {
        ID = id;
        this.position = position;
        this.passengers = passengers ?? new List<Visitor>();
        this.IsMoving = isMoving;
        this.CurrentPassengerCount = this.passengers.Count;
    }

    public Jeep(Vector3 position)
    {
        ID = _nextID++;
        this.position = position;
        passengers = new List<Visitor>();
    }

    public void AddPassenger(Visitor visitor)
    {
        if (CurrentPassengerCount < maxPassengerCount && !IsMoving)
        {
            passengers.Add(visitor);
            ++CurrentPassengerCount;
        }
    }

    public void RemovePassenger(Visitor visitor)
    {
        passengers.Remove(visitor);
        --CurrentPassengerCount;
    }

    public void StartTour() => IsMoving = true;
    public void EndTour() => IsMoving = false;
    public bool IsFull() => CurrentPassengerCount == maxPassengerCount; 
}

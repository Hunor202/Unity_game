using Newtonsoft.Json;
using UnityEngine;

public class TerrainObject
{
    public static int _nextID = 1;
    public int ID { get; private set; }
    public TerrainObjectType type { get; private set; }
    public Vector3 position { get; private set; }

    [JsonConstructor]
    public TerrainObject(int id, TerrainObjectType type, Vector3 position)
    {
        ID = id;
        this.type = type;
        this.position = position;
    }

    public TerrainObject(TerrainObjectType type, Vector3 position)
    {
        ID = _nextID++;
        this.type = type;
        this.position = position;
    }




    // i dont think we need inheritance here, since they dont have much functionality
    // simple factory methods
    public static TerrainObject createBush(Vector3 position)
    {
        return new TerrainObject(TerrainObjectType.BUSH, position);
    }
    public static TerrainObject createGrass(Vector3 position)
    {
        return new TerrainObject(TerrainObjectType.GRASS, position);
    }
    public static TerrainObject createTree(Vector3 position)
    {
        return new TerrainObject(TerrainObjectType.TREE, position);
    }
    public static TerrainObject createLake(Vector3 position)
    {
        return new TerrainObject(TerrainObjectType.LAKE, position);
    }
    public static TerrainObject createRoad(Vector3 position)
    {
        return new TerrainObject(TerrainObjectType.ROAD, position);
    }
}

public enum TerrainObjectType
{
    BUSH, GRASS, TREE, LAKE, ROAD
}
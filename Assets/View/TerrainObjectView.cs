using UnityEngine;

public class TerrainObjectView : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TerrainObject terrain { get; private set; }

    public void Initialize(TerrainObject terrain)
    {
        this.terrain = terrain;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

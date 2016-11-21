using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
    public GameObject[] squarePrefab;

    //TODO: Make it so the user can save the world.
    //NOTE: Possibly add a seed generator in to this world creator.

    //Size of the map in terms of hexes not in terms of world space.
    public int mapWidth;
    public int mapHeight;

    //Storing the last type of hex placed in the world
    int lastTile = 0;
    int nextTile = 0;
    int listLength;
    string leftTileTag;
    public List<GameObject> tileList = new List<GameObject>();

    // Use this for initialization
    void Start () {

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                nextTile = TileSelector(lastTile , leftTileTag);

                GameObject tileGameObject = (GameObject)Instantiate(squarePrefab[nextTile], new Vector3(x, 0, y), Quaternion.identity);
                               
                tileList.Add(tileGameObject);
                listLength = tileList.Count;

                GameObject leftTile = tileList[GetTileLeft(x, listLength)];
                leftTileTag = leftTile.tag;
                lastTile = nextTile;

                tileGameObject.name = "Tile_" + x + "_" + y;
                tileGameObject.transform.SetParent(this.transform);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

    }

    //This selects the element from GameObject[] squarePrefab with some built in logic.
    //TODO: Make it so it can/does use a pixel map made by the user to generate the game world allowing for more complex structures.
     int TileSelector(int lastTile , string leftTileTag)
    {
        bool leftTileWasWater = true;

        if(leftTileTag == "Grass" || leftTileTag == "Forrest" || leftTileTag == "Mountain")
        {
            leftTileWasWater = false;
        }

        int maxWaterChance = WaterTileCheck(lastTile , leftTileWasWater);

        int chance = (int)Random.Range(0f, 101f);

        if (chance <= maxWaterChance)
        {            
            return 0;
        }
        else if(chance >= maxWaterChance + 1 && chance <= 75)
        {            
            return 1;
        }
        else if(chance >= 76 && chance <= 91)
        {
            return 2;
        }
        else if(chance >= 92 && chance <= 100)
        {
             return 3;
        }
        else
        {
            Debug.Log("Error");
            return 0;
        }
    }

    //Sets the final percentages for the spawning of water.
    int WaterTileCheck(int lastTile , bool leftTile)
    {
        if (lastTile == 0 || leftTile == true)
        {
            return 50;
        }
        else if(lastTile == 0 && leftTile == true)
        {
            return 73;
        }
        else
        { 
            return 25;
        }
    }

    int GetTileLeft(int currentXCoord , int listLength)
    {
        if(currentXCoord == 0)
        {
            return 0;
        }
        else
        {            
            return listLength - 10;
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

//Ingredient Spawner - STK
//

public class spawnIngredient : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    private List<GameObject> bin = new List<GameObject>();

    //on start spawns objects for debug
    public void Start()
    {
        //spawnObjects();
    }
    //spawns all objects in list
    public void spawnObjects()
    {
        float i = 0f;

        foreach(GameObject obj in ingredients)
        {
            Debug.Log($"{obj} spawning");
            //add objects to list for cleanup later
            bin.Add(Instantiate(obj, new Vector3((-0.5f + i), 1.4f, 24.5f), Quaternion.identity));
            i += 0.3f;
        }
    }
    //deletes all objects in bin
    public void removeObjects()
    {
        foreach(GameObject obj in bin)
        {
            Destroy(obj);
        }
        bin = new List<GameObject>();
    }
}

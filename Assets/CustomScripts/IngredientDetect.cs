using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Ingredient Detector - STK
//

public class MiniGameBurger : MonoBehaviour
{
    //Tags of ingredients
    private List<string> recipeList = new List<string>()
    {
        "topBun","bottomBun","patty","cheese","lettuce","tomato"
    };
    //Lists to check placed ingredients and new recipe
    private List<string> recipe = new List<string>();
    private List<string> recipeCheck = new List<string>();
    
    //List to see what ingredients are on the counter
    public List<GameObject> ingredientsInside = new List<GameObject>();
    //List of images to show on order details
    public List<GameObject> imagesIngredients = new List<GameObject>();
    //List of images to end day
    public List<GameObject> endDay = new List<GameObject>();
    //Total orders
    public int totalOrders = 3;
    //Reference to ingredient spawner
    public GameObject ingredientSpawner;
    //entire floor teleportation collider
    public GameObject teleportFloor;

    private int recipesCompleted = 0;
    private int randomNum = 0;
    private int number = 0;

    private float time = 0f;
    private bool timerRun = false;

    public AudioSource sucessAudio;
    //When ingredient enters it is added to list of ingredients on the counter
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (!recipe.Contains(obj.tag)) return;

        if (!ingredientsInside.Contains(obj))
        {
            ingredientsInside.Add(obj);
            if(!recipeCheck.Contains(obj.tag)) recipeCheck.Add(obj.tag);
            //Extreme logging to find out what was happening . intense pain and suffering during this part-stk
            Debug.Log($"Added: {obj.name} | Count: {ingredientsInside.Count}");
            Debug.Log($"{randomNum} random num | {recipeCheck.Count} recipes count");
            Debug.Log($"{recipesCompleted} recipes complete | {recipe.Count} recipes count");
        }
    }

    //When ingredient exits it is removed from list of ingredients on the counter
    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (!recipe.Contains(obj.tag)) return;

        if (ingredientsInside.Contains(obj))
        {
            ingredientsInside.Remove(obj);
            if(recipeCheck.Contains(obj.tag)) recipeCheck.Remove(obj.tag);
            Debug.Log($"Removed: {obj.name} | Count: {ingredientsInside.Count}");
        }
    }
    //On awake start timer and make new recipe
    private void Awake()
    {
        timerRun = true;
        NewRecipe();
    }
    //Check ever 0.2 seconds whether the counter has the required ingredients
    private void Update()
    {
        if (timerRun)
        {
            time += Time.deltaTime;
            //Check Recipe every quarter second
            if(System.Math.Round(time, 2)%0.25f == 0.00f)
            {
                //Debug.Log($"{System.Math.Round(time,2)} timer");
                CheckRecipe();
            }
        }
    }
    //Check if the counter ingredients match the recipe
    public void CheckRecipe()
    {
        //If recipes complete is less than total orders and recipe list matches the recipes on the counter, execute
        if((recipe.Count <= recipeCheck.Count) && (recipesCompleted < totalOrders))
        {
            recipesCompleted++;
            Debug.Log($"{recipesCompleted} Recipes Complete!!!!");
            OnRecipeCompleted();
        }
        //if recipes completed is over or equal to toal orders
        else if(recipesCompleted >= totalOrders)
        {
            //End script
            Debug.Log("Ending Fast Food Script");
            clearOrder();
            timerRun = false;
            ingredientSpawner.GetComponent<spawnIngredient>().removeObjects();
            teleportFloor.SetActive(true);
            //Change sign to say exit
            endDay[0].GetComponent<TextMeshProUGUI>().enabled = false;
            endDay[1].GetComponent<TextMeshProUGUI>().enabled = true;
            this.enabled = false;
        }     
    }
    //testing stuff
    private void printNum()
    {
        number++;
        Debug.Log($"{number}");
    }
    //Makes a new recipe with random number of ingredients
    private void NewRecipe()
    {
        //Easier for me to find it in log
        Debug.Log("AWAKEN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        randomNum = Random.Range(3,7);
        Debug.Log($"{randomNum} random number");
        //Reset recipe lists
        recipe = new List<string>();
        recipeCheck = new List<string>();
        //Add ingredients to recipe list
        for (int i = 0 ; i < randomNum ; i++)
        {
            recipe.Add(recipeList[i]);
            Debug.Log($"{imagesIngredients} enable image");
            //Enables specific images of ingredients on order details board
            imagesIngredients[i].GetComponent<Image>().enabled = true;
            Debug.Log($"{recipeList[i]} ingredient");
        }
        //spawn ingredients
        ingredientSpawner.GetComponent<spawnIngredient>().spawnObjects();
    }
    //Clears order detail board
    private void clearOrder()
    {
        Debug.Log("Clearing Order");
        for(int i = 0; i < imagesIngredients.Count; i++)
        {
            Debug.Log($"{imagesIngredients} disable image");
            imagesIngredients[i].GetComponent<Image>().enabled = false;
        }
    }
    //Deletes ingredients on counter and plays audio
    private void OnRecipeCompleted()
    {
        Debug.Log("Recipes continues!");
        
        //Play sound
        sucessAudio.Play();
        //Remove ingredients
        foreach (GameObject obj in ingredientsInside)
        {
            Destroy(obj);
        }
        ingredientsInside = new List<GameObject>();
        //clear order board
        clearOrder();
        //clear ingredients
        ingredientSpawner.GetComponent<spawnIngredient>().removeObjects();
        //make new recipe
        NewRecipe();
    }
}

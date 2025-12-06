using System.Collections.Generic;
using UnityEngine;

public class IngredientDetect : MonoBehaviour
{
    private List<string> recipeList = new List<string>()
    {
        "topBun","bottomBun","patty","cheese","lettuce","tomato"
    };
    private List<string> recipe = new List<string>();
    private List<string> recipeCheck = new List<string>();

    public List<GameObject> ingredientsInside = new List<GameObject>();

    public int totalOrders = 3;
    private int recipesCompleted = 0;
    private int randomNum = 0;

    private int number = 0;

    public AudioSource sucessAudio;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (!recipe.Contains(obj.tag)) return;

        if (!ingredientsInside.Contains(obj))
        {
            ingredientsInside.Add(obj);
            if(!recipeCheck.Contains(obj.tag)) recipeCheck.Add(obj.tag);
            Debug.Log($"Added: {obj.name} | Count: {ingredientsInside.Count}");
            Debug.Log($"{randomNum} random num | {recipeCheck.Count} recipes count");
            Debug.Log($"{recipesCompleted} recipes complete | {recipe.Count} recipes count");
        }
    }

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

    private void Awake()
    {
        NewRecipe();
    }

    private void FixedUpdate()
    {
        CheckRecipe();
    }

    public void CheckRecipe()
    {
        if((recipe.Count <= recipeCheck.Count) && (recipesCompleted < totalOrders))
        {
            recipesCompleted++;
            Debug.Log($"{recipesCompleted} Recipes Complete!!!!");
            OnRecipeCompleted();
        }
        else if(recipesCompleted >= totalOrders)
        {
            //End script
            Debug.Log("Ending Fast Food Script");
            if(recipesCompleted >= totalOrders) this.enabled = false;
        }     
    }

    private void printNum()
    {
        number++;
        Debug.Log($"{number}");
    }

    private void NewRecipe()
    {
        Debug.Log("AWAKEN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        randomNum = Random.Range(3,7);
        Debug.Log($"{randomNum} random number");

        recipe = new List<string>();
        recipeCheck = new List<string>();

        for (int i = 0 ; i < randomNum ; i++)
        {
            recipe.Add(recipeList[i]);
            Debug.Log($"{recipeList[i]} ingredient");
        }
    }

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

        //Delay 1 sec
        //New recipe
        NewRecipe();
        

        // Send UnityEvent, etc.
    }
}

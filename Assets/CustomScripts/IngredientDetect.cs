using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDetect : MonoBehaviour
{
    private List<string> recipeList = new List<string>()
    {
        "topBun","bottomBun","patty","cheese","lettuce","tomato"
    };
    private List<string> recipe = new List<string>();
    private List<string> recipeCheck = new List<string>();

    public List<GameObject> ingredientsInside = new List<GameObject>();
    public List<GameObject> imagesIngredients = new List<GameObject>();

    public int totalOrders = 3;
    private int recipesCompleted = 0;
    private int randomNum = 0;

    private int number = 0;
    private bool timerRun = false;
    private float time = 0f;

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
        timerRun = true;
        NewRecipe();
    }

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
            clearOrder();
            timerRun = false;
            this.enabled = false;
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
            Debug.Log($"{imagesIngredients} enable image");
            imagesIngredients[i].GetComponent<Image>().enabled = true;
            Debug.Log($"{recipeList[i]} ingredient");
        }
    }

    private void clearOrder()
    {
        Debug.Log("Clearing Order");
        for(int i = 0; i < imagesIngredients.Count; i++)
        {
            Debug.Log($"{imagesIngredients} disable image");
            imagesIngredients[i].GetComponent<Image>().enabled = false;
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
        clearOrder();
        NewRecipe();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class IngredientDetect : MonoBehaviour
{
      public List<string> recipe = new List<string>()
    {
        "lettuce","cheese","tomato","patty","topBun","bottomBun","drink","fries"
    };

    public List<GameObject> ingredientsInside = new List<GameObject>();
    private List<string> recipeCheck = new List<string>();

    private bool recipeCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (!recipe.Contains(obj.tag)) return;

        if (!ingredientsInside.Contains(obj))
        {
            ingredientsInside.Add(obj);
            if(!recipeCheck.Contains(obj.tag)) recipeCheck.Add(obj.tag);
            Debug.Log($"Added: {obj.name} | Count: {ingredientsInside.Count}");
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

    private void Update()
    {
        if (CheckRecipe() && (!recipeCompleted))
        {
            recipeCompleted = true;
            Debug.Log("Recipe Complete!!!!");
            OnRecipeCompleted();
        }
    }

    public bool CheckRecipe()
    {
        if(recipe.Count <= recipeCheck.Count)
        {
            //OnRecipeCompleted();
            return true;
        }
        else
        {
            //Debug.Log("Recipe Not Complete!!!");
            return false;
        }
    }

    private void OnRecipeCompleted()
    {
        Debug.Log("Recipe continues!");
        return;
        //Debug.Log("Recipe Completed!");

        // Add your game logic here:
        // Play sound
        // Trigger animation
        // Spawn completed burger
        // Score points
        // Send UnityEvent, etc.
    }





    // Get the number of ingredients inside
    public int GetIngredientCount()
    {
        return ingredientsInside.Count;
    }
}

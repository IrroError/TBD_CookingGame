using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    
    public static DeliveryManager Instance { get; private set; }
    
    
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private int successRecipesAmount;


    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        successRecipesAmount = 0;
    }

    public void AddCustomerRecipe()
    {
        // Called externally when a new customer joins the line
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
        
        waitingRecipeSOList.Add(waitingRecipeSO);
        
        OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
    }
    

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        // If no one in line, do nothing
        if (waitingRecipeSOList.Count == 0) return;
        
        // Only look at the front guy (Index 0)
        RecipeSO waitingRecipeSO = waitingRecipeSOList[0];
        
        bool plateContentsMatchesRecipe = true;

        if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
        {
            plateContentsMatchesRecipe = true;
            foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
            {
                bool ingredientFound = false;
                foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                {
                    if (plateKitchenObjectSO == recipeKitchenObjectSO)
                    {
                        ingredientFound = true;
                        break;
                    }
                }

                if (!ingredientFound)
                {
                    plateContentsMatchesRecipe = false;
                }
            }
        }

        if (plateContentsMatchesRecipe)
        {
            // Player delivered the correct recipe to the front guy
            waitingRecipeSOList.RemoveAt(0);
            successRecipesAmount += 1;
            
            OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
            OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            waitingRecipeSOList.RemoveAt(0);
            
            OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
        
        
        // *** Old logic for handling delivers base on comparing to the whole waiting list
        // for (int i = 0; i < waitingRecipeSOList.Count; i++)
        // {
        //     RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
        //     if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
        //     {
        //         // Quick test if the number of ingredients match (In this loop is match)
        //         bool plateContentsMatchesRecipe = true;
        //         foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
        //         {
        //             bool ingredientFound = false;
        //             // Cycling through all ingredients in the Recipe
        //             foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        //             {
        //                 // Cycling through all ingredients on the Plate
        //                 if (plateKitchenObjectSO == recipeKitchenObjectSO)
        //                 {
        //                     // Ingredient match!
        //                     ingredientFound = true;
        //                     break;
        //                 }
        //             }
        //
        //             if (!ingredientFound)
        //             {
        //                 // This Recipe ingredient was not found on the Plate
        //                 plateContentsMatchesRecipe = false;
        //             }
        //         }
        //
        //         if (plateContentsMatchesRecipe)
        //         {
        //             // Player delivered the correct recipe
        //             waitingRecipeSOList.RemoveAt(i);
        //             successRecipesAmount++;
        //             
        //             OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        //             OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
        //             
        //             return;
        //         }
        //     }
        // }
        // // No matches found, player did not deliver a correct recipe
        // OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessRecipesAmount()
    {
        return successRecipesAmount;
    }
}
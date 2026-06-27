using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLineManager : MonoBehaviour
{
    
    [SerializeField] private Transform[] customerPositionArray;
    [SerializeField] private Customer customerPrefab;

    private List<Customer> customerList;
    private float spawnTimer;
    private float spawnTimerMax = 4f;   // Timer before a new Customer join the queue

    private void Awake()
    {
        customerList = new List<Customer>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        HandleFrontCustomerLeaving(false);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        HandleFrontCustomerLeaving(true);
    }

    private void HandleFrontCustomerLeaving(bool isHappy)
    {
        if (customerList.Count > 0)
        {
            Customer frontCustomer = customerList[0];
            customerList.RemoveAt(0);
            
            // Pass the outcome to the customer so they react accordingly
            frontCustomer.Leave(isHappy);
            
            // Tell everyone else to step forward to their new positions
            for (int i = 0; i < customerList.Count; i++)
            {
                customerList[i].SetTargetPosition(customerPositionArray[i].position);
            }
        }
    }

    private void Update()
    {
        // Only spawn new customers if there is room in the line
        if (customerList.Count < customerPositionArray.Length)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                spawnTimer = spawnTimerMax;
                SpawnCustomer();
            }
        }
    }

    private void SpawnCustomer()
    {
        // Spawn them physically at the back of the line
        Transform spawnTransform = customerPositionArray[customerPositionArray.Length - 1];
        Customer newCustomer = Instantiate(customerPrefab, spawnTransform.position, spawnTransform.rotation);
        newCustomer.InitializePosition(spawnTransform.position);
        
        // Immediately assign them their actual spot in the line
        newCustomer.SetTargetPosition(customerPositionArray[customerList.Count].position);
        
        customerList.Add(newCustomer);
        
        // Tell the DeliveryManager to grab a random recipe and update the UI
        DeliveryManager.Instance.AddCustomerRecipe();
    }
}
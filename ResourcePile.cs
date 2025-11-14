using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item; // Reference to the resource item it will produce

    public float ProductionSpeed = 0.5f;

    private float m_CurrentProduction = 0.0f;

    // Each update, incremenet a production counter by the production speed; Once above 1, new resources are created and the counter is decremented  
    private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += ProductionSpeed * Time.deltaTime;
        }
    }

    // Overrides the GetData function for  IUIInfoContent of Building to give InfoPanel the production speed as data
    public override string GetData()
    {
        return $"Producing at the speed of {ProductionSpeed}/s";
        
    }
    
    
}

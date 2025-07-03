using NUnit.Framework.Constraints;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Visibility : MonoBehaviour
{
    public GameObject cupboard;
    public GameObject water;
 
    
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cupboard != null)
            {
                SetCupboardVisibility(false);
            }
            
            if (water != null)
            {
                SetWaterVisibility(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetCupboardVisibility(true);
            SetWaterVisibility(false);
        }
        
    }

    void SetCupboardVisibility(bool visibile)
    {
        if (cupboard != null)
        {
            Renderer renderer = cupboard.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = visibile;
            }
        }
    }

    void SetWaterVisibility(bool isVisible)
    {
        if (water != null)
        {
            Renderer waterRenderer = water.GetComponent<Renderer>();
                if (waterRenderer != null)
            {
                waterRenderer.enabled = isVisible;
            }
        }
    }

    private void Start()
    {
        SetWaterVisibility(false);
    }

  
  

}



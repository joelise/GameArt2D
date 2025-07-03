using NUnit.Framework.Constraints;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Visibility : MonoBehaviour
{
    public GameObject cupboard;
    public bool initiallyVisible = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cupboard != null)
            {
                SetVisibility(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetVisibility(true);
        }
        
    }

    void SetVisibility(bool visibile)
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {

    }
}


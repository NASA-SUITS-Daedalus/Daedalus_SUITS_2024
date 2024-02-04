using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePin : MonoBehaviour
{
     public GameObject imagePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = 0f; // Ensure the image is at the same depth as the camera

            GameObject newImage = Instantiate(imagePrefab, mousePosition, Quaternion.identity);

            // Set the parent to the top-level object (null) or a specific parent object
           // newImage.transform.parent = ActiveNavigationScreen.transform; // Set to null for top-level hierarchy

        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Button DE1VideoButton;
    public Button DE2VideoButton;
    public Button RoverVideoButton;
    public Image image1;
    public Image image2;
    public Image image3;

    void Start()
    {
        // Add listeners to the buttons
        DE1VideoButton.onClick.AddListener(ShowImage1);
        DE2VideoButton.onClick.AddListener(ShowImage2); 
        RoverVideoButton.onClick.AddListener(ShowImage3);
    }

    void ShowImage1()
    {
        // Activate image1 and deactivate image2, 3
        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(false);
        image3.gameObject.SetActive(false);
    }

    void ShowImage2()
    {
        // Activate image2 and deactivate image1, 3
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(true);
        image3.gameObject.SetActive(false);
    }

    void ShowImage3()
    {
        // Activate image3 and deactivate image1, 2
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
        image3.gameObject.SetActive(true);
    }
}

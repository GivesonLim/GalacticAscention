using UnityEngine;

public class ControlUIManager : MonoBehaviour
{
    public GameObject controlPanel;  // Reference to the Control Panel (UI Panel with controls)

    // Start is called before the first frame update
    void Start()
    {
        controlPanel.SetActive(false);  // Hide the control panel initially
    }

    // Call this function to show the control panel
    public void ShowControlPanel()
    {
        controlPanel.SetActive(true);  // Show the control panel
    }

    // Call this function to hide the control panel
    public void HideControlPanel()
    {
        controlPanel.SetActive(false);  // Hide the control panel
    }
}

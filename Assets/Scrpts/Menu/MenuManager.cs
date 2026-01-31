using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCamvas;
    public GameObject openMenuButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCamvas.SetActive(false);
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.Tab))
    //    {
    //        menuCamvas.SetActive(!menuCamvas.activeSelf);
    //    }
    //}

    public void OnClose()
    {
        menuCamvas.SetActive(false);
        openMenuButton.SetActive(true);
    }

    public void OnOpen()
    {
        menuCamvas.SetActive(true);
        openMenuButton.SetActive(false);
    }
}

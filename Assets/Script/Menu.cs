using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    GameObject g_canvas;
    GameObject infoPanel;
    GameObject infoPanelLive;
    // Start is called before the first frame update
    void Start()
    {
        g_canvas = GameObject.Find("Canvas");
        GameObject start = GameObject.Find("Canvas/Start");
        GameObject info = GameObject.Find("Canvas/Info");
        infoPanel = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Prefabs/UI/InfoPanel");  
        infoPanelLive = Utility.GameObjectRelate.InstantiateGameObject(g_canvas, infoPanel);
        infoPanelLive.name = infoPanel.name;
        Utility.GameObjectRelate.SetObjectActiveToggle(infoPanelLive);

        EventTriggerListener.Get(start).onUp += OnUp;
        EventTriggerListener.Get(info).onUp += OnUp;

    }

    private void OnUp(GameObject Btn)  //UI偵測
    {
        switch (Btn.name)
        {
            case "Start":
                SceneManager.LoadScene("GameLevel1");
                break;
            case "Info":
                CallInfoPanel();
                break;
            case "Back":
                Utility.GameObjectRelate.SetObjectActiveToggle(infoPanelLive);
                break;

        }
    }

    private void CallInfoPanel()
    {
        Utility.GameObjectRelate.SetObjectActiveToggle(infoPanelLive);

        GameObject back = GameObject.Find("Canvas/InfoPanel/Back");

        if (EventTriggerListener.Get(back).onUp == null)
        {
            EventTriggerListener.Get(back).onUp += OnUp;
        }
    }


}

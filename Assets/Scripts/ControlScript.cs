using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScript : MonoBehaviour
{

    public readonly static string[] POSSIBLECAMNAMES =
    {
        "Integrated Webcam",
        "Intel(R) RealSense(TM) Depth Camera 455  Depth",
        "Intel(R) RealSense(TM) Depth Camera 455  RGB",
        "Microsoft® LifeCam Studio(TM)",
        "OBS Virtual Camera"
        };

    public enum State : int
    {
        Main = 0, 
        Tooltip = 1,
        Spotlight = 2,
        Spotlight2 = 3
    }

    public State state;
    public readonly static string[] SCENES = { "MainScene", "ToolTip", "Spotlight", "Spotlight2" };
    public readonly static string MAINSCENE = "MainScene";
    public readonly static string TOOLTIP = "ToolTip";
    public readonly static string SPOTLIGHT = "Spotlight";
    public readonly static string SPOTLIGHT2 = "Spotlight2";


    // Start is called before the first frame update
    void Start()
    {
        string cScene = SceneManager.GetActiveScene().name;
        Debug.Log("Start " + cScene);
        Debug.Log("WebCams: " + WebCamTexture.devices.Length);
        switch (cScene)
        {
            case "MainScene":
                state = State.Main;
                break;
            case "ToolTip":
                state = State.Tooltip;
                break;
            case "Spotlight":
                state = State.Spotlight;
                break;
            case "Spotlight2":
                state = State.Spotlight2;
                break;
            default:
                Debug.Log("Scene not specified in State");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Choosing next scene
    public void NextScene(int nextScene)
    {
        Debug.Log("NextScene " + nextScene);
        switch (nextScene)
        {
            case 0:
                SceneManager.UnloadSceneAsync(SCENES[((int)state)]);
                state = State.Main;
                SceneManager.LoadScene(MAINSCENE);
                break;
            case 1:
                SceneManager.UnloadSceneAsync(SCENES[((int)state)]);
                state = State.Tooltip;
                SceneManager.LoadScene(TOOLTIP);
                break;
            case 2:
                SceneManager.UnloadSceneAsync(SCENES[((int)state)]);
                state = State.Spotlight;
                SceneManager.LoadScene(SPOTLIGHT);
                break;
            case 3:
                SceneManager.UnloadSceneAsync(SCENES[((int)state)]);
                state = State.Spotlight2;
                SceneManager.LoadScene(SPOTLIGHT2);
                break;
            default:
                Debug.Log("nextScene value out of bounds");
                break;
        }
    }
}

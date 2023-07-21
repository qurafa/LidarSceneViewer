using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpotlightCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject depthObject;
    [SerializeField]
    private GameObject videoObject;
    [SerializeField]
    private Canvas viewCanvas;
    [SerializeField]
    private bool depth = true;

    public GameObject observer;//the game object for the person observing the view
    static WebCamTexture spotLightTex;
    static WebCamDevice device;

    private readonly static string SPOTLIGHTCAM = "Intel(R) RealSense(TM) Depth Camera 455  RGB";

    // Start is called before the first frame update
    void Start()
    {
        if (!depth)
        {
            //Debug.Log("WebCams: " + WebCamTexture.devices.Length);
            int camIndex = -1;
            foreach (WebCamDevice d in WebCamTexture.devices)
            {
                camIndex++;
                if (d.name == SPOTLIGHTCAM)
                {
                    Debug.Log("RealSense CamFound");
                    break;
                }

            }

            if (spotLightTex == null && WebCamTexture.devices.Length >= 1)
            {
                device = WebCamTexture.devices[camIndex];
                spotLightTex = new WebCamTexture(device.name);
            }
            else return;

            videoObject.GetComponent<RawImage>().material.mainTexture = spotLightTex;
            
            spotLightTex.Play();
        }
    }

    //to toggle the lidar on or off
    public void SwitchFeed()
    {
        if (!depth)
            spotLightTex.Stop();
    }

    public void PausePlayFeed()
    {
        if (spotLightTex.isPlaying)
            spotLightTex.Pause();
        else
            spotLightTex.Play();
    }

    public void LockUnlock()
    {
        switch (viewCanvas.renderMode)
        {
            case RenderMode.ScreenSpaceCamera:
                viewCanvas.renderMode = RenderMode.WorldSpace;
                break;
            case RenderMode.WorldSpace:
                viewCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                break;
            default:
                viewCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                break;
        }
    }
}

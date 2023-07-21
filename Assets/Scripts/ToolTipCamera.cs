using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipCamera : MonoBehaviour
{

    static WebCamTexture toolTipTex;
    static WebCamDevice device;
    [SerializeField]
    private Canvas viewCanvas;

    private readonly static string[] POSSIBLECAMNAMES = 
        { 
        "Integrated Webcam",
        "Intel(R) RealSense(TM) Depth Camera 455  Depth",
        "Intel(R) RealSense(TM) Depth Camera 455  RGB",
        "Microsoft® LifeCam Studio(TM)", 
        "OBS Virtual Camera"
        };

    private readonly static string TOOLTIPCAM = "Microsoft® LifeCam Studio(TM)";

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("WebCams: " + WebCamTexture.devices.Length);
        //get index of the tool-tip cam
        int camIndex = -1;
        foreach(WebCamDevice d in WebCamTexture.devices)
        {
            camIndex++;
            if (d.name == TOOLTIPCAM)
                break;
        }

        if (toolTipTex == null && WebCamTexture.devices.Length >= 1)
        {
            device = WebCamTexture.devices[camIndex];
            toolTipTex = new WebCamTexture(device.name);
        }
        else return;

        GetComponent<RawImage>().material.mainTexture = toolTipTex;
        
        PausePlayFeed();
        LockUnlock();
    }

    public void PausePlayFeed()
    {
        if (toolTipTex.isPlaying)
            toolTipTex.Pause();
        else
            toolTipTex.Play();
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

    public void SwitchView()
    {
        toolTipTex.Stop();
    }
}

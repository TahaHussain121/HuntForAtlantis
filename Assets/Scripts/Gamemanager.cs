using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
public class Gamemanager : Singleton<Gamemanager>
{
    [SerializeField]
    private CinemachineVirtualCamera mainVCam;
    [SerializeField]
    private Transform character1;
    [SerializeField]
    private Transform character2;

    public static IInputHandler Character1 => Instance.character1.GetComponent<IInputHandler>();
    public static IInputHandler Character2 => Instance.character2.GetComponent<IInputHandler>();
    public static CinemachineVirtualCamera MainVCam=> Instance.mainVCam;

    private IInputHandler activeIInputHandler;
    public static IInputHandler ActiveIInputHandler => Instance.activeIInputHandler;

    private void Awake()
    {
        activeIInputHandler = Character1;
    }

    public static void SwitchCharacter()
    {
        Instance.activeIInputHandler = ActiveIInputHandler == Character1 ? Character2 : Character1;
        SwitchCamTarget();
    }

    
    private static void SwitchCamTarget()
    {
        MainVCam.Follow = ActiveIInputHandler.GetTransform();
    }

    
}

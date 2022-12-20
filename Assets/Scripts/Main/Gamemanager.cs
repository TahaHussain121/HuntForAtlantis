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
    private GamestateManager stateManager;

    internal static void GameOver()
    {
        //throw new NotImplementedException();
    }

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
        Switch(Instance.character1, true);
        Switch(Instance.character2, false);
    }

    public static void SwitchCharacter()
    {
        if (ActiveIInputHandler == Character1)
        {
            Instance.activeIInputHandler = Character2;

            Switch(Instance.character2, true);

            Switch(Instance.character1, false);
        }
        else
        {
            Instance.activeIInputHandler = Character1;
            Switch(Instance.character1, true);

            Switch(Instance.character2, false);
        }

        SwitchCamTarget();
    }

    
    private static void SwitchCamTarget()
    {
        MainVCam.Follow = ActiveIInputHandler.GetTransform();
    }

    private static void Switch(Transform Char,  bool chControl)
    {

        Char.GetComponent<CharacterController>().enabled = chControl;

    }

    //if levels are tuts levels off character switching
}

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
    private GamestateManager stateManager; //empty?


    [SerializeField]
    private Transform character2;
    internal static void GameOver()
    {
        //throw new NotImplementedException();
    }

    public static IInputHandler Character1 => Instance.character1.GetComponent<IInputHandler>();
    public static IInputHandler Character2 => Instance.character2.GetComponent<IInputHandler>();
    public static CinemachineVirtualCamera MainVCam=> Instance.mainVCam;

    private IInputHandler activeIInputHandler;
    public static IInputHandler ActiveIInputHandler => Instance.activeIInputHandler;

    private void Awake()
    {
        activeIInputHandler = Character1;

        if (Instance.character1 != null) Switch(Instance.character1, true);
        if (Instance.character2 != null) Switch(Instance.character2, false);

    }

    public static void SwitchCharacter()
    {
        if (Instance.character1 == null || Instance.character2 == null) return;
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
        Char.GetComponent<CapsuleCollider>().enabled = chControl;


    }
}

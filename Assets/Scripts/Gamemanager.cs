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
    private CharacterController _controller_1;
    [SerializeField]
    private CharacterController _controller_2;

    public static CharacterController Controller1 => Instance._controller_1;
    public static CharacterController Controller2 => Instance._controller_2;
    public static CinemachineVirtualCamera MainVCam=> Instance.mainVCam;


    private static CharacterController _activeCaracterController;
    private void Awake()
    {
        _activeCaracterController = _controller_1;
    }

    public static CharacterController SwitchCharacter()
    {
        _activeCaracterController.GetComponent<PlayerFighter>().enabled = false;
        _activeCaracterController = _activeCaracterController == Controller1 ? Controller2 : Controller1;
        //print("New Active Character = " + _activeCaracterController.name);
        MainVCam.Follow = _activeCaracterController.transform;
        _activeCaracterController.GetComponent<PlayerFighter>().enabled = true;
        return _activeCaracterController;
    }
}

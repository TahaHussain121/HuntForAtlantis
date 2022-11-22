using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attach this script to a collider
/// </summary>
public class LevelManager : MonoBehaviour
{

    /// <summary>
    /// add name of scenes here
    /// </summary>
    public enum Levels { MainMenu, GamePlay }

    /// <summary>
    /// Assign Name of next scene to be loaded
    /// </summary>
    [SerializeField] Levels nextLevel;


/// <summary>
/// if player pass throw collider next scene will be loaded
/// </summary>
/// <param name="other"></param>
     void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
            Debug.Log("scene change");
            LoadNextLevel();
        }

    }


    /// <summary>
    /// This fucntion restart the same scene
    /// </summary>
    /// <param name="other"></param>
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    /// <summary>
    /// Function to load next level
    /// </summary>
     void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel.ToString());
    }
}

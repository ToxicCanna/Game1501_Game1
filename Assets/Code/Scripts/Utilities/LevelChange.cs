using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] int sceneBuildIndex;

    public void SceneSwitch()
    {
        print("Changing Scene");
        print("Switching to " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}

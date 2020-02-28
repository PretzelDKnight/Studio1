using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    UnityEngine.SceneManagement.LoadSceneMode mode;
    public void SceneChange()
    {
        SceneManager.LoadScene("NewLevelGreyboix", mode = LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void continueLevel2()
    {
        SceneManager.LoadScene(2);
        print("You Won2");
        print("oldu");
    }
}

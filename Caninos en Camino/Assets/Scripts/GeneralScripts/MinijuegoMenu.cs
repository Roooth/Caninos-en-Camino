using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinijuegoMenu : MonoBehaviour
{
    public void REGRESAR()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1.0f;
    }
}

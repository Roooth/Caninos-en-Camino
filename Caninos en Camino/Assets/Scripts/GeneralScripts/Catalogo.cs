using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Catalogo : MonoBehaviour
{
    public void EMOCIONAL()
    {
        SceneManager.LoadScene(2);
    }

    public void INTERACIONAL()
    {
        SceneManager.LoadScene(3);
    }

    public void FISICA()
    {
        SceneManager.LoadScene(4);
    }

    public void COGNITIVA()
    {
        SceneManager.LoadScene(5);
    }
}

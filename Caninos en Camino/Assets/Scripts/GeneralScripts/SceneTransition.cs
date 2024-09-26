using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fillSprite;
    [SerializeField] private GameObject transition;
    [SerializeField] private string sceneToLoad;
    [Header("Initial transform")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 startScale;
    [Header("Final transform")]
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private Vector3 endScale;


    void Start()
    {
        StartCoroutine(Transition(true, ""));
    }

    public void INTRO()
    {
        StartCoroutine(Transition(false, "Intro"));
    }

    public void MAINMENU()
    {
        StartCoroutine(Transition(false, "MainMenu"));
    }
    public void CATALOGO()
    {
        StartCoroutine(Transition(false, "Catalogo"));

    }
    public void EMOCIONAL()
    {
        StartCoroutine(Transition(false, "Emocional"));
    }

    public void FISICO()
    {
        StartCoroutine(Transition(false, "Fisica"));
    }

    public void CREDITOS()
    {
        StartCoroutine(Transition(false, "Creditos"));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(Transition(false, sceneToLoad));
        }
    }
    #region FUNCIONAMIENTO DE TRASICION
    private IEnumerator Transition(bool started, string sceneString)
    {
        transition.SetActive(true);
        if (started)
        {
            // Undo Fill, then scale out.
            yield return FillFade(1f, 0f, 0.5f);
            yield return Scale(true, 1f);
            transition.SetActive(false);
        }
        else
        {
            // Scale in, then do fill.
            yield return Scale(false, 1f);
            yield return FillFade(0f, 1f, 0.5f);
            // Load the next scene.
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(sceneString);
        }
    }

    private IEnumerator FillFade(float alphaStart, float alphaEnd, float duration)
    {
        Color fillColour = fillSprite.color;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = elapsed / duration;
            // Fade smoothly rather than linearly.
            fillColour.a = Mathf.SmoothStep(alphaStart, alphaEnd, percent);
            // Set the new colour.
            fillSprite.color = fillColour;
            yield return null;
        }
        // Ensure the colour finishes up correctly.
        fillColour.a = alphaEnd;
        fillSprite.color = fillColour;
    }

    private IEnumerator Scale(bool scaleOut, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = elapsed / duration;
            if (scaleOut)
            {
                // Go from start to end.
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, percent);
                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), percent);
                transform.localScale = Vector3.Lerp(startScale, endScale, percent);
            }
            else
            {
                // Go from end back to start.
                transform.localPosition = Vector3.Lerp(endPosition, startPosition, percent);
                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(endRotation), Quaternion.Euler(startRotation), percent);
                transform.localScale = Vector3.Lerp(endScale, startScale, percent);
            }
            yield return null;
        }
        // Ensure placed at the right position at the end.
        if (scaleOut)
        {
            transform.localPosition = endPosition;
            transform.localRotation = Quaternion.Euler(endRotation);
            transform.localScale = endScale;
        }
        else
        {
            transform.localPosition = startPosition;
            transform.localRotation = Quaternion.Euler(startRotation);
            transform.localScale = startScale;
        }
    }
    #endregion
}

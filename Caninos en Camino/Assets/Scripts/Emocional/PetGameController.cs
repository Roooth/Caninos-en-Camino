using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetGameController : MonoBehaviour
{
    public CircleController circleController;
    public GameObject objectToChangeColor;
    public int successClicks = 5;
    private int currentStreak = 0;
    private float successThreshold = 0.9f;
    private SpriteRenderer spriteRenderer;

    // Lista de colores hexadecimales
    private string[] hexColors = { "#FF5733", "#33FF57", "#3357FF", "#F333FF", "#FF33A2" };
    private int currentColorIndex = 0;

    // Lista de sonidos
    public AudioClip[] successSounds;
    public AudioClip failSound;
    private int currentSoundIndex = 0;

    private AudioSource audioSource;

    void Start()
    {
        if (objectToChangeColor != null)
        {
            spriteRenderer = objectToChangeColor.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("El objeto no tiene un SpriteRenderer.");
            }
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un componente AudioSource.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float currentScale = circleController.transform.localScale.x;
            float maxScale = circleController.maxSize;

            if (currentScale >= maxScale * successThreshold)
            {
                currentStreak++;
                ChangeColor();
                PlaySuccessSound();
                if (currentStreak >= successClicks)
                {
                    Debug.Log("You Win!");
                    // Aquí podrías añadir más lógica, como mostrar un mensaje de victoria.
                }
            }
            else
            {
                currentStreak = 0;
                PlayFailSound();
            }

            Debug.Log("Current Streak: " + currentStreak);
        }
    }

    void ChangeColor()
    {
        if (spriteRenderer != null)
        {
            currentColorIndex = (currentColorIndex + 1) % hexColors.Length;
            Color newColor;
            if (ColorUtility.TryParseHtmlString(hexColors[currentColorIndex], out newColor))
            {
                spriteRenderer.color = newColor;
            }
            else
            {
                Debug.LogError("No se pudo convertir el color hex a Color.");
            }
        }
    }

    void PlaySuccessSound()
    {
        if (successSounds.Length > 0 && audioSource != null)
        {
            audioSource.clip = successSounds[currentSoundIndex];
            audioSource.Play();
            currentSoundIndex = (currentSoundIndex + 1) % successSounds.Length;
        }
    }

    void PlayFailSound()
    {
        if (failSound != null && audioSource != null)
        {
            audioSource.clip = failSound;
            audioSource.Play();
            currentSoundIndex = 0; // Reiniciar al primer sonido de la lista de éxitos
        }
    }
}

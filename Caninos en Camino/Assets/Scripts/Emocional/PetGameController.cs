using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class PetGameController : MonoBehaviour
{
    public CircleController circleController;
    public GameObject objectToChangeColor;
    public Slider progressBar; // Barra de progreso
    public TextMeshProUGUI winText; // Texto de victoria usando TextMeshPro
    public TextMeshProUGUI comboText; // Texto de combo usando TextMeshPro
    public GameObject pauseMenu; // Menú de pausa
    public TextMeshProUGUI pauseMenuText; // Texto del menú de pausa
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

        if (progressBar != null)
        {
            progressBar.maxValue = successClicks;
            progressBar.value = 0;
        }

        if (winText != null)
        {
            winText.gameObject.SetActive(false); // Ocultar el texto de victoria al inicio
        }

        if (comboText != null)
        {
            comboText.gameObject.SetActive(false); // Ocultar el texto de combo al inicio
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // Ocultar el menú de pausa al inicio
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pauseMenu.activeSelf)
        {
            float currentScale = circleController.transform.localScale.x;
            float maxScale = circleController.maxSize;

            if (currentScale >= maxScale * successThreshold)
            {
                currentStreak++;
                ChangeColor();
                PlaySuccessSound();
                UpdateProgressBar(true);
                ShowComboText();

                if (currentStreak >= successClicks)
                {
                    ShowWinMenu();
                }
            }
            else
            {
                currentStreak = 0;
                PlayFailSound();
                UpdateProgressBar(false);
                HideComboText();
            }

            Debug.Log("Current Streak: " + currentStreak);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Pausar el juego
        if (pauseMenu != null && pauseMenuText != null)
        {
            pauseMenu.SetActive(true);
            pauseMenuText.text = "Pausa";
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Reanudar el juego
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
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

    void UpdateProgressBar(bool success)
    {
        if (progressBar != null)
        {
            if (success)
            {
                progressBar.value++;
            }
            else
            {
                progressBar.value = Mathf.Max(0, progressBar.value - 1); // Evitar que la barra de progreso sea negativa
            }
        }
    }

    void ShowWinMessage()
    {
        if (winText != null)
        {
            winText.gameObject.SetActive(true);
            winText.text = "¡GANASTE!"; // Cambiar el texto a "GANASTE"
        }
    }

    void ShowComboText()
    {
        if (comboText != null)
        {
            comboText.gameObject.SetActive(true);
            comboText.text = "X " + currentStreak;
        }
    }

    void HideComboText()
    {
        if (comboText != null)
        {
            comboText.gameObject.SetActive(false);
        }
    }

    void ShowWinMenu()
    {
        Time.timeScale = 0; // Pausar el juego
        if (pauseMenu != null && pauseMenuText != null)
        {
            pauseMenu.SetActive(true);
            pauseMenuText.text = "¡Ganaste!";
        }
    }
}


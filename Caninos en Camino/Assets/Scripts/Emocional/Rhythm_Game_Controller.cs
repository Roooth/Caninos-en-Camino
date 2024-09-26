using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rhythm_Game_Controller : MonoBehaviour
{
    public Animator dogAnimator; // Referencia al Animator del perro
    public SpriteRenderer circleSpriteRenderer; // SpriteRenderer del círculo
    public Transform timingIndicator; // Indicador en la barra
    public Slider timingSlider; // Slider para el ritmo
    public TMP_Text feedbackText; // Texto para feedback
    public TMP_Text countdownText; // Texto para cuenta regresiva
    public TMP_Text comboText; // Texto para el combo
    public TMP_Text scoreText; // Texto para mostrar el puntaje
    public AudioSource musicSource; // Fuente de audio para la música
    public AudioClip[] successSounds; // Sonidos de éxito
    public AudioClip failSound; // Sonido de fallo
    public Color[] circleColors = new Color[3]; // Colores para el círculo seleccionables desde el inspector
    public GameObject pauseCanvas; // Canvas para la pausa

    private float bpm = 65f; // BPM de la canción
    private float beatDuration;
    private float elapsedTime;
    private int currentStreak = 0;
    private int comboCount = 0; // Contador de combo
    private int currentSoundIndex = 0;
    private bool isPaused = false;
    private bool isReincorporating = false; // Para la reincorporación tras pausa
    private float failAnimationDuration = .5f; // Duración de la animación de tristeza
    private int score = 0; // Almacena el puntaje total

    // Parámetros para suavizar el movimiento del handle
    public float handleSmoothTime = 0.1f;
    private float handlePosition = 0f;

    void Start()
    {
        beatDuration = 60f / bpm;
        musicSource.Play(); // Iniciar la música
        countdownText.gameObject.SetActive(false); // Ocultar la cuenta regresiva
        comboText.gameObject.SetActive(false); // Ocultar el combo al inicio
        timingSlider.maxValue = 1f;
        circleSpriteRenderer.enabled = true; // Hacer visible el círculo
        dogAnimator.SetBool("isHappy", false);
        dogAnimator.SetBool("isSad", false);
        pauseCanvas.SetActive(false); // El canvas de pausa está desactivado al inicio

        score = 0; // Inicializa el puntaje a 0
        scoreText.text = "Puntaje: " + score.ToString(); // Actualiza el texto del puntaje

        handlePosition = 0f; // Inicializar la posición visual del handle
    }

    void Update()
    {
        if (!isPaused && !isReincorporating)
        {
            elapsedTime += Time.deltaTime;

            // Mueve el indicador y el valor del slider
            float actualProgress = (elapsedTime % beatDuration) / beatDuration;
            timingSlider.value = actualProgress;

            // Suavizar solo el movimiento visual del handle
            handlePosition = Mathf.Lerp(handlePosition, actualProgress, handleSmoothTime);
            MoveHandle(handlePosition);

            // Detecta si el jugador hizo clic
            if (Input.GetMouseButtonDown(0))
            {
                CheckTiming(actualProgress);
            }
        }

        // Pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.P)) // Por ejemplo, la tecla P para pausar
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Mover el handle visualmente de manera suave
    void MoveHandle(float position)
    {
        // Ajustar el anclaje del handle para moverse suavemente según el progreso suavizado
        timingSlider.fillRect.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(position * timingSlider.GetComponent<RectTransform>().rect.width, 0f);
    }

    void CheckTiming(float progress)
    {
        // Evaluar el momento del clic
        if (progress > 0.45f && progress < 0.55f)
        {
            currentStreak++;
            comboCount++;
            ChangeCircleColor();
            PlaySuccessSound();
            DisplayFeedback("Perfecto!");
            UpdateComboText();
            UpdateScore(100); // Suma 100 puntos por "Perfecto"
            TriggerHappyAnimation();
        }
        else if (progress > 0.35f && progress < 0.65f)
        {
            currentStreak++;
            comboCount++;
            ChangeCircleColor();
            PlaySuccessSound();
            DisplayFeedback("Excelente!");
            UpdateComboText();
            UpdateScore(80); // Suma 80 puntos por "Excelente"
            TriggerHappyAnimation();
        }
        else if (progress > 0.25f && progress < 0.75f)
        {
            currentStreak++;
            comboCount++;
            ChangeCircleColor();
            PlaySuccessSound();
            DisplayFeedback("Bien");
            UpdateComboText();
            UpdateScore(50); // Suma 50 puntos por "Bien"
            TriggerHappyAnimation();
        }
        else
        {
            currentStreak = 0;
            comboCount = 0; // Reiniciar el combo en caso de fallo
            PlayFailSound();
            DisplayFeedback(progress < 0.25f ? "Muy pronto" : "Muy tarde");
            TriggerSadAnimation();
            UpdateComboText(); // Actualizar el texto del combo
        }
    }

    void UpdateComboText()
    {
        if (comboCount > 1)
        {
            comboText.gameObject.SetActive(true);
            comboText.text = "X " + comboCount.ToString();
        }
        else
        {
            comboText.gameObject.SetActive(false); // Ocultar el combo cuando no hay suficientes aciertos
        }
    }

    void UpdateScore(int points)
    {
        score += points; // Suma los puntos obtenidos
        scoreText.text = "Puntaje: " + score.ToString(); // Actualiza el texto en pantalla
    }

    void ChangeCircleColor()
    {
        int colorIndex = currentStreak % circleColors.Length;
        circleSpriteRenderer.color = circleColors[colorIndex];
    }

    void PlaySuccessSound()
    {
        if (successSounds.Length > 0)
        {
            musicSource.PlayOneShot(successSounds[currentSoundIndex]);
            currentSoundIndex = (currentSoundIndex + 1) % successSounds.Length;
        }
    }

    void PlayFailSound()
    {
        if (failSound != null)
        {
            musicSource.PlayOneShot(failSound);
            currentSoundIndex = 0; // Reinicia al primer sonido
        }
    }

    void DisplayFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        Invoke("HideFeedback", 1f); // Ocultar después de 1 segundo
    }

    void HideFeedback()
    {
        feedbackText.gameObject.SetActive(false);
    }

    // Animaciones usando booleanos
    void TriggerHappyAnimation()
    {
        dogAnimator.SetBool("isHappy", true);
        dogAnimator.SetBool("isSad", false);
        Invoke("ResetToIdle", 1f); // Volver a Idle después de 1 segundo
    }

    void TriggerSadAnimation()
    {
        dogAnimator.SetBool("isSad", true);
        dogAnimator.SetBool("isHappy", false);
        Invoke("ResetToIdle", failAnimationDuration); // Volver a Idle después de la animación de tristeza
    }

    void ResetToIdle()
    {
        dogAnimator.SetBool("isHappy", false);
        dogAnimator.SetBool("isSad", false);
    }

    // Función para pausar el juego
    public void PauseGame()
    {
        isPaused = true;
        pauseCanvas.SetActive(true); // Mostrar el Canvas de Pausa
    }

    // Función para reanudar con cuenta regresiva
    public void ResumeGame()
    {
        pauseCanvas.SetActive(false); // Ocultar el Canvas de Pausa
        StartCoroutine(ResumeWithCountdown());
    }

    private IEnumerator ResumeWithCountdown()
    {
        isReincorporating = true;
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);
        isReincorporating = false;
        isPaused = false;
    }
}


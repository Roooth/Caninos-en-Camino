using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importa TextMeshPro

public class PopUpController : MonoBehaviour
{
    public GameObject popUpPanel; // Panel del pop-up
    public TextMeshProUGUI popUpText; // Texto del pop-up
    public string instructionText = "¡Acaricia al perrito en el mejor momento!"; // Texto configurable desde Unity

    void Start()
    {
        if (popUpPanel != null && popUpText != null)
        {
            popUpPanel.SetActive(true); // Mostrar el pop-up al inicio
            popUpText.text = instructionText; // Configurar el texto del pop-up desde la variable pública
        }
        else
        {
            Debug.LogError("Pop-up Panel o TextMeshProUGUI no asignados.");
        }
    }

    void Update()
    {
        if (Input.anyKeyDown) // Desaparecer el pop-up cuando el usuario presione cualquier tecla
        {
            if (popUpPanel != null)
            {
                popUpPanel.SetActive(false); // Ocultar el pop-up
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuStart : MonoBehaviour
{
    //2 objetos publicos para los canvas a activar/desactivar
    public GameObject mainmenu;
    public GameObject caeto;

    public void INICIAR()
    {
        //desactivar rl mrnu principal y activar canvas de opciones
        mainmenu.gameObject.SetActive(false);
        caeto.gameObject.SetActive(true);
    }
}
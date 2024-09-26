using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D normalCursor;   // Sprite para estado normal
    public Texture2D clickCursor;    // Sprite para cuando se hace clic

    private Vector2 cursorHotspot = Vector2.zero;
    private static CustomCursor instance;

    // Escala y transparencia del cursor
    public float cursorScale = 2f;  // Escalar a 2x el tamaño original
    public float cursorTransparency = 0.7f;  // Transparencia del cursor (0 = invisible, 1 = opaco)

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Ajustar el tamaño del cursor normal con transparencia
            SetCursor(normalCursor);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetCursor(clickCursor); // Ajustar el cursor al hacer clic
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetCursor(normalCursor); // Volver al cursor normal
        }
    }

    // Función para ajustar el tamaño del cursor y aplicar transparencia
    void SetCursor(Texture2D cursorTexture)
    {
        // Redimensionar el sprite según el valor de cursorScale
        Texture2D scaledCursor = ScaleTexture(cursorTexture, Mathf.RoundToInt(cursorTexture.width * cursorScale), Mathf.RoundToInt(cursorTexture.height * cursorScale));

        // Aplicar transparencia al cursor
        Texture2D transparentCursor = ApplyTransparency(scaledCursor, cursorTransparency);

        // Establecer el cursor con el nuevo tamaño y transparencia
        Cursor.SetCursor(transparentCursor, cursorHotspot, CursorMode.ForceSoftware);
    }

    // Función para escalar la textura del cursor
    Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

        // Escalar la textura pixel por pixel (bilinear interpolation)
        for (int y = 0; y < targetHeight; y++)
        {
            for (int x = 0; x < targetWidth; x++)
            {
                Color newColor = source.GetPixelBilinear((float)x / targetWidth, (float)y / targetHeight);
                result.SetPixel(x, y, newColor);
            }
        }

        result.Apply(); // Aplicar cambios
        return result;
    }

    // Función para aplicar transparencia a la textura
    Texture2D ApplyTransparency(Texture2D source, float alpha)
    {
        Texture2D result = new Texture2D(source.width, source.height, source.format, false);

        // Aplicar la transparencia a cada píxel
        for (int y = 0; y < source.height; y++)
        {
            for (int x = 0; x < source.width; x++)
            {
                Color pixelColor = source.GetPixel(x, y);

                // Ajustar solo si el píxel no es completamente transparente
                if (pixelColor.a > 0)
                {
                    pixelColor.a = alpha;  // Cambiar la transparencia (alfa)
                }

                result.SetPixel(x, y, pixelColor);
            }
        }

        result.Apply(); // Aplicar cambios
        return result;
    }
}


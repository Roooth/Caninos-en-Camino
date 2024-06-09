using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClips (AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawnea en el GameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //Asignar el audioClip
        audioSource.clip = audioClip;

        //Asignar volumen
        audioSource.volume = volume;

        //Play soundClip
        audioSource.Play();

        //Obtener largo del sonido
        float clipLenght = audioSource.clip.length;

        //Destruir el clip cuando acabe de sonar
        Destroy(audioSource.gameObject, clipLenght);

    }
}

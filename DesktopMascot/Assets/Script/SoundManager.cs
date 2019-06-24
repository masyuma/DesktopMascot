using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public AudioSource[] audioSource;

    private static SoundManager thisObj;

    void Start()
    {
        thisObj = this;
    }

    public static void PlaySE(int i)
    {
        if (thisObj != null)
        {
            thisObj.audioSource[i].Play();
        }
    }
}

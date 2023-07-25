using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private void Awake()
    {
        instance = this;
    }
    public AudioSource gemSound, exploreSound, diceSound, roundOverSound;

    public void PlayGemBreak()
    {
        gemSound.Stop();
        gemSound.pitch = Random.Range(.8f, 1.2f);
        gemSound.Play();
    }
    public void PlayExplore()
    {
        exploreSound.Stop();
        exploreSound.pitch = Random.Range(.8f, 1.2f);
        exploreSound.Play();
    }
    public void PlayDiceBreak()
    {
        diceSound.Stop();
        diceSound.pitch = Random.Range(.8f, 1.2f);
        diceSound.Play();
    }
    public void PlayRoundOver()
    {
        roundOverSound.Play();
    }
}

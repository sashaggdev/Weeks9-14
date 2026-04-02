using UnityEngine;

public class KnightSound : MonoBehaviour
{
    public AudioSource FootstepSFX;

    public void Footstep()
    {
        FootstepSFX.Play();
    }
}

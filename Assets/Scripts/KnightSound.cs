using UnityEngine;

public class KnightSound : MonoBehaviour
{

    public AudioSource SFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Footstep()
    {
        //Debug.Log("Footstep");
        SFX.Play();
    }
}

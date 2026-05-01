using UnityEngine;

public class ParticleAnimationHandler : MonoBehaviour
{
    public ParticleSystem myParticles;

    public void FireParticles()
    {
        if (myParticles != null)
        {
            myParticles.Play();
        }
    }
}
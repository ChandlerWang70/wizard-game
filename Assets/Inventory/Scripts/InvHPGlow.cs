using UnityEngine;

public class InvHPGlow : MonoBehaviour
{
    public Renderer rend;
    public float glowIntensity = 0.5f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("_GlowIntensity", glowIntensity);
    }

    public void SetGlowIntensity(float intensity)
    {
        glowIntensity = intensity;
        rend.material.SetFloat("_GlowIntensity", glowIntensity);
    }
}

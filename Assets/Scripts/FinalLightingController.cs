using UnityEngine;
using System.Collections.Generic;

public class FinalLightingController : MonoBehaviour
{
    [Header("Timing Configuration")]
    public float startTime = 15.0f;          // Time in seconds to start the finale
    public float transitionDuration = 3.0f;  // Duration of the lighting transition

    [Header("Lights to Highlight (Reflectors)")]
    public List<Light> highlightLights;      // The two new spotlights
    public float targetIntensity = 5.0f;     // Final intensity for these lights

    [Header("Lights to Dim (Ambient/Others)")]
    public List<Light> dimLights;            // Existing scene lights
    public float dimmedIntensity = 0.0f;     // Final intensity for these lights (usually 0)

    [Header("Animation Control")]
    public List<SoldadoCaminata> soldiers;   // References to the soldier scripts

    private bool finaleTriggered = false;
    private float timer = 0.0f;
    
    // Store initial intensities to lerp from
    private Dictionary<Light, float> initialIntensities = new Dictionary<Light, float>();

    void Start()
    {
        // Store initial intensities
        foreach (Light l in highlightLights)
        {
            if(l != null) initialIntensities[l] = l.intensity;
        }
        foreach (Light l in dimLights)
        {
            if(l != null) initialIntensities[l] = l.intensity;
        }
    }

    void Update()
    {
        // Check if it's time to start the finale
        if (!finaleTriggered && Time.timeSinceLevelLoad >= startTime)
        {
            StartFinale();
        }

        if (finaleTriggered)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / transitionDuration);

            // Lerp Highlight Lights UP
            foreach (Light l in highlightLights)
            {
                if (l != null && initialIntensities.ContainsKey(l))
                {
                    l.intensity = Mathf.Lerp(initialIntensities[l], targetIntensity, progress);
                }
            }

            // Lerp Dim Lights DOWN
            foreach (Light l in dimLights)
            {
                if (l != null && initialIntensities.ContainsKey(l))
                {
                    l.intensity = Mathf.Lerp(initialIntensities[l], dimmedIntensity, progress);
                }
            }
        }
    }

    void StartFinale()
    {
        finaleTriggered = true;
        Debug.Log("Starting Final Sequence: Lighting Accent & Animation Stop");

        // Stop the soldiers
        foreach (var soldier in soldiers)
        {
            if (soldier != null)
            {
                soldier.Detenerse();
            }
        }
    }
}

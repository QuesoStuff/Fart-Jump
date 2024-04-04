using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public static SpecialEffectsManager instance_;

    [SerializeField] private List<ParticleSystem> particleEffects_ = new List<ParticleSystem>();

    private void Awake()
    {
        GENERIC.MakeSingleton(ref instance_, this, this.gameObject, true);
    }

    public ParticleSystem GetParticleEffect(int index)
    {
        if (index >= 0 && index < particleEffects_.Count)
        {
            return particleEffects_[index];
        }
        else
        {
            Debug.LogWarning("Particle effect index out of range.");
            return null;
        }
    }
    // Simplified method for creating a particle effect
    public void CreateSimpleParticleEffect(int index, Vector3 position, Color color = default)
    {
        if (color == default) color = Color.white;
        CreateParticleEffect(index, position, color, true, true);
    }
    public void CreateParticleEffect(int index, Vector3 position, Color startColor = default,
                                     bool shouldRotate = false, bool randomizeProperties = false,
                                     float minSimulationSpeed = 1f, // EXPLOSION_SIM_SPEED_MIN
                                     float maxSimulationSpeed = 5f, // EXPLOSION_SIM_SPEED_MAX
                                     float minStartSize = 0.1f, // EXPLOSION_SIM_START_SIZE_MIN
                                     float maxStartSize = 0.3f, // EXPLOSION_SIM_START_SIZE_MAX
                                     float minLifetime = 0.5f, // EXPLOSION_END_LIFE_MIN
                                     float maxLifetime = 1.5f, // EXPLOSION_END_LIFE_MAX
                                     float minDuration = 0.5f, // EXPLOSION_DURATION_MIN
                                     float maxDuration = 2.7f) //EXPLOSION_DURATION_MAX
    {
        if (index < 0 || index >= particleEffects_.Count) return;
        ParticleSystem prefab = particleEffects_[index];
        if (prefab == null) return;
        ParticleSystem explosionInstance;
        if (shouldRotate)
        {
            explosionInstance = Instantiate(prefab, position, Random.rotation);
        }
        else
        {
            explosionInstance = Instantiate(prefab, position, Quaternion.identity);
        }
        explosionInstance.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var main = explosionInstance.main;
        if (randomizeProperties)
        {
            main.simulationSpeed = Random.Range(minSimulationSpeed, maxSimulationSpeed);
            main.startSize = Random.Range(minStartSize, maxStartSize);
            main.startLifetime = Random.Range(minLifetime, maxLifetime);
            main.duration = Random.Range(minDuration, maxDuration);
        }
        main.startColor = startColor;
        explosionInstance.Play();
        Destroy(explosionInstance.gameObject, main.duration + main.startLifetime.constantMax);
    }


}

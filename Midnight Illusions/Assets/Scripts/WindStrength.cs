using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WindStrength : MonoBehaviour
{
    public ParticleSystem ps;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // particles
        List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();

        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle particle = enterParticles[i];

            Component collider = null;
            float distance = float.MaxValue;

            for (int c = 0; c < ps.trigger.maxColliderCount; c++)
            {
                var partCollider = ps.trigger.GetCollider(c);

                if (partCollider != null)
                {
                    float currentDistance = Vector3.Distance(partCollider.transform.position, particle.position);

                    if (currentDistance < distance)
                    {
                        distance = currentDistance;
                        collider = partCollider;
                    }
                }
                else
                    break;
            }

            if (collider != null)
            {
                var windZoneColor = collider.GetComponent<WindZoneColor>();
                particle.startColor = windZoneColor.color;

                //particle.velocity = new Vector3(windZoneColor.particleVelocity, 0);
            }
            enterParticles[i] = particle;
        }
        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
    }
}

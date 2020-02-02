using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsCollision : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        DamageReceiver dr = other.GetComponent<DamageReceiver>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (dr)
            {
                //Debug.Log(force);
                dr.SetDamage(10);
                AudioManager.instance.PlayAudio(AudioManager.instance.SFXAudio, AudioKey.Crash);
            }
            i++;
        }
    }
}
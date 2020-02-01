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
                
                var pos = collisionEvents[i].intersection;
                var force = collisionEvents[i].velocity.magnitude * 10;
                dr.SetDamage(pos, force);
            }
            i++;
        }
    }
}
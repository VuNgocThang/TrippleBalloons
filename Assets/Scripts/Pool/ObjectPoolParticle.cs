using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolParticle : ObjectPool<ParticleSystem>
{
    public ParticleSystem GetPooledParticleSystem()
    {
        return GetPooledObject();
    }
}

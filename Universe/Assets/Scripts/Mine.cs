using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] int hitPoints = 7;
    [SerializeField] ParticleSystem mineExplosion;
    [SerializeField] ParticleSystem hitSpark;
    [SerializeField] ParticleSystem fuse;


    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitPoints = hitPoints - 1;
            hitSpark.Play();            
            if (hitPoints == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Mine>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<Animator>().enabled = false;
                if (!mineExplosion.isPlaying)
                {
                    mineExplosion.Play();
                }
                if (!fuse.isStopped)
                {
                    fuse.Stop();
                }
                Invoke(nameof(DestroyWhenDestroyed), 2f);                
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!mineExplosion.isPlaying)
            {
                mineExplosion.Play();
            }
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Mine>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Animator>().enabled = false;
            if (!fuse.isStopped)
            {
                fuse.Stop();
            }
            Invoke(nameof(DestroyWhenDestroyed), 2f);
        }
    }
    
    void DestroyWhenDestroyed()
    {
        Destroy(gameObject);
    }
}
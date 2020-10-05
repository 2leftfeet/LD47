using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Vector3 startPosition;

    Wander wander;
    Reload reload;
    Animator animator;
    FieldOfView fieldOfView;

    bool isDead = false;
    bool usesWander;

    [Header("Audio")]
    public List<AudioClip> deathAudio = new List<AudioClip>();
    [Range(0,1)]
    public float deathAudioVolume;

    void Start()
    {
        LoopManager.ResetReplay += ResetEnemy;
        animator = GetComponent<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        usesWander = wander.enabled;
    }

    void OnDestroy()
    {
        LoopManager.ResetReplay -= ResetEnemy;
    }

    void Awake()
    {
        startPosition = transform.position;
        wander = GetComponent<Wander>();
        reload = GetComponent<Reload>();
    }

    public void ResetEnemy()
    {
        fieldOfView.enabled = true;
        reload.enabled = true;
        wander.enabled = usesWander;
        transform.position = startPosition;
        //gameObject.SetActive(true);
        if(wander.enabled)
        {
            wander.Reset();
        }
        reload.Reset();
        if(isDead)
        {
            animator.SetTrigger("Reset");
        }
        isDead = false;
    }

    void KillEnemy()
    {
        AudioManager.PlayClip(deathAudio[UnityEngine.Random.Range(0,deathAudio.Count-1)],deathAudioVolume);
        if(!isDead)
        {
            animator.SetTrigger("Die");
        }
        isDead = true;
        fieldOfView.enabled = false;
        wander.enabled = false;
        reload.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var projectile = other.GetComponent<ProjectileMover>();

        if(other && !isDead)
        {
            Destroy(other.gameObject);
            KillEnemy();
            //gameObject.SetActive(false);
        }
    }
}

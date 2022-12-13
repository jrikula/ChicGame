using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject original;
    public Transform platformPosition;

    Animator anim;
    TouchingDirections touchingDirections;

    [SerializeField]
    private AudioSource spawnSoundEffect;

    [SerializeField]
    private AudioSource crackSoundEffect;

    void Awake()
    {
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !touchingDirections.IsGrounded)
        {
            spawnSoundEffect.Play();
            GameObject clone = (GameObject)Instantiate(original, platformPosition.position, platformPosition.rotation);
            anim.Play("egg_crack", 0, 0.0f);
            if (!touchingDirections.IsGrounded)
            {
                crackSoundEffect.PlayDelayed(3);
                Destroy(clone, 3f);
            }
        }
    }
}
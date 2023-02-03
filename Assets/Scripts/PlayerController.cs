using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BoxCollider playerCollider;
    [SerializeField] BoxCollider playerCubeCollider;
    [SerializeField] Renderer m_Renderer;
    public static PlayerController instance;
    [SerializeField] float xRange = 5.5f;
    [SerializeField] float speed = 12.0f;
    private float horizontalInput;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    [SerializeField] AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boingSound;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    void Update()
    {
        horizontalInput = -Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        if (transform.position.x <= -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }

    public void PlayBoingSound()
    {
        playerAudio.PlayOneShot(boingSound, 1.0f);
    }
    public void PlayOkEffect()
    {
        fireworksParticle.Play();
        playerAudio.PlayOneShot(moneySound, 1.0f);
    }
    public void PlayFailEffect()
    {
        playerCollider.isTrigger = true;
        playerCubeCollider.isTrigger = true;
        m_Renderer.gameObject.SetActive(false);
        StartCoroutine(HideForAmoment());
        explosionParticle.Play();
        playerAudio.PlayOneShot(explodeSound, 1.0f);
    }

    IEnumerator HideForAmoment()
    {
        yield return new WaitForSeconds(6);
        transform.position = new Vector3(0, 0.45f, -4);
        playerCollider.isTrigger = false;
        playerCubeCollider.isTrigger = false;
        m_Renderer.gameObject.SetActive(true);
    }
}

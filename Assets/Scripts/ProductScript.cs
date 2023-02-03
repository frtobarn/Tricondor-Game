using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductScript : MonoBehaviour
{
    public bool isCorrect;
    //[SerializeField] Texture m_ProductTexture;

    [SerializeField] Renderer m_Renderer;
    [SerializeField] Rigidbody productRb;

    void OnEnable()
    {
        // gameObject.transform.rotation = Quaternion.Euler(90, 90, -90);
        //productRb.AddTorque(Vector3.back.normalized * Random.Range(-2.0f, 2.0f), ForceMode.Impulse);
        productRb.drag = Random.Range(4.0f, 8.0f);
    }

    public void setTexture(Texture productTexture)
    {
        m_Renderer.material.SetTexture("_MainTex", productTexture);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isCorrect)
            {
                GameManager.instance.AddScore(1);
                PlayerController.instance.PlayOkEffect();
            }
            else
            {
                GameManager.instance.AddScore(-1);
                PlayerController.instance.PlayFailEffect();
            }
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}

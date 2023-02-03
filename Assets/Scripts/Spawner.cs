using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public Texture[] productsTextures;

    void Start()
    {
        if (instance == null) { instance = this; } else { Destroy(this); }
    }
    public void spawnProduct(int productTextureId, bool isCorrect)
    {
        GameObject productObject = ProductPooling.instance.GetProduct(isCorrect, productsTextures[productTextureId]);
        productObject.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(9.0f, 11.0f), productObject.transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score = 0;
    int totalProducts = 10;
    bool[] evaluatedProducts;
    int stage = 0;

    WaitForSeconds initialWaitTime = new WaitForSeconds(5);


    void Start()
    {
        if (instance == null) { instance = this; } else { Destroy(this); }

        totalProducts = 10;

        //
        evaluatedProducts = new bool[totalProducts];
        for (int i = 0; i < totalProducts; i++)
        {
            evaluatedProducts[i] = false;
        }

        //
        StartCoroutine(StartGame());
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        CanvasController.instance.UpdateScore(score);
    }

    public int GetTotalProducts()
    {
        return totalProducts;
    }

    IEnumerator SpawnTriplette()
    {
        int curretProductIndex = Random.Range(0, totalProducts);
        if (!evaluatedProducts[curretProductIndex] && stage <= totalProducts)
        {

            stage++;
            PlayerController.instance.PlayBoingSound();

            CanvasController.instance.UpdateRemainingProducts();
            CanvasController.instance.UpdateCommingProduct(Spawner.instance.productsTextures[curretProductIndex]);
            //Debug.Log(stage);

            int[] triplette = new int[3];
            int randomPosition = Random.Range(0, 3);
            int randomProduct;

            for (int i = 0; i < 3; i++)
            {
                if (i == randomPosition)
                {
                    triplette[i] = curretProductIndex;
                }
                else
                {
                    do
                    {
                        randomProduct = Random.Range(0, totalProducts);
                        triplette[i] = randomProduct;
                    }
                    while (randomProduct == curretProductIndex);
                }
            }
            yield return new WaitForSeconds(1);
            Spawner.instance.spawnProduct(triplette[0], curretProductIndex == triplette[0] ? true : false);
            yield return new WaitForSeconds(1);
            Spawner.instance.spawnProduct(triplette[1], curretProductIndex == triplette[1] ? true : false);
            yield return new WaitForSeconds(1);
            Spawner.instance.spawnProduct(triplette[2], curretProductIndex == triplette[2] ? true : false);
            evaluatedProducts[curretProductIndex] = true;

            yield return new WaitForSeconds(10);
            StartCoroutine(SpawnTriplette());
        }
        else if (stage <= totalProducts - 1)
        {
            StartCoroutine(SpawnTriplette());
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
    IEnumerator StartGame()
    {
        yield return initialWaitTime;
        StartCoroutine(SpawnTriplette());
    }
}

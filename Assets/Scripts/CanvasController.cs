using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    [SerializeField] Text scoreText;
    int remainingProducts;
    [SerializeField] Text remainingProductsText;
    [SerializeField] RawImage commingProductImage;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }

        remainingProducts = GameManager.instance.GetTotalProducts() + 1;
    }
    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    public void UpdateRemainingProducts()
    {
        remainingProducts--;
        remainingProductsText.text = remainingProducts.ToString();
    }
    public void UpdateCommingProduct(Texture commingProductTexture)
    {
        if (!commingProductImage.gameObject.activeInHierarchy)
        {
            commingProductImage.gameObject.SetActive(true);
            remainingProductsText.gameObject.SetActive(true);
        }
        commingProductImage.texture = commingProductTexture;
    }

}

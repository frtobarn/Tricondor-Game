using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPooling : MonoBehaviour
{
    //Taking the reference to the objectPoolingManager instance.
    public static ProductPooling instance;

    //Sctruct to store product's info.
    struct ProductInfo
    {
        public GameObject productPrefab;
        public ProductScript productScript;
    }


    //Storing the product's prefabs in a list.
    public GameObject productPrefab;
    public int productAmount = 3;
    private List<ProductInfo> products;


    //Awake function
    void Awake()
    {
        //Making this class a singleton.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //Initializing products,s list
        products = new List<ProductInfo>(productAmount);

        //Loading products into the list
        for (int i = 0; i < productAmount; i++)
        {
            ProductInfo pPrefab;
            pPrefab.productPrefab = Instantiate(productPrefab);
            pPrefab.productPrefab.transform.SetParent(transform);
            pPrefab.productPrefab.SetActive(false);
            pPrefab.productScript = pPrefab.productPrefab.GetComponent<ProductScript>();

            //
            products.Add(pPrefab);
        }
    }


    public GameObject GetProduct(bool isCorrect, Texture productTexture)
    {
        //Traverse bullet's array
        int totalProducts = products.Count;

        for (int i = 0; i < totalProducts; i++)
        {
            //If a product prefab is inactive, set active and return it with its isCorrect-flag fixed.
            if (!products[i].productPrefab.activeInHierarchy)
            {
                products[i].productPrefab.SetActive(true);
                products[i].productScript.isCorrect = isCorrect;
                products[i].productScript.setTexture(productTexture);
                return products[i].productPrefab;
            }
        }

        //If any product prefab is inactive, create another one yet active and return it with isPlayer-id fixed.
        ProductInfo pPrefab;
        pPrefab.productPrefab = Instantiate(productPrefab);
        pPrefab.productPrefab.transform.SetParent(transform);
        pPrefab.productPrefab.SetActive(true);
        pPrefab.productScript = pPrefab.productPrefab.GetComponent<ProductScript>();
        pPrefab.productScript.isCorrect = isCorrect;
        pPrefab.productScript.setTexture(productTexture);
        products.Add(pPrefab);
        return pPrefab.productPrefab;
    }
}

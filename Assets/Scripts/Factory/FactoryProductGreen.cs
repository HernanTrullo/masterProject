using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryProductGreen : Factory
{
    [SerializeField] private ProductoGreen productGreenPrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        GameObject instance = Instantiate(productGreenPrefab.gameObject, position, Quaternion.identity);
        ProductoGreen newProduct = instance.GetComponent<ProductoGreen>();
        newProduct.Initialize();
        return newProduct;
    }
}

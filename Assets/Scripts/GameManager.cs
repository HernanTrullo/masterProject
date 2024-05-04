using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [Header("Spawn reference")]
    [SerializeField] GameObject spawnArea;
    Vector3 spawnAreaCenter;
    Vector3 spawnAreaSize;

    [Header("Product references")]
    [SerializeField] public int numberOfItems = 10;

    [Header("Main menu")]
    [SerializeField] public GameObject mainMenuCanvas;
    [SerializeField] public Button startBtn;

    [Header("Gameplay")]
    [SerializeField] public GameObject gameplayCanvas;
    [SerializeField] public TMP_Text timeTxt;
    [SerializeField] public TMP_Text timeTotalTxt;
    [SerializeField] public float _duration = 10.0f;
    [SerializeField] public GameObject blueBox;
    [SerializeField] public GameObject redBox;
    [SerializeField] public GameObject greenBox;

    [Header("Endgame")]
    [SerializeField] public GameObject scoreCanvas;
    [SerializeField] public TMP_Text scoreValue;
    [SerializeField] public Button saveBtn;
    [SerializeField] public Button exitBtn;

    [Header("Endgame Panel Question")]
    [SerializeField] public GameObject exitCanvasQuestion;
    [SerializeField] public Button exitBtnPanelQuestion;
    [SerializeField] public Button cancelBtnPanelQuestion;

    public StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new(this);
        stateMachine.Initialize(stateMachine.mainMenuState);

        // Inicialización de las variables de control
        numberOfItems = Random.Range(5, 9);
        _duration = Random.Range(15, 25);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void GenerateInteractables()
    {
        // spawn area
        spawnAreaCenter = spawnArea.transform.position;
        spawnAreaSize = spawnArea.transform.localScale;

        // blue product factory
        FactoryProductBlue factoryProductBlue = this.GetComponent<FactoryProductBlue>();
        FactoryProductRed factoryProductRed = this.GetComponent<FactoryProductRed>();
        FactoryProductGreen factoryProducGreen = this.GetComponent<FactoryProductGreen>();

        
        // create n products
        for (int i = 0; i < numberOfItems; i++)
        {
            Vector3 randomPos = GetRandomPointInArea();

            IProduct item;

            int randomFactory = Random.Range(1, 4);
            if (randomFactory == 1)
            {
                item = factoryProductBlue.GetProduct(randomPos);
            }
            else
            {
                if (randomFactory==2){
                    item = factoryProducGreen.GetProduct(randomPos);
                }
                else{
                    item = factoryProductRed.GetProduct(randomPos);
                }
                
            }

        }
        
    }

    private Vector3 GetRandomPointInArea()
    {
        float x = UnityEngine.Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float y = UnityEngine.Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);
        float z = UnityEngine.Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2);
        return new Vector3(x, y, z);
    }

    public void ClearIProductsOnScene()
    {
        // Encuentra todos los GameObjects en la escena con el componente "ProductBlue"
        ProductBlue[] productsBlue = FindObjectsOfType<ProductBlue>();

        // Itera sobre cada GameObject encontrado
        foreach (ProductBlue product in productsBlue)
        {
            // Haz algo con cada GameObject encontrado
            Destroy(product.gameObject);
        }

        // Encuentra todos los GameObjects en la escena con el componente "ProductRed"
        ProductRed[] productsRed = FindObjectsOfType<ProductRed>();

        // Itera sobre cada GameObject encontrado
        foreach (ProductRed product in productsRed)
        {
            // Haz algo con cada GameObject encontrado
            Destroy(product.gameObject);
        }

        // Encuentra todos los GameObjects en la escena con el componente "ProductGreen"
        ProductoGreen[] productsGreen = FindObjectsOfType<ProductoGreen>();

        // Itera sobre cada GameObject encontrado
        foreach (ProductoGreen product in productsGreen)
        {
            // Haz algo con cada GameObject encontrado
            Destroy(product.gameObject);
        }

        // Reinicio las variables de cada caja
        blueBox.GetComponentInChildren<BoxCounter>().RestartScore();
        redBox.GetComponentInChildren<BoxCounter>().RestartScore();
        
        // Se reinicia el contador de la cada verde
        greenBox.GetComponentInChildren<BoxCounter>().RestartScore();
    }
}

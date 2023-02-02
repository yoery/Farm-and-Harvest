using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{

    bool isPlanted = false;
    SpriteRenderer plant;
    BoxCollider2D plantCollider;

 
    int plantStage = 0;
    float timer;

    //plot colors
    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;

    SpriteRenderer plot;


    PlantObject selectedPlant;

    //Reference to FarmManager script
    FarmManager fm;

    bool isDry = true;
    public Sprite  drySprite;
    public Sprite normalSprite;
    public Sprite unavailableSprite;

    float speed = 1f;
    public bool isBought=true;

    // Start is called before the first frame update
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = transform.parent.GetComponent<FarmManager>();
        plot = GetComponent<SpriteRenderer>();
        if (isBought)
        {
            plot.sprite = drySprite;
        }
        else
        {
            plot.sprite = unavailableSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted && !isDry)
        {
            timer -= speed*Time.deltaTime;

            if ( timer < 0 && plantStage < selectedPlant.plantStages.Length - 1)
            {
                timer = selectedPlant.timeBtwStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }

    //To tell when you can harvest
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            //harvest
            if(plantStage == selectedPlant.plantStages.Length - 1 && !fm.isPlanting && !fm.isSelecting)
            {
                Harvest();
            }
            
        }
        else if(fm.isPlanting && fm.selectPlant.plant.buyPrice <= fm.money && isBought)
        {
            //plant
            Plant(fm.selectPlant.plant);
        }
        if (fm.isSelecting)
        {
            switch(fm.selectedTool)
            {
                //Water
                case 1:
                    if (fm.money >= 20 && isBought)
                    {
                        fm.Transaction(-20);
                        isDry = false;
                        plot.sprite = normalSprite;
                        if (isPlanted) UpdatePlant();
                    }
                    break;
                //Fertilizer
                case 2:
                    if (fm.money>= 150 && isBought)
                    {
                        fm.Transaction(-150);
                        if (speed < 2) speed += .2f;
                    }
                    break;
                //to buy plot
                case 3:
                    if(fm.money >- 100 && !isBought)
                    {
                        fm.Transaction(-1000);
                        isBought = true;
                        plot.sprite = drySprite;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //available and unavailable plots while placing and hovering over it
    private void OnMouseOver()
    {
        if (fm.isPlanting)
        {
            if (isPlanted || fm.selectPlant.plant.buyPrice > fm.money || !isBought)
            {
                //can't buy
                plot.color = unavailableColor;
            }
            else
            {
                //can buy
                plot.color = availableColor;
            }
        }
        if (fm.isSelecting)
        {
            switch (fm.selectedTool)
            {
                case 1:
                case 2:
                    if (isBought && fm.money>=(fm.selectedTool-1)*100)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                case 3:
                    if (!isBought && fm.money >= 1000)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }
    }

    //when you dont have a plant selected
    private void OnMouseExit()
    {
        plot.color = Color.white;
    }

    //for harvesting plants
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
        plot = GetComponent<SpriteRenderer>();
        isDry = true;
        plot.sprite = drySprite;
        speed = 1f;
    }

    //Buying a plant and losing money
    void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        fm.Transaction(-selectedPlant.buyPrice);

        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    //The plant stages and collider growth
    void UpdatePlant()
    {
        if (isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    //reference to scriptical object
    public PlantObject plant;

    public Text nameTxt;
    public Text priceTxt;
    public Image icon;

    public Image btnImage;
    public Text btnTxt;

    //Reference to FarmManager script
    FarmManager fm;


    // Start is called before the first frame update
    void Start()
    {
        //Find plant in FarmManager for UI
        fm = FindObjectOfType<FarmManager>();
        InitializeUI();
    }

    //Select and buy plant log
    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.plantName);
        fm.SelectPlant(this);
    }

    //Plant name, price, icon in UI
    void InitializeUI()
    {
        nameTxt.text = plant.plantName;
        priceTxt.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : Item
{    
    int hpIncrement;
    int mpIncrement;
        
    public int HpIncrement { get { return hpIncrement; } }
    public int MpIncrement { get { return mpIncrement; } }    

    protected override void SetCategory()
    {
        ItemCatecory = Defines.ItemCategory.Consume;
    }

    public ConsumeItem() { }

    public ConsumeItem(int itemNumber, string itemName, int hpIncrement, int mpIncrement,int price) {
        this.itemNumber = itemNumber;
        this.itemName = itemName;
        this.hpIncrement = hpIncrement;
        this.mpIncrement = mpIncrement;
        this.price = price;
        SetCategory();
    }
}


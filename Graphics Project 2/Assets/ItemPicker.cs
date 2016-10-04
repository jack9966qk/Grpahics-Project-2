using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ItemPicker {

    static List<Item> items;
    static System.Random rnd;

    static ItemPicker() {
        rnd = new System.Random();
        items = new List<Item>();
        items.Add(new HealthRecovery());
        items.Add(new InvincibleBoost());
    }

    public static Item PickOneRandom() {
        int r = rnd.Next(items.Count);
        return items[r];
    }
}

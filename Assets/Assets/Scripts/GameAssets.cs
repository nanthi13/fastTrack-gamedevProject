using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Sprite TravelerSprite1;
    public Sprite TravelerSprite2;
    
    public Sprite LuggageSprite1;
    public Sprite LuggageInside1;

    public Sprite HazardSprite1;
    public Sprite HazardSprite2;
    public Sprite HazardSprite3;
    public Sprite NonHazardSprite1;
    public Sprite NonHazardSprite2;
    public Sprite NonHazardSprite3;
}

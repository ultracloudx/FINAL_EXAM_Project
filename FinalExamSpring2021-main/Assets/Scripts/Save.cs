using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{

    public int score;
    public string name;
    public int lives;
    public float time;
    //public float fallSpeed;

    /*public Save(Save save)
    {
        score = save.score;
    }*/

    public string GetName()
    {
        return this.name;
    }

}
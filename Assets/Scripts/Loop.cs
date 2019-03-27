using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop
{
    private int initialIndex;
    private int finalIndex;
    private int numberIterations;
    private dynamic iterationVariable;
    
    public Loop(int initialIndex, int finalIndex,int numberIterations)
    {
        this.initialIndex = initialIndex;
        this.finalIndex = finalIndex;
        this.NumberIterations = numberIterations;
    }
    
    public Loop(int initialIndex, int finalIndex,CEnemy enemy)
    {
        this.initialIndex = initialIndex;
        this.finalIndex = finalIndex;
        this.iterationVariable = enemy.MaxHealth;
    }

    public Loop(int initialIndex,int numberIterations)
    {
        this.initialIndex = initialIndex;
        this.finalIndex = initialIndex;
        this.numberIterations = numberIterations;
    }

    public int InitialIndex
    {
        get
        {
            return initialIndex;
        }

        set
        {
            initialIndex = value;
        }
    }

    public int FinalIndex
    {
        get
        {
            return finalIndex;
        }

        set
        {
            finalIndex = value;
        }
    }

    public int NumberIterations
    {
        get
        {
            return numberIterations;
        }

        set
        {
            numberIterations = value;
        }
    }
}

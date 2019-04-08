using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Loop
{
    private int initialIndex;
    private int? finalIndex = null ;
    private int numberIterations;
    private Variable iterationVariable;
    
    public Loop(int initialIndex, int finalIndex,int numberIterations)
    {
        this.initialIndex = initialIndex;
        this.finalIndex = finalIndex;
        this.NumberIterations = numberIterations;
    }
    

    public Loop(int initialIndex,int numberIterations)
    {
        this.initialIndex = initialIndex;
        this.finalIndex = initialIndex;
        this.numberIterations = numberIterations;
    }
    
    public Loop(int initialIndex,Variable iterationVariable)
    {
        this.initialIndex = initialIndex;
        this.finalIndex = initialIndex;
        this.iterationVariable = iterationVariable;
        this.numberIterations = iterationVariable.GetValue();
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

    public int? FinalIndex
    {
        get => finalIndex;
        set => finalIndex = value;
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CTowerJorble : System.Object
{
    private string jorbleColor;
    public string JorbleColor
    {
        get
        {
            return jorbleColor;
        }

        set
        {
            jorbleColor = value;
        }
    }

    public CTowerJorble(string jColor)
    {
        JorbleColor = jColor;
    }

    public CTowerJorble(CTowerJorble other)
    {
        JorbleColor = other.JorbleColor;
    }
}

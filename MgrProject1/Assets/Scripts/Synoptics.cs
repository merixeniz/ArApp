using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synoptics 
{
    
    public uint Run { get; set; }
    public uint Stop { get; set; }
    public uint Error { get; set; }
    public uint TP { get; set; }
    public uint PS { get; set; }
    public uint PL { get; set; }
    public uint SF { get; set; }
    public uint M { get; set; }

    public bool TappedRun { get; set; }
    public bool TappedStop { get; set; }
    public bool TappedError { get; set; }
    public bool TappedTP { get; set; }
    public bool TappedPS { get; set; }
    public bool TappedPL { get; set; }
    public bool TappedSF { get; set; }
    public bool TappedM { get; set; }

    public bool IgnoreUI { get; set; } // uzywane do blokowania main panelu 
    public bool LoadService = false; // uzywane do wczytania sceny z service panel on

    private static Synoptics diodes;

    private Synoptics()
    {

    }

    public static Synoptics CreateSynoptics()
    {
        if (diodes == null)
        {
            diodes = new Synoptics();

            diodes.Run = 0;
            diodes.Stop = 0;
            diodes.Error = 0;
            diodes.TP = 0;
            diodes.PS = 0;
            diodes.PL = 0;
            diodes.SF = 0;
            diodes.M = 0;

            diodes.IgnoreUI = false;

            diodes.TappedRun = false;
            diodes.TappedStop = false;
            diodes.TappedError = false;
            diodes.TappedTP = false;
            diodes.TappedPS = false;
            diodes.TappedPL = false;
            diodes.TappedSF = false;
            diodes.TappedM = false;
        }            

        return diodes;
    }

   
}

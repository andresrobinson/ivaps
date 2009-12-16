using System;
using System.Collections.Generic;
using System.Text;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Questa enum elenca gli stati possibili di un volo
    /// </summary>
    public enum FlightStates : byte
    {
        Before_Departed = 0,
        Engine_Started = 1,
        TakeOffTaxi = 2,
        Airborne = 3,
        Landed = 4,
        OnBlocks = 5,
        EngineOff = 6
    }
}

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
        Before_Departed,
        Engine_Started,
        TakeOffTaxi,
        Airborne,
        Landed,
        OnBlocks,
        EngineOff
    }
}

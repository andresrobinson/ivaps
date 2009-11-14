//=========================================================
// This software is distributed under GPL v2 Licence
//
// Developed by Federico Castellari (fede.caste@gmail.com)
// November 2009
//
// Developed using Microsoft Visual C# 2008 Express Edition
//=========================================================

using System;
using System.Collections.Generic;
using System.Text;
using Castellari.IVaPS.Model;
using System.Net;
using System.IO;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Raccolta di metodi di utility di business logic
    /// </summary>
    public class IPSUtils
    {
        private const double R = 3440;//miglia nautiche
        private const string IVAO_FLIGHTPLANS_URL = "http://de.www.ivao.aero/whazzup.txt";

        /// <summary>
        /// Calcola la distanza in MIGLIA NAUTICHE (nm) tra due punti geografici. Il calcolo è svolto con la
        /// formula di Aversine
        /// </summary>
        /// <param name="pos1">punto uno</param>
        /// <param name="pos2">punto 2</param>
        /// <returns>la distanza in miglia nautiche</returns>
        public static double CalulateDistance(GeoPosition pos1, GeoPosition pos2)
        { 
            double dLat = toRadian(pos2.Latitude - pos1.Latitude);
            double dLon = toRadian(pos2.Longitude - pos1.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(toRadian(pos1.Latitude)) * Math.Cos(toRadian(pos2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;
            return d;
        }

        /// <summary>
        /// Reperisce il piano di volo dai server di IVAO
        /// </summary>
        /// <param name="ivaoCallsign">il callsign del pilota di cui scaricare il piano di volo</param>
        /// <returns>il piano di volo richiesto se presente, null altrimenti</returns>
        public static IvaoFlightPlan RetrivePlan(String ivaoCallsign)
        {
            //Istanzio le variabili che servono per la chiamata http
            WebClient client = new WebClient();
            Stream data = client.OpenRead(IVAO_FLIGHTPLANS_URL);
            StreamReader reader = new StreamReader(data);
            string str = "";
            string rightLine = null;

            //sequenza di lettura: riga per riga si va alla ricerca di quella che inizia col callsign desiderato
            str = reader.ReadLine();
            while (str != null)
            {
                string[] tmp = str.Split(':');
                if (tmp[0].Equals(ivaoCallsign))
                    rightLine = str;
                str = reader.ReadLine();
            }

            if (rightLine != null)
            {
                //trovata la linea vado a cercare le colonne che mi interessano
                string[] tmp = rightLine.Split(':');
                IvaoFlightPlan toBeRet = new IvaoFlightPlan();
                toBeRet.Route = tmp[30];
                toBeRet.Departure = new Airport();
                toBeRet.Departure.ICAOCode = tmp[11];
                toBeRet.Arrival = new Airport();
                toBeRet.Arrival.ICAOCode = tmp[13];
                toBeRet.Alternate = new Airport();
                toBeRet.Alternate.ICAOCode = tmp[28];
                toBeRet.FlightType = tmp[21];
                toBeRet.Aircraft = tmp[9].Split('/')[1];
                return toBeRet;
            }
            else
                return null;

        }

        private static double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}

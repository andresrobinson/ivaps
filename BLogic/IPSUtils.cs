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

        public static IvaoFlightPlan RetrivePlan(String ivaoCallsign)
        {
            WebClient client = new WebClient();
            Stream data = client.OpenRead("http://de.www.ivao.aero/whazzup.txt");
            StreamReader reader = new StreamReader(data);
            string str = "";
            string rightLine = null;
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

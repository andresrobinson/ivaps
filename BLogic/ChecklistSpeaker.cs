using System;
using System.Collections.Generic;
using System.Text;
using System.Speech.Synthesis;
using Castellari.IVaPS.Model;
using System.Threading;


namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Classe di utility per la lettura delle checklist
    /// </summary>
    public class ChecklistSpeaker
    {
        private static int VOLUME = 100;
        private static string SSML_HEADER = "<?xml version=\"1.0\"?><speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.w3.org/2001/10/synthesis http://www.w3.org/TR/speech-synthesis/synthesis.xsd\" xml:lang=\"en-US\">";
        private static string SSML_FOOTER = "</speak>";
        private static string SSML_PAUSE_SHORT = "<break time=\"100ms\"/>";
        private static string SSML_PAUSE_MEDIUM = "<break time=\"1s\"/>";
        private static string SSML_PAUSE_LONG = "<break time=\"2s\"/>";
        private static string TEMPLATE_SSML_PAUSE_IN_SECONDS = "<break time=\"{0}s\"/>";
        private static string TEMPLATE_READ_COUPLE = "<p><prosody rate=\"-20%\">{0}: " + SSML_PAUSE_SHORT + "{1}.</prosody></p>";
        private static string TEMPLATE_READ_CHECKLISTPHASE = "<p>{0} checklist</p>";
        private static string TEMPLATE_READ_CHECKLISTPHASECOMPLETED = "<p>{0} checklist completed.</p><break time=\"1s\"/>";
        private static string CARACTERISTIC_SPEEDS = "Caracteristic speeds:";
        private static string TEMPLATE_HEADING = "Heading: <prosody rate=\"-20%\">{0}</prosody> degreeds.";
        private static string TEMPLATE_ALTITUDE = "Altitude: {0} feets.";
        private static string TEMPLATE_SPEED = "True airspeed: {0} knots.";

        private static SpeechSynthesizer voice = new SpeechSynthesizer();

        /// <summary>
        /// Legge la fase di checklist richiesta
        /// </summary>
        /// <param name="phase"></param>
        public static void ReadPhase(ChecklistPhase phase)
        {
            if (phase == null)
            {
                RealRead(new StringBuilder("Checklist phase unavailable, select it on config section"), VOLUME);
                return;
            }

            StringBuilder toBeSpeeked = new StringBuilder();
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_CHECKLISTPHASE, phase.PhaseName));
            toBeSpeeked.Append(SSML_PAUSE_MEDIUM);

            foreach (ChecklistItem item in phase.Items)
            {
                toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, item.Description, item.Value));
                toBeSpeeked.Append(string.Format(TEMPLATE_SSML_PAUSE_IN_SECONDS,item.Delay));
            }
            
            toBeSpeeked.Append(SSML_PAUSE_MEDIUM);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_CHECKLISTPHASECOMPLETED, phase.PhaseName));

            RealRead(toBeSpeeked, VOLUME);
        }

        /// <summary>
        /// Legge le velocità caratteristiche dell'aereomobile a cui appartiene la checklist
        /// </summary>
        /// <param name="cklst"></param>
        public static void ReadAllSpeeds(Checklist cklst)
        {
            if (cklst == null)
            {
                RealRead(new StringBuilder("Checklist unavailable, select it on config section"), VOLUME);
                return;
            }


            StringBuilder toBeSpeeked = new StringBuilder();
            toBeSpeeked.Append(CARACTERISTIC_SPEEDS);
            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "V rotate" , cklst.Vr));
            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "approach speed", cklst.Vapp));
            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "flaps extension speed", cklst.Vf0));
            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "touchdown speed", cklst.Vldg));
            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "never exceed speed", cklst.Vne));
            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "stall speed", cklst.Vs));
            toBeSpeeked.Append(SSML_PAUSE_MEDIUM);
            RealRead(toBeSpeeked, VOLUME);
        }

        public static void ReadPosition(AircraftPosition pos)
        {
            if (pos != null)
            {
                StringBuilder toBeSpeeked = new StringBuilder();
                string tmp = Convert.ToInt32(pos.Heading).ToString("000");
                string tmp2 = tmp[0] + ", " + tmp[1] + ", " + tmp[2] + ", ";
                toBeSpeeked.Append(string.Format(TEMPLATE_HEADING,tmp2));
                toBeSpeeked.Append(SSML_PAUSE_SHORT);
                toBeSpeeked.Append(string.Format(TEMPLATE_ALTITUDE, Convert.ToInt32(pos.Altitude)/100*100));
                toBeSpeeked.Append(SSML_PAUSE_SHORT);
                toBeSpeeked.Append(string.Format(TEMPLATE_SPEED, Convert.ToInt32(pos.TrueAirspeedSpeed)));
                toBeSpeeked.Append(SSML_PAUSE_SHORT);
                RealRead(toBeSpeeked, VOLUME);
            }
            else
            {
                RealRead(new StringBuilder("Position unknown"), VOLUME);
            }
        }

        /// <summary>
        /// Annncia la frase
        /// </summary>
        /// <param name="message">la frase da annunciare (non SSML)</param>
        public static void Speak(string message)
        {
            if (voice.State == SynthesizerState.Speaking) return;
            voice.SpeakAsync(message);
        }

        /// <summary>
        /// Se è in corso uno speak lo interrompe. Altrimenti noop
        /// </summary>
        public static void StopSpeaking()
        {
            //metodo aggiunto per issue 66
            if (voice.State != SynthesizerState.Speaking) return;
            voice.SpeakAsyncCancelAll();
        }

        /// <summary>
        /// Restituisce true se al momento della richiesta vi è un messaggio in erogazione.
        /// </summary>
        /// <returns></returns>
        public static bool IsCurrentlySpeaking()
        {
            return (voice.State == SynthesizerState.Speaking);
        }

        private static void RealRead(StringBuilder content, int volume)
        {
            if (voice.State == SynthesizerState.Speaking) return;

            voice.Volume = volume;
            StringBuilder toBeSpeeked = new StringBuilder(SSML_HEADER);
            toBeSpeeked.Append(content);
            toBeSpeeked.Append(SSML_FOOTER);

            voice.SpeakSsmlAsync(toBeSpeeked.ToString());//issue 66
        }

    }
}

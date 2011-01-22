using System;
using System.Collections.Generic;
using System.Text;
using System.Speech.Synthesis;
using Castellari.IVaPS.Model;
using System.Threading;
using Castellari.IVaPS.Control;


namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Classe di utility per la lettura delle checklist
    /// </summary>
    public class ChecklistSpeaker
    {
        private static ChecklistSpeaker singleton = new ChecklistSpeaker();

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

        private Thread backgroundThread = null;
        private SpeakingThread backgroundSpeaker = null;
        
        public IPSController Controller { get; set; }

        public static ChecklistSpeaker Instance
        {
            get
            {
                return singleton;
            }
        }

        /// <summary>
        /// Legge la fase di checklist richiesta
        /// </summary>
        /// <param name="phase"></param>
        public void ReadPhase(ChecklistPhase phase)
        {

            backgroundSpeaker = new SpeakingThread(Controller);
 
            if (phase == null)
            {
                string s = "Checklist phase unavailable, select it on config section";
                backgroundSpeaker.AddInSpeakingBuffer(s,s);
                backgroundThread = new Thread(new ThreadStart(backgroundSpeaker.Run));
                backgroundThread.Start();
                return;
            }

            StringBuilder toBeSpeeked = new StringBuilder();
            string msg = string.Empty;

            #region lettura dell'header di checklist
            msg = string.Format(TEMPLATE_READ_CHECKLISTPHASE, phase.PhaseName);
            toBeSpeeked.Append(msg);
            toBeSpeeked.Append(SSML_PAUSE_MEDIUM);
            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), phase.PhaseName + " checklist:");
            #endregion


            foreach (ChecklistItem item in phase.Items)
            {
                toBeSpeeked = new StringBuilder();
                msg = string.Format(TEMPLATE_READ_COUPLE, item.Description, item.Value);
                toBeSpeeked.Append(msg);
                toBeSpeeked.Append(string.Format(TEMPLATE_SSML_PAUSE_IN_SECONDS, item.Delay));

                backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), item.Description + " : " + item.Value);
            }

            #region lettura del footer
            toBeSpeeked = new StringBuilder();
            msg = string.Empty;

            toBeSpeeked.Append(SSML_PAUSE_MEDIUM);
            msg = string.Format(TEMPLATE_READ_CHECKLISTPHASECOMPLETED, phase.PhaseName);
            toBeSpeeked.Append(msg);

            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), phase.PhaseName + " checklist completed");
            #endregion


            backgroundThread = new Thread(new ThreadStart(backgroundSpeaker.Run));
            backgroundThread.Start();
        }

        /// <summary>
        /// Legge le velocità caratteristiche dell'aereomobile a cui appartiene la checklist
        /// </summary>
        /// <param name="cklst"></param>
        public void ReadAllSpeeds(Checklist cklst)
        {
            backgroundSpeaker = new SpeakingThread(Controller);

            if (cklst == null)
            {
                string s = "Checklist unavailable, select it on config section";
                backgroundSpeaker.AddInSpeakingBuffer(s, s);
                backgroundThread = new Thread(new ThreadStart(backgroundSpeaker.Run));
                backgroundThread.Start();
                return;
            }


            StringBuilder toBeSpeeked = new StringBuilder();
            toBeSpeeked.Append(CARACTERISTIC_SPEEDS);
            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "V rotate" , cklst.Vr));

            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), "V rotate: " + cklst.Vr);
            toBeSpeeked = new StringBuilder();

            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "approach speed", cklst.Vapp));

            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), "approach speed: " + cklst.Vapp);
            toBeSpeeked = new StringBuilder();

            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "flaps extension speed", cklst.Vf0));

            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), "flaps extension speed: " + cklst.Vf0);
            toBeSpeeked = new StringBuilder();

            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "touchdown speed", cklst.Vldg));

            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), "touchdown speed: " + cklst.Vldg);
            toBeSpeeked = new StringBuilder();

            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "never exceed speed", cklst.Vne));

            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), "never exceed speed: " + cklst.Vne);
            toBeSpeeked = new StringBuilder();

            toBeSpeeked.Append(SSML_PAUSE_SHORT);
            toBeSpeeked.Append(string.Format(TEMPLATE_READ_COUPLE, "stall speed", cklst.Vs));

            backgroundSpeaker.AddInSpeakingBuffer(toBeSpeeked.ToString(), "stall speed: " + cklst.Vs);
            toBeSpeeked = new StringBuilder();

            backgroundThread = new Thread(new ThreadStart(backgroundSpeaker.Run));
            backgroundThread.Start();
        }

        public void ReadPosition(AircraftPosition pos)
        {
            backgroundSpeaker = new SpeakingThread(Controller);

            if (pos != null)
            {
                StringBuilder toBeSpeeked = new StringBuilder();
                string tmp = Convert.ToInt32(pos.Heading).ToString("000");
                string tmp2 = tmp[0] + ", " + tmp[1] + ", " + tmp[2] + ", ";
                toBeSpeeked.Append(string.Format(TEMPLATE_HEADING,tmp2));
                toBeSpeeked.Append(SSML_PAUSE_SHORT);
                int RoundedAltitude = Convert.ToInt32(pos.Altitude)/100*100;// /100*100 inserito per arrotondare ai 100 piedi;
                toBeSpeeked.Append(string.Format(TEMPLATE_ALTITUDE, RoundedAltitude.ToString("0,000").Replace('.', ','))); //RoundedAltitude.ToString("0,000").Replace('.',',') per issue 75
                toBeSpeeked.Append(SSML_PAUSE_SHORT);
                toBeSpeeked.Append(string.Format(TEMPLATE_SPEED, Convert.ToInt32(pos.TrueAirspeedSpeed)));
                toBeSpeeked.Append(SSML_PAUSE_SHORT);
                
                backgroundThread = new Thread(new ThreadStart(backgroundSpeaker.Run));
            }
            else
            {
                backgroundSpeaker.AddInSpeakingBuffer("Position unknown", "Position unknown");
                backgroundThread = new Thread(new ThreadStart(backgroundSpeaker.Run));
            }
            
            backgroundThread.Start();
        }

        /// <summary>
        /// Annncia la frase
        /// </summary>
        /// <param name="message">la frase da annunciare (non SSML)</param>
        public void Speak(string message)
        {
            if (IsCurrentlySpeaking()) return;
            backgroundSpeaker = new SpeakingThread(Controller);
            backgroundSpeaker.AddInSpeakingBuffer(message, message);
            
            backgroundThread = new Thread(new ThreadStart(backgroundSpeaker.Run));
            backgroundThread.Start();
        }

        /// <summary>
        /// Se è in corso (o in pausa) uno speak lo interrompe. Altrimenti noop
        /// </summary>
        public void StopSpeaking()
        {
            if (backgroundSpeaker != null)
                backgroundSpeaker.Kill();
        }

        /// <summary>
        /// Se è in corso uno speak lo mette in pausa. Altrimenti noop
        /// </summary>
        public void PauseSpeaking()
        {
            if (backgroundSpeaker != null)
                backgroundSpeaker.IsPaused = true;
        }

        /// <summary>
        /// Se è in corso uno speak in pausa lo rimette in esecuzione. Altrimenti noop
        /// </summary>
        public void ResumeSpeaking()
        {
            if (backgroundSpeaker != null)
                backgroundSpeaker.IsPaused = false;
        }

        /// <summary>
        /// Restituisce true se al momento della richiesta vi è un messaggio in erogazione.
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentlySpeaking()
        {
            if (backgroundSpeaker != null)
                return backgroundSpeaker.IsSpeaking;
            else
                return false;
        }

        public bool IsCurrentlyPaused()
        {
            if (backgroundSpeaker != null)
                return backgroundSpeaker.IsPaused;
            else
                return false;
        }

        private class SpeakingThread
        {
            private SpeechSynthesizer voice = new SpeechSynthesizer();

            private List<string> toBeSpeakeds = new List<string>();
            private List<string> toBeShoweds = new List<string>();
            private int currentIndex = 0;
            private IPSController ctrl = null;
            private bool isAborted = false;
            private bool isStarted = false;
            
            public bool IsPaused { get; set; }

            public bool IsSpeaking
            {
                get 
                {
                    return isStarted && (currentIndex < toBeShoweds.Count) && !isAborted && !IsPaused;
                }
            }

            public SpeakingThread(IPSController ctrl)
            {
                this.ctrl = ctrl;
            }

            public void Kill()
            {
                isAborted = true;
            }

            public void AddInSpeakingBuffer(string toBeSpeaked, string toBeShowed)
            {
                if (currentIndex != 0) throw new InvalidOperationException("sto già parlando!!");
                toBeSpeakeds.Add(toBeSpeaked);
                toBeShoweds.Add(toBeShowed);
            }

            public void Run()
            {
                isStarted = true;
                for (currentIndex = 0; currentIndex < toBeShoweds.Count && !isAborted; currentIndex++)
                {
                    while (IsPaused) Thread.Sleep(100);

                    ctrl.ShowMessage(toBeShoweds[currentIndex]);
                    RealRead(new StringBuilder(toBeSpeakeds[currentIndex]), IPSConfiguration.TTS_VOLUME);
                }
            }

            private void RealRead(StringBuilder content, int volume)
            {
                if (voice.State == SynthesizerState.Speaking) return;

                voice.Volume = volume;
                StringBuilder toBeSpeeked = new StringBuilder(SSML_HEADER);
                toBeSpeeked.Append(content);
                toBeSpeeked.Append(SSML_FOOTER);

                try
                {
                    voice.SpeakSsml(toBeSpeeked.ToString());
                }
                catch (Exception ex)
                {
                    //noop
                }
            }
        }
    }
}

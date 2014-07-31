using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castellari.IVaPS.Control;
using System.Speech.Recognition;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Classe deputata all'intepretazione del parlato per poter dare comandi per esempio di lettura checklist
    /// </summary>
    public class VoiceCommandRecognizer
    {
        private SpeechRecognizer recognizer;
        private EventHandler<SpeechRecognizedEventArgs> handler;
        private bool recogStarted = false;

        public VoiceCommandRecognizer()
        {
            recognizer = new SpeechRecognizer();
            Choices commands = new Choices();
            commands.Add(new string[] { "test", "example" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(commands);
            Grammar g = new Grammar(gb);
            recognizer.LoadGrammar(g);
            handler = new EventHandler<SpeechRecognizedEventArgs>(recognitionHandler);

            //per ora lo faccio così, ma è assolutamente da CAMBIARE
            StartRecognition();
        }

        public IPSController Controller { get; set; }

        public void StartRecognition()
        {
            recognizer.SpeechRecognized += handler;
            recogStarted = true;
        }

        public void StopRecognition()
        {
            recognizer.SpeechRecognized -= handler;
            recogStarted = false;
        }
        
        public bool isRecognitionStarded()
        {
            return recogStarted;
        }

        private void recognitionHandler(object sender, SpeechRecognizedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

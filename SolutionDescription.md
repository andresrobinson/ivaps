# Descrizione della solution e architettura di massima #

## Introduzione ##

L'applicazione è sviluppata in C#, compilata per .NET Framework 2.0. Il progetto è una solution per Visual C# 2008 Express Edition.
A livello architetturale si tratta di una classica applicazione basata su MVC.


## Struttura dei nemespace (e delle folder) ##

**/ la root contiene unicamente la classe col main applicativo** /Model contiene le classi di DataObject che rappresentano le entità del modello dati applicativo
**/View contiene i componenti grafici che rappresentano la vista** /Control contiene il controller applicativo (statefull)
**/Blogic contiene la business logic statless dell'applicazione**

L'unica libreria esterna utilizzata è la FSUIPC.dll liberamente scaricabile qui:
http://www.schiratti.com/dowson.html

## Logica generale dell'applicazione ##

Sebbene si rimandi alla documentazione in-line al codice per i dattagli implementativi il "giro" primario è il seguente.
Il controller attiva la blogic (FSWrapper) in modo da leggere i dati dalle FSUIPC. Il FSWrapper genera degli eventi applicativi (FSEvent) sulla base dei dati letti in polling da FS via FSUIPC. DI questi eventi è listener il controller, che quindi aggiorna il modello (FlightStatus) e notifica gli update sulle viste
using System.Timers;

Console.WriteLine("Hello, World!");

int keuze = 0;
System.Timers.Timer timer = null;
TimeSpan sluimerTijd = TimeSpan.FromMinutes(5);
Wekmethode wekMethode = delegate { };
AlarmActie alarmActie = delegate { };

do
{
    Console.WriteLine("1. tijd kan instellen waarop je wekker \"afloopt\"");
    Console.WriteLine("2. sluimertijd kan instellen nadat je wekker is afgelopen!");
    Console.WriteLine("3. kies manier van wekken");
    Console.WriteLine("0. Sluit Af");

    keuze = Int32.Parse(Console.ReadLine());

    switch (keuze)
    {
        case 1:
            Console.WriteLine("stel de alarmtijd in: ");
            DateTime tijd = DateTime.Parse(Console.ReadLine());
            stelTijdIn(tijd);
            break;
        case 2:
            Console.WriteLine("stel de sluimertijd in: ");
            sluimerTijd = TimeSpan.Parse(Console.ReadLine());
            break;
        case 3:
            Console.WriteLine("A. geluid");
            Console.WriteLine("B. boodschap");
            Console.WriteLine("C. knipperlicht");
            String choice = Console.ReadLine();
            switch (choice)
            {
                case "A":
                    wekMethode += (maakGeluid);
                    Console.WriteLine("geluid gekozen");
                    break;
                case "B":
                    wekMethode += (toonBoodschap);
                    Console.WriteLine("boodschap gekozen");
                    break;
                case "C":
                    wekMethode += (knipperLicht);
                    Console.WriteLine("knipperlicht gekozen");
                    break;
                default:
                    Console.WriteLine("Onjuiste keuze");
                    break;
            }
            break;
        case 0:
            stopAlarm();
            break;
        default:
            Console.WriteLine("geen correcte invoer!");
            break;
    }
}
while (keuze != 0);

void stelTijdIn(DateTime alarmTijd) //timer actieveren van: https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-9.0
{
    stopAlarm();
    double ms = (alarmTijd - DateTime.Now).TotalMilliseconds;
    timer = new System.Timers.Timer(ms);
    timer.Elapsed += (sender, e) => wekMethode();
    timer.AutoReset = true;
    timer.Start();
    Console.WriteLine("Alarm gezet op: " + alarmTijd + ". Dat is over " + (alarmTijd - DateTime.Now));
}
void stopAlarm()
{
    timer?.Stop();
    timer?.Dispose();
}
void sluimer()
{
    stelTijdIn(DateTime.Now.Add(sluimerTijd));
    Console.WriteLine("Sluimeren voor: " + (sluimerTijd));
}
void maakGeluid()
{
    trigger("BEEP BEEP BEEP BEEP");
}
void toonBoodschap()
{
    trigger("Tijd om op te staan!");
}
void knipperLicht()
{
    trigger("KNIPPER KNIPPER KNIPPER");
}

void trigger(String methode)
{
    Console.WriteLine(methode);
    Console.WriteLine("1. stop alarm");
    Console.WriteLine("2. sluimer");
    int keuze = Int32.Parse(Console.ReadLine());
    switch (keuze)
    {
        case 1:
            stopAlarm();
            break;
        case 2:
            sluimer();
            break;
        default:
            Console.WriteLine("Onjuiste keuze");
            break;
    }

}

delegate void Wekmethode();
delegate void AlarmActie();
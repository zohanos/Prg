using System;
using System.Reflection.Metadata;
using System.Linq;

namespace MyApp
{
    public class Film
    {
        public string Nazev { get; }
        public string JmenoRezisera { get; }
        public string PrijmeniRezisera { get; }
        public int RokVzniku { get; }

        // Veřejná vlastnost s privátním setrem
        public double Hodnoceni { get; private set; }


        private List<double> hodnoceniList = new List<double>();

        public Film(string nazev, string jmenoRezisera, string prijmeniRezisera, int rokVzniku)
        {
            Nazev = nazev;
            JmenoRezisera = jmenoRezisera;
            PrijmeniRezisera = prijmeniRezisera;
            RokVzniku = rokVzniku;
        }

        public void pridejhodnoceni(int hodnoceni)
        {
            if (hodnoceni < 0 || hodnoceni > 5)
            {
                Console.WriteLine("Hodnocení musí být mezi 0 a 5");
            }
            else
            {
                hodnoceniList.Add(hodnoceni);
            }
        }

        public double avgHodnoceni()
        {
            double avgh = hodnoceniList.Sum()/hodnoceniList.Count();
            return avgh;
        }

        public void vypisHodnoceni()
        {
            foreach (int i in hodnoceniList) { Console.WriteLine(i); }
        }

        public string ToString()
        {
            string str = $"{Nazev} ({RokVzniku}, {PrijmeniRezisera} {JmenoRezisera}): {this.avgHodnoceni()}";
            return str ;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Film film_a = new Film("The Dark Knight", "Christopher", "Nolan", 2008);
            Film film_b = new Film("Inception", "Christopher", "Nolan", 2010);
            Film film_c = new Film("Pulp Fiction", "Quentin", "Tarantino", 1994);

            List<Film> filmy = new List<Film>();
            filmy.Add(film_a);
            filmy.Add(film_b); 
            filmy.Add(film_c);
            foreach (Film y in filmy) {
                for (int i = 0; i < 15; i++)
                {
                    Random rnd = new Random();
                    y.pridejhodnoceni(rnd.Next(0, 6));
                }
            }

            List<int> odpad = new List<int>();
            int max = 0;
            int min = 0;
            double nejlepsi = 0;
            double nejhorsi = 5;
            foreach (Film i in filmy) 
            { 
                if(i.avgHodnoceni() > max)
                {
                    max = filmy.IndexOf(i);
                    nejlepsi = i.avgHodnoceni();
                }
                if (i.avgHodnoceni() < min)
                {
                    min = filmy.IndexOf(i);
                    nejhorsi = i.avgHodnoceni();
                }
                if (i.avgHodnoceni() < 3)
                {
                    odpad.Add(filmy.IndexOf(i));
                }
            }

            Console.WriteLine($"Nejlépe hodnocený film je {filmy[max].Nazev} a to s průměrným hodnocením {filmy[max].avgHodnoceni()}");
            Console.WriteLine($"Nejhůře hodnocený film je {filmy[min].Nazev} a to s průměrným hodnocením {filmy[min].avgHodnoceni()}");
            Console.WriteLine();

            foreach (int i in odpad)
            {
                Console.WriteLine($"{filmy[i].Nazev} je odpad! Má hodnocení jen {filmy[i].avgHodnoceni()}");
            }

            Console.WriteLine();
            foreach(Film i in filmy)
            {
                Console.WriteLine(i.ToString());
            }

            film_a.vypisHodnoceni();
        }
    }
}
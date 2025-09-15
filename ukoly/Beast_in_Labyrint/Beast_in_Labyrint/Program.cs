using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Beast_in_Labyrint
{
    public class Labyrint
    {
        public int Sirka { get;}
        public int Vyska { get;}
        private string[,] mapa;

        public Labyrint(int sirka, int vyska)
        { 
            Sirka = sirka;
            Vyska = vyska;
            mapa = new string[vyska, sirka];
        }

        public void Showmap()
        {
            int x = 0;
            foreach(string i in mapa)
            {
                if (x < Sirka)
                {
                    Console.Write(i);
                }
                else
                {
                    Console.Write("\n");
                    Console.Write(i);
                    x = 0;
                }
                x++;
            }
        }
        
        public void Loadmap()
        {
            for (int y = 0; y < Vyska; y++)
            {
                string[] radek = Regex.Split(Console.ReadLine(), string.Empty);
                for (int x = 0; x < Sirka; x++)
                {
                    mapa[y,x] = radek[x+1];
                }
            }
        }
         
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Labyrint lab = new Labyrint(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
            lab.Loadmap();
            lab.Showmap();
        }
    }
}

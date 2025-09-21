using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Beast_in_Labyrint
{
    public class Labyrint
    {
        public int Sirka { get; }   // šířka bludiště
        public int Vyska { get; }   // výška bludiště
        private string[,] mapa;     // dvourozměrné pole reprezentující mapu

        static int monsterX, monsterY; // souřadnice příšery
        static int dir; // směr příšery: 0 = nahoru, 1 = doprava, 2 = dolů, 3 = doleva
        public bool rovne = false; // pomocná proměnná: zda šla příšera v posledním kroku rovně

        // Posuny podle směru (dx, dy)
        static readonly int[] dx = { 0, 1, 0, -1 };
        static readonly int[] dy = { -1, 0, 1, 0 };

        // Konstruktor - inicializace bludiště
        public Labyrint(int sirka, int vyska)
        {
            Sirka = sirka;
            Vyska = vyska;
            mapa = new string[vyska, sirka];
        }

        // Vykreslení aktuální mapy do konzole
        public void Showmap()
        {
            // Na aktuální pozici příšery vložíme její symbol podle směru
            mapa[monsterY, monsterX] = Convert.ToString("^>v<"[dir]);

            int x = 0;
            foreach (string i in mapa)
            {
                if (x < Sirka)
                {
                    Console.Write(i);
                }
                else
                {
                    // nový řádek po vypsání celé šířky
                    Console.Write("\n");
                    Console.Write(i);
                    x = 0;
                }
                x++;
            }
            Console.Write("\n");
        }

        // Načtení mapy z konzole
        public void Loadmap()
        {
            for (int y = 0; y < Vyska; y++)
            {
                // rozdělí řádek na jednotlivé znaky
                string[] radek = Regex.Split(Console.ReadLine(), string.Empty);

                for (int x = 0; x < Sirka; x++)
                {
                    mapa[y, x] = radek[x + 1]; // +1 protože Split přidá prázdný string na začátek

                    // pokud se zde nachází příšera, uložíme její polohu a směr
                    if ("^>v<".Contains(mapa[y, x]))
                    {
                        monsterX = x;
                        monsterY = y;
                        dir = "^>v<".IndexOf(mapa[y, x]);

                        // na mapě po startu je to volné pole
                        mapa[y, x] = ".";
                    }
                }
            }
        }

        // Pohyb příšery podle pravidla pravé ruky
        public void MoveMonster()
        {
            // současné políčko se vyprázdní (už tam nestojí příšera)
            mapa[monsterY, monsterX] = ".";

            // směr doprava od aktuálního směru
            int rightDir = (dir + 1) % 4;
            int nx = monsterX + dx[rightDir];
            int ny = monsterY + dy[rightDir];

            // pokud je vpravo volno a příšera zrovna nešla rovně,
            // otočí se doprava
            if (IsFree(nx, ny) && !rovne)
            {
                dir = rightDir;
                rovne = true; // další krok se pokusí jít rovně
                return;
            }

            // jinak zkusí jít rovně
            nx = monsterX + dx[dir];
            ny = monsterY + dy[dir];
            if (IsFree(nx, ny))
            {
                monsterX = nx;
                monsterY = ny;
                rovne = false;
                return;
            }

            // jinak doleva
            int leftDir = (dir + 3) % 4;
            nx = monsterX + dx[leftDir];
            ny = monsterY + dy[leftDir];
            if (IsFree(nx, ny))
            {
                dir = leftDir;
                return;
            }

            // jinak se otočí o 180° (dozadu)
            dir = (dir + 2) % 4;
        }

        // Kontrola, zda je dané políčko volné
        public bool IsFree(int x, int y)
        {
            return x >= 0 && x < Sirka && y >= 0 && y < Vyska && mapa[y, x] == ".";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // načtení rozměrů
            Labyrint lab = new Labyrint(
                Convert.ToInt32(Console.ReadLine()),
                Convert.ToInt32(Console.ReadLine())
            );

            // načtení mapy
            lab.Loadmap();

            // vypsání mapy se startovní polohou
            lab.Showmap();

            // kolik kroků má příšera udělat
            Console.Write("Zadej počet kroků: ");
            int x = Convert.ToInt32(Console.ReadLine());

            // simulace kroků
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine($"{i+1}. krok");
                lab.MoveMonster();
                lab.Showmap();
                Console.WriteLine();
            }
        }
    }
}

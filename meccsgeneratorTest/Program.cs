using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meccsgeneratorTest
{
    class Program
    {
        static List<Resztvevo> Resztvevok = new List<Resztvevo>() { 
            new Resztvevo() { Id = 1, Nev = "Balazs" },
            new Resztvevo() { Id = 2, Nev = "Gabor" },
            new Resztvevo() { Id = 3, Nev = "Jozsi" },
            new Resztvevo() { Id = 4, Nev = "Abigel" },
            new Resztvevo() { Id = 5, Nev = "Karcsi" },
            new Resztvevo() { Id = 6, Nev = "Karcsi" },
            new Resztvevo() { Id = 7, Nev = "Karcsi" },
            new Resztvevo() { Id = 8, Nev = "Karcsi" },
            new Resztvevo() { Id = 9, Nev = "Karcsi" },
            new Resztvevo() { Id = 10, Nev = "Karcsi" },
            new Resztvevo() { Id = 11, Nev = "Karcsi" },
            //new Resztvevo() { Id = 12, Nev = "Karcsi" },
            //new Resztvevo() { Id = 13, Nev = "Karcsi" },
            //new Resztvevo() { Id = 14, Nev = "Karcsi" },
            //new Resztvevo() { Id = 15, Nev = "Karcsi" },
            //new Resztvevo() { Id = 16, Nev = "Karcsi" },
        }; 

        static void Main(string[] args)
        {
            
            Seedeles(Resztvevok, true);


            Console.ReadKey();
        }

        private static Dictionary<int, Meccs>[] Seedeles(List<Resztvevo> resztvevok, bool harmadikHelyezes)
        {
            // A korok szama  = resztvevok szama negyzetgyoke felkerekitve
            int korokSzama = (int)Math.Ceiling(Math.Log(resztvevok.Count, 2));

            // A ketto a korok szamaval hatvanyozva
            int poziciokSzama = (int)Math.Pow(2, korokSzama);


            // A jatekos altal nem elfoglalt poziciok szama 
            int byeokSzama = poziciokSzama - resztvevok.Count;

            List<Resztvevo> beultetettResztvevok = new List<Resztvevo>();
            Dictionary<int, Meccs>[] korok = new Dictionary<int, Meccs>[korokSzama]; 
            

            for (int i = 0; i < korokSzama; i++)
            {
                korok[i] = new Dictionary<int, Meccs>();


                for (int j = 0; j < Math.Pow(2,korokSzama-i-1); j++)
                {
                    // Az elso korben el kell oszatni a nem elfogalt poziciokat
                    if (i == 0)
                    {
                        var meccs = new Meccs();
                        meccs.Id = j;
                        meccs.KihivoId = resztvevok[resztvevok.Count-(resztvevok.Count-beultetettResztvevok.Count)].Id;
                        meccs.KihivoPontszam = 0;
                        meccs.Kor = i;
                        beultetettResztvevok.Add(resztvevok[resztvevok.Count - (resztvevok.Count - beultetettResztvevok.Count)]);
                        if (byeokSzama > 0)
                        {
                            meccs.KihivottId = 0;
                            meccs.KihivottPontszam = - 1;
                            byeokSzama--;
                        }
                        else
                        {
                            meccs.KihivottId = resztvevok[resztvevok.Count - (resztvevok.Count - beultetettResztvevok.Count)].Id;
                            meccs.KihivottPontszam = 0;
                            beultetettResztvevok.Add(resztvevok[resztvevok.Count - (resztvevok.Count - beultetettResztvevok.Count)]);
                        }
                        meccs.Statusz = "Keszen all";
                        korok[i].Add(j, meccs);
                    }
                    else
                    {
                        korok[i].Add(j, new Meccs() { Statusz = "jatekosra var" });
                    }


                }


            }

            //Ha kell harmadik helyezes az utolso korbe egy uj meccs kerul 
            if (harmadikHelyezes)
            {
                korok[korokSzama-1].Add(1, new Meccs() { Statusz = "jatekosra var" });
            }



            //A nem elfoglalt poziciok miatti nyertesek tovabb juttatasa az elso korbol a masodikba
            List<int> tovabbJutottak = new List<int>();

            // Kilistazni a tovabbjutottakat
            for (int i = 0; i < korok[0].Count; i++)
            {

                int nyertesId = 0;

                if (korok[0][i].KihivottId == 0 )
                {
                    nyertesId = korok[0][i].KihivoId;
                    tovabbJutottak.Add(nyertesId);
                    korok[0][i].Statusz = "Lejatszott";
                }


               

            }

            // Az automatikusan tovabbjutottak behelyezese a masodik korbe
            int masodikKorMeccsSzamlalo = 0;
            for (int i = 1; i <= tovabbJutottak.Count; i++)
            {
                if (korok[1][masodikKorMeccsSzamlalo].KihivoId == 0)
                {
                    korok[1][masodikKorMeccsSzamlalo].KihivoId = i;
                }
                else
                {
                    korok[1][masodikKorMeccsSzamlalo].KihivottId = i;
                    korok[1][masodikKorMeccsSzamlalo].Statusz = "Keszen all";
                    masodikKorMeccsSzamlalo++;
                    
                }


            }


            // Kiiras
            for (int i = 0; i < korok.Length; i++)
            {
                Console.WriteLine($"{i+1} Kor\n");
                for (int j = 0; j < korok[i].Count; j++)
                {

                    Console.WriteLine($"{j} meccs, Kihivo : {korok[i][j].KihivoId}\t Kihivott: {korok[i][j].KihivottId} \t Statusz: {korok[i][j].Statusz}");
                }
                Console.WriteLine();
            }

            return korok;

        }

        

    }
}

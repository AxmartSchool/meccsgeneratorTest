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

        static Dictionary<int, Meccs>[] VersenyAllas;

        static void Main(string[] args)
        {
            
            VersenyAllas = Seedeles(Resztvevok, true);
            AllasKiirasa();
            EredmenytBekuld(0,5,2,1);
            EredmenytBekuld(0,6,2,1);
            EredmenytBekuld(0,7,2,1);
            EredmenytBekuld(1,0,2,1);
            EredmenytBekuld(1,1,2,1);
            EredmenytBekuld(1,2,2,1);
            EredmenytBekuld(1,3,2,1);
            EredmenytBekuld(2,0,2,1);
            EredmenytBekuld(2,1,2,1);
            EredmenytBekuld(3,0,2,1);
            EredmenytBekuld(3,1,2,1);
            AllasKiirasa();
            HelyezesekKiirasa();


            Console.ReadKey();
        }

        private static void HelyezesekKiirasa()
        {
            if (VersenyAllas[VersenyAllas.Length - 1][0].KihivoPontszam > VersenyAllas[VersenyAllas.Length - 1][0].KihivottPontszam)
            {
                Console.WriteLine($"1. Helyezes {VersenyAllas[VersenyAllas.Length - 1][0].KihivoId}");
                Console.WriteLine($"2. Helyezes {VersenyAllas[VersenyAllas.Length - 1][0].KihivottId}");
            }
            else
            {
                Console.WriteLine($"1. Helyezes {VersenyAllas[VersenyAllas.Length - 1][0].KihivottId}");
                Console.WriteLine($"2. Helyezes {VersenyAllas[VersenyAllas.Length - 1][0].KihivoId}");
            }

            if (VersenyAllas[VersenyAllas.Length - 1].Count > 1)
            {
                if (VersenyAllas[VersenyAllas.Length - 1][1].KihivoPontszam > VersenyAllas[VersenyAllas.Length - 1][1].KihivottPontszam)
                {
                    Console.WriteLine($"3. Helyezes {VersenyAllas[VersenyAllas.Length - 1][1].KihivoId}");
                    Console.WriteLine($"4. Helyezes {VersenyAllas[VersenyAllas.Length - 1][1].KihivottId}");
                }
                else
                {
                    Console.WriteLine($"3. Helyezes {VersenyAllas[VersenyAllas.Length - 1][1].KihivottId}");
                    Console.WriteLine($"4. Helyezes {VersenyAllas[VersenyAllas.Length - 1][1].KihivoId}");
                }
            }
            
        }

        private static void AllasKiirasa()
        {
            for (int i = 0; i < VersenyAllas.Length; i++)
            {
                Console.WriteLine($"{i + 1} Kor\n");
                for (int j = 0; j < VersenyAllas[i].Count; j++)
                {

                    Console.WriteLine($"{j} meccs, Kihivo : {VersenyAllas[i][j].KihivoId}\t Kihivott: {VersenyAllas[i][j].KihivottId} \t Statusz: {VersenyAllas[i][j].Statusz}");
                }
                Console.WriteLine();
            }
        }

        private static void EredmenytBekuld(int kor, int meccsId, int kihivoPontszam, int KihivottPontszam)
        {
            VersenyAllas[kor][meccsId].KihivoPontszam = kihivoPontszam;
            VersenyAllas[kor][meccsId].KihivottPontszam = KihivottPontszam;
            VersenyAllas[kor][meccsId].Statusz = "Lejatszott";
            int nyertesId;
            int vesztesId;
            if (kihivoPontszam > KihivottPontszam)
            {
                nyertesId = VersenyAllas[kor][meccsId].KihivoId;
                vesztesId = VersenyAllas[kor][meccsId].KihivottId;
            }
            else
            {
                nyertesId = VersenyAllas[kor][meccsId].KihivottId;
                vesztesId = VersenyAllas[kor][meccsId].KihivoId;
            }

            // Harmadik helyezes check
            if (VersenyAllas[VersenyAllas.Length-1].Count > 1)
            {
                // Van harmadik kor
                if (kor == VersenyAllas.Length -2)
                {
                    if (meccsId % 2 == 0)
                    {
                        VersenyAllas[VersenyAllas.Length - 1][1].KihivoId = vesztesId;
                    }
                    else
                    {
                        VersenyAllas[VersenyAllas.Length - 1][1].KihivottId = vesztesId;
                        VersenyAllas[VersenyAllas.Length - 1][1].Statusz = "Keszen All";
                    }


                }
            }

            if (VersenyAllas.Length == kor+1)
            {
                return;
                //verseny vege;
            }

            if (meccsId % 2 == 0)
            {
                VersenyAllas[kor + 1][meccsId / 2].KihivoId = VersenyAllas[kor][meccsId].KihivoId = nyertesId;
            }
            else
            {
                VersenyAllas[kor + 1][(meccsId-1) / 2].KihivottId = VersenyAllas[kor][meccsId].KihivoId = nyertesId;
                VersenyAllas[kor + 1][(meccsId - 1) / 2].Statusz = "Keszen All";
            }

            


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



            

            return korok;

        }

        

    }
}

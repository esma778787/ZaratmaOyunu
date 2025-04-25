using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pisti
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("İsminiz:");
            string isim = Console.ReadLine();
            Console.Write("Soyisminiz:");
            string soyisim = Console.ReadLine();

            Console.Write("Doğum Gününüz: ");
            int dogum_gunu = int.Parse(Console.ReadLine());

            Oyuncu oy0 = new Oyuncu(isim);
            Oyuncu oy1 = new Oyuncu(soyisim);

            Oyun oyn = new Oyun(oy0, oy1, dogum_gunu);



            Console.ReadKey();
        }



        class Oyuncu
        {
            public string adi;
            public List<Kart> el = new List<Kart>();
            public List<Kart> alınanlar = new List<Kart>();

            public Oyuncu(string isim)
            {
                adi = isim;
                Console.WriteLine();
            }

            public Kart KartAt()
            {
                Kart son = el[el.Count - 1];
                el.Remove(son);
                return son;
            }
        }

        class Kart
        {
            public int tip;
            public int deger;

            public Kart(int tip, int deger)
            {
                this.tip = tip;
                this.deger = deger;
            }
        }

        class Oyun
        {
            Oyuncu oyuncu0;
            Oyuncu oyuncu1;
            List<Kart> deste = new List<Kart>();
            List<Kart> masa = new List<Kart>();


            public Oyun(Oyuncu oyuncu0, Oyuncu oyuncu1, int karma_sayısı)
            {
                this.oyuncu0 = oyuncu0;
                this.oyuncu1 = oyuncu1;
                DesteOlustur();

                for (int i = 0; i < karma_sayısı; i++)
                {
                    KatlartıKaristir();
                }

                KartlarıDagıt();


                ListeYazdır(oyuncu1.el);




            }



            void DesteOlustur()
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 1; j < 14; j++)
                    {
                        Kart k = new Kart(i, j);
                        deste.Add(k);
                    }
                }
            }
            void ListeYazdır(List<Kart> liste)
            {
                for (int i = 0; i < liste.Count; i++)
                {
                    int tp = liste[i].tip;
                    string typ = "";
                    if (tp == 0)
                    {
                        typ = "sinek";
                    }
                    else if (tp == 1)
                    {
                        typ = "karo";
                    }
                    else if (tp == 2)
                    {
                        typ = "kupa";
                    }
                    else if (tp == 3)
                    {
                        typ = "maço";
                    }

                    Console.WriteLine(typ + " " + liste[i].deger);

                }
            }


            void KatlartıKaristir()
            {

                // kar
                Random rng = new Random();
                int n = deste.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Kart ilk = deste[k];
                    Kart iki = deste[n];
                    deste[k] = iki;
                    deste[n] = ilk;
                }

                //ortadan kes, alt kısmı üste koy
                for (int i = 0; i < deste.Count; i++)
                {
                    if (i + 1 > (deste.Count / 2))
                    {
                        break;
                    }
                    int ikinci = i + (deste.Count / 2);

                    Kart ilk = deste[i];
                    Kart iki = deste[ikinci];
                    deste[i] = iki;
                    deste[ikinci] = ilk;

                }

            }


            void KartlarıDagıt()
            {
                //masaya 4 kart
                for (int i = 0; i < 4; i++)
                {
                    int index = 51 - i;

                    masa.Add(deste[index]);
                    deste.RemoveAt(index);
                }

                //kalan kartları iki oyuncuya bölüştür
                for (int i = 0; i < (52 - 4); i++)
                {
                    if (i < (52 - 4) / 2)
                    {
                        oyuncu0.el.Add(deste[i]);
                    }
                    else
                    {
                        oyuncu1.el.Add(deste[i]);
                    }
                }
                deste.Clear();

            }


            void AnaOyunDongusu()
            {
                int turn = 0;
                while (oyuncu0.el.Count > 0)
                {
                    Kart atılan;

                    Oyuncu SimdikiOyuncu = (turn == 0) ? oyuncu0 : oyuncu1;
                    // ortaya kart at
                    atılan = SimdikiOyuncu.KartAt();

                    if (masa[masa.Count - 1].deger == atılan.deger)
                    {
                        //pişti

                        SimdikiOyuncu.alınanlar.Add(atılan);
                        foreach (Kart krt in masa)
                        {
                            SimdikiOyuncu.alınanlar.Add(atılan);
                        }


                    }
                    else
                    {
                        masa.Add(atılan);
                    }



                    //sırayı değiştir
                    turn = (turn == 0) ? 1 : 0;
                }

            }


        }






    }
}
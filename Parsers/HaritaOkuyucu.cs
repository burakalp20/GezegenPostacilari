
using System;
using System.Collections.Generic;
using System.IO;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;
using GezegenPostacilari.Terrains;

namespace GezegenPostacilari.Parsers
{
    // Harita dosyasındaki boyut, enerji, robot ve hücre bilgilerini okuyup doğrular.
    public class HaritaOkuyucu
    {
        public HaritaVerisi Oku(string dosyaYolu)
        {
            string[] satirlar = File.ReadAllLines(dosyaYolu);
            int index = 0;

            string SonrakiSatir(bool kenarlariKirp = true)
            {
                while (index < satirlar.Length)
                {
                    string hamSatir = satirlar[index++];
                    string satir = hamSatir.Trim();

                    if (satir.Length == 0 || satir.StartsWith("#"))
                        continue;

                    return kenarlariKirp ? satir : hamSatir;
                }

                throw new FormatException($"{satirlar.Length + 1}. satır: Beklenen veri bulunamadı.");
            }

            string[] boyut = SonrakiSatir().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (boyut.Length != 3 || boyut[0] != "BOYUT" ||
                !int.TryParse(boyut[1], out int genislik) ||
                !int.TryParse(boyut[2], out int yukseklik) ||
                genislik <= 0 || yukseklik <= 0)
            {
                throw new FormatException($"{index}. satır: Geçersiz BOYUT tanımı.");
            }

            string[] enerji = SonrakiSatir()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (enerji.Length != 2 || enerji[0] != "ENERJI" ||
                !int.TryParse(enerji[1], out int baslangicEnerjisi) ||
                baslangicEnerjisi < 0)
            {
                throw new FormatException(
                    $"{index}. satır: Geçersiz ENERJI tanımı.");
            }

            Dictionary<int, RobotGovdesi> govdeler = new();
            Dictionary<int, int> robotTanimSatirlari = new Dictionary<int, int>();

            while (true)
            {
                string satir = SonrakiSatir();

                if (satir == "HARITA")
                    break;

                string[] parcalar = satir.Split(
                    ' ', StringSplitOptions.RemoveEmptyEntries);

                if (parcalar.Length != 3 || parcalar[0] != "ROBOT" ||
                    !int.TryParse(parcalar[1], out int numara) ||
                    numara < 1 || numara > 9)
                {
                    throw new FormatException(
                        $"{index}. satır: Geçersiz ROBOT tanımı.");
                }

                if (govdeler.ContainsKey(numara))
                {
                    throw new FormatException(
                        $"{index}. satır: Robot numarası tekrar ediyor.");
                }

                if (!GovdeFabrikasi.TanimliMi(parcalar[2]))
                {
                    throw new FormatException(
                        $"{index}. satır: Bilinmeyen robot gövdesi.");
                }
                RobotGovdesi govde = GovdeFabrikasi.Olustur(parcalar[2]);

                govdeler.Add(numara, govde);
                robotTanimSatirlari.Add(numara, index);
            }

            Dictionary<int, Konum> konumlar = new();
            List<string> hucreSatirlari = new List<string>();

            for (int y = 0; y < yukseklik; y++)
            {
                string satir = SonrakiSatir(kenarlariKirp: false);

                if (satir.Length != genislik)
                {
                    throw new FormatException(
                        $"{index}. satır: Harita genişliği hatalı.");
                }

                for (int x = 0; x < genislik; x++)
                {
                    char karakter = satir[x];

                    if (karakter >= '1' && karakter <= '9')
                    {
                        int numara = karakter - '0';

                        if (!govdeler.ContainsKey(numara))
                        {
                            throw new FormatException(
                                $"{index}. satır: Tanımsız robot numarası.");
                        }

                        if (konumlar.ContainsKey(numara))
                        {
                            throw new FormatException(
                                $"{index}. satır: Robot haritada tekrar ediyor.");
                        }

                        konumlar.Add(numara, new Konum(x, y));
                    }
                    else if (!ZeminFabrikasi.TanimliMi(karakter))
                    {
                        throw new FormatException(
                            $"{index}. satır: Geçersiz harita karakteri.");
                    }
                }
                hucreSatirlari.Add(satir);
            }

            while (index < satirlar.Length)
            {
                string kalanSatir = satirlar[index++].Trim();
                if (kalanSatir.Length != 0 && !kalanSatir.StartsWith("#", StringComparison.Ordinal))
                {
                    throw new FormatException(
                        $"{index}. satır: Harita sonrasında beklenmeyen veri.");
                }
            }

            List<Robot> robotlar = new();

            foreach (int numara in govdeler.Keys)
            {
                if (!konumlar.ContainsKey(numara))
                {
                    throw new FormatException(
                        $"{robotTanimSatirlari[numara]}. satır: Robot {numara} için başlangıç konumu bulunamadı.");
                }

                robotlar.Add(new Robot(
                    numara,
                    govdeler[numara],
                    konumlar[numara],
                    baslangicEnerjisi));
            }

            robotlar.Sort((a, b) => a.Numara.CompareTo(b.Numara));

            Harita harita = new Harita(genislik, yukseklik);
            for (int satirSirasi = 0; satirSirasi < yukseklik; satirSirasi++)
            {
                for (int sutunSirasi = 0; sutunSirasi < genislik; sutunSirasi++)
                {
                    char sembol = hucreSatirlari[satirSirasi][sutunSirasi];
                    char zeminSembolu = sembol >= '1' && sembol <= '9' ? '.' : sembol;
                    harita.HucreAyarla(sutunSirasi, satirSirasi, zeminSembolu);
                }
            }

            return new HaritaVerisi(harita, baslangicEnerjisi, robotlar);
        }
    }
}

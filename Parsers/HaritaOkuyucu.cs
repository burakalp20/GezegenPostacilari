
using System;
using System.Collections.Generic;
using System.IO;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Parsers
{
    // Harita dosyasındaki boyut, enerji, robot ve hücre bilgilerini okuyup doğrular.
    public class HaritaOkuyucu
    {
        public HaritaVerisi Oku(string dosyaYolu)
        {
            string[] satirlar = File.ReadAllLines(dosyaYolu);
            int index = 0;

            string SonrakiSatir()
            {
                while (index < satirlar.Length)
                {
                    string satir = satirlar[index++].Trim();

                    if (satir.Length == 0 || satir.StartsWith("#"))
                        continue;

                    return satir;
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

            Harita harita = new Harita(genislik, yukseklik);

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

                RobotGovdesi govde = parcalar[2] switch
                {
                    "PALETLI" => new PaletliGovde(),
                    "SICRAYAN" => new SicrayanGovde(),
                    "HAVALI" => new HavaliGovde(),
                    "KIZAKLI" => new KizakliGovde(),
                    _ => throw new FormatException(
                        $"{index}. satır: Bilinmeyen robot gövdesi.")
                };

                govdeler.Add(numara, govde);
            }

            Dictionary<int, Konum> konumlar = new();

            for (int y = 0; y < yukseklik; y++)
            {
                string satir = SonrakiSatir();

                if (satir.Length != genislik)
                {
                    throw new FormatException(
                        $"{index}. satır: Harita genişliği hatalı.");
                }

                for (int x = 0; x < genislik; x++)
                {
                    char karakter = satir[x];

                    if (karakter == '.' || karakter == 'T' ||
                        karakter == 'K' || karakter == '~' ||
                        karakter == 'I' || karakter == 'X' ||
                        karakter == 'H')
                    {
                        harita.HucreAyarla(x, y, karakter);
                    }
                    else if (karakter >= '1' && karakter <= '9')
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
                        harita.HucreAyarla(x, y, '.');
                    }
                    else
                    {
                        throw new FormatException(
                            $"{index}. satır: Geçersiz harita karakteri.");
                    }
                }
            }

            List<Robot> robotlar = new();

            foreach (int numara in govdeler.Keys)
            {
                if (!konumlar.ContainsKey(numara))
                {
                    throw new FormatException(
                        $"Robot {numara} için başlangıç konumu bulunamadı.");
                }

                robotlar.Add(new Robot(
                    numara,
                    govdeler[numara],
                    konumlar[numara],
                    baslangicEnerjisi));
            }

            robotlar.Sort((a, b) => a.Numara.CompareTo(b.Numara));

            return new HaritaVerisi(harita, baslangicEnerjisi, robotlar);
        }
    }
}

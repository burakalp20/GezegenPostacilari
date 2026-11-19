using GezegenPostacilari.Commands;
using GezegenPostacilari.Models;
using GezegenPostacilari.Parsers;
using GezegenPostacilari.Presentation;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;
using GezegenPostacilari.Terrains;
using System.Diagnostics;
using System.Text;

namespace GezegenPostacilari.Dogrulama
{
    // Hareket, giriş ve görünüm kurallarını bağımsız beklenen sonuçlarla doğrular.
    internal static class Program
    {
        private static int basariliSayisi;
        private static int basarisizSayisi;
        private static readonly string[] GovdeAdlari =
            { "PALETLI", "SICRAYAN", "HAVALI", "KIZAKLI", "HIBRIT" };

        private static int Main()
        {
            MaliyetTablosunuDogrula();
            YonleriDogrula();
            SinirDurumlariniDogrula();
            DosyalariDogrula();
            ReferanslariDogrula();
            GorunumuDogrula();
            GenisletmeyiDogrula();
            ProgramGirisiDogrula();
            Console.WriteLine($"SONUC: {basariliSayisi} başarılı, {basarisizSayisi} başarısız.");
            return basarisizSayisi == 0 ? 0 : 1;
        }

        private static void MaliyetTablosunuDogrula()
        {
            char[] semboller = { '.', 'T', 'K', '~', 'I', 'B', 'H', 'X' };
            int[][] beklenenMaliyetler =
            {
                new[] { 1, 2, 3, -1, 2, 3, 1, -1 },
                new[] { 1, 2, 2, -1, 2, 3, 1, -1 },
                new[] { 1, 1, 1, 1, 1, 1, 1, -1 },
                new[] { 1, 2, 3, -1, 1, 3, 1, -1 },
                new[] { 1, 2, 2, -1, 1, 3, 1, -1 }
            };
            for (int govdeSirasi = 0; govdeSirasi < GovdeAdlari.Length; govdeSirasi++)
            {
                for (int zeminSirasi = 0; zeminSirasi < semboller.Length; zeminSirasi++)
                {
                    string govdeAdi = GovdeAdlari[govdeSirasi];
                    char sembol = semboller[zeminSirasi];
                    int maliyet = beklenenMaliyetler[govdeSirasi][zeminSirasi];
                    Test($"{govdeAdi}/{sembol} enerji ve hareket", () =>
                    {
                        Simulasyon simulasyon = TekRobot($".{sembol}..", govdeAdi, 20);
                        Robot robot = simulasyon.Robotlar[0];
                        new IleriKomutu(1).Uygula(robot, simulasyon);
                        int hedefX = 1;
                        if (maliyet < 0 || (govdeAdi == "HAVALI" && sembol == 'T'))
                        {
                            hedefX = 0;
                        }
                        else if (sembol == 'K' && (govdeAdi == "SICRAYAN" || govdeAdi == "HIBRIT"))
                        {
                            hedefX = 2;
                        }
                        RobotDogrula(robot, hedefX, 0, 20 - Math.Max(maliyet, 0),
                            sembol == 'H' ? RobotDurumu.TESLIM : RobotDurumu.AKTIF);
                    });
                }
            }
        }

        private static void YonleriDogrula()
        {
            (Yon Yon, int Yatay, int Dikey)[] yonler =
            {
                (Yon.N, 0, -1), (Yon.E, 1, 0), (Yon.S, 0, 1), (Yon.W, -1, 0)
            };
            foreach ((Yon yon, int yatay, int dikey) in yonler)
            {
                foreach (string govde in new[] { "PALETLI", "SICRAYAN", "KIZAKLI", "HIBRIT" })
                {
                    Test($"{yon}/{govde} yönlü hareket", () =>
                    {
                        Harita harita = HaritaOlustur(7, 7);
                        Robot robot = new Robot(1, GovdeFabrikasi.Olustur(govde), new Konum(3, 3), 10);
                        while (robot.Yon != yon)
                        {
                            robot.SagaDon();
                        }
                        int mesafe = 1;
                        int maliyet = 1;
                        if (govde == "SICRAYAN" || govde == "HIBRIT")
                        {
                            harita.HucreAyarla(3 + yatay, 3 + dikey, 'K');
                            mesafe = 2;
                            maliyet = 2;
                        }
                        else if (govde == "KIZAKLI")
                        {
                            for (int adim = 1; adim <= 3; adim++)
                            {
                                harita.HucreAyarla(3 + adim * yatay, 3 + adim * dikey, 'I');
                            }
                            mesafe = 3;
                        }
                        Simulasyon simulasyon = new Simulasyon(harita, new List<Robot> { robot });
                        new IleriKomutu(1).Uygula(robot, simulasyon);
                        RobotDogrula(robot, 3 + mesafe * yatay, 3 + mesafe * dikey,
                            10 - maliyet, RobotDurumu.AKTIF);
                        Esit(yon, robot.Yon, "Hareket yönü korumalı");
                    });
                }
            }
        }

        private static void SinirDurumlariniDogrula()
        {
            foreach (char inis in new[] { 'X', 'K', '~', 'B' })
            {
                HareketTesti($"Yasak sıçrama inişi {inis}", $".K{inis}", "HIBRIT", 10, 5,
                    0, 10, RobotDurumu.AKTIF);
            }
            HareketTesti("Sıçrama harita dışına inemez", ".K", "SICRAYAN", 10, 1, 0, 10, RobotDurumu.AKTIF);
            HareketTesti("Sıçramaya enerji yetmez", ".K.", "SICRAYAN", 1, 1, 0, 1, RobotDurumu.ENERJISIZ);
            HareketTesti("Sıçramaya enerji tam yeter", ".K.", "SICRAYAN", 2, 1, 2, 0, RobotDurumu.AKTIF);
            HareketTesti("Kayma tek adım ve tek maliyet", ".III.", "KIZAKLI", 10, 1, 3, 9, RobotDurumu.AKTIF);
            HareketTesti("Hibrit kayar", ".III.", "HIBRIT", 10, 1, 3, 9, RobotDurumu.AKTIF);
            HareketTesti("Kayma sonrası normal adım", ".III.", "KIZAKLI", 10, 2, 4, 8, RobotDurumu.AKTIF);
            HareketTesti("Kayma harita kenarında biter", ".III", "KIZAKLI", 1, 99, 3, 0, RobotDurumu.AKTIF);
            HareketTesti("Kayma enerji yoksa başlamaz", ".III", "KIZAKLI", 0, 1, 0, 0, RobotDurumu.ENERJISIZ);
            HareketTesti("Kayma engelde durur", ".IIX", "KIZAKLI", 10, 99, 2, 9, RobotDurumu.AKTIF);
            HareketTesti("Kayma gölde durur", ".II~", "KIZAKLI", 10, 99, 2, 9, RobotDurumu.AKTIF);
            HareketTesti("Buzdan rampaya geçiş ayrıca ödenir", ".IIH", "KIZAKLI", 2, 2, 3, 0, RobotDurumu.TESLIM);
            HareketTesti("Tozda savrulma kalan adımları iptal eder", ".T..", "HAVALI", 5, 99, 0, 4, RobotDurumu.AKTIF);
            HareketTesti("Toz savrulmasına enerji yetmez", ".T.", "HAVALI", 0, 1, 0, 0, RobotDurumu.ENERJISIZ);
            HareketTesti("Engel kontrolü enerji kontrolünden önce", ".X", "PALETLI", 0, 1, 0, 0, RobotDurumu.AKTIF);
            HareketTesti("Normal adımda enerji tükenir", "...", "PALETLI", 1, 2, 1, 0, RobotDurumu.ENERJISIZ);
            HareketTesti("Rampaya enerji tam yeter", ".H.", "PALETLI", 1, 99, 1, 0, RobotDurumu.TESLIM);
            HareketTesti("Rampaya enerji yetmez", ".H", "PALETLI", 0, 1, 0, 0, RobotDurumu.ENERJISIZ);
            HareketTesti("Hibritin buza inişi ikinci hareket başlatmaz", ".KII.", "HIBRIT", 10, 1, 2, 8, RobotDurumu.AKTIF);
            HareketTesti("Sıçrama toza inişte toplam iki enerji harcar", ".KT", "SICRAYAN", 10, 1, 2, 8, RobotDurumu.AKTIF);
            HareketTesti("Mevcut yorum: rampaya sıçrama iki enerjiyle teslim eder", ".KH", "SICRAYAN", 2, 1, 2, 0, RobotDurumu.TESLIM);
            HareketTesti("Mevcut yorum: rampaya sıçramaya bir enerji yetmez", ".KH", "SICRAYAN", 1, 1, 0, 1, RobotDurumu.ENERJISIZ);

            foreach ((string harita, string govde, int engelX, int hedefX, int enerji) in
                new (string, string, int, int, int)[]
                {
                    ("....", "PALETLI", 1, 0, 10),
                    (".K..", "SICRAYAN", 1, 0, 10),
                    (".K..", "SICRAYAN", 2, 0, 10),
                    (".III", "KIZAKLI", 2, 1, 9)
                })
            {
                Test($"{govde} dolu hücre {engelX}", () =>
                {
                    Simulasyon simulasyon = TekRobot(harita, govde, 10);
                    Robot engel = new Robot(2, new PaletliGovde(), new Konum(engelX, 0), 0);
                    engel.EnerjiHarca(1);
                    simulasyon.Robotlar.Add(engel);
                    new IleriKomutu(99).Uygula(simulasyon.Robotlar[0], simulasyon);
                    RobotDogrula(simulasyon.Robotlar[0], hedefX, 0, enerji, RobotDurumu.AKTIF);
                });
            }

            Test("Teslim edilen robot rampayı boşaltır", () =>
            {
                Simulasyon simulasyon = TekRobot("..H", "PALETLI", 10);
                Robot ondeki = new Robot(2, new PaletliGovde(), new Konum(1, 0), 1);
                simulasyon.Robotlar.Add(ondeki);
                new IleriKomutu(1).Uygula(ondeki, simulasyon);
                new IleriKomutu(2).Uygula(simulasyon.Robotlar[0], simulasyon);
                RobotDogrula(simulasyon.Robotlar[0], 2, 0, 8, RobotDurumu.TESLIM);
            });
            Test("Komutlar ve robotlar numara sırasıyla yürür", () =>
            {
                Simulasyon simulasyon = TekRobot("...", "PALETLI", 10);
                Robot birinci = simulasyon.Robotlar[0];
                Robot ikinci = new Robot(2, new PaletliGovde(), new Konum(1, 0), 10);
                simulasyon.Robotlar.Insert(0, ikinci);
                simulasyon.Calistir(new List<Komut> { new IleriKomutu(1), new IleriKomutu(1) });
                RobotDogrula(birinci, 1, 0, 9, RobotDurumu.AKTIF);
                RobotDogrula(ikinci, 2, 0, 9, RobotDurumu.AKTIF);
            });
            foreach (bool teslim in new[] { false, true })
            {
                Test($"Pasif robot komut uygulamaz, teslim={teslim}", () =>
                {
                    Simulasyon simulasyon = TekRobot("...", "PALETLI", 0);
                    Robot robot = simulasyon.Robotlar[0];
                    if (teslim) { robot.TeslimEt(); }
                    else { robot.EnerjiHarca(1); }
                    new SagKomutu().Uygula(robot, simulasyon);
                    Esit(Yon.E, robot.Yon, "Pasif robot sağa dönmemeli");
                    new SolKomutu().Uygula(robot, simulasyon);
                    Esit(Yon.E, robot.Yon, "Pasif robot sola dönmemeli");
                    simulasyon.Calistir(new List<Komut> { new IleriKomutu(1) });
                    RobotDogrula(robot, 0, 0, 0, teslim ? RobotDurumu.TESLIM : RobotDurumu.ENERJISIZ);
                    Esit(Yon.E, robot.Yon, "Pasif robotun yönü");
                });
            }
            Test("Dört sağ ve dört sol dönüş enerji harcamaz", () =>
            {
                Simulasyon simulasyon = TekRobot(".", "PALETLI", 0);
                Robot robot = simulasyon.Robotlar[0];
                for (int donus = 0; donus < 4; donus++) { robot.SagaDon(); }
                Esit(Yon.E, robot.Yon, "Dört sağ dönüş");
                for (int donus = 0; donus < 4; donus++) { robot.SolaDon(); }
                Esit(Yon.E, robot.Yon, "Dört sol dönüş");
                Esit(0, robot.Enerji, "Dönüş enerjisi");
            });
        }

        private static void DosyalariDogrula()
        {
            foreach (string komut in new[]
                { "ILERI 0", "ILERI 100", "ILERI -1", "ILERI 1.5", "ILERI 999999999999",
                  "ILERI", "ILERI 1 2", "SAG 1", "SOL 1", "GERI", "ileri 1" })
            {
                Test($"Geçersiz komut: {komut}", () =>
                    DosyaIle("# açıklama\n\n" + komut, dosya =>
                        FormatHatasi(() => new KomutOkuyucu().Oku(dosya), 3)));
            }
            Test("Komut yorumları, sekmeler ve sınır değerleri", () =>
                DosyaIle("# açıklama\n\nILERI\t1 # ileri\nSAG\nSOL\nILERI 99", dosya =>
                {
                    List<Komut> komutlar = new KomutOkuyucu().Oku(dosya);
                    Esit(4, komutlar.Count, "Komut sayısı");
                    Simulasyon simulasyon = TekRobot("....", "PALETLI", 10);
                    simulasyon.Calistir(komutlar);
                    RobotDogrula(simulasyon.Robotlar[0], 3, 0, 7, RobotDurumu.AKTIF);
                }));
            Test("Boş komut listesi", () => DosyaIle("# açıklama\n", dosya =>
                Esit(0, new KomutOkuyucu().Oku(dosya).Count, "Komut sayısı")));

            (string Metin, int Satir)[] hataliHaritalar =
            {
                ("BOYUT 0 1\n", 1),
                ("BOYUT 3 1\nENERJI -1\n", 2),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 BILINMEYEN\n", 3),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nROBOT 1 HAVALI\n", 4),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n11H\n", 5),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n2.H\n", 5),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n1?H\n", 5),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n..H\n", 3),
                ("BOYUT 3 2\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n1.H\n", 6),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n1.H\nFAZLA", 6),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n 1.H", 5),
                ("BOYUT 3 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n1.H ", 5),
                ("BOYUT 2147483647 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n1.H", 5),
                ("BOYUT 3 2147483647\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n1.H", 6)
            };
            for (int senaryo = 0; senaryo < hataliHaritalar.Length; senaryo++)
            {
                (string metin, int satir) = hataliHaritalar[senaryo];
                Test($"Hatalı harita {senaryo + 1}, satır {satir}", () =>
                    DosyaIle(metin, dosya => FormatHatasi(() => new HaritaOkuyucu().Oku(dosya), satir)));
            }
            Test("Haritada yorumlar ve sondaki boşluk satırları", () =>
                DosyaIle("# açıklama\nBOYUT 3 1\nENERJI 2\nROBOT 1 HIBRIT\nHARITA\n1BH\n\n# son",
                    dosya => Esit('B', new HaritaOkuyucu().Oku(dosya).Harita.HucreGetir(1, 0), "Bataklık")));
        }

        private static void ReferanslariDogrula()
        {
            (string Dosya, Komut[] Komutlar, int Teslim, int Enerji)[] ornekler =
            {
                ("harita.txt", new Komut[] { new IleriKomutu(11) }, 2, 43),
                ("harita.txt", new Komut[] { new IleriKomutu(6), new SagKomutu(), new IleriKomutu(2), new SolKomutu(), new IleriKomutu(11) }, 3, 33),
                ("harita.txt", new Komut[] { new SolKomutu(), new IleriKomutu(1), new SagKomutu(), new IleriKomutu(11) }, 4, 26),
                ("harita_gun7.txt", new Komut[] { new IleriKomutu(8) }, 5, 46)
            };
            foreach ((string dosya, Komut[] komutlar, int teslim, int enerji) in ornekler)
            {
                Test($"Referans {dosya}, teslim {teslim}, enerji {enerji}", () =>
                {
                    HaritaVerisi veri = new HaritaOkuyucu().Oku(Path.Combine(AppContext.BaseDirectory, "TestData", dosya));
                    Simulasyon simulasyon = new Simulasyon(veri.Harita, veri.Robotlar);
                    simulasyon.Calistir(komutlar);
                    Esit(teslim, veri.Robotlar.Count(robot => robot.Durum == RobotDurumu.TESLIM), "Teslim sayısı");
                    Esit(enerji, veri.Robotlar.Sum(robot => robot.Enerji), "Enerji toplamı");
                    if (teslim == 2)
                    {
                        RobotDogrula(veri.Robotlar[0], 11, 0, 6, RobotDurumu.TESLIM);
                        RobotDogrula(veri.Robotlar[1], 6, 2, 9, RobotDurumu.AKTIF);
                        RobotDogrula(veri.Robotlar[2], 11, 4, 9, RobotDurumu.TESLIM);
                        RobotDogrula(veri.Robotlar[3], 1, 6, 19, RobotDurumu.AKTIF);
                    }
                });
            }
        }

        private static void GorunumuDogrula()
        {
            Test("Görünüm zemini ve robot durumunu değiştirmez", () =>
            {
                Simulasyon simulasyon = TekRobot(".BH", "HIBRIT", 2);
                new IleriKomutu(1).Uygula(simulasyon.Robotlar[0], simulasyon);
                using StringWriter cikti = new StringWriter();
                new KonsolGorunumu().Yaz(simulasyon, cikti, "TEST");
                RobotDogrula(simulasyon.Robotlar[0], 0, 0, 2, RobotDurumu.ENERJISIZ);
                Esit('B', simulasyon.Harita.HucreGetir(1, 0), "Görünüm sonrası zemin");
                if (!cikti.ToString().Contains("ENERJISIZ", StringComparison.Ordinal))
                {
                    throw new InvalidOperationException("Enerjisiz durumu görünümde bulunamadı.");
                }
            });
            Test("Komut sonrası çağrı bütün robotların ardından yapılır", () =>
            {
                Harita harita = HaritaOlustur(4, 2);
                Robot birinci = new Robot(1, new PaletliGovde(), new Konum(0, 0), 10);
                Robot ikinci = new Robot(2, new PaletliGovde(), new Konum(0, 1), 10);
                Simulasyon simulasyon = new Simulasyon(harita, new List<Robot> { birinci, ikinci });
                int cagriSayisi = 0;
                simulasyon.Calistir(new List<Komut> { new IleriKomutu(1), new IleriKomutu(1) },
                    sira =>
                    {
                        cagriSayisi++;
                        Esit(sira, simulasyon.Robotlar[0].Konum.X, "Komut sonrası konum");
                        Esit(sira, simulasyon.Robotlar[1].Konum.X, "İkinci robotun komut sonrası konumu");
                    });
                Esit(2, cagriSayisi, "Çağrı sayısı");
            });
        }

        private static void GenisletmeyiDogrula()
        {
            Test("Yeni gövde ve zemin kendi sınıflarından otomatik kaydolur", () =>
            {
                Esit(true, GovdeFabrikasi.TanimliMi("DENEMEUCAN"), "Gövde kaydı");
                Esit(true, ZeminFabrikasi.TanimliMi('Q'), "Zemin kaydı");
            });
            Test("Yeni türler dosyadan okunur ve mevcut gölden geçer", () =>
                DosyaIle("BOYUT 4 1\nENERJI 10\nROBOT 1 DENEMEUCAN\nHARITA\n1~QH", dosya =>
                {
                    HaritaVerisi veri = new HaritaOkuyucu().Oku(dosya);
                    Simulasyon simulasyon = new Simulasyon(veri.Harita, veri.Robotlar);
                    simulasyon.Calistir(new List<Komut> { new IleriKomutu(3) });
                    RobotDogrula(veri.Robotlar[0], 3, 0, 6, RobotDurumu.TESLIM);
                }));
            Test("Yeni zemin mevcut paletli gövdeyle çalışır", () =>
            {
                Simulasyon simulasyon = TekRobot(".QH", "PALETLI", 10);
                new IleriKomutu(2).Uygula(simulasyon.Robotlar[0], simulasyon);
                RobotDogrula(simulasyon.Robotlar[0], 2, 0, 5, RobotDurumu.TESLIM);
            });
            Test("Görünüm yeni zemin açıklamasını kayıttan alır", () =>
            {
                Simulasyon simulasyon = TekRobot(".QH", "DENEMEUCAN", 10);
                using StringWriter cikti = new StringWriter();
                new KonsolGorunumu().Yaz(simulasyon, cikti, "TEST");
                Esit(true, cikti.ToString().Contains("Q DenemeZemini", StringComparison.Ordinal), "Yeni zemin açıklaması");
            });
            Test("Tekrarlanan gövde kaydı mevcut kaydı değiştirmez", () =>
            {
                RobotGovdesi onceki = GovdeFabrikasi.Olustur("PALETLI");
                ArgumanHatasi(() => GovdeFabrikasi.Kaydet(new DenemeGovdesi("PALETLI")));
                Esit(true, ReferenceEquals(onceki, GovdeFabrikasi.Olustur("PALETLI")), "Eski gövde kaydı");
            });
            Test("Tekrarlanan zemin kaydı mevcut kaydı değiştirmez", () =>
            {
                Zemin onceki = ZeminFabrikasi.Olustur('.');
                ArgumanHatasi(() => ZeminFabrikasi.Kaydet(new DenemeZemini('.')));
                Esit(true, ReferenceEquals(onceki, ZeminFabrikasi.Olustur('.')), "Eski zemin kaydı");
            });
            Test("Null gövde kaydı reddedilir", () => ArgumanHatasi(() => GovdeFabrikasi.Kaydet(null!)));
            Test("Null zemin kaydı reddedilir", () => ArgumanHatasi(() => ZeminFabrikasi.Kaydet(null!)));
            foreach (string ad in new[] { "", " ", "IKI KELIME", "ROBOT#YORUM", "TAB\tAD" })
            {
                Test($"Geçersiz gövde adı [{ad}]", () =>
                    ArgumanHatasi(() => GovdeFabrikasi.Kaydet(new DenemeGovdesi(ad))));
            }
            foreach (char sembol in new[] { '0', '1', '9', '#', ' ', '\t', '\0' })
            {
                Test($"Geçersiz zemin kodu {(int)sembol}", () =>
                    ArgumanHatasi(() => ZeminFabrikasi.Kaydet(new DenemeZemini(sembol))));
            }
        }

        private static void ProgramGirisiDogrula()
        {
            string harita = Path.Combine(AppContext.BaseDirectory, "TestData", "harita.txt");
            Test("Büyük hatalı boyut normal hata mesajıyla reddedilir", () =>
                DosyaIle("BOYUT 2147483647 1\nENERJI 1\nROBOT 1 PALETLI\nHARITA\n1.H", dosya =>
                {
                    string komutlar = Path.Combine(AppContext.BaseDirectory, "TestData", "komutlar.txt");
                    (int kod, string cikti, string hata) = ProgramiCalistir(new[] { dosya, komutlar });
                    Esit(1, kod, "Geçersiz boyut çıkışı");
                    Esit("", cikti, "Sonuç oluşmamalı");
                    Esit(true, hata.Contains("5. satır: Harita genişliği hatalı.", StringComparison.Ordinal), "Boyut hatası");
                }));
            Test("Gerçek programın resmi çıktısı satır satır eşleşir", () =>
                DosyaIle("ILERI 11", dosya =>
                {
                    (int kod, string cikti, string hata) = ProgramiCalistir(new[] { harita, dosya });
                    Esit(0, kod, "Çıkış kodu");
                    Esit("", hata, "Hata çıktısı");
                    string beklenen = string.Join("\n", new[]
                    {
                        "ROBOT 1 KIZAKLI KONUM 11 0 YON E ENERJI 6 DURUM TESLIM",
                        "ROBOT 2 PALETLI KONUM 6 2 YON E ENERJI 9 DURUM AKTIF",
                        "ROBOT 3 HAVALI KONUM 11 4 YON E ENERJI 9 DURUM TESLIM",
                        "ROBOT 4 SICRAYAN KONUM 1 6 YON E ENERJI 19 DURUM AKTIF",
                        "TESLIM 2", "KALAN_ENERJI 43"
                    });
                    Esit(beklenen, cikti.Replace("\r\n", "\n").TrimEnd(), "Resmi çıktı");
                }));
            Test("Ayrı süreçte hibrit ve bataklık kaydı, görünüm ve standart çıktı", () =>
            {
                string ozgunHarita = Path.Combine(AppContext.BaseDirectory, "TestData", "harita_gun7.txt");
                string komutlar = Path.Combine(AppContext.BaseDirectory, "TestData", "komutlar_gun7.txt");
                (int normalKod, string normalCikti, string normalHata) = ProgramiCalistir(new[] { ozgunHarita, komutlar });
                (int gorunumKod, string gorunumCikti, string gorunum) = ProgramiCalistir(new[] { ozgunHarita, komutlar, "--goster" });
                Esit(0, normalKod, "Normal çıkış");
                Esit(0, gorunumKod, "Görünüm çıkışı");
                Esit("", normalHata, "Normal hata çıktısı");
                Esit(normalCikti, gorunumCikti, "Görünüm sonucu değiştirmemeli");
                Esit(true, normalCikti.Contains("KALAN_ENERJI 46", StringComparison.Ordinal), "Özgün sonuç");
                Esit(true, gorunum.Contains("B Bataklik", StringComparison.Ordinal), "Bataklık açıklaması");
                Esit(true, gorunum.Contains("HIBRIT", StringComparison.Ordinal), "Hibrit görünümü");
                Esit(false, gorunum.Contains("DenemeZemini", StringComparison.Ordinal), "Test türleri ana uygulamaya karışmamalı");
            });
            Test("Gerçek program hatalı dosyada simülasyonu başlatmaz", () =>
                DosyaIle("ILERI 1\nILERI 0", dosya =>
                {
                    (int kod, string cikti, string hata) = ProgramiCalistir(new[] { harita, dosya, "--goster" });
                    Esit(1, kod, "Hatalı dosya çıkışı");
                    Esit("", cikti, "Sonuç üretilmemeli");
                    Esit(true, hata.Contains("2. satır:", StringComparison.Ordinal), $"Hata satırı, çıktı [{hata}]");
                    Esit(false, hata.Contains("BASLANGIC", StringComparison.Ordinal), "Görünüm açılmamalı");
                }));
        }

        private static (int Kod, string Cikti, string Hata) ProgramiCalistir(IReadOnlyList<string> argumanlar)
        {
            ProcessStartInfo baslangic = new ProcessStartInfo("dotnet");
            baslangic.UseShellExecute = false;
            baslangic.CreateNoWindow = true;
            baslangic.RedirectStandardOutput = true;
            baslangic.RedirectStandardError = true;
            baslangic.StandardOutputEncoding = Encoding.UTF8;
            baslangic.StandardErrorEncoding = Encoding.UTF8;
            baslangic.ArgumentList.Add("exec");
            baslangic.ArgumentList.Add("--runtimeconfig");
            baslangic.ArgumentList.Add(Path.Combine(AppContext.BaseDirectory,
                "GezegenPostacilari.Dogrulama.runtimeconfig.json"));
            baslangic.ArgumentList.Add(typeof(Robot).Assembly.Location);
            foreach (string arguman in argumanlar)
            {
                baslangic.ArgumentList.Add(arguman);
            }
            using Process islem = Process.Start(baslangic)
                ?? throw new InvalidOperationException("Program başlatılamadı.");
            Task<string> cikti = islem.StandardOutput.ReadToEndAsync();
            Task<string> hata = islem.StandardError.ReadToEndAsync();
            if (!islem.WaitForExit(30000))
            {
                islem.Kill(entireProcessTree: true);
                islem.WaitForExit();
                throw new TimeoutException("Program otuz saniyede tamamlanmadı.");
            }
            return (islem.ExitCode, cikti.GetAwaiter().GetResult(), hata.GetAwaiter().GetResult());
        }

        private static void ArgumanHatasi(Action islem)
        {
            try
            {
                islem();
            }
            catch (ArgumentException)
            {
                return;
            }
            throw new InvalidOperationException("Geçersiz kayıt kabul edildi.");
        }

        private static void HareketTesti(string ad, string satir, string govde, int enerji,
            int adim, int hedefX, int kalanEnerji, RobotDurumu durum)
        {
            Test(ad, () =>
            {
                Simulasyon simulasyon = TekRobot(satir, govde, enerji);
                new IleriKomutu(adim).Uygula(simulasyon.Robotlar[0], simulasyon);
                RobotDogrula(simulasyon.Robotlar[0], hedefX, 0, kalanEnerji, durum);
            });
        }

        private static Simulasyon TekRobot(string satir, string govde, int enerji)
        {
            Harita harita = HaritaOlustur(satir.Length, 1);
            for (int sutun = 0; sutun < satir.Length; sutun++)
            {
                harita.HucreAyarla(sutun, 0, satir[sutun]);
            }
            Robot robot = new Robot(1, GovdeFabrikasi.Olustur(govde), new Konum(0, 0), enerji);
            return new Simulasyon(harita, new List<Robot> { robot });
        }

        private static Harita HaritaOlustur(int genislik, int yukseklik)
        {
            Harita harita = new Harita(genislik, yukseklik);
            for (int satir = 0; satir < yukseklik; satir++)
            {
                for (int sutun = 0; sutun < genislik; sutun++)
                {
                    harita.HucreAyarla(sutun, satir, '.');
                }
            }
            return harita;
        }

        private static void DosyaIle(string metin, Action<string> dogrulama)
        {
            string dosya = Path.GetTempFileName();
            try
            {
                File.WriteAllText(dosya, metin);
                dogrulama(dosya);
            }
            finally
            {
                File.Delete(dosya);
            }
        }

        private static void FormatHatasi(Action islem, int satir)
        {
            try
            {
                islem();
            }
            catch (FormatException hata)
            {
                if (!hata.Message.StartsWith($"{satir}. satır:", StringComparison.Ordinal))
                {
                    throw new InvalidOperationException($"Beklenen satır {satir}, gerçek hata: {hata.Message}");
                }
                return;
            }
            throw new InvalidOperationException("Geçersiz dosya kabul edildi.");
        }

        private static void RobotDogrula(Robot robot, int beklenenX, int beklenenY,
            int enerji, RobotDurumu durum)
        {
            Esit(beklenenX, robot.Konum.X, "X");
            Esit(beklenenY, robot.Konum.Y, "Y");
            Esit(enerji, robot.Enerji, "Enerji");
            Esit(durum, robot.Durum, "Durum");
        }

        private static void Esit<T>(T beklenen, T gercek, string alan)
        {
            if (!EqualityComparer<T>.Default.Equals(beklenen, gercek))
            {
                throw new InvalidOperationException($"{alan}: beklenen {beklenen}, gerçek {gercek}.");
            }
        }

        private static void Test(string ad, Action dogrulama)
        {
            try
            {
                dogrulama();
                basariliSayisi++;
                Console.WriteLine($"BASARILI: {ad}");
            }
            catch (Exception hata)
            {
                basarisizSayisi++;
                Console.WriteLine($"BASARISIZ: {ad} — {hata.Message}");
            }
        }
    }
}

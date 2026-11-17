# 🚀 Gezegen Postacıları

### C# | Nesne Yönelimli Programlama | Robot Simülasyonu

![C#](https://img.shields.io/badge/C%23-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)
![Status](https://img.shields.io/badge/Durum-Geliştiriliyor-orange?style=for-the-badge)

## 📖 Proje Hakkında

**Gezegen Postacıları**, farklı hareket yeteneklerine sahip kargo robotlarının uzak bir gezegende paket teslimatı gerçekleştirmesini simüle eden, C# ile geliştirilen nesne yönelimli bir programlama projesidir.

Simülasyonda dört farklı robot, aynı komut listesini takip ederek gezegen yüzeyinde ilerlemeye çalışır. Ancak her robotun hareket kabiliyetleri ve zeminlerle etkileşimleri farklıdır.

Projenin temel amacı, robotların hareketlerini ve enerji tüketimlerini simüle ederken **kalıtım, soyutlama, polimorfizm ve kapsülleme** prensiplerini uygulayan genişletilebilir bir yazılım mimarisi oluşturmaktır.

## 🤖 Robot Türleri

| Robot | Özelliği |
|---|---|
| 🛞 Paletli | Standart hareket kabiliyetine sahiptir. |
| 🦘 Sıçrayan | Kraterli kayaların üzerinden atlayabilir. |
| 🛸 Havalı | Metan göllerini geçebilir ancak toz fırtınasında savrulur. |
| 🛷 Kızaklı | Buz üzerinde kayarak ilerleyebilir. |

## 🪐 Gezegen Yüzeyi

Gezegen yüzeyi farklı zemin türlerinden oluşan iki boyutlu bir harita ile temsil edilir.

| Sembol | Zemin | Açıklama |
|---|---|---|
| `.` | Düz zemin | Bütün robotlar geçebilir. |
| `T` | Toz fırtınası | Robotun hareketini etkiler. |
| `K` | Kraterli kaya | Sıçrama yeteneği kullanılabilir. |
| `~` | Metan gölü | Yalnızca uçabilen robotlar geçebilir. |
| `I` | Buz kabuğu | Kayabilen robotlar için özel hareket sağlar. |
| `X` | Engel | Geçilemez. |
| `H` | Teslim rampası | Paketlerin teslim edildiği noktadır. |

## ⚙️ Kullanılan Teknolojiler

- **Programlama Dili:** C#
- **Platform:** .NET
- **Geliştirme Ortamı:** Visual Studio 2022
- **Programlama Yaklaşımı:** Nesne Yönelimli Programlama (OOP)
- **Veri Girişi:** TXT dosyaları

## 🏗️ Yazılım Mimarisi

Proje, sorumlulukları birbirinden ayrılmış sınıflar kullanılarak geliştirilmektedir.

```text
GezegenPostacilari/
│
├── Models/          # Veri modelleri ve robot durumları
├── Robots/          # Robotlar ve gövde türleri
├── Terrains/        # Zemin türleri ve kuralları
├── Commands/        # Hareket komutları
├── Simulation/      # Harita ve simülasyon yönetimi
├── Parsers/         # Dosya okuma ve doğrulama
├── Constants/       # Sabit enerji maliyetleri
├── TestData/        # Örnek giriş dosyaları
│
└── Program.cs       # Programın başlangıç noktası
```

### Nesne Yönelimli Programlama

**Soyutlama (Abstraction)**

Robot gövdeleri ve zemin türleri soyut üst sınıflar kullanılarak modellenir.

**Kalıtım (Inheritance)**

Farklı robot ve zemin türleri ortak üst sınıflardan türetilir.

**Polimorfizm (Polymorphism)**

Robotların yeteneklerine göre zeminlerde farklı enerji maliyetleri ve hareket davranışları tanımlanır.

**Kapsülleme (Encapsulation)**

Robotların konum, yön, enerji ve durum bilgileri kontrollü metotlar aracılığıyla yönetilir.

## 📂 Giriş Dosyaları

Simülasyon iki farklı metin dosyası üzerinden çalışacak şekilde tasarlanmaktadır.

**harita.txt**

Harita boyutlarını, robotların başlangıç enerjilerini, gövde türlerini ve gezegen yüzeyini tanımlar.

```text
BOYUT 12 8
ENERJI 20
ROBOT 1 KIZAKLI
ROBOT 2 PALETLI
ROBOT 3 HAVALI
ROBOT 4 SICRAYAN
HARITA
1.I.TTTT.IIH
..IK.......H
2.IK.IIX...H
..I.K......H
3...K..III.H
.........T.H
4.~~~~IIII.H
..KKKK...T.H
```

**komutlar.txt**

Robotların sırasıyla uygulayacağı hareket komutlarını içerir.

```text
ILERI 6
SAG
ILERI 2
SOL
ILERI 11
```


## 📌 Geliştirme Durumu

- [x] Proje ve klasör yapısının oluşturulması
- [x] Robot gövdelerinin modellenmesi
- [x] Temel OOP mimarisinin hazırlanması
- [x] Harita dosyasının okunması
- [x] Temel dosya doğrulama kontrolleri
- [x] Zemin sınıfları ve enerji maliyetlerinin tanımlanması
- [x] Temel hareket komutlarının oluşturulması
- [x] Sağa ve sola dönüş işlemleri
- [x] Harita sınırı, engel ve robot çarpışma kontrolleri
- [x] Temel hareket ve dönüş testleri
- [x] Zeminlere özgü enerji tüketiminin uygulanması
- [x] Sıçrama, kayma ve savrulma davranışları
- [x] Komut dosyasının okunması ve sırayla uygulanması
- [x] Nihai simülasyon çıktılarının oluşturulması
- [x] Özgün robot ve zemin türlerinin eklenmesi
- [ ] Kapsamlı testler ve son düzenlemeler



## Gün 7 — Özgün gövde ve zemin

Özgün tasarım kararları aşağıda tanımlanmıştır. Bunlar standart zeminlerin şartnamedeki kurallarına ek olarak uygulanır.

- HIBRIT gövde hem sıçrayabilir hem kayabilir. Uçamaz. Kayayı 2 enerjiyle atlar ve kesintisiz buz üzerinde 1 enerjiyle kayar. Tozdan 2 enerjiyle geçer, metan gölüne giremez.
- B sembollü bataklıktan uçabilen gövdeler 1, diğer gövdeler 3 enerjiyle geçer. Bataklık sıçrama inişine uygun değildir. Kayma bataklığa geçmeden son buz hücresinde biter.
- Hibrit kaya üzerinden buza indiğinde sıçrama sonlanır. İniş kendi başına ikinci bir kayma başlatmaz. Sonraki adımda önündeki buz hücresine giriş kaymayı başlatır.
- Enerji yetmediğinde hareket gerçekleşmez ve robot ENERJISIZ olur. Engellenen sıçrama enerji harcamaz.

Gövde ve zemin sınıfları davranışlarını kendi sınıflarında tanımlar. Dosya tanımları GovdeFabrikasi ve ZeminFabrikasi kayıtlarında tutulur. Yeni türler eklendiğinde ilgili kayda eklenir; HaritaOkuyucu bu kayıtlardan doğrulama yapar.

Proje klasöründen örneği çalıştırmak için:

```powershell
dotnet run -- "TestData/harita_gun7.txt" "TestData/komutlar_gun7.txt"
```

Beş robot da teslim eder. HIBRIT, SICRAYAN, KIZAKLI, HAVALI ve PALETLI için kalan enerjiler sırasıyla 11, 8, 9, 12 ve 6 olur. Toplam kalan enerji 46'dır.

Standart haritanın üç referans senaryosu doğrulanmıştır: ILERI 11 için 2 teslim/43 enerji, beş komutlu örnek için 3 teslim/33 enerji ve dönüşlü örnek için 4 teslim/26 enerji.

*Arayüz, kapsamlı sınır durumu testleri, UML ve teslim raporu hazırlığı devam etmektedir.*

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

Gövde ve zemin sınıfları davranışlarını kendi sınıflarında tanımlar. Dosya tanımları GovdeFabrikasi ve ZeminFabrikasi kayıtlarında tutulur. Özgün türler kendi sınıflarındaki ModuleInitializer işaretli metotla kaydolur; HaritaOkuyucu bu kayıtlardan doğrulama yapar.

Proje klasöründen örneği çalıştırmak için:

```powershell
dotnet run -- "TestData/harita_gun7.txt" "TestData/komutlar_gun7.txt"
```

Beş robot da teslim eder. HIBRIT, SICRAYAN, KIZAKLI, HAVALI ve PALETLI için kalan enerjiler sırasıyla 11, 8, 9, 12 ve 6 olur. Toplam kalan enerji 46'dır.

Standart haritanın üç referans senaryosu doğrulanmıştır: ILERI 11 için 2 teslim/43 enerji, beş komutlu örnek için 3 teslim/33 enerji ve dönüşlü örnek için 4 teslim/26 enerji.

## Gün 8 — Konsol görünümü ve giriş doğrulama

Koordinatlı konsol görünümü, başlangıçta ve her komut bütün robotlar tarafından tamamlandığında haritayı gösterir. Robotlar numaralarıyla görünür. Alt bölümde gövde, konum, yön, enerji, durum ve robotun bulunduğu zemin listelenir. Teslim edilen robot haritadan kalkar; enerjisiz robot hücresinde görünmeye devam eder.

Varsayılan dosyalarla:

```powershell
dotnet run -- --goster
```

Özgün gövde ve zemin örneğiyle:

```powershell
dotnet run -- "TestData/harita_gun7.txt" "TestData/komutlar_gun7.txt" --goster
```

Dosya yolları proje klasörüne göre verilmiştir. --goster seçeneği son argüman olmalıdır. Görünüm, komut sonlarındaki durumları art arda yazar; tuşla bekleme veya animasyon içermez.

Görünüm Console.Error üzerinden, şartnamedeki robot sonuçları ve toplamlar Console.Out üzerinden yazılır. İkisi konsolda birlikte izlenebilir; standart çıktıya yönlendirilen sonuç dosyası yalnızca resmi sonuçları içerir. --goster verilmediğinde yalnızca sonuç yazılır. Hatalı girişte görünüm açılmadan hata bildirilir.

Harita satırlarından sonraki boş satırlar ve # ile başlayan açıklamalar kabul edilir. Diğer fazla veriler satır numarasıyla reddedilir. Haritada başlangıç konumu bulunmayan robot için tanım satırı raporlanır.

Gün 8 doğrulamasında 19 kontrol geçti: üç standart referans senaryo, özgün harita, görünümün sonuç çıktısını koruması, komut sonrası görüntüler, teslim ve enerjisiz robotların gösterimi, hatalı/eksik harita verileri, boş komutlar ve seçenek doğrulaması.

## Gün 9 — Tekrarlanabilir doğrulama

Tests/GezegenPostacilari.Dogrulama.csproj standart .NET sınıflarıyla çalışan bir C# konsol doğrulama projesidir. Visual Studio çözümüne eklenmiştir. Testleri proje klasöründen çalıştırın:

```powershell
dotnet run --project Tests/GezegenPostacilari.Dogrulama.csproj
```

Başarılı çalışmanın son satırı:

```text
SONUC: 122 başarılı, 0 başarısız.
```

Başarılı çalıştırma 0, herhangi bir testin başarısız olması 1 çıkış kodu verir. Her test adı ve hata ayrıntısı yazılır; başarısız bir test diğer senaryoların çalışmasını durdurmaz. Beklenen maliyetler uygulamadaki sabitlerden kopyalanmadan test tablosunda tanımlanmıştır.

| Test grubu | Senaryo sayısı |
|---|---:|
| Beş gövde ve sekiz zemin kombinasyonu | 40 |
| Dört yönde normal hareket, sıçrama ve kayma | 16 |
| Enerji, sınır, çarpışma, teslim ve özel hareketler | 34 |
| Harita ve komut dosyaları | 26 |
| Üç standart referans ve özgün harita | 4 |
| Görünümün durumu koruması ve komut sonrası bildirim | 2 |

İlk test koşusunda harita hücre satırlarının başında veya sonunda fazladan boşluk olduğunda geçersiz satırların kabul edildiği bulundu. HaritaOkuyucu artık hücre satırını kırpmadan genişlik ve sembol kontrolüne sokar. Başlıkların boşluk temizliği, boş satırların ve açıklamaların atlanması devam eder. Bu iki durum düzeltme sonrasında geçti.

Yorum gerektiren birleşik kural: mevcut uygulama kayanın üzerinden doğrudan H rampasına sıçramayı toplam 2 enerjiyle teslim olarak ele alır. Şartnamedeki standart örnekler bu birleşik durumun maliyetini doğrulamaz. İlgili iki test mevcut yorumun değişmeden kaldığını kontrol eder; öğretim görevlisinin farklı açıklaması olursa kural ve beklentiler birlikte güncellenmelidir. Hibritin buza sıçrayarak inişi aynı adımda ikinci bir kayma başlatmaz; bu, özgün gövde için belirlenen kuraldır.

Test projesi kendi örnek dosyalarını çıktı klasörüne alır. Geçici dosya testleri oluşturdukları dosyayı işlem sonunda siler. Ana projedeki Compile Remove ayarı test kaynaklarının ana uygulamaya derlenmesini engeller.

## Şartname denetimi sonrası düzenlemeler

Yeni gövde ve zemin eklemek için fabrika sınıflarındaki kaynak listelerini değiştirmek gerekmiyor. HibritGovde ve Bataklik, kendi dosyalarında bulunan ModuleInitializer işaretli statik KayitOl metotlarıyla kaydolur. C# çalışma zamanı bu metotları modül başlangıcında çağırır. Yeni bir alt sınıf aynı yöntemi kullanarak kendi adını veya sembolünü Kaydet metoduna iletebilir. Ana okuyucu, hareket motoru ve Program sınıfı yeni tür için değiştirilmez.

Fabrikalar boş adları, dosya biçimiyle çakışan sembolleri ve yinelenen kayıtları reddeder. Başarısız bir kayıt mevcut türün üzerine yazmaz. Kayıt erişimleri kilitle korunur. Gövde ve zeminler paylaşılan kural nesneleridir; robota özel değişken durum Robot içinde tutulmalıdır.

Konsoldaki zemin açıklamaları kayıtlı zeminlerden oluşturulur ve üçlü gruplar halinde yazılır. Yeni zemin eklendiğinde görünüm sınıfının açıklama listesine ayrıca satır eklenmez. Görünümde sınıf adı yalnızca açıklama etiketi olarak kullanılır; hareket davranışı seçmek için kullanılmaz.

Programın çıktı kodlaması UTF-8 olarak belirlenmiştir. Bu, özellikle yönlendirilmiş Türkçe hata mesajlarının doğru okunmasını sağlar.

Kayıt düzenlemesi sonrasında 145 senaryo başarıyla doğrulanmıştır. Gün 9'daki 122 senaryoya, kayıt sistemini sınayan 20 test ve ana programı ayrı süreçte çalıştıran 3 test eklenmiştir. DenemeGovdesi ve DenemeZemini yalnızca test projesinde bulunur. Bu dosyalar yeni türün otomatik kaydını, dosyadan okunmasını, mevcut gövde/zeminlerle çalışmasını ve görünümde gösterilmesini doğrular. Ayrı süreç testleri resmi çıktının tam metnini, hibrit/bataklık kaydını, görünüm açıkken standart çıktının korunmasını ve geçersiz komutta simülasyonun başlamamasını kontrol eder.

Rampaya doğrudan sıçramanın toplam maliyeti için proje klasöründeki belgelerde ek bir açıklama bulunmamıştır. Mevcut 2 enerji yorumu korunur; bu birleşimin resmi referansla uyumu henüz doğrulanmış değildir.

## Temiz derleme ve çalıştırma kontrolü

Güncel test sonucu 148 başarılı, 0 başarısızdır. Büyük fakat dosya içeriğiyle tutarsız genişlik/yükseklik tanımları için üç kontrol daha eklenmiştir. HaritaOkuyucu önce hücre satırlarını, robot tanımlarını ve dosyanın sonunu doğrular; iki boyutlu harita dizisini bu kontrollerden sonra oluşturur. Böylece kısa bir hatalı dosyanın aşırı büyük boyut bildirerek gereksiz dizi tahsisi başlatması önlenir.

TestData klasöründeki bütün TXT örnekleri hem derleme hem yayın çıktısına kopyalanır. Hibrit ve bataklık örneği de dağıtım klasöründen çalıştırılabilir.

Kaynak dosyalar eski bin/obj klasörleri olmadan ayrı bir klasöre kopyalanıp Release olarak derlenmiştir. Derleme 0 hata ve 0 uyarıyla tamamlanmış, 148 test bu kopyada da geçmiştir. Yayın çıktısından standart örnek 3 teslim/33 enerji, özgün örnek 5 teslim/46 enerji üretmiştir. --goster ile başlangıç ve komut sonrası haritalar kontrol edilmiştir. Bu yayın biçimi çalıştırılan bilgisayarda .NET 8 çalışma ortamı gerektirir.

*UML ve IEEE teslim raporu sonraki aşamaya bırakılmıştır. Test sayısı bütün olası haritaların doğrulandığı anlamına gelmez.*

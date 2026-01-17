using System;
using System.Collections.Generic;
using System.Linq;
#nullable disable
namespace KutuphaneProjesi
{
    class Program
    {
        static List<Kitap> kitaplar = new List<Kitap>();
        static List<Uye> uyeler = new List<Uye>();
        static List<OduncIslem> oduncListesi = new List<OduncIslem>();

        static void Main(string[] args)
        {
            VeriYukle();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("   KÜTÜPHANE SİSTEMİ   ");
                Console.WriteLine("1. Kitap İşlemleri");
                Console.WriteLine("2. Üye İşlemleri");
                Console.WriteLine("3. Emanet Verme");
                Console.WriteLine("4. Emanet Geri Alma");
                Console.WriteLine("5. Kayıp Kitap İşlemi");
                Console.WriteLine("6. Raporlar");
                Console.WriteLine("7. Genel İstatistikler");
                Console.WriteLine("0. Çıkış");
                Console.WriteLine("----------------------------------------");
                Console.Write("Yapmak istediğiniz işlem: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1": KitaplariListele(); break;
                    case "2": UyeleriListele(); break;
                    case "3": KitapVer(); break;
                    case "4": KitapAl(); break;
                    case "5": KayipKitapIslemi(); break;
                    case "6": DetayliRaporlar(); break;
                    case "7": GenelIstatistikler(); break;
                    case "0":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Lütfen geçerli bir seçim yapınız!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void VeriYukle()
        {
            kitaplar.Add(new Kitap("Suç ve Ceza", "Dostoyevski", "Can Y.", 1866, "101", "Roman", 150));
            kitaplar.Add(new Kitap("Sefiller", "Victor Hugo", "İş Bankası", 1862, "102", "Roman", 200));
            kitaplar.Add(new Kitap("Nutuk", "M.Kemal Atatürk", "Alfa", 1927, "103", "Tarih", 250));
            kitaplar.Add(new Kitap("Şeker Portakalı", "Jose Mauro", "Can Y.", 1968, "104", "Roman", 120));
            kitaplar.Add(new Kitap("1984", "George Orwell", "İş Bankası", 1949, "105", "Roman", 130));
            kitaplar.Add(new Kitap("Hayvan Çiftliği", "George Orwell", "Can Yayınları", 1945, "106", "Roman", 110));
            kitaplar.Add(new Kitap("Simyacı", "Paulo Coelho", "April Yayınları", 1988, "107", "Roman", 100));
            kitaplar.Add(new Kitap("Tutunamayanlar", "Oğuz Atay", "İletişim", 1972, "108", "Roman", 180));
            kitaplar.Add(new Kitap("Beyaz Diş", "Jack London", "İş Bankası", 1906, "109", "Roman", 140));
            kitaplar.Add(new Kitap("Martin Eden", "Jack London", "İş Bankası", 1909, "110", "Roman", 160));

            uyeler.Add(new Uye("Ahmet", "Yılmaz", "1001", "5551112233", "ahmet@okul.edu.tr"));
            uyeler.Add(new Uye("Ayşe", "Kaya", "1002", "5554445566", "ayse@okul.edu.tr"));
            uyeler.Add(new Uye("Mehmet", "Demir", "1003", "5557778899", "mehmet@okul.edu.tr"));
            uyeler.Add(new Uye("Mehmet", "Kaya", "1004", "05551234503", "mehmet.kaya@mail.com"));
            uyeler.Add(new Uye("Zeynep", "Çelik", "1005", "05551234504", "zeynep.celik@mail.com"));
            uyeler.Add(new Uye("Ahmet", "Şahin", "1006", "05551234505", "ahmet.sahin@mail.com"));
            uyeler.Add(new Uye("Elif", "Koç", "1007", "05551234506", "elif.koc@mail.com"));
            uyeler.Add(new Uye("Murat", "Aydın", "1008", "05551234507", "murat.aydin@mail.com"));
            uyeler.Add(new Uye("Fatma", "Arslan", "1009", "05551234508", "fatma.arslan@mail.com"));
        }

        static void KitaplariListele()
        {
            Console.Clear();
            Console.WriteLine("KÜTÜPHANE KİTAP LİSTESİ");
            Console.WriteLine(String.Format("{0,-20} {1,-15} {2,-10} {3,-10}", "Kitap Adı", "Yazar", "ISBN", "Durum"));
            Console.WriteLine("------------------------------------------------------------");

            foreach (var k in kitaplar)
            {
                string durum = k.MevcutMu ? "Rafta" : "Ödünçte";
                Console.WriteLine(String.Format("{0,-20} {1,-15} {2,-10} {3,-10}", k.Ad, k.Yazar, k.ISBN, durum));
            }
            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
        }

        static void UyeleriListele()
        {
            Console.Clear();
            Console.WriteLine("ÜYE LİSTESİ");
            foreach (var u in uyeler)
            {
                Console.WriteLine($"No: {u.OgrNo} | {u.Ad} {u.Soyad} | Ceza: {u.Borc} TL");
            }
            Console.ReadKey();
        }

        static void KitapVer()
        {
            Console.Clear();
            Console.WriteLine("KİTAP ÖDÜNÇ VERME EKRANI");

            Console.Write("Kitap ISBN Numarası: ");
            string isbn = Console.ReadLine();

            Kitap k = kitaplar.FirstOrDefault(x => x.ISBN == isbn);

            if (k == null)
            {
                Console.WriteLine("HATA: Böyle bir kitap bulunamadı!");
                Console.ReadKey(); return;
            }
            if (k.MevcutMu == false)
            {
                Console.WriteLine("HATA: Bu kitap şu an başkasında!");
                Console.ReadKey(); return;
            }

            Console.Write("Öğrenci Numarası: ");
            string ogrNo = Console.ReadLine();
            Uye u = uyeler.FirstOrDefault(x => x.OgrNo == ogrNo);

            if (u == null)
            {
                Console.WriteLine("HATA: Üye bulunamadı!");
                Console.ReadKey(); return;
            }

            DateTime bugun = DateTime.Now;

            OduncIslem yeniIslem = new OduncIslem(k, u, bugun);
            oduncListesi.Add(yeniIslem);

            k.MevcutMu = false;
            k.OkunmaSayisi++;
            u.OkuduguKitapSayisi++;

            Console.WriteLine($"\nİşlem Başarılı! '{k.Ad}' kitabı '{u.Ad}' adlı üyeye verildi.");
            Console.WriteLine($"Son Teslim Tarihi: {yeniIslem.PlanlananIadeTarihi.ToShortDateString()}");
            Console.ReadKey();
        }

        static void KitapAl()
        {
            Console.Clear();
            Console.WriteLine("KİTAP İADE ALMA EKRANI");

            Console.Write("İade edilecek kitabın ISBN numarası: ");
            string isbn = Console.ReadLine();

            var islem = oduncListesi.FirstOrDefault(x => x.KitapBilgisi.ISBN == isbn && x.IadeEdildiMi == false);

            if (islem == null)
            {
                Console.WriteLine("Bu kitap için aktif bir ödünç kaydı bulunamadı.");
                Console.ReadKey(); return;
            }

            DateTime iadeTarihi = DateTime.Now;

            int gecikmeGun = (iadeTarihi - islem.PlanlananIadeTarihi).Days;
            decimal ceza = 0;

            if (gecikmeGun > 0)
            {
                ceza = gecikmeGun * 5;
                islem.UyeBilgisi.Borc += ceza;
                Console.WriteLine($"UYARI: Kitap {gecikmeGun} gün gecikmiş!");
                Console.WriteLine($"Üyeye {ceza} TL ceza yansıtıldı.");
            }

            islem.IadeEdildiMi = true;
            islem.GercekleseneIadeTarihi = iadeTarihi;
            islem.KitapBilgisi.MevcutMu = true;

            Console.WriteLine("\nKitap başarıyla iade alındı.");
            Console.ReadKey();
        }

        static void KayipKitapIslemi()
        {
            Console.Clear();
            Console.WriteLine("KAYIP KİTAP BİLDİRİMİ");
            Console.Write("Kayıp kitabın ISBN numarası: ");
            string isbn = Console.ReadLine();

            var islem = oduncListesi.FirstOrDefault(x => x.KitapBilgisi.ISBN == isbn && x.IadeEdildiMi == false);

            if (islem == null)
            {
                Console.WriteLine("Bu kitap zaten rafta veya kaydı yok.");
                Console.ReadKey(); return;
            }

            decimal kitapBedeli = islem.KitapBilgisi.Fiyat;
            islem.UyeBilgisi.Borc += kitapBedeli;

            islem.IadeEdildiMi = true;
            islem.KitapBilgisi.MevcutMu = false;

            Console.WriteLine($"\nKitabın bedeli ({kitapBedeli} TL) üye hesabına ceza olarak işlendi.");
            Console.ReadKey();
        }

        static void DetayliRaporlar()
        {
            Console.Clear();
            Console.WriteLine("RAPORLAR");

            var gecikenler = oduncListesi.Where(x => !x.IadeEdildiMi && DateTime.Now > x.PlanlananIadeTarihi).ToList();

            if (gecikenler.Count == 0) Console.WriteLine("- Geciken kitap yok.");

            foreach (var g in gecikenler)
            {
                Console.WriteLine($"   -> {g.KitapBilgisi.Ad} (Üye: {g.UyeBilgisi.Ad} {g.UyeBilgisi.Soyad})");
            }

            var enAktifUye = uyeler.OrderByDescending(x => x.OkuduguKitapSayisi).FirstOrDefault();

            if (enAktifUye != null)
                Console.WriteLine($"   -> {enAktifUye.Ad} {enAktifUye.Soyad} ({enAktifUye.OkuduguKitapSayisi} kitap)");

            var populerKitap = kitaplar.OrderByDescending(x => x.OkunmaSayisi).FirstOrDefault();

            if (populerKitap != null)
                Console.WriteLine($"   -> {populerKitap.Ad} ({populerKitap.OkunmaSayisi} kez)");

            Console.ReadKey();
        }

        static void GenelIstatistikler()
        {
            Console.Clear();
            Console.WriteLine("GENEL İSTATİSTİKLER");

            Console.WriteLine($"Toplam Kitap Sayısı       : {kitaplar.Count}");
            Console.WriteLine($"Raftaki Mevcut Kitap      : {kitaplar.Count(x => x.MevcutMu)}");
            Console.WriteLine($"Toplam Üye Sayısı         : {uyeler.Count}");
            Console.WriteLine($"Ceza/Borcu Olan Üye Sayısı: {uyeler.Count(x => x.Borc > 0)}");
            Console.WriteLine($"Şu An Dışarıdaki Kitaplar : {oduncListesi.Count(x => !x.IadeEdildiMi)}");

            Console.ReadKey();
        }
    }

    class Kitap
    {
        public string Ad { get; set; }
        public string Yazar { get; set; }
        public string Yayinevi { get; set; }
        public int Yil { get; set; }
        public string ISBN { get; set; }
        public string Kategori { get; set; }
        public decimal Fiyat { get; set; }
        public bool MevcutMu { get; set; } = true;
        public int OkunmaSayisi { get; set; } = 0;

        public Kitap(string ad, string yazar, string yayin, int yil, string isbn, string tur, decimal fiyat)
        {
            Ad = ad; Yazar = yazar; Yayinevi = yayin; Yil = yil; ISBN = isbn; Kategori = tur; Fiyat = fiyat;
        }
    }

    class Uye
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string OgrNo { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public decimal Borc { get; set; } = 0;
        public int OkuduguKitapSayisi { get; set; } = 0;

        public Uye(string ad, string soyad, string no, string tel, string mail)
        {
            Ad = ad; Soyad = soyad; OgrNo = no; Tel = tel; Email = mail;
        }
    }

    class OduncIslem
    {
        public Kitap KitapBilgisi { get; set; }
        public Uye UyeBilgisi { get; set; }
        public DateTime VerilisTarihi { get; set; }
        public DateTime PlanlananIadeTarihi { get; set; }
        public DateTime? GercekleseneIadeTarihi { get; set; }
        public bool IadeEdildiMi { get; set; } = false;

        public OduncIslem(Kitap k, Uye u, DateTime verilis)
        {
            KitapBilgisi = k;
            UyeBilgisi = u;
            VerilisTarihi = verilis;
            PlanlananIadeTarihi = verilis.AddDays(5);
        }
    }
}
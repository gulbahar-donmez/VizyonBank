
# Vizyon Bank

VizyonBank, finansal işlemleri yönetmek için geliştirilmiş bir bankacılık uygulamasıdır. Bu proje, kullanıcıların hesaplarını yönetmelerine, para transferi yapmalarına, hesap bakiyelerini kontrol etmelerine ve daha birçok bankacılık işlemini gerçekleştirmelerine olanak tanır.




## Özellikler

- Kullanıcı kayıt ve giriş işlemleri
- Hesap bakiyesini görüntüleme
- Para transferi yapma
- Hesap hareketlerini görüntüleme
- Fatura ödeme işlemleri
- Güvenli ve kullanıcı dostu arayüz




 

## Kullanılan Teknolojiler

**İstemci:** C#, T-SQL

**Ide:** Microsoft Visual Studio, Microsoft Server Management Studio 
 
## XML Kullanımı

#### Tüm öğeleri getir

```http
 https://www.tcmb.gov.tr/kurlar/today.xml
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `/Currency[@Kod='USD']/BanknoteBuying` | `string` | **Güncel kur** Alış değeri |
| `/Currency[@Kod='USD']/BanknoteSelling` | `string` | **Güncel kur** Satış değeri|   



  
## Bilgisayarınızda Çalıştırın

Projeyi klonlayın

```bash
  git clone https://github.com/gulbahar-donmez/VizyonBank.git
```

Sunucuyu çalıştırın

```bash
  bankaDb.rar dosyasını açın ve restore backup ile kurun
```

  
## Yazarlar ve Teşekkür

- [@uekapps](https://www.github.com/uekapps) tasarım ve geliştirme için.

  

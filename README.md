# HAZAR API

Bu proje, gelişmiş arama özelliklerine sahip bir ürün yönetim sistemi API'sini içermektedir. .NET Core ve çeşitli teknolojilerle geliştirilmiş olup, ElasticSearch, Redis, SignalR gibi araçlarla performans artırımı, gerçek zamanlı bildirimler ve cache yönetimi sağlanmıştır.

## Proje İçeriği

- **Ürün Yönetimi:** Ürün ekleme, güncelleme, silme ve listeleme işlemleri.
- **Gelişmiş Arama:** ElasticSearch kullanarak gelişmiş arama ve filtreleme özellikleri.
- **Cache Yönetimi:** Redis ile ürünlerin cache'lenmesi ve cache temizleme işlemleri.
- **Sıralama ve Pagination:** Ürünlerin en son eklenenler önce olacak şekilde sıralanması ve sayfalama özelliği.
- **Background Service:** ElasticSearch için arka planda ürünlerin düzenli olarak indekslenmesini sağlayan background service.
- **Gerçek Zamanlı Bildirimler:** SignalR kullanarak kullanıcıya gerçek zamanlı bildirimler gönderme.

## Kullanılan Teknolojiler

- **.NET Core 8:** API geliştirme için kullanılan temel teknoloji.
- **ElasticSearch:** Ürünler üzerinde gelişmiş arama ve filtreleme işlemleri için kullanılır.
- **Redis:** Ürün verilerinin cache'lenmesi ve hızlı erişim için kullanılır.
- **MediatR:** CQRS ve MediatR desenleri kullanılarak sorumlulukların ayrılması sağlanır.
- **ValidationBehavior :** 
- **Entity Framework Core:** Veritabanı işlemleri için ORM aracı.
- **PostgreSQL:** Veritabanı yönetim sistemi.
- **SignalR:** Gerçek zamanlı bildirimler için kullanılır.
- **Background Services:** Arka planda sürekli çalışan servisler ile veri indeksleme gibi işlemler yürütülür.
- **Notification:**
- **EmailService:**
- **LogService:**
- **TokenBlacklistService:**
- **Hangfire:** 
- **EventHandlers:** 
- **Seq:**
- **Health Checks:**

## Kurulum ve Çalıştırma

### Gerekli Bağımlılıklar

- .NET 8 SDK
- PostgreSQL
- Redis
- ElasticSearch

### Adımlar

1. **Projeyi Klonlayın:**
    ```bash
    git clone https://github.com/unalhazar/HAZAR-API.git
    cd HAZAR-API
    ```

2. **Veritabanını Konfigüre Edin:**
   `appsettings.json` dosyasında PostgreSQL bağlantı dizesini yapılandırın:
   ```json
   "ConnectionStrings": {
       "Default": "Host=localhost;Port=5432;Database=HazarDB;Username=postgres;Password=yourpassword"
   }
   ```

3. **Redis ve ElasticSearch Bağlantılarını Yapılandırın:**
   `appsettings.json` dosyasında Redis ve ElasticSearch bağlantı ayarlarını yapılandırın:
   ```json
   "Redis": {
       "ConnectionString": "localhost:6379,password=myStrongPassword"
   },
   "ElasticSearch": {
       "ConnectionString": "http://localhost:9200"
   }
   ```

4. **Bağımlılıkları Yükleyin:**
   ```bash
   dotnet restore
   ```

5. **Veritabanını Güncelleyin:**
   ```bash
   dotnet ef database update
   ```

6. **Projeyi Çalıştırın:**
   ```bash
   dotnet run
   ```

## Özellikler

### Ürün Listeleme ve Cacheleme

- Ürünler, sayfalama ve sıralama işlemleriyle liste halinde sunulur.
- Veriler Redis üzerinde cache'lenir. Cache süresi 30 dakika olarak ayarlanmıştır.
- Yeni bir ürün eklendiğinde, ilgili cache temizlenir. Ancak yalnızca ilgili sayfanın cache’i temizlenir, diğer sayfalar korunur.

### Gelişmiş Arama

- **ElasticSearch:** Ürünler üzerinde isim, fiyat aralığı, stok miktarı gibi kriterlerle gelişmiş arama yapılabilir.
- Arama işlemleri için ElasticSearch’te indeksleme yapılır.

### Background Service (Arka Plan Servisi)

- **ProductElasticIndexerBackgroundService:** Bu background service, ElasticSearch üzerinde ürünlerin düzenli olarak indekslenmesini sağlar. Servis, her 3 dakikada bir çalışarak veritabanındaki ürünleri ElasticSearch'e indeksler.

### Gerçek Zamanlı Bildirimler

- **SignalR:** Kullanıcıya ürün ekleme, güncelleme veya silme gibi işlemler hakkında anında bildirim gönderir.

### Kullanım Örnekleri

#### Ürün Listeleme (GET)

```http
GET /api/product?PageNumber=1&PageSize=10
```

#### Ürün Ekleme (POST)

```http
POST /api/product
Content-Type: application/json

{
  "name": "Yeni Ürün",
  "price": 120.0,
  "stock": 50,
  "categoryId": 1,
  "brandId": 2
}
```

#### ElasticSearch ile Ürün Arama (GET)

```http
GET /api/product/search-elasticsearch?query=elektronik
```

### Gerçek Zamanlı Bildirim (SignalR)

```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceiveNotification", (user, message) => {
    console.log(`${user}: ${message}`);
});

connection.start().catch(err => console.error(err.toString()));
```
## Katkıda Bulunma

Katkıda bulunmak isterseniz, lütfen bir pull request oluşturun veya bir issue açın.

## Lisans

Bu proje MIT lisansı ile lisanslanmıştır.

---

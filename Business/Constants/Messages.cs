using Entities.Concrete;

namespace Business.Constants;

public static class Messages
{
    public static string UserAdded= "Kullanıcı eklendi";
    public static string ProductDetailsListed= "Ürün detayları listelendi";
    public static string ProductAdded = "Ürün eklendi";
    public static string ProductNameInvalid = "Ürün ismi geçersiz";
    public static string ProductsListed = "Ürünler listelendi";
    public static string MaintenanceTime = "Sistem bakımda";
    public static string ProductCountOfCategoryError=" Bir kategoride en fazla 10 ürün olabilir";
    public static string ProductNameAlreadyExists="Bu isimde zaten başka bir ürün var";
    public static string CategoryLimitExceded="Kategori limiti aşıldığı için yeni ürün eklenemiyor";
    public static string AuthorizationDenied="Yetkiniz yok";
    public static string UserRegistered="Kullanıcı kayıt oldu";
    public static string UserNotFound="Kullanıcı bulunamadı";
    public static string PasswordError="Parola hatası";
    public static string SuccessfulLogin="Sisteme giriş başarılı";
    public static string UserAlreadyExists="Bu kullanıcı zaten mevcut";
    public static string AccessTokenCreated="Access token oluşturuldu";
    public static string ProductUpdated = "Ürün güncellendi";
    public static string ProductNotFound="Ürün bulunamadı";
}
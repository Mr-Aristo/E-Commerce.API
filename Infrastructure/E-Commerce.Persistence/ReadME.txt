-Bu katmandaki Concrede yani serviceler sadece veri tabani islemeleri icindir.
-Application katmanini ref eder.
-Context sinifi burada bulunur

-Ayrica serviceRegistration icinde context tanimlamasi ve DI tanimlamalri yapilir. 

MIGRATION HATA: Consol (dotnet cli) uzerinden migration yapmak istendiginde hata alina bilir. ClassLibraryde Context clasiminizn
contstructure na ne gidecegini bilmedigimizden hata veriyor. DesignTimeFactory olusturursak hata giderir. Migration islemeleri PackageMAnager 
uzerinden de yapilabilir fakat eger Visual Studio da calismiyorsak konsol gerekli oluyor.
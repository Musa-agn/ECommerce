Bu proje ile; 
	ürün oluþturabilirsiniz,
	ürün bilgisini alabilirsiniz,
	kampanya oluþturabilirsiniz,
	kampanya bilgisni alabilirsiniz,
	sipariþ oluþturabilirsiniz,
	zamaný arttýrabilirsiniz.

Kampanya Algoritmasý
Saat arttýkça kampanya algoritmam çalýþýr.
Kampanyalý ürünün satýlýp satýlmamasý durumuna göre ürünümün fiyatýný arttýrýyorum veya azaltýyorum.
Not: Ürünümün orjinal fiyatýný bozmuyorum. 

Tanýmlý Komutlar
create_product
create_campaign
create_order
get_product_info
get_campaign_info
increase_time


Solutionumun üzerindeki projeler:
ECommerceUI/ECommerce.Scenario  
Test/ECommerce.ApiTest
Test/ECommerce.BusinessTest
ECommerce.API
ECommerce.BusinessLayer 
ECommerce.Data


1)ECommerceUI/ECommerce.Scenario  
	UI projem. Senaryo dosyalarýný okur ve iþler.
2)Test/ECommerce.ApiTest
	ECommerce.API projesinin testi için oluþturuldu. Bir test clientý oluþturur ve api testlerimi çalýþtýrýr.
3)Test/ECommerce.BusinessTest
	ECommerce.BusinessLayer projesinin testi için oluþturuldu. Birim testlerim bulunmakta.
4)ECommerce.API
	Atýlacak istekleri karþýlayacak API projem. Buradaki controller methodlarý üzerinden Eccommerce.BusinessLayer daki servisler çaðýrýlýr.
5)ECommerce.BusinessLayer 
	Gerekli kontrollerin iþlerin yapýldýðý katman.
6)ECommerce.Data
	Veritabaný iþlemlerinin yapýldýðý katman


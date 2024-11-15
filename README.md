This project is a comprehensive News Management System developed with ASP.NET Core. It provides a platform where users can manage news content, add comments, reply to these comments, and publish content across various channels.

News Features:

  
  News Management: Users can add, edit, and delete news. News can be categorized, and category change requests can be submitted.
  
  Publishing and Unpublishing: Authors or editors can publish or unpublish news articles.
  
  Comments and Replies: Users can add comments under news articles, reply to them, and perform these actions through an AJAX-based system.
  
  Like/Dislike System: Users can like or dislike a comment only once, preventing multiple votes for the same comment.
  
  Comment Reporting and Removal: If inappropriate content is found in comments, users can report them, and the comments can be removed.
  
Channel Features:

   
   Channel Management: Users can create, manage, and delete channels.
  
   Membership Requests: Non-members can send channel membership requests, which can be approved or rejected by channel admins.
  
   Notifications: Instant notifications are sent to users regarding membership requests and channel activities.
  	
SuperAdmin Features:

   
   User Role Management: The SuperAdmin can manage and update user roles.
  
   Category Change Requests: The SuperAdmin can review and approve news category change requests.
  	
   Request Management: User-submitted requests can be evaluated and processed through the SuperAdmin panel.

Technical Details:  
   
   
   Technologies: The project was developed using ASP.NET Core, Entity Framework Core, AJAX, and Razor Pages.
  	
   Design Principles: Developed following SOLID principles. A layered architecture ensures clear separation between business logic, data access, and user interface layers.
  	
   Security: User authentication and authorization are handled through ASP.NET Core Identity. Features such as email verification and password reset are integrated.
  	
   Notifications: The system informs users of events like new comments on news or membership requests via email and in-system notifications.

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Bu proje, ASP.NET Core ile geliştirilen kapsamlı bir Haber Yönetim Sistemi'dir. Sistem, kullanıcıların haber içeriklerini yönetebileceği, yorum yapabileceği, bu yorumlara cevap verebileceği ve çeşitli kanallar üzerinden içerik yayınlayabileceği bir platform sunmaktadır.

Haber Özellikleri:
 
 Haber Yönetimi: Kullanıcılar haber ekleyebilir, düzenleyebilir ve silebilir. Haberler, çeşitli kategorilere ayrılabilir ve kategoriler arası geçiş talepleri yapılabilir.
	
 Yayınlama ve Yayından Kaldırma: Yazarlar veya editörler, haberleri yayınlayabilir veya yayından kaldırabilir.

 Yorum ve Yanıtlar: Kullanıcılar haberlerin altına yorum ekleyebilir, bu yorumlara yanıt verebilir ve AJAX tabanlı bir sistemle bu işlemleri gerçekleştirebilir.

 Beğeni/Beğenmeme Sistemi: Kullanıcılar, bir yorumu yalnızca bir kez beğenebilir veya beğenmeyebilir, bu sayede aynı yorum için çoklu oy kullanımı engellenir.

 Yorum Uyarıları ve Kaldırma: Yorumlarda uygunsuz içerik bulunması durumunda, kullanıcılar bu yorumları bildirebilir ve yorumlar kaldırılabilir.

Kanal Özellikleri:
 
 Kanal Yönetimi: Kullanıcılar, kanallar oluşturabilir, bu kanalları yönetebilir ve silebilir.

 Üyelik Talepleri: Üye olmayan kullanıcılar, kanal üyeliği talebinde bulunabilir. Bu talepler, kanal yöneticileri tarafından onaylanabilir veya reddedilebilir.

 Bildirimler: Üyelik talepleri ve kanal aktiviteleri ile ilgili bildirimler, kullanıcılara anında iletilir.

SuperAdmin Özellikleri:
 
 Kullanıcı Rol Yönetimi: SuperAdmin, kullanıcıların rollerini yönetebilir ve güncelleyebilir.

 Kategori Değişiklik Talepleri: SuperAdmin, haber kategorisi değişiklik taleplerini inceleyip onaylayabilir.

 İstek Yönetimi: Kullanıcılardan gelen talepler, SuperAdmin paneli üzerinden değerlendirilir ve işlem yapılır.

Teknik Detaylar:

 Teknolojiler: Proje, ASP.NET Core, Entity Framework Core, AJAX ve Razor Pages teknolojilerini kullanarak geliştirilmiştir.

 Tasarım Prensipleri: SOLID prensiplerine uygun bir şekilde geliştirilmiştir. Katmanlı mimari, iş mantığı, veri erişim ve kullanıcı arayüzü katmanları arasında net bir ayrım sağlar.

 Güvenlik: Projede kullanıcı kimlik doğrulama ve yetkilendirme işlemleri ASP.NET Core Identity ile sağlanmıştır. Kullanıcılar için email doğrulama ve parola sıfırlama gibi özellikler entegre edilmiştir.

 Bildirimler: Sistem, kullanıcıları haberlerdeki yeni yorumlar, üyelik talepleri gibi durumlar hakkında bilgilendirmek için e-posta ve sistem içi bildirimler sağlar.

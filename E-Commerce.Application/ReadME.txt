Bu kataman sadece interfaceler icindir. 
Burada veri tabani CRUD ve diger islemleri iceren
interfaceler bulur. Bu katmani uygulayan diger katman
Persistance katmanidir. Persistance katamnindaki servisler
veri tabani islemleri icindir. Context bu katamnada bulunur.

Not: Single responsibility uymak icin IRep.. de read ve write i ayirdik. Genellikle Repo patern SOLID e aykiri olarak kabul edilir. 

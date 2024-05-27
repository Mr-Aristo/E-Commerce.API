using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.RequestParameters
{
    /// <summary>
    /// Bu class pageleme islemleri icin olusturuldu.
    /// Yani butun veriyi siteye yukleyip pagelemek yerine
    /// secili olan araliga gore sorgu islemi yapilaccak. 
    /// Bu gelecek veriyi karsilayacak olan yapidir.
    /// </summary>
    public record Pagination
    {
        /*Default degerler eger bir deger gelemez ise kullanilacak degerlerdir.*/
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;

    }
}

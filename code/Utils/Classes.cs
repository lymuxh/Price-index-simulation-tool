using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZI.Utils
{
    public class Category
    {
        public Category()
        {
            ID = 0;
            Name = "";
        }

        public long ID { get; set; }
        public string Name { get; set; }   
    }
    public class SubCategory
    {
        public SubCategory()
        {
            CategoryID ="";
            SubcategoryCode = "";
            Name = "";
        }

        public string  CategoryID { get; set; }
        public string SubcategoryCode { get; set; }
        public string Name { get; set; }
    }
    public class Brand
    {
        public Brand()
        {
            CategoryID = 0;
            SubcategoryCode = 0;
            BrandCode = 0;
            Name = "";
        }
        public long CategoryID { get; set; }
        public long SubcategoryCode { get; set; }
        public long BrandCode { get; set; }
        public string Name { get; set; }

    }
    public class SubCategoryElem
    {
        public SubCategoryElem()
        {
            PinYChar = new List<string>();
            ListSubcategory = new List<SubCategory>();
        }
        public List<string> PinYChar {get;set;}
        public List<SubCategory> ListSubcategory { get; set; }
    }
}

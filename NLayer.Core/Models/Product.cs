﻿namespace NLayer.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        //Dikkat edersek Products ve productfeature arasındaki ilişki one-to-one olduğundan tek bir nesne belittik.
        public ProductFeature ProductFeature { get; set; }

    }
}

using System.Collections.Generic;
using System.Linq;

namespace API.Entities
{
    // [Table("Baskets")]
    public class Basket
    {
        public int Id { get; set; }
        public string CostumerId { get; set; }
        public List<BasketItem> Items { get; set; } = new();


        //if there is a product quant increase if not, the prod is added 
        public void AddItem(Product product, int quantity)
        {
            if (Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem { Product = product, Quantity = quantity });
            }
            //adjust the quantity
            var existingItem = Items.FirstOrDefault( item => item.ProductId == product.Id);
            if(existingItem != null) existingItem.Quantity += quantity;
        }
        /*
        public int BasketId { get; set; }
        public Basket Basket { get; set; }*/

        //remove item
        public void RemoveItem(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if(item == null) return;
            item.Quantity -= quantity;
            if(item.Quantity == 0) Items.Remove(item);
        }
    }
}
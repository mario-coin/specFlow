using NUnit.Framework;
using TechTalk.SpecFlow;
using ui.test.Drivers;
using ui.test.Pages;

namespace ui.test.Steps
{
    [Binding]
    class Desafio : Browser
    {
        private InventoryPage inventoryPage = new InventoryPage();
        private InventoryItemPage inventoryItemPage = new InventoryItemPage();

        [Then(@"cada produto estará com o valor da página de inventário igual na sua página de item de inventário")]
        public void EntaoCadaProdutoEstaraComOValorDaPaginaDeInventarioIgualNaSuaPaginaDeItemDeInventario()
        {
            var products = inventoryPage.getInventoryItemCardPrice();
            foreach(var product in products)
            {
                inventoryPage.getProduto(product.Name);
                string inventoryItemPrice = inventoryItemPage.getProductPrice();
                Assert.That(product.Price, Is.EqualTo(inventoryItemPrice));
                inventoryItemPage.backToInventoryClick();
            }
        }

    }
}

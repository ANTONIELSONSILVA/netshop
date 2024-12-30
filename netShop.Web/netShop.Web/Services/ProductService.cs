using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using netShop.Web.Models;
using netShop.Web.Services.Contracts;

namespace netShop.Web.Services;

public class ProductService : IProductServices
{

    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/Products";
    private readonly JsonSerializerOptions _options;
    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> productsVM;


    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                // Pegue o Stream diretamente
                var stream = await response.Content.ReadAsStreamAsync();

                productsVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(stream, _options);
            }
            else
            {
                return null;
            }
        }

        return productsVM;
    }


    public async Task<ProductViewModel> FindProductById(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync($"{apiEndpoint}/{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                // Pegue o Stream diretamente do conteúdo da resposta
                var stream = await response.Content.ReadAsStreamAsync();

                // Deserializa diretamente do Stream
                productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(stream, _options);
            }
            else
            {
                return null;
            }
        }

        return productVM;
    }


    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        // Corrigir `Encoding.UTF8` com a classe estática `Encoding`
        StringContent content = new StringContent(JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                // Corrigir para usar um Stream no `DeserializeAsync`
                var stream = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(stream, _options);
            }
            else
            {
                return null;
            }
        }

        return productVM;
    }


    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productUpdated = new ProductViewModel();

        // `PutAsJsonAsync` está disponível no pacote Microsoft.AspNet.WebApi.Client
        using (var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
        {
            if (response.IsSuccessStatusCode)
            {
                // Usar ReadAsStreamAsync para obter o Stream
                var stream = await response.Content.ReadAsStreamAsync();

                // Deserializar do Stream
                productUpdated = await JsonSerializer.DeserializeAsync<ProductViewModel>(stream, _options);
            }
            else
            {
                return null;
            }
        }

        return productUpdated;
    }


    public async Task<bool> DeleteProductById(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        //using (var response = await client.DeleteAsync(apiEndpoint + id))
        using (var response = await client.DeleteAsync($"{apiEndpoint}/{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }


}

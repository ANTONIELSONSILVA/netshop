using System.Text.Json;
using netShop.Web.Models;
using netShop.Web.Services.Contracts;

namespace netShop.Web.Services;

public class CategoryService: ICategoryService
{

    private readonly IHttpClientFactory _ClientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/categories";


    public CategoryService(IHttpClientFactory clientFactory)
    {
        _ClientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }




    public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
    {
        var client = _ClientFactory.CreateClient("ProductApi");

        IEnumerable<CategoryViewModel> categories;

        var response = await client.GetAsync(apiEndpoint); // Substituído por 'client'

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStringAsync();
            categories = JsonSerializer.Deserialize<IEnumerable<CategoryViewModel>>(apiResponse, _options); // Correção do Deserialize
        }
        else
        {
            return null;
        }

        return categories;
    }













    //public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
    //{
    //    var client = _ClientFactory.CreateClient("ProductApi"); // Usa o HttpClient correto
    //    IEnumerable<CategoryViewModel> categories = null;

    //    var response = await client.GetAsync(apiEndpoint); // Substituído por 'client'

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var apiResponse = await response.Content.ReadAsStringAsync();
    //        categories = JsonSerializer.Deserialize<IEnumerable<CategoryViewModel>>(apiResponse); // Correção do Deserialize
    //    }
    //    else
    //    {
    //        return null;
    //    }

    //    return categories;
    //}







}

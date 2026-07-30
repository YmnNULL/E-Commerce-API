using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domin.Entities.Products;
using Microsoft.Extensions.Options;

namespace E_Commerce.Application.Profiles
{
    internal class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings urlSettings;

        public PictureUrlResolver(IOptions<UrlSettings> options)
        {
            this.urlSettings = options.Value;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = urlSettings.BaseUrl.Trim('/');
            var path = source.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{path}";
        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; }
    }

}

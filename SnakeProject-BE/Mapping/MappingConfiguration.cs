using SnakeProject.Application.DTOs.Products;
using SnakeProject.Domain.Entities;
namespace SnakeProject.API.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PsnCodeRequest, PsnCode>()
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.ProductId, src => src.ProductId)
                .Map(dest => dest.IsUsed, src => src.IsUsed)
                .Map(dest => dest.UsedAt, src => src.UsedAt)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Product);

        }
    }
}

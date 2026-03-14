using SnakeProject_BE.Contracts.Product;

namespace SnakeProject_BE.Mapping
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

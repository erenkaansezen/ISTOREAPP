using AutoMapper;
using ISTOREAPP.Data.Entities;
using ISTOREAPP.ViewModels;

namespace ISTOREAPP.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product,ProductViewModel>();
        }
    }
}

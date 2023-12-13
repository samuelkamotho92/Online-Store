using AutoMapper;


using Online_Store.Dtos;
using Online_Store.Models;

namespace Online_Store.Profiles
{
    public class onlineStoreProfile:Profile
    {
        public onlineStoreProfile() {

           CreateMap<AddProductDto, Products>().ReverseMap();
           CreateMap<AddOrderDto, Order>().ReverseMap();    
        }
    }
}

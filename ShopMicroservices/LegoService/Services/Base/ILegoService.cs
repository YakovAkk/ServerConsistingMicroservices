using LegoData.Data.Models;
using LegoService.DTOs;

namespace LegoService.Services.Base
{
    public interface ILegoService : IService<LegoModel, LegoModelDTO>
    {
    }
}

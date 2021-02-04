using ECommerce.BusinessLayer.Dto.Output;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IIncreaseTimeService
    {
        IncreaseTimeOutput IncreaseTime(int hour);
    }
}

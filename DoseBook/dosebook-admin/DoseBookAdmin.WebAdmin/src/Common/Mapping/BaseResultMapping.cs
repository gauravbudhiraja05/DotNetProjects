using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Entity.Global;

namespace DoseBookAdmin.WebAdmin.Common.Mapping
{
    public class BaseResultMapping
    {
        public BaseResultDto Entity2Dto(BaseResultEntity baseResultEntity)
        {
            BaseResultDto baseResultDto = new BaseResultDto()
            {
                IsSuccess = baseResultEntity.IsSuccess,
                Message = baseResultEntity.Message
            };

            return baseResultDto;
        }
    }
}

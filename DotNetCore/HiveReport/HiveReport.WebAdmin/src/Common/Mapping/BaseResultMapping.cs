using HiveReport.Dto.Common;
using HiveReport.Entity.Common;

namespace HiveReport.WebAdmin.Common.Mapping
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

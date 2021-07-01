using HiveReport.Dto.Common;
using HiveReport.Entity.Common;

namespace HiveReport.WebAdmin.Common.Mapping
{
    public class DeleteItemMapping
    {
        public DeleteItemEntity Dto2Entity(DeleteItemDto deleteItemDto)
        {
            DeleteItemEntity deleteItemEntity = new DeleteItemEntity()
            {
                ItemIds = deleteItemDto.ItemIds,
            };

            return deleteItemEntity;
        }
    }
}

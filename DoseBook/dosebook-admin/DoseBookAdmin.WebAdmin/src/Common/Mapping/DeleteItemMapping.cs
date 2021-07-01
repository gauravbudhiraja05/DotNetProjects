using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Entity.Global;
using System;

namespace DoseBookAdmin.WebAdmin.Common.Mapping
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

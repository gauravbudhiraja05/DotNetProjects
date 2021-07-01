using HiveReport.Dto.Common;
using HiveReport.Entity.Common;
using System;

namespace HiveReport.WebAdmin.Common.Mapping
{
    public class SharedLayoutMapping
    {
        public SharedLayoutDto Entity2Dto(SharedLayoutEntity sharedLayoutEntity)
        {
            return new SharedLayoutDto
            {
                UserId = sharedLayoutEntity.UserId,
                UserName = sharedLayoutEntity.UserName,
                UserAdminCheck = sharedLayoutEntity.UserAdminCheck,
                UserType = sharedLayoutEntity.UserType,
                DepartmentDtoList = DepartmentEntityList2DtoList(sharedLayoutEntity.DepartmentEntityList),
                ClientDtoList = ClientEntityList2DtoList(sharedLayoutEntity.ClientEntityList),
                LOBDtoList = LOBEntityList2DtoList(sharedLayoutEntity.LOBEntityList),
                LeftMenuDtoList = LeftMenuEntityList2DtoList(sharedLayoutEntity.LeftMenuEntityList),
                ParentNodeName = sharedLayoutEntity.ParentNodeName,
                IsDashboard = sharedLayoutEntity.IsDashboard,
                TopMenuDtoList = TopMenuEntityList2DtoList(sharedLayoutEntity.TopMenuEntityList),
            };
        }

        private static DepartmentDtoList DepartmentEntityList2DtoList(DepartmentEntityList departmentEntityList)
        {
            DepartmentDtoList departmentDtoList = new DepartmentDtoList();

            if (departmentEntityList != null)
            {
                departmentEntityList.ForEach(departmentEntity =>
                {
                    departmentDtoList.Add(new DepartmentDto
                    {
                        DepartmentId = departmentEntity.DepartmentId,
                        DepartmentName = departmentEntity.DepartmentName,
                        Message = departmentEntity.Message,
                    });
                });
            }
            return departmentDtoList;
        }

        private static ClientDtoList ClientEntityList2DtoList(ClientEntityList clientEntityList)
        {
            ClientDtoList clientDtoList = new ClientDtoList();

            if (clientEntityList != null)
            {
                clientEntityList.ForEach(clientEntity =>
                {
                    clientDtoList.Add(new ClientDto
                    {
                        ClientId = clientEntity.ClientId,
                        DepartmentId = clientEntity.DepartmentId,
                        ClientName = clientEntity.ClientName,
                        Message = clientEntity.Message,
                    });
                });
            }
            return clientDtoList;
        }

        private static LOBDtoList LOBEntityList2DtoList(LOBEntityList lobEntityList)
        {
            LOBDtoList lobDtoList = new LOBDtoList();

            if (lobEntityList != null)
            {
                lobEntityList.ForEach(lobEntity =>
                {
                    lobDtoList.Add(new LOBDto
                    {
                        LOBId = lobEntity.LOBId,
                        ClientId = lobEntity.ClientId,
                        DepartmentId = lobEntity.DepartmentId,
                        LOBName = lobEntity.LOBName,
                        Message = lobEntity.Message,
                    });
                });
            }
            return lobDtoList;
        }

        private static LeftMenuDtoList LeftMenuEntityList2DtoList(LeftMenuEntityList leftMenuEntityList)
        {
            LeftMenuDtoList leftMenuDtoList = new LeftMenuDtoList();

            if (leftMenuEntityList != null)
            {
                leftMenuEntityList.ForEach(leftMenuEntity =>
                {
                    leftMenuDtoList.Add(new LeftMenuDto
                    {
                        MenuDescription = leftMenuEntity.MenuDescription,
                        ToolTip = leftMenuEntity.ToolTip,
                        UrlLink = leftMenuEntity.UrlLink
                    });
                });
            }
            return leftMenuDtoList;
        }

        private static TopMenuDtoList TopMenuEntityList2DtoList(TopMenuEntityList menuEntityList)
        {
            TopMenuDtoList menuDtoList = new TopMenuDtoList();

            menuEntityList.ForEach(menuEntity =>
            {
                menuDtoList.Add(new TopMenuDto
                {
                    Name = menuEntity.Name,
                    Url = menuEntity.Url,
                    ToolTip = menuEntity.ToolTip,
                });
            });

            return menuDtoList;
        }
    }
}

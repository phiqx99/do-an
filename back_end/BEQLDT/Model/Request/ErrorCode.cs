using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BEQLDT.Model.Request
{
    public enum ErrorCode
    {
        //ALL
        ThereAreNoUsers = 6,
        DeleteFailed = 8,
        //User
        UserNotFound = 1,
        UserBlock = 2,
        DataInvalid = 3,
        NameAlreadyExists = 4,
        //Group
        GroupNotFound = 5,
        //Group User
        GroupUserNotFound = 7,
        //Permission
        PermissionNotFound = 9,
        //Role
        RoleNotFound = 10,
        ObjectExist = 11,
        UsernameDontChange =12,
        ObjectNotExits=13,
        ObjectDontChange=14
    }
}

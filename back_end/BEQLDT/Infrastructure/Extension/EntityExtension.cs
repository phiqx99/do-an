using BEQLDT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BEQLDT.Infrastructure.Extension
{
    public static class EntityExtension
    {
        public static void UpdateUserModel(this User user, UserViewModel userVM)
        {
            //user.Id = userVM.Id;
            user.Username = userVM.Username;
            user.Password = userVM.Password;
            user.Gender = userVM.Gender;
            user.FullName = userVM.FullName;
            user.Degree = userVM.Degree;
            user.Phone = userVM.Phone;
            user.Email = userVM.Email;
            user.Address = userVM.Address;
            user.DateOfBirth = userVM.DateOfBirth;
            user.Active = userVM.Active;
            user.CreatedAt = userVM.CreatedAt;
            user.UpdateAt = userVM.UpdateAt;
            user.CreatedUser = userVM.CreatedUser;
            user.UpdateUser = userVM.UpdateUser;
            user.Description = userVM.Description;
        }
        public static void UpdateRoleModel(this Role role, RoleViewModel roleVM)
        {
            role.NameRole = roleVM.NameRole;
            role.Description = roleVM.Description;
            role.Active = roleVM.Active;
            role.CreatedAt = roleVM.CreatedAt;
            role.UpdateAt = roleVM.UpdateAt;
            role.CreatedUser = roleVM.CreatedUser;
            role.UpdateUser = roleVM.UpdateUser;
            role.Description = roleVM.Description;
        }
        public static void UpdatePermissionModel(this Permission permission, PermissionViewModel permissionVM)
        {
            permission.GroupId = permissionVM.GroupId;
            permission.RoleId = permissionVM.RoleId;
            permission.Active = permissionVM.Active;
            permission.CreatedAt = permissionVM.CreatedAt;
            permission.UpdateAt = permissionVM.UpdateAt;
            permission.CreatedUser = permissionVM.CreatedUser;
            permission.UpdateUser = permissionVM.UpdateUser;
            permission.Description = permissionVM.Description;
        }
        public static void UpdateGroupModel(this Group group, GroupViewModel groupVM)
        {
            group.NameGroup = groupVM.NameGroup;
            group.Description = groupVM.Description;
            group.Active = groupVM.Active;
            group.CreatedAt = groupVM.CreatedAt;
            group.UpdateAt = groupVM.UpdateAt;
            group.CreatedUser = groupVM.CreatedUser;
            group.UpdateUser = groupVM.UpdateUser;
            group.Description = groupVM.Description;
        }
        public static void UpdateGroupUserModel(this GroupUser groupUser, GroupUserViewModel groupUserVM)
        {
            groupUser.CouncilId = groupUserVM.CouncilId;
            groupUser.GroupId = groupUserVM.GroupId;
            groupUser.UserId = groupUserVM.UserId;
            groupUser.Active = groupUserVM.Active;
            groupUser.CreatedAt = groupUserVM.CreatedAt;
            groupUser.UpdateAt = groupUserVM.UpdateAt;
            groupUser.CreatedUser = groupUserVM.CreatedUser;
            groupUser.UpdateUser = groupUserVM.UpdateUser;
            groupUser.Description = groupUserVM.Description;
        }
        public static void UpdateCouncilModel(this Council council, CouncilViewModel councilVm)
        {
            council.NameCouncil = councilVm.NameCouncil;
            council.Active = councilVm.Active;
            council.CreatedAt = councilVm.CreatedAt;
            council.CreatedUser = councilVm.CreatedUser;
            council.UpdateAt = councilVm.UpdateAt;
            council.UpdateUser = councilVm.UpdateUser;
            council.Description = councilVm.Description;
        }
        public static void UpdateDecisionModel(this Decision decision, DecisionViewModel decisionVm)
        {
            decision.NameDecision = decisionVm.NameDecision;
            decision.Active = decisionVm.Active;
            decision.CreatedAt = decisionVm.CreatedAt;
            decision.CreatedUser = decisionVm.CreatedUser;
            decision.UpdateAt = decisionVm.UpdateAt;
            decision.UpdateUser = decisionVm.UpdateUser;
            decision.Description = decisionVm.Description;
        }
        public static void UpdateFileModel(this Filed filed, FileViewModel fileVm)
        {
            filed.NameFile = fileVm.NameFile;
            filed.Base64File = fileVm.Base64File;
            filed.Active = fileVm.Active;
            filed.CreatedAt = fileVm.CreatedAt;
            filed.CreatedUser = fileVm.CreatedUser;
            filed.UpdateAt = fileVm.UpdateAt;
            filed.UpdateUser = fileVm.UpdateUser;
            filed.Description = fileVm.Description;
            filed.TopicId = fileVm.TopicId;
        }
        public static void UpdatePeriodModel(this Period period, PeriodViewModel periodVm)
        {
            period.StartDay = periodVm.StartDay;
            period.EndDay = periodVm.EndDay;
            period.Caption = periodVm.Caption;
            period.Active = periodVm.Active;
            period.CreatedAt = periodVm.CreatedAt;
            period.CreatedUser = periodVm.CreatedUser;
            period.UpdateAt = periodVm.UpdateAt;
            period.UpdateUser = periodVm.UpdateUser;
            period.Description = periodVm.Description;
        }
        public static void UpdateSchoolModel(this School school, SchoolViewModel schoolVm)
        {
            school.NameSchool = schoolVm.NameSchool;
            school.Phone = schoolVm.Phone;
            school.Email = schoolVm.Email;
            school.Address = schoolVm.Address;
            school.Active = schoolVm.Active;
            school.CreatedAt = schoolVm.CreatedAt;
            school.CreatedUser = schoolVm.CreatedUser;
            school.UpdateAt = schoolVm.UpdateAt;
            school.UpdateUser = schoolVm.UpdateUser;
            school.Description = schoolVm.Description;
        }
        public static void UpdateTopicModel(this Topic topic, TopicViewModel topicVm)
        {
            topic.UserId = topicVm.UserId;
            topic.NameTopic = topicVm.NameTopic;
            topic.PeriodId = topicVm.PeriodId;
            topic.SchoolId = topicVm.SchoolId;
            topic.CategoryId = topicVm.CategoryId;
            topic.DecisionId = topicVm.DecisionId;
            topic.Active = topicVm.Active;
            topic.CreatedAt = topicVm.CreatedAt;
            topic.CreatedUser = topicVm.CreatedUser;
            topic.UpdateAt = topicVm.UpdateAt;
            topic.UpdateUser = topicVm.UpdateUser;
            topic.Description = topicVm.Description;
        }
        public static void UpdateCategoryModel(this Category category, CategoryViewModel categoryVm)
        {
            category.NameCategory = categoryVm.NameCategory;
            category.Active = categoryVm.Active;
            category.CreatedAt = categoryVm.CreatedAt;
            category.CreatedUser = categoryVm.CreatedUser;
            category.UpdateAt = categoryVm.UpdateAt;
            category.UpdateUser = categoryVm.UpdateUser;
            category.Description = categoryVm.Description;
        }
        public static void UpdateTopicCouncilModel(this TopicCouncil topicCouncil, TopicCouncilViewModel topicCouncilVM)
        {
            topicCouncil.CouncilId = topicCouncilVM.CouncilId;
            topicCouncil.TopicId = topicCouncilVM.TopicId;
            topicCouncil.Active = topicCouncilVM.Active;
            topicCouncil.CreatedAt = topicCouncilVM.CreatedAt;
            topicCouncil.CreatedUser = topicCouncilVM.CreatedUser;
            topicCouncil.UpdateAt = topicCouncilVM.UpdateAt;
            topicCouncil.UpdateUser = topicCouncilVM.UpdateUser;
            topicCouncil.Description = topicCouncilVM.Description;
        }
    }
}

using System;

namespace BEQLDT.Service.Model
{
    public class UserRoles
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }

        public bool Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public string NameRole { get; set; }
    }
}

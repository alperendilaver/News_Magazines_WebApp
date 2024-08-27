using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Data.DTOs.UserDTOs
{
    public class UserRegisterDTO
    {
        public string FirstName { get; set; }=string.Empty;
 
       
        public string SurName { get; set; }=string.Empty;
        
       
        public string Mail { get; set; }=string.Empty;
       
        public string PhoneNumber { get; set; }=string.Empty;
       
        public string Password { get; set; }=string.Empty;
        
        public string ConfirmPassword { get; set; }=string.Empty;
        public int RoleId { get; set; }
    }
}
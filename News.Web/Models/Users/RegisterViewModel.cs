using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Models.Users
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="İsim Girmek Zorunludur")]     
        public string FirstName { get; set; }=string.Empty;
 
        [Required(ErrorMessage = "Soyad Girmek Zorunludur")]
        public string SurName { get; set; }=string.Empty;
        
        [Required(ErrorMessage ="Mail Zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }=string.Empty;
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }=string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }=string.Empty;
        [Required]
        [Compare(nameof(Password),ErrorMessage ="Parolalar Eş Değil")]
        public string ConfirmPassword { get; set; }=string.Empty;
        public int RoleId { get; set; }
    }
}
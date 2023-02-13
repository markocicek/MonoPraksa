using Business.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Business.WebApi.Models
{
    public class EmployeeAddRest
    {
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        [RegularExpression(@".*\S+.*$", ErrorMessage = "Field Cannot be Blank Or Whitespace")]
        [Required(ErrorMessage = "First name is null")]
        public string FirstName { get { return firstName; } set { firstName = value; } }
        [RegularExpression(@".*\S+.*$", ErrorMessage = "Field Cannot be Blank Or Whitespace")]
        [Required(ErrorMessage = "Last name is null!")]
        public string LastName { get { return lastName; } set { lastName = value; } }
        [RegularExpression(@".*\S+.*$", ErrorMessage = "Field Cannot be Blank Or Whitespace")]
        [Required(ErrorMessage = "Date of birth is null")]
        public DateTime DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }

    }
}

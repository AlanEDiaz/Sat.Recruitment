using NUnit.Framework;
using Sat.Recruitment.Domain.DTO;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Linq;

namespace Sat.Recruitment.Test
{
    [TestFixture]
    public class UserModelValidationTests
    {
        [Test]
        public void User_TestValidationOnGoodData()
        {
            UserDto userDto = new UserDto()
            {
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Name = "Juan",
                Phone = "+5491154762312",
                UserType = "Normal"
            };
            var lstErrors = ValidateModel(userDto);
            Assert.IsTrue(lstErrors.Count == 0);
        }
        [Test]
        [TestCase("")]
        [TestCase("Juanmarmol.com")]
        public void User_TestValidationOnEmailBadData(string email)
        {
            UserDto userDto = new UserDto()
            {
                Address = "Peru 2464",
                Email = email,
                Money = 1234,
                Name = "Juan",
                Phone = "+5491154762312",
                UserType = "Normal"
            };
            var lstErrors = ValidateModel(userDto);

            switch (email)
            {
                case "":
                    lstErrors.Where(e => e.ErrorMessage == "The Email is required").Count().Should().Be(1);
                    break;
                case "Juanmarmol.com":
                    lstErrors.Where(e => e.ErrorMessage == "E-mail is not valid").Count().Should().Be(1);
                    break;

                default:
                    break;
            }
            
        }
        [Test]
        public void User_TestValidationOnPhoneNull()
        {
            UserDto userDto = new UserDto()
            {
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Name = "Juan",
                Phone = "",
                UserType = "Normal"
            };
            var lstErrors = ValidateModel(userDto);

            lstErrors.Where(e => e.ErrorMessage == "The Phone is required").Count().Should().Be(1);

        }
        [Test]
        public void User_TestValidationOnNameNull()
        {
            UserDto userDto = new UserDto()
            {
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Name = "",
                Phone = "+5491154762312",
                UserType = "Normal"
            };
            var lstErrors = ValidateModel(userDto);

            lstErrors.Where(e=>e.ErrorMessage== "The Name is required").Count().Should().Be(1);
        }
        [Test]
        public void User_TestValidationOnAddressNull()
        {
            UserDto userDto = new UserDto()
            {
                Address = "",
                Email = "Juan@marmol.com",
                Money = 1234,
                Name = "Juan",
                Phone = "+5491154762312",
                UserType = "Normal"
            };
            var lstErrors = ValidateModel(userDto);

            lstErrors.Where(e => e.ErrorMessage == "The Address is required").Count().Should().Be(1);
        }
        [Test]
        public void User_TestValidationOnUseTypeNull()
        {
            UserDto userDto = new UserDto()
            {
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Name = "Juan",
                Phone = "+5491154762312",
                UserType = ""
            };
            var lstErrors = ValidateModel(userDto);

            lstErrors.Where(e => e.ErrorMessage == "The UserType is required").Count().Should().Be(1);
        }
        [Test]
        public void User_TestValidationOnUseTypeNotValid()
        {
            UserDto userDto = new UserDto()
            {
                Address = "Peru 2464",
                Email = "Juan@marmol.com",
                Money = 1234,
                Name = "Juan",
                Phone = "+5491154762312",
                UserType = "SuperSuperUser"
            };
            var lstErrors = ValidateModel(userDto);

            lstErrors.Where(e => e.ErrorMessage == "The field UserType is invalid.").Count().Should().Be(1);
        }
        #region auxMethod
        private static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
        #endregion

    }

}

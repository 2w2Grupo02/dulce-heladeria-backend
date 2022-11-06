using Dulce.Heladeria.Api.Controllers;
using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodLogin()
        {
            //given
            UserLoginDto usuario = new UserLoginDto();
            usuario.User = "admin@gmail.com";
            usuario.Password = "admin1234";

            //when
            bool result = Dulce.Heladeria.Api.Program.Login(usuario);

            //then
            Assert.IsTrue(result);
        }
    }
}

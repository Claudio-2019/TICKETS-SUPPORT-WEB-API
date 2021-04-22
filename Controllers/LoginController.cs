﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services;

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ServiceUserRegister RegisterUserService = new ServiceUserRegister();
        private readonly ArrayList listaUsuario = new ArrayList();
        private readonly ArrayList listaAdministradores = new ArrayList();
        private List<string> RolEncontrado = new List<string>();

        [HttpPost]
        public async Task<IActionResult> GetUserAuth([FromBody] UserRegisterModel credentials)
        {
            string[] roles = { "User", "Admin" };

            var sesionCurrentUsers = await RegisterUserService.GetCurrentUsers();
            var sesionCurrentAdmins = await RegisterUserService.GetCurrentAdmin();

            foreach (var datos in sesionCurrentUsers)
            {
                listaUsuario.Add(datos.Email);
                listaUsuario.Add(datos.Pass);

            }
            foreach (var datos in sesionCurrentAdmins)
            {
                listaAdministradores.Add(datos.Email);
                listaAdministradores.Add(datos.Pass);

            }
            if (credentials == null)
            {
                return NoContent();
            }
            else
            {
                if (listaUsuario.Contains(credentials.Email) && listaUsuario.Contains(credentials.Pass))
                {
                    RolEncontrado.Add(roles[0]);
                    return Ok(RolEncontrado);
                }
                else if (listaAdministradores.Contains(credentials.Email) && listaAdministradores.Contains(credentials.Pass))
                {
                    RolEncontrado.Add(roles[1]);
                    return Ok(RolEncontrado);
                }
                else
                {
                    return NotFound("El Usuario no se encuentra registrado!");
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostNewUser([FromBody] UserRegisterModel user)
        {

            if (user != null && user.Role.Contains("User"))
            {
                await RegisterUserService.CreateUserAccount(user);

                return Ok("Se ha registrado el Usuario: " + user.Name);
            }
            else if(User != null && user.Role == "Admin")
            {
                UserAdminRegister AdminData = new UserAdminRegister()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Pass = user.Pass,
                    Role = user.Role
                };
                await RegisterUserService.CreateAdminAccount(AdminData);

                return Ok("Se ha registrado el Usuario: " + AdminData.Name + "como administrador del sistema");
            }
            else
            {
                return BadRequest("Ocurrio un error al registrar el usuario");
            }

        }


    }
}

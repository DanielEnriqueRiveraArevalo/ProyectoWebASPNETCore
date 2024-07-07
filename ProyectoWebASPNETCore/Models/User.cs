using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProyectoWebASPNETCore.Models
{
    public partial class User
    {
        //Se genera Id de usuario
        public int Id { get; set; }

        //Se utiliza Required para hacer obligatorio el ingreso del nombre de usuario.
        [Required(ErrorMessage = "Por favor Ingrese su nombre.")]
        public string? Nombre { get; set; }

        //Se utiliza Required para hacer obligatorio el ingreso del dato
        //EmailAddress y RegularExpression se usa para validar que el correo electronico tenga el formato adecuado con los caracteres corespondientes.
        [Required(ErrorMessage = "Por favor Ingrese su correo electrónico.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo electrónico no es valido")]
        public string? Email { get; set; }

        //Se utiliza Required para hacer obligatorio el ingreso del dato ya qeu debe de explicar el porque desea ser contactado.
        [Required(ErrorMessage = "Por favor ingresa tu Mensaje para ser contactado.")]
        public string? Mensaje { get; set; }
    }
}

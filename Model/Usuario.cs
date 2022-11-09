using System;
using System.Collections.Generic;

namespace codename_boquete.Model
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Username { get; set; }
        public string? Nombre { get; set; }
    }
}

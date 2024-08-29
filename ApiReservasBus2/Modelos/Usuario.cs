

namespace ApiReservasBus2.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }

        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Pwd { get; set; }

        public int Tipo { get; set; }
    }
}

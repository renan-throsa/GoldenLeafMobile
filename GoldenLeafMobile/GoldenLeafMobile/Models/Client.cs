using System.Collections.Generic;

namespace GoldenLeafMobile.Models
{
    public class Client : User
    {
        public string Identification { get; set; }
        public bool Status { get; set; }
        public bool Notifiable { get; set; }

        public override bool Equals(object obj)
        {
            var another = obj as Client;
            if (another == null)
            {
                return false;
            }
            return Identification.Equals(another.Identification);
        }

        public override int GetHashCode()
        {
            return -1687189325 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public override string ToString()
        {
            return $"Nome: {Name} Telefone: {PhoneNumber} Rg: {Identification} Notificável{Notifiable} Status: {Status}";
        }
    }
}

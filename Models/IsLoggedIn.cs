using MessagePack;
namespace move_store_app.Models
{
    public class IsLoggedIn
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public bool IsLogged { get; set; }
    }
}

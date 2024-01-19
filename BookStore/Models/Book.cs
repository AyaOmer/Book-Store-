using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
     
        public byte[]? ProfilePicture { get; set; }
        public string ? Description { get; set; }
        public Author? Author { get; set; }
        [NotMapped]
        public string? imageSrc
        {
            get
            {
                if (ProfilePicture != null)
                {
                    string base64String = Convert.ToBase64String(ProfilePicture, 0, ProfilePicture.Length);
                    return "data:image/*;base64," + base64String;
                }
                return string.Empty;
            }
        }

    }
}

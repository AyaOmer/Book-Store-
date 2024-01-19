using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [StringLength(120,MinimumLength =3)]
        public string Description { get; set; }
        public int  AuthorId { get; set; }
        public List<Author> ? Authors { get; set; }
    
        public IFormFile ? clientFile { get; set; }
  
        public byte[]? ProfilePicture { get; set; }
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

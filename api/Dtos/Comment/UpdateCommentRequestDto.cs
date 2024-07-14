using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Title cannot be less 4 characters")]
        [MaxLength(10, ErrorMessage = "Title cannot be over 10 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage = "Content cannot be less 8 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 charcters")]
        public string Content { get; set; } = string.Empty;
    }
}
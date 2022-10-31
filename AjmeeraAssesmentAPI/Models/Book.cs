using System;
using System.Collections.Generic;

namespace AjmeeraAssesmentAPI.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
    }
}

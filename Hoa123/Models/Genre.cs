﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Hoa123.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Films = new HashSet<Film>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}

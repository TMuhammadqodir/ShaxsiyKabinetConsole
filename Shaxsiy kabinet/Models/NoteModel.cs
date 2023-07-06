using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Models
{
    internal record NoteModel{

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; }

    }
}

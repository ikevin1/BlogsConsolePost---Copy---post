using System.Collections.Generic;
using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;


namespace BlogsConsole.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; }

        
    }
}

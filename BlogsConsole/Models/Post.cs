using System;
using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;


namespace BlogsConsole.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        //public static implicit operator Post(Post v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

using NLog;
using BlogsConsole.Models;
using System;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Program");
            Console.ReadLine();
            logger.Info("Program started");
            string name;
            try
            {


                // Create and save a new Blog
               
                
                using (var dbContext = new BloggingContext())
                {
                    Console.Write("Enter a Blog name for a new Post: ");
                    var Blogname = Console.ReadLine();



                    Blog MyBlog = dbContext.Blogs.Where(id => id.Name==Blogname).FirstOrDefault();

                    Console.WriteLine("BlogID for Blogname {0} is {1}", Blogname, MyBlog.BlogId);
                    Console.Read();

                    Console.WriteLine("Enter Title: ");
                    var myTitle = Console.Read();                    
                    Console.WriteLine("\nEnter Content: ");
                    var myContent = Console.Read();

                    //Question to Jeff: Why is it asking to convert to int while both Title and Content are of string datatypes!!
                    Post post = new Post { Title=myTitle,Content=myContent,BlogId=MyBlog.BlogId};
                    dbContext.AddPost(post);

                    //display the post contents here
                    var PostResults = dbContext.Posts.Where(b => b.BlogId == MyBlog.BlogId);
                    var query2 = dbContext.Posts.OrderBy(p => p.Title);
                    Console.WriteLine("All posts belonging to the selected Blog:");
                    foreach (var item in PostResults)
                    {
                        Console.WriteLine("Title: {0}\nContent: {1}\n\n", item.Title, item.Content);
                    }

                    
                   

                    // Display all Blogs from the database
                    Console.Write("Enter a name for a new Blog: ");
                    name = Console.ReadLine();

                    var blog = new Blog { Name = name };
                    dbContext.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);
                    var query = dbContext.Blogs.OrderBy(b => b.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine("BlogID: {0}\nBlogName: {1}",item.BlogId,item.Name);
                }
          
                
            }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                
                logger.Error(ex.Message);
                Console.ReadLine();
            }

            Console.WriteLine("Press enter to quit");
            string x = Console.ReadLine();

            logger.Info("Program ended");
        }
    }
}

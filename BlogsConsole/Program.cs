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
            //Console.WriteLine("Starting Program");
            //Console.ReadLine();
            logger.Info("Program started");
            string choice = "";
            do
            {
                try
            {


                    // Create and save a new Blog

                    DisplayMenu();
                    using (var dbContext = new BloggingContext())
                        DisplayMenu();

                    logger.Info("User choice: {Choice}", choice);
                    if (choice == "3")
                    {
                        using (var dbContext = new BloggingContext())
                            Console.Write("Enter a Blog name for a new Post: ");
                        var Blogname = Console.ReadLine();



                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();

                        Console.WriteLine("BlogID for Blogname {0} is {1}", Blogname, MyBlog.BlogId);
                        Console.ReadLine();

                        Console.WriteLine("Enter Title: ");
                        var myTitle = Console.ReadLine();
                        Console.WriteLine("\nEnter Content: ");
                        var myContent = Console.ReadLine();

                        else if (choice == "2")

                        {
                            Post post = new Post { Title = myTitle, Content = myContent, BlogId = MyBlog.BlogId };
                            dbContext.AddPost(post);

                            //display the post contents here
                            var PostResults = dbContext.Posts.Where(b => b.BlogId == MyBlog.BlogId);
                            var query2 = dbContext.Posts.OrderBy(p => p.Title);
                            Console.WriteLine("All posts belonging to the selected Blog:");
                            foreach (var item in PostResults)
                            {
                                Console.WriteLine("Title: {0}\nContent: {1}\n\n", item.Title, item.Content);
                            }

                        }
                        else if (choice == "1")
                        {

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
                                Console.WriteLine("BlogID: {0}\nBlogName: {1}", item.BlogId, item.Name);
                            }

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
            } while (choice == "1" || choice == "2" || choice == "3");
            Console.WriteLine("Press enter to quit");
            string x = Console.ReadLine();

            logger.Info("Program ended");
            void DisplayMenu()
            {
                Console.WriteLine("1) Display All Blogs");
                Console.WriteLine("2) Add Blogs");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
            }
        }
    }
}

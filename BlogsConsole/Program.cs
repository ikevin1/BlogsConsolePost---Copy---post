﻿using NLog;
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
            logger.Info("Program started");
            string choice = "";
            do
            {
                try
                {
                   
                    DisplayMenu();
                    var dbContext = new BloggingContext();
                   
                    logger.Info("User choice: {Choice}", choice);

                    if (choice == "3") //add post
                    {
                        
                        Console.Write("Enter a Blog name for a new Post: ");
                        var Blogname = Console.ReadLine();

                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();
                        
                        Console.WriteLine("Enter Title: ");
                        var myTitle = Console.ReadLine();
                        Console.WriteLine("\nEnter Content: ");
                        var myContent = Console.ReadLine();

                        //add post
                        Post post = new Post { Title = myTitle, Content = myContent, BlogId = MyBlog.BlogId };
                        dbContext.AddPost(post);
                    }
                    else if (choice == "4") //view posts per blog

                    {
                       /* prompt user for the name of the blog they would like to view posts from
                          save input
                          query blogs by name
                          loop through blog for each post
                          display each post during the loop
                        */
                        Console.WriteLine("Enter the blog name you would want to view");
                        var Blogname = Console.ReadLine();

                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();
                        Console.WriteLine("Posts for blog name " + Blogname);
                    
                        IQueryable<Post> PostResults = dbContext.Posts.Where(b => b.BlogId == MyBlog.BlogId);
                        foreach (Post p in PostResults)
                        {
                            Console.WriteLine("Title: {0}\nContent: {1}\n\n", p.Title, p.Content);
                        }
                        

                    }
                    else if (choice == "2") //add blog
                    {                        
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };
                        dbContext.AddBlog(blog);
                    }
                    else if (choice == "1") //display all blogs
                    {
                        Console.WriteLine("All blogs in the database:");

                        var query = dbContext.Blogs.OrderBy(b => b.Name);
                        foreach (var item in query)
                        {
                            Console.WriteLine("BlogID: {0}\nBlogName: {1}", item.BlogId, item.Name);
                        }
                    }
                    else if (choice == "5") //delete blog
                    {
                        Console.Write("Enter blog name to delete: ");
                        var Blogname = Console.ReadLine();
                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();
                        dbContext.DeleteBlog(MyBlog);
                    }
                }



                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();

                    logger.Error(ex.Message);
                    Console.ReadLine();
                }
            } while (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5");
            Console.WriteLine("Press enter to quit");
            string x = Console.ReadLine();

            logger.Info("Program ended");
            void DisplayMenu()
            {
                Console.WriteLine("1) Display All Blogs");
                Console.WriteLine("2) Add Blogs");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("4)  Display posts for blogs");
                Console.WriteLine("5) Delete Blog");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
            }
        }
    }
}

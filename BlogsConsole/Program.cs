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
                    var dbContext = new BloggingContext();
                    //using (var dbContext = new BloggingContext())
                    //DisplayMenu();

                    logger.Info("User choice: {Choice}", choice);
                    if (choice == "3")
                    {
                        // using (var dbContext = new BloggingContext())
                        Console.Write("Enter a Blog name for a new Post: ");
                        var Blogname = Console.ReadLine();



                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();

                        Console.WriteLine("BlogID for Blogname {0} is {1}", Blogname, MyBlog.BlogId);
                        Console.ReadLine();

                        Console.WriteLine("Enter Title: ");
                        var myTitle = Console.ReadLine();
                        Console.WriteLine("\nEnter Content: ");
                        var myContent = Console.ReadLine();
                    }
                    else if (choice == "3")

                    {
                        //string myTitle = "";
                        //string myContent = "";
                        //string MyBlog = "";
                        Console.WriteLine("Enter a name for post title: ");
                        var myTitle = Console.ReadLine();
                        string Blogname = "";
                        Console.WriteLine("Enter a name for post Content: ");
                        var myContent = Console.ReadLine();

                        //var BlogId = MyBlog.BlogId
                        //BlogId = MyBlog.BlogId();
                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();
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
                    else if (choice == "2")
                    {

                        // Create and Display all Blogs from the database
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

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
                    else if (choice == "4")
                    {
                        var query = dbContext.Blogs.OrderBy(b => b.Name);
                        //var blog = new Blog { Name = name };

                        Blog MyBlog = dbContext.Blogs.Where(id => id.Name == Blogname).FirstOrDefault();
                        Console.WriteLine("Total Blogs: " + dbContext.Blogs.Count());                        
                        Console.WriteLine("0) Total Posts from blogs");
                        foreach (var item in query)
                        {
                            Console.WriteLine("BlogID: {0}\nBlogName: {1}", item.BlogId, item.Name);
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
            } while (choice == "1" || choice == "2" || choice == "3" || choice == "4");
            Console.WriteLine("Press enter to quit");
            string x = Console.ReadLine();

            logger.Info("Program ended");
            void DisplayMenu()
            {
                Console.WriteLine("1) Display All Blogs");
                Console.WriteLine("2) Add Blogs");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("4) Display All Post");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
            }
        }
    }
}

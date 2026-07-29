using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create first video
        Video video1 = new Video("C# Tutorial for Beginners", "John Smith", 1250);
        video1.AddComment(new Comment("Alice Johnson", "Great explanation! Very helpful for learning C#."));
        video1.AddComment(new Comment("Bob Williams", "The pacing was perfect. I could follow along easily."));
        video1.AddComment(new Comment("Carol Davis", "Thanks for breaking down the concepts. Subscribed!"));
        video1.AddComment(new Comment("David Brown", "Best tutorial I've found so far."));
        videos.Add(video1);

        // Create second video
        Video video2 = new Video("Advanced OOP Concepts", "Sarah Wilson", 2150);
        video2.AddComment(new Comment("Emma White", "This really helped me understand inheritance."));
        video2.AddComment(new Comment("Frank Miller", "Could you explain interfaces more? Loved this video."));
        video2.AddComment(new Comment("Grace Lee", "Professional content. Keep it up!"));
        videos.Add(video2);

        // Create third video
        Video video3 = new Video("Building Web APIs with C#", "Michael Chen", 1875);
        video3.AddComment(new Comment("Henry Clark", "Finally understand how to build REST APIs properly."));
        video3.AddComment(new Comment("Isabella Green", "The example project was amazing!"));
        video3.AddComment(new Comment("Jack Martin", "Clear and concise. Perfect for beginners."));
        video3.AddComment(new Comment("Karen Robinson", "This is exactly what I needed for my project."));
        videos.Add(video3);

        // Create fourth video
        Video video4 = new Video("Database Design with SQL Server", "Robert Taylor", 2300);
        video4.AddComment(new Comment("Laura Hall", "Best explanation of normalization I've ever seen."));
        video4.AddComment(new Comment("Mark Anderson", "Very thorough and well-structured."));
        video4.AddComment(new Comment("Nancy Thomas", "Helped me pass my database exam!"));
        videos.Add(video4);

        // Display all videos and their comments
        foreach (Video video in videos)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment.GetCommenterName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}
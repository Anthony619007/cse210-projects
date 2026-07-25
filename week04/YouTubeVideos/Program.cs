using System;
using System.Collections.Generic;
class Program
{
static void Main(string[] args)
{
List videos = new List(); 
Video video1 = new Video("C# Programming Tutorial for Beginners"
"CodeMaster", 720);
video1.AddComment(new Comment("JohnDoe", "Great tutorial! Very helpful."));
video1.AddComment(new Comment("JaneSmith", "I finally understand classes!"));
video1.AddComment(new Comment("DevGuy", "Could you make a video on inheritance?"));
video1.AddComment(new Comment("CodeNewbie", "This was exactly what I needed. Thanks!"));
videos.Add(video1);
Video video2 = new Video("Understanding OOP Principles"
"TechTeacher", 540);
video2.AddComment(new Comment("StudentOne", "Encapsulation makes so much sense now."));
video2.AddComment(new Comment("PolyMorph", "Polymorphism example was perfect!"));
video2.AddComment(new Comment("CSharpFan", "Great explanation of abstraction."));
videos.Add(video2);
Video video3 = new Video("Building a To-Do List App in C#"
"DevPro", 900);
video3.AddComment(new Comment("AppBuilder", "This project was fun to follow along!"));
video3.AddComment(new Comment("CleanCode", "I like how you organized the code."));
video3.AddComment(new Comment("NewDev", "My first working app! Thanks!"));
video3.AddComment(new Comment("SeniorDev", "Good practices shown here."));
videos.Add(video3);
Video video4 = new Video("C# LINQ Tutorial", "QueryMaster", 480);
video4.AddComment(new Comment("DataGuy", "LINQ is so powerful!"));
video4.AddComment(new Comment("SqlPro", "This is like SQL but in C# awesome!"));
video4.AddComment(new Comment("CodeNinja", "The lambda expressions make sense now."));
videos.Add(video4);
Console.WriteLine("===== YOUTUBE VIDEO COMMENT TRACKER ===== ");
foreach (Video video in videos)
{
Console.WriteLine($"Title: {video.GetTitle()});
Console.WriteLine($"Author: {video.GetAuthor()});
Console.WriteLine($"Length: {video.GetLengthInSeconds()} seconds ({video.GetLengthInMinutes():F1} minutes));
Console.Write($"Number of Comments: {video.GetNumberOfComments()});
public Comment(string commenterName, string commentText)
{this.commenterName = commenterName;this.commentText = commentText;}
public string GetCommenterName()=>commenterName;
private string commenterName Comment: this;CommentText: This is like SQL but in C# awesome!
private string commentText
public string GetCommenterName()

﻿using LearningLinq.Queries;

namespace LearningLinq;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Student and Teacher queries:");
        //StudentTeacherQueries.Query();
        //StudentTeacherQueries.QueryToXml();
        StudentTeacherQueries.QueryStudentData();

        //Console.WriteLine("Basic number queries");
        //BasicNumberQueries.RadiusQuery();
    }
}
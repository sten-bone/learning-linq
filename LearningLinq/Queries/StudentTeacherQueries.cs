using System.Xml.Linq;
using LearningLinq.Utils;

namespace LearningLinq.Queries;

public class StudentTeacherQueries
{
    private static readonly List<Student> Students = CreateStudents();
    private static readonly List<Teacher> Teachers = CreateTeachers();
    private static readonly List<Student> StudentsLong = CreateStudentsLong();
    public static void Query()
    {
        // query for teachers/students in Seattle
        var peopleInSeattleQuery = Students
            .Where(x => x.City == "Seattle")
            .Select(x => x.Last)
            .Concat(
                Teachers
                .Where(y => y.City == "Seattle")
                .Select(y => y.Last)
            );
        QueryUtils.DisplayQuery(peopleInSeattleQuery, "People in Seattle");

        var specificPeopleWithAddressQuery = Students
            .Where(x => x.City == "Seattle")
            .Select(x => $"{x.First} {x.Last} on {x.Street}")
            .Concat(Teachers
                .Where(y => y.City == "Seattle")
                .Select(y => $"{y.First} {y.Last} (teacher)")
            );
        QueryUtils.DisplayQuery(specificPeopleWithAddressQuery, "More specific people in Seattle");
    }

    public static void QueryToXml()
    {
        var studentsToXml = new XElement("Root",
            Students
                .OrderBy(x => x.Last)
                .ThenBy(x => x.First)
                .Select(x => new XElement("student",
                    new XElement("First", x.First),
                    new XElement("Last", x.Last),
                    new XElement("Scores", string.Join(",", x.Scores))
                ))
        );
        Console.WriteLine($"Students to XML:\n{studentsToXml}");
    }

    public static void QueryStudentData()
    {
        var lastNameGroupedQuery = StudentsLong
            .GroupBy(x => x.Last[0])
            .OrderBy(x => x.Key);
        foreach (var studentGroup in lastNameGroupedQuery)
        {
            Console.WriteLine(studentGroup.Key);
            foreach (var student in studentGroup)
            {
                Console.WriteLine($"\t{student.Last}, {student.First}");
            }
        }
        Console.WriteLine();

        var studentsAverageLessThanFirstScoreQuery = StudentsLong
            .Select(x => new
            {
                Student = x,
                AverageScore = x.Scores.Average()
            })
            .Where(x => x.AverageScore < x.Student.Scores[0])
            .OrderByDescending(x => x.AverageScore)
            .Select(x => $"{x.AverageScore:F2}: {x.Student.Last}, {x.Student.First} (First score = {x.Student.Scores[0]})");
        QueryUtils.DisplayQuery(studentsAverageLessThanFirstScoreQuery, "Students whose average score was less than their first score");
    }

    private class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public List<int> Scores;
    }

    private class Teacher
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public string City { get; set; }
    }

    private static List<Student> CreateStudents()
    {
        return new List<Student>()
        {
            new Student { First="Svetlana",
                Last="Omelchenko",
                ID=111,
                Street="123 Main Street",
                City="Seattle",
                Scores= new List<int> { 97, 92, 81, 60 } },
            new Student { First="Claire",
                Last="O’Donnell",
                ID=112,
                Street="124 Main Street",
                City="Redmond",
                Scores= new List<int> { 75, 84, 91, 39 } },
            new Student { First="Sven",
                Last="Mortensen",
                ID=113,
                Street="125 Main Street",
                City="Lake City",
                Scores= new List<int> { 88, 94, 65, 91 } },
            new Student { First="Aaron",
                Last="O'Donnel",
                ID=114,
                Street="124 Main Street",
                City="Redmond",
                Scores= new List<int> { 63, 81, 77, 76 } },
            new Student { First="Sylvia",
                Last="Brown",
                ID=115,
                Street="126 Main Street",
                City="Iowa City",
                Scores= new List<int> { 99, 94, 95, 91 } },
        };
    }

    private static List<Student> CreateStudentsLong()
    {
        return new List<Student>
        {
            new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
            new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
            new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
            new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
            new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
            new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
            new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
            new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
            new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
            new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
            new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
            new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91}}
        };
    }

    private static List<Teacher> CreateTeachers()
    {
        return new List<Teacher>()
        {
            new Teacher { First="Ann", Last="Beebe", ID=945, City="Seattle" },
            new Teacher { First="Alex", Last="Robinson", ID=956, City="Redmond" },
            new Teacher { First="Michiyo", Last="Sato", ID=972, City="Tacoma" }
        };
    }
}
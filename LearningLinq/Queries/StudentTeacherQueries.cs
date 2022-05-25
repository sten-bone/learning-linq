using System.Xml.Linq;
using LearningLinq.Utils;

namespace LearningLinq.Queries;

public class StudentTeacherQueries
{
    private static List<Student> _students = CreateStudents();
    private static List<Teacher> _teachers = CreateTeachers();
    public static void Query()
    {
        // query for teachers/students in Seattle
        var peopleInSeattleQuery = _students
            .Where(x => x.City == "Seattle")
            .Select(x => x.Last)
            .Concat(
                _teachers
                .Where(y => y.City == "Seattle")
                .Select(y => y.Last)
            );
        QueryUtils.DisplayQuery(peopleInSeattleQuery, "People in Seattle");

        var specificPeopleWithAddressQuery = _students
            .Where(x => x.City == "Seattle")
            .Select(x => $"{x.First} {x.Last} on {x.Street}")
            .Concat(_teachers
                .Where(y => y.City == "Seattle")
                .Select(y => $"{y.First} {y.Last} (teacher)")
            );
        QueryUtils.DisplayQuery(specificPeopleWithAddressQuery, "More specific people in Seattle");
    }

    public static void QueryToXml()
    {
        var studentsToXML = new XElement("Root",
            _students
                .OrderBy(x => x.Last)
                .ThenBy(x => x.First)
                .Select(x => new XElement("student",
                    new XElement("First", x.First),
                    new XElement("Last", x.Last),
                    new XElement("Scores", string.Join(",", x.Scores))
                ))
        );
        Console.WriteLine($"Students to XML:\n{studentsToXML}");
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
using System.Collections.Generic;
using Newtonsoft.Json;

public class Teacher {
    public string name { get; set; }
    public string details { get; set; }
}

public class ConvResponse {
    public string role { get; set; }
    public string content { get; set; }
}

public class ScheduleItem {
    public string class_id { get; set; }
    public string start_time { get; set; }
    public string end_time { get; set; }
    public string current_unit { get; set; }
}

public class ClassModel {
    public int id { get; set; }
    public int current_unit_index { get; set; }
    public List<string> units { get; set; }
    public List<Student> students { get; set; }
    public Teacher teacher { get; set; }
    public List<string> messages { get; set; }
}

public class Student {
    public string name { get; set; }
    public string details { get; set; }
    public List<string> messages { get; set; }
}

public class SessionModel {
    public int id { get; set; }
    public string subject { get; set; }
    public string grade { get; set; }
    public List<ClassModel> classes { get; set; }
    [JsonProperty]
    public Dictionary<string, ScheduleItem> schedule { get; set; }
}
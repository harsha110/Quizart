using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using  Newtonsoft.Json.Linq;
namespace Tables {
    public class Template
    {
        [Key]
        public int template_id { get; set; }
        public string topic { get; set; }
        public string category { get; set; }
        public string sparql { get; set; }
        public string text { get; set; }
        public List<Question_table> questions_topic { get; set; }
    }

    public class Question_table 
    {
        [Key]
        public int question_id { get; set; }
        public string topic { get; set; }
        public string category { get; set; }
        public string questions { get; set; }
        public List<option_table> options {get;set;}
        public int template_id { get; set; }


    }

    public class option_table
    {
        [Key]
        public int option_id;
        public string option;
        public bool iscorrect;
        public int question_id;

    }

    public class get_method_object
    {
        public JObject related_questions;
        public string question;
    }

}
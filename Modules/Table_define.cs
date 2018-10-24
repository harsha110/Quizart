using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using  Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
namespace Tables {
    public class Template
    {
        [Key]
        public int template_id { get; set; }
        public string topic { get; set; }
        public string category { get; set; }
        public string sparql { get; set; }
        public string text { get; set; }
        public List<Question_table> questions_topic {get; set;}
    }

    public class Question_table 
    {
        [Key]
        public int question_id { get; set; }
        public string topic { get; set; }
        public string category { get; set; }
        public string questions { get; set; }
        public List<option_table> options {get; set;}
        public int template_id {get; set;}
        
    }

    public class option_table
    {
        [Key]
        public int option_id { get; set; }
        public string option { get; set; }
        public bool iscorrect { get; set; }
        public int question_id {get; set;}
    }

    public class get_method_object
    {
        public  List<string> related_questions { get; set; }
        public  List<string> related_options { get; set; }
    }

    public class post_object
    {
       public string topic_id { get; set; }
       public string category_id { get; set; }
       public string topic_name { get; set; }
       public string category_name { get; set; }
    }

}
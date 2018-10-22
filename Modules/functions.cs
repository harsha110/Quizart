using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Tables;
using Microsoft.EntityFrameworkCore;
using database_rules;
using System.Net.Http;
using Newtonsoft.Json;
using System.Xml;
using  Newtonsoft.Json.Linq;
namespace API_functions 
{
    public class API_methods 
    {
        Database db = new Database();
        int Question_table_id = 1 ;
        List<String> list = new List<String>();
        
        option_table options = new option_table();
        string final = "dfjlsdfjl";
        get_method_object Container = new get_method_object();
        public List<Template> retrive()
        {
            List<Template> allvalues = db.Template
                                        .ToList();
            return allvalues;
        }

        public List<Question_table> retrive1()
        {
            List<Question_table> allvalues = db.Question_Table
                                        .Where(s=>s.topic=="film actor")
                                        .Include(s=>s.options)
                                        .ToList();
            return allvalues;
        }

        async void httpcall(string url)
        {
            list.Clear();
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.GetAsync(url);
            HttpContent content = response.Content;
            this.final = content.ReadAsStringAsync().Result;
            JObject json = JObject.Parse(this.final);
            JArray items = (JArray)json["results"]["bindings"];
            //Console.WriteLine(items);
            for (int i = 0; i < items.Count; i++)
                {
                    var item = (JObject)items[i];
                    //Console.WriteLine(item["sLabel"]["value"]);
                    list.Add((string)item["sLabel"]["value"]);
                    //Console.WriteLine(list[i]);
                    //do something with item
                }
                Container.related_questions=list;
            //Console.WriteLine("Harsha");
             
            //doc.LoadXml("Hello:world");
            //Console.WriteLine("Hello");
            //JObject json = JObject.Parse(this.final);
             //Container = JsonConvert.SerializeObject(Container);
            //string jsonText = JsonConvert.DeserializeString(final);
            
            //var contact = JsonConvert. (final);
            
            //string final = content.ReadAsStringAsync().Result;
            //return final ;
        }

        // public get_method_object get_Questions(string Q1)
        // {
        //     //string[] pArray = Q1.Split(" ");
        //     //get_method_object Container = new get_method_object();
        //     List<Template> Present= db.Template
        //                             .Where(s=> s.category==Q1)
        //                             .Include(s => s.questions_topic)
        //                             .ToList(); 
        //     if(Present.Count>0)
        //     {
        //         foreach(Template T in Present)
        //         {
        //             Console.WriteLine("forlooplikf");
        //             foreach(Question_table Q in T.questions_topic)
        //             {
        //                 Container.question=Q.questions;
        //             }
                    
        //         }
                
        //     }
        //     //Task<string> g = httpcall("");
        //     return Container  ;                         
        // }

        // public bool remove_in(string del)
        // {
        //     List<Template> all=null;
        //     all = db.Template
        //     .Where(s=>s.category==del)
        //     .Include(s => s.questions_topic).ToList();
        //     if(all.Count>0)
        //     {
        //         db.RemoveRange(all);
        //         db.SaveChanges();
        //         return true;
        //     }  
        //     return false;
        // }


         public List<Template> Insert_in_template(post_object T)
        {
            Template TT = new Template();
            List<Template> Present= db.Template
                                    .Where(s=>s.topic==T.topic_name && s.category==T.category_name).ToList();
                                   
            if(Present.Count==0)
            {
                    TT.topic=T.topic_name;
                    TT.category=T.category_name;
                    TT.sparql="https://query.wikidata.org/sparql?query=SELECT ?sLabel WHERE{?s wdt:"+T.category_id+" wd:"+T.topic_id+". SERVICE wikibase:label{bd:serviceParam wikibase:language 'en'.}}LIMIT 20&format=json";
                    TT.text="What is $name"+" "+TT.category;
                    db.Template.Add(TT);
                    
                    // if(T.questions_topic.Count>0)
                    // {
                    //     foreach(Question_table Q in T.questions_topic)
                    //     {
                    //          db.Question_Table.Add(Q);                   
                    //     }
                
                    // }
                    httpcall(TT.sparql);
                    Thread.Sleep(7000);
                   
                    //Console.WriteLine("came");
                    //Console.WriteLine(this.Container.related_questions.Count);
                    foreach(string i in this.Container.related_questions)
                {
                    Question_table generate_and_insert = new Question_table();
                    option_table OP = new option_table();
                    //Console.WriteLine(i+"value");
                    var sub = TT.text;
                    sub=sub.Replace("$name",i );
                   
                    // generate_and_insert.question_id=Question_table_id;
                    // Question_table_id++;
                    generate_and_insert.category=TT.category;
                    generate_and_insert.topic=TT.topic;
                    generate_and_insert.questions=sub;
                    generate_and_insert.template_id=TT.template_id;
                    db.Question_Table.Add(generate_and_insert);
                    OP.iscorrect=true;
                    option_table(T.category_id);
                    OP.option=TT.topic;
                    OP.question_id=generate_and_insert.question_id;
                    db.option_Table.Add(OP);
                    Console.WriteLine(generate_and_insert);
                    
                    // Console.WriteLine(generate_and_insert.questions);
                    // options.option=T.topic;
                    // options.iscorrect=true;
                    // db.option_Table.Add(options);
                    //do something with item
                    
                }
                    db.SaveChanges();
                    List<Template> allvalues = db.Template
                                        .Where(s=>s.topic==TT.topic&&s.category==TT.category)
                                        .Include(s=>s.questions_topic)
                                        .ToList();
                    return allvalues;
                     //return true;
                    
                    //return true;
                
            }
            else{
                List<Template> allvalues = db.Template
                                        .Where(s=>s.topic==T.topic_name&&s.category==T.category_name)
                                        .Include(s=>s.questions_topic)
                                        .ToList();
                    return allvalues;
            }
            List<Template> allvalues1=new List<Template>(); 
            return allvalues1;
        }
    }

}
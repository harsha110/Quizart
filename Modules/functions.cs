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
        List<String> list1 = new List<String>();
        option_table options = new option_table();
        string final = "dfjlsdfjl";
        get_method_object Container = new get_method_object();
        public List<Template> retrive()
        {
            List<Template> allvalues = db.Template
                                        .Where(s=>(s.topic=="community organizer"&&s.category=="Occupation"))
                                        .Include("questions_topic.options")
                                        .ToList();
                    return allvalues;
        }

        public List<Question_table> retrive1()
        {
            List<Question_table> allvalues = db.Question_Table
                                        .Where(s=>s.topic=="community organizer")
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
        }

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

        async void httpcall_other_options (string other_options)
        {
            list1.Clear();
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.GetAsync(other_options);
            HttpContent content = response.Content;
            this.final =content.ReadAsStringAsync().Result;
            JObject json = JObject.Parse(this.final);
            JArray items = (JArray)json["results"]["bindings"];
            Console.WriteLine(items.Count+"length");
            for(int i=0;i<items.Count;i++)
            {
                //Random generator = new Random();
                //int r = generator.Next(0, items.Count);
                //Console.WriteLine(r+"r");
                // int r1 = generator.Next(0, items.Count);
                // Console.WriteLine(r1+"r1");
                // r=(r+r1)%(items.Count);
                var item = (JObject)items[i];
                list1.Add((string)item["options"]["value"]);
                Console.WriteLine(list1[i]+"options");
            }
            Container.related_options=list1;
        }
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

                    httpcall(TT.sparql);
                    Thread.Sleep(2000);

                    httpcall_other_options("https://query.wikidata.org/sparql?query=SELECT ?cid ?options WHERE {?cid wdt:P31 wd:Q28640. OPTIONAL {?cid rdfs:label ?options filter (lang(?options) = 'en') . }}Limit 100 &format=json");
                    Thread.Sleep(2000);
                    
                    foreach(string i in this.Container.related_questions)
                    {
                        Console.WriteLine(i);
                    }
                    for(int i=0;i<this.Container.related_questions.Count;i++)
                {
                    Console.WriteLine(this.Container.related_questions.Count+" total");
                    Console.WriteLine(i+ "  forlooping");
                    Question_table generate_and_insert = new Question_table();
                    option_table OP = new option_table();
                    //Console.WriteLine(i+"value");
                    var sub = TT.text;
                    sub=sub.Replace("$name",this.Container.related_questions[i] );
                    generate_and_insert.category=TT.category;
                    generate_and_insert.topic=TT.topic;
                    generate_and_insert.questions=sub;
                    generate_and_insert.template_id=TT.template_id;
                    db.Question_Table.Add(generate_and_insert);
                    OP.iscorrect=true;
                    OP.option=TT.topic;
                    OP.question_id=generate_and_insert.question_id;
                    db.option_Table.Add(OP);
                    for(int j=0;j<3;j++)
                    {
                        Random generator = new Random();
                        int r = generator.Next(0, Container.related_options.Count);
                        option_table OPA = new option_table();
                        OPA.iscorrect=false;
                        OPA.option=Container.related_options[r];
                        Console.WriteLine(OPA.option);
                        OPA.question_id=generate_and_insert.question_id;
                        db.option_Table.Add(OPA);
                    }
                    Console.WriteLine("last line");
                }
                    Console.WriteLine("sachin");
                    db.SaveChanges();
                    List<Template> allvalues = db.Template
                                        .Where(s=>s.topic==TT.topic&&s.category==TT.category)
                                        .Include("questions_topic.options")
                                        .ToList();
                    Console.WriteLine(allvalues);                    
                    return allvalues;
            }
            else
            {
                List<Template> allvalues = db.Template
                                        .Where(s=>s.topic==T.topic_name&&s.category==T.category_name)
                                        .Include("questions_topic.options")
                                        .ToList();
                    return allvalues;
            }
            List<Template> allvalues1=new List<Template>(); 
            return allvalues1;
        }
    }

}
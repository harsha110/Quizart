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
        string final = "dfjlsdfjl";
        get_method_object Container = new get_method_object();
        public List<Template> retrive()
        {
            List<Template> allvalues = db.Template
                                        .Include(s => s.questions_topic)
                                        .ToList();
            return allvalues;
        }

        async void httpcall(string url)
        {
            HttpClient http = new HttpClient();
            // Console.WriteLine("Hello");
            HttpResponseMessage response = await http.GetAsync(url);
            HttpContent content = response.Content;
            this.final = content.ReadAsStringAsync().Result;
            //string final = "hello:world";
            //XmlDocument doc = new XmlDocument();
            JObject json = JObject.Parse(this.final);
            Container.related_questions=json;
             Console.WriteLine(Container.related_questions);
            //doc.LoadXml("Hello:world");
            //Console.WriteLine("Hello");
            //JObject json = JObject.Parse(this.final);
             //Container = JsonConvert.SerializeObject(Container);
            //string jsonText = JsonConvert.DeserializeString(final);
            
            //var contact = JsonConvert. (final);
            
            //string final = content.ReadAsStringAsync().Result;
            //return final ;
        }

        public get_method_object get_Questions(string Q1)
        {
            //string[] pArray = Q1.Split(" ");
            //get_method_object Container = new get_method_object();
            List<Template> Present= db.Template
                                    .Where(s=> s.category==Q1)
                                    .Include(s => s.questions_topic)
                                    .ToList(); 
            if(Present.Count>0)
            {
                foreach(Template T in Present)
                {
                    Console.WriteLine("forlooplikf");
                    foreach(Question_table Q in T.questions_topic)
                    {
                        Container.question=Q.questions;
                    }
                    httpcall(T.sparql);
                    Thread.Sleep(3000);
                    
                    
                    return Container;
                }
                
            }
            //Task<string> g = httpcall("");
            return Container  ;                         
        }

        public bool remove_in(string del)
        {
            List<Template> all=null;
            all = db.Template
            .Where(s=>s.category==del)
            .Include(s => s.questions_topic).ToList();
            if(all.Count>0)
            {
                db.RemoveRange(all);
                db.SaveChanges();
                return true;
            }  
            return false;
        }


         public bool Insert_in_template(Template T)
        {
            List<Template> Present= db.Template
                                    .Where(s=>s.topic==T.topic && s.category==T.category).ToList();
            if(Present.Count==0)
            {
        
                    db.Template.Add(T);
                    if(T.questions_topic.Count>0)
                    {
                        foreach(Question_table Q in T.questions_topic)
                        {
                             db.Question_Table.Add(Q);                   
                        }
                
                    }
                    db.SaveChanges();
                    return true;
                
            }
            return false;

        }
    }

}
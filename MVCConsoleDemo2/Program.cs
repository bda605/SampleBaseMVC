using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCConsoleDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lst = new List<User>(){
               new User{Id =1 , Name = "A1",Age =21}, 
               new User{Id =2 , Name = "A2",Age =22}, 
               new User{Id =3 , Name = "A3",Age =23}, 
               new User{Id =4 , Name = "A4",Age =24}, 
               new User{Id =5 , Name = "A5",Age =25}, 
               new User{Id =6 , Name = "A6",Age =26}, 
               new User{Id =7 , Name = "A7",Age =27}, 
               new User{Id =8 , Name = "A8",Age =28}, 
               new User{Id =9 , Name = "A9",Age =29}, 
               new User{Id =10 , Name = "A10",Age =30}, 
               new User{Id =11 , Name = "A11",Age =31}, 
           };

            var result = lst.Where(x => x.Age >= 30).ToList();
            result.ForEach(r => Console.WriteLine(string.Format("{0},{1},{2}",r.Id,r.Name,r.Age)));
           
            //查詢select
            var result1 = lst.Where(x => x.Age >= 30).Select(s => s.Name).ToList();
            result1.ForEach( x=> Console.WriteLine(x));
            //count
            int cnt = lst.Where(x => x.Age >= 30).Count();
            //sort 
            var result2 = lst.OrderBy(x => x.Age).OrderBy(x => x.Id).ToList();

            //延遲載入
            IEnumerable<User> usr = lst.Where(x => x.Age >= 30);

            //即時加載
            List<User> lstUsr = lst.FindAll(x => x.Age >= 30);


            
        }
        
       
    }
   
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }
    public class Student 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ClassName { get; set; }
    }

}

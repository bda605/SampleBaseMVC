using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using MVCConsoleDemo3.Model;
namespace MVCConsoleDemo3
{
    //Entity Framework Implementation CRUD  
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindEntities db = new NorthwindEntities();
            //EZ Query
            var result = from c in db.Customers select c;
            result = from c in db.Customers  where c.CustomerID=="A001"  select c;
            result = db.Customers.Where<Customers>(c => c.CustomerID == "A001");
            //分頁
            IQueryable<Customers> cust10 = (from c in db.Customers orderby c.CustomerID select c).Skip(0).Take(10);
            //Average Max Count Min Sum
            var maxuprice = db.Products.Max(p => p.UnitPrice);
            Console.WriteLine(maxuprice.Value);
            //join 
            var query = from d in db.Order_Details
                        join order in db.Orders
                        on d.OrderID equals order.OrderID
                        select new
                        {
                            OrderId = order.OrderID,
                            ProductId = d.ProductID,
                            UnitPrice = d.UnitPrice
                        };
            foreach (var q in query)
                Console.WriteLine("{0},{1},{2}", q.OrderId, q.ProductId, q.UnitPrice);

            //group by
           var query1 = from c in db.Categories
                        join p in db.Products
                        on c.CategoryID equals p.CategoryID
                        group new { c, p } by new { c.CategoryName } into g
                        select new
                        {
                            g.Key.CategoryName,
                            SumPrice = (decimal?)g.Sum(pt=>pt.p.UnitPrice),
                            Count = g.Select(x=>x.c.CategoryID).Distinct().Count()
                        };
        }
        static int Add() 
        {
            using (NorthwindEntities db = new NorthwindEntities()) 
            {
                Customers _Customers = new Customers 
                { 
                    CustomerID ="A001",
                    Address = "仁愛區",
                    City="基隆市",
                    Phone = "11111111",
                    CompanyName = "基隆電商",
                    ContactName = "Eddie"
                };
                //Method1 
                //db.Customers.Add(_Customers);

                //Method2
                DbEntityEntry<Customers> entry = db.Entry<Customers>(_Customers);
                entry.State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
            
        }

        static void QueryDelay1() 
        {
            using (NorthwindEntities db = new NorthwindEntities()) 
            {
                DbQuery<Customers> dbQuery = db.Customers.Where(u => u.ContactName == "Eddie")
                    .OrderBy(u => u.ContactName).Take(1) as DbQuery<Customers>;
                //dbQuery 
                Customers _Customers = dbQuery.FirstOrDefault();
                Console.WriteLine(_Customers.ContactName);
              
            }
        }

        static void QueryDelay2() 
        {
            using (NorthwindEntities db = new NorthwindEntities()) 
            {
                IQueryable<Orders> _Orders = db.Orders.Where(a => a.CustomerID == "A001");
                Orders order = _Orders.FirstOrDefault();
                Console.WriteLine(order.Customers.ContactName);
                IQueryable<Orders> orderList = db.Orders;
                foreach (Orders o in orderList)
                    Console.WriteLine(o.OrderID + ":ContactName=" + o.Customers.ContactName);
            }
        }
        /// <summary>
        /// 條件排序查詢
        /// </summary>
        /// <typeparam name="TKey">排序類形</typeparam>
        /// <param name="whereLambda">查詢條件</param>
        /// <param name="orderLambda">排序條件</param>
        /// <returns></returns>
        public List<Customers> GetListBy<TKey>(Expression<Func<Customers,bool>> whereLambda,
                                                Expression<Func<Customers,TKey>> orderLambda)
        {
            using (NorthwindEntities db = new NorthwindEntities()) 
                return db.Customers.Where(whereLambda).OrderBy(orderLambda).ToList();

        }
        /// <summary>
        /// 分頁查詢
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">頁碼</param>
        /// <param name="pageSize">頁容量Lambda</param>
        /// <param name="whereLambda">條件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public List<Customers> GetPagedList<TKey>(int pageIndex, int pageSize,Expression<Func<Customers, bool>> whereLambda,
                                                                              Expression<Func<Customers, TKey>> orderBy)
        {
            using (NorthwindEntities db = new NorthwindEntities())
                return db.Customers.Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        static void Edit() 
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                Customers _Customers = db.Customers.Where(u => u.CustomerID == "A001").FirstOrDefault();
                Console.WriteLine("修改前:" + _Customers.ContactName);
                _Customers.ContactName = "Eddie";
                db.SaveChanges();
                Console.WriteLine("修改成功");
                Console.WriteLine(_Customers.ContactName);
            }
        }

        static void Edit2() 
        {

            Customers _Customers = new Customers() 
            {
              CustomerID ="A001",
              Address = "南山區",
              City = "基隆",
              Phone ="11111111",
              CompanyName = "台北電商",
              ContactName  = "Eddie"
            };

            using (NorthwindEntities db = new NorthwindEntities())
            {
                //物件加入EF對象裡面
                DbEntityEntry<Customers> entry = db.Entry<Customers>(_Customers);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("ContactName").IsModified = true;

                //var u = db.Customers.Attach(_Customers);
                //u.ContactName = "郭";
                db.SaveChanges();
                Console.WriteLine("修改成功:");
                Console.WriteLine(_Customers.ContactName);
            }
        }

        static void Delete() 
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {

                Customers u = new Customers() { CustomerID = "A001"};
                db.Customers.Attach(u);
                db.Customers.Remove(u);

                //DbEntityEntry<Customers> entry = db.Entry<Customers>(u);
                //entry.State = System.Data.Entity.EntityState.Deleted;

                db.SaveChanges();
                Console.WriteLine("刪除成功");
            }
            
        }
        static void SaveBatched() 
        {
            Customers _Customers = new Customers
            {
                CustomerID = "A001",
                Address = "仁愛區",
                City = "基隆市",
                Phone = "11111111",
                CompanyName = "基隆電商",
                ContactName = "Eddie"
            };
            using (NorthwindEntities db = new NorthwindEntities())
            {
                db.Customers.Add(_Customers);
                Customers _Customers2 = new Customers 
                { 
                  CustomerID = "A002",
                  Address = "信義區",
                  City = "台北市",
                  Phone = "11111111",
                  CompanyName = "台北電商",
                  ContactName = "Eddie"
                };
                db.Customers.Add(_Customers2);

                Customers usr = new Customers() 
                {
                  CustomerID="A002",
                  ContactName = "Eddie"
                };
                DbEntityEntry<Customers> entry = db.Entry<Customers>(usr);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("ContactName").IsModified = true;
                Customers u = new Customers() { CustomerID ="A003"};
                db.Customers.Attach(u);
                db.Customers.Remove(u);
                db.SaveChanges();
                Console.WriteLine("批次處理完成");
            }
        }
        static void BatcheAdd() 
        {
            using(NorthwindEntities db = new NorthwindEntities())
            {
                for(int i =0 ;i<50;i++)
                {
                    Customers _Customers = new Customers
                    {
                        CustomerID = "A00" +i,
                        Address = "信義區",
                        City = "台北市",
                        Phone = "22222222",
                        CompanyName = "台北電商",
                        ContactName = "Eddie" + i
                    };
                    db.Customers.Add(_Customers);
                }
                db.SaveChanges();
            }
        }
    }
}

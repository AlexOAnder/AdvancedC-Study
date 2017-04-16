using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtributeLessonBCL1
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorAttribute : Attribute
    {
        public string Name = string.Empty;
        public string Email = string.Empty;
        public CustomAuthorAttribute(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }

    public class OtherModel
    {
        public string AuthorsInfo { get; private set; }

        public OtherModel()
        {
            if (Attribute.IsDefined(this.GetType(), typeof(CustomAuthorAttribute)))
            {
                var attibute = (CustomAuthorAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(CustomAuthorAttribute));
                AuthorsInfo = String.Format("Name is {0}, Email is {1}", attibute.Name, attibute.Email);
            }
            else {
                AuthorsInfo = "Not defined";
            }
           
        }
    }

    [CustomAuthor("Alex", "SomeEmail@gmail.com")]
    public class SecondModel : OtherModel
    {
        // inherits logic from OtherModel class, but have other attribute
    }

    [CustomAuthor("AlexOAnder", "SomeEmail2@gmail.com")]
    public class ThirdModel : OtherModel
    {
        // inherits logic from OtherModel class, but have no attribute
    }

    class Program
    {
        //BSL1) Напишите атрибут, который можно применить только к классу. 
        //Атрибут содержит информацию об авторе кода (имя, e-mail). 
        //Напишите функционал, который позволит вывести эту информацию в консоль.

        static void Main(string[] args)
        {
            OtherModel model = new OtherModel();
            SecondModel model2 = new SecondModel();
            ThirdModel model3 = new ThirdModel();

            Type modelType = model.GetType();
            Type modelType2 = model2.GetType();

            Console.WriteLine(model.AuthorsInfo);
            Console.WriteLine(model2.AuthorsInfo);
            Console.WriteLine(model3.AuthorsInfo);
            Console.ReadLine();
        }
    }
}

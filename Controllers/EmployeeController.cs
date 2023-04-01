using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.Models;
using Npgsql;
using System.Data;

namespace MVC_CRUD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        [HttpGet]
        [Route("Display")]
        //select all data
        public ActionResult Index()
        {
            List<empModels> displayemp= new List<empModels>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=temp;User Id=postgres;Password=4321;");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection= conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT id, name, address FROM public.emp";
            NpgsqlDataReader reader = comm.ExecuteReader();
            while(reader.Read())
            {
                var stlist = new empModels();
                stlist.Id= reader.GetInt32("id");
                stlist.Name= reader.GetString("name");
                stlist.Address= reader.GetString("address");
                displayemp.Add(stlist);
            }
            return Ok(displayemp);
        }

        [HttpGet]
        [Route("DisplayById/{id}")]
        //select onle perfect one data
        public ActionResult Get(int id)
        {
            List<empModels> displayemp = new List<empModels>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=temp;User Id=postgres;Password=4321;");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT id, name, address FROM public.emp where id=@id";
            comm.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                var stlist = new empModels();
                stlist.Id = reader.GetInt32("id");
                stlist.Name = reader.GetString("name");
                stlist.Address = reader.GetString("address");
                displayemp.Add(stlist);
            }
            return Ok(displayemp);
        }
        [HttpDelete]
        [Route("Deleteemp/{id}")]
        public ActionResult Delete(int id)
        {
            
            List<empModels> displayemp = new List<empModels>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=temp;User Id=postgres;Password=4321;");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "DELETE FROM public.emp WHERE id=@id";
            comm.Parameters.AddWithValue("@id", id);
            int a=comm.ExecuteNonQuery();
            if(a==0)
            { return BadRequest("NOT DELETE"); }
            else
            {
                return Ok("Done");
            }       
        }
        [HttpPost]
        [Route("insertemp")]
        public ActionResult insert(empModels emp) 
        {
            List<empModels> displayemp = new List<empModels>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=temp;User Id=postgres;Password=4321;");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "INSERT INTO public.emp(id, name, address) VALUES (@id, @name, @address);";
            comm.Parameters.AddWithValue("@id", emp.Id);
            comm.Parameters.AddWithValue("@name", emp.Name);
            comm.Parameters.AddWithValue("@address", emp.Address);
            int a = comm.ExecuteNonQuery();
            if (a == 0)
            { return BadRequest("NOT Insert"); }
            else
            {
                return Ok("Insert");
            }
        }
        [HttpPut]
        [Route("Updateemp/{id}")]
        public ActionResult update(empModels emp,int id)
        {
            List<empModels> displayemp = new List<empModels>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=temp;User Id=postgres;Password=back1810;");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "UPDATE public.emp SET  name=@name, address=@address WHERE id=@id;";
            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@name", emp.Name);
            comm.Parameters.AddWithValue("@address", emp.Address);
            int a = comm.ExecuteNonQuery();
            if (a == 0)
            { return BadRequest("NOT UPDATE"); }
            else
            {
                return Ok("UPDATE");
            }
        }

    }
}

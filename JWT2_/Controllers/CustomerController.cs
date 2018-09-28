using DB;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWT2_.Controllers
{
    public class CustomerController : ApiController
    {
        [Authorize]
        [RoutePrefix("api/customers")]
        public class CustomersController : ApiController
        {
            [HttpGet]
            public IEnumerable<Student> Get()
            {
                ObservableCollection<Student> products = new ObservableCollection<Student>();
                Conexion _conexion = new Conexion();
                List<SqlParameter> _Parametros = new List<SqlParameter>();
                DataTableReader dtr = null;
                Student p = null;
                _conexion.Conectar();
                _conexion.PrepararProcedimiento("dbo.GetAllStudents", _Parametros);
                dtr = _conexion.EjecutarTableReader();
                while (dtr.Read())
                {
                    p = new Student();
                    p.IdStudent = Int32.Parse(dtr["IdStudent"].ToString());
                    p.Name = dtr["Name"].ToString();
                    p.LastName = dtr["LastName"].ToString();
                    p.LastNameM = dtr["LastNameM"].ToString();
                    p.DOB = DateTime.Parse(dtr["DOB"].ToString());
                    p.Status = Int32.Parse(dtr["Status"].ToString());
                    if (p.Status == 1) products.Add(p);
                }
                _conexion.Desconectar();
                _conexion = null;
                if (dtr != null) { dtr.Close(); dtr.Dispose(); }
                return products;
            }
            [HttpGet]
            public Student Get(int id)
            {
                Conexion _conexion = new Conexion();
                List<SqlParameter> _Parametros = new List<SqlParameter>();
                DataTableReader dtr = null;
                Student p = null;
                _conexion.Conectar();
                _conexion.PrepararProcedimiento("dbo.GetStudent", _Parametros);
                _Parametros.Add(new SqlParameter("@Id", id));
                dtr = _conexion.EjecutarTableReader();
                while (dtr.Read())
                {
                    p = new Student();
                    p.IdStudent = Int32.Parse(dtr["IdStudent"].ToString());
                    p.Name = dtr["Name"].ToString();
                    p.LastName = dtr["LastName"].ToString();
                    p.LastNameM = dtr["LastNameM"].ToString();
                    p.DOB = DateTime.Parse(dtr["DOB"].ToString());
                    p.Status = Int32.Parse(dtr["Status"].ToString());
                }
                _conexion.Desconectar();
                _conexion = null;
                return p;
            }

            [HttpPost]
            public void Post(Student s)
            {
                Conexion _conexion = new Conexion();
                List<SqlParameter> _Parametros = new List<SqlParameter>();
                _conexion.Conectar();
                _conexion.PrepararProcedimiento("dbo.InsertStudent", _Parametros);
                _Parametros.Add(new SqlParameter("@Name", s.Name));
                _Parametros.Add(new SqlParameter("@LastM", s.LastNameM));
                _Parametros.Add(new SqlParameter("@LastP", s.LastName));
                _Parametros.Add(new SqlParameter("@DOB", s.DOB.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                _conexion.EjecutarProcedimiento();
            }

            // PUT: api/Student/5
            [HttpPut]
            public void Put(Student s)
            {
                Conexion _conexion = new Conexion();
                List<SqlParameter> _Parametros = new List<SqlParameter>();
                _conexion.Conectar();
                _conexion.PrepararProcedimiento("dbo.UpdateStudent", _Parametros);
                _Parametros.Add(new SqlParameter("@Name", s.Name));
                _Parametros.Add(new SqlParameter("@LastM", s.LastNameM));
                _Parametros.Add(new SqlParameter("@LastP", s.LastName));
                _Parametros.Add(new SqlParameter("@Id", s.IdStudent));
                _Parametros.Add(new SqlParameter("@DOB", s.DOB.ToString("yyyy-MM-dd HH-mm-ss.fff")));
                _conexion.EjecutarProcedimiento();
            }

            // DELETE: api/Student/5
            [HttpDelete]
            public void Delete(int id)
            {
                Conexion _conexion = new Conexion();
                List<SqlParameter> _Parametros = new List<SqlParameter>();
                _conexion.Conectar();
                _conexion.PrepararProcedimiento("dbo.DeleteStudent", _Parametros);
                _Parametros.Add(new SqlParameter("@Id", id));
                _conexion.EjecutarProcedimiento();
            }
        }

    }
}

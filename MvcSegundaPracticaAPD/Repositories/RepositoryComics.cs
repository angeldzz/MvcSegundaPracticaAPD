using Microsoft.Data.SqlClient;
using MvcSegundaPracticaAPD.Models;
using System.Data;

namespace MvcSegundaPracticaAPD.Repositories
{

    public class RepositoryComics
    {
        private SqlConnection cn;
        private SqlCommand com;
        private DataTable tablaComics;
        public RepositoryComics()
        {
            string connectionString = "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=COMICS;Persist Security Info=True;User ID=SA;Trust Server Certificate=True";
            string sql = "Select * from COMICS";
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            this.tablaComics = new DataTable();
            ad.Fill(this.tablaComics);
            //Para las funciones de accion necesitamos el sql de antes no Linq
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }
        public List<Comic> GetComics()
        {
            List<Comic> comics = new List<Comic>();
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           select datos;
            foreach (var row in consulta)
            {
                Comic comic = new Comic();
                comic.IdComic = Convert.ToInt32(row.Field<int>("IDCOMIC"));
                comic.Nombre = row.Field<string>("NOMBRE");
                comic.Imagen = row.Field<string>("IMAGEN");
                comic.Descripcion = row.Field<string>("DESCRIPCION");
                comics.Add(comic);
            }
            return comics;
        }
        public Comic GetComic_id(int id)
        {
            var consulta = from datos in this.tablaComics.AsEnumerable()
                           where datos.Field<int>("IDCOMIC") == id
                           select datos;
            Comic comic = new Comic();
            foreach (var row in consulta)
            {
                comic.IdComic = Convert.ToInt32(row.Field<int>("IDCOMIC"));
                comic.Nombre = row.Field<string>("NOMBRE");
                comic.Imagen = row.Field<string>("IMAGEN");
                comic.Descripcion = row.Field<string>("DESCRIPCION");
            }
            return comic;
        }
        public int GetMaxId()
        {
            var consulta = (from datos in this.tablaComics.AsEnumerable()
                           select datos.Field<int>("IDCOMIC")).Max();
            return consulta;
        }
        public void CreateComic(Comic comic)
        {
            int maxid = this.GetMaxId() + 1;
            string sql = "INSERT INTO COMICS VALUES (@IDCOMIC, @NOMBRE, @IMAGEN, @DESCRIPCION)";
            this.com.Parameters.AddWithValue("@IDCOMIC", maxid);
            this.com.Parameters.AddWithValue("@NOMBRE", comic.Nombre);
            this.com.Parameters.AddWithValue("@IMAGEN", comic.Imagen);
            this.com.Parameters.AddWithValue("@DESCRIPCION", comic.Descripcion);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();
        }
    }
}

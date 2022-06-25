using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RestDemo.Repositories
{
    public class Connection
    {
        private string _cnnString;
        private SqlConnection _cnn;

        public Connection()
        {
            _cnnString = ConfigurationManager.ConnectionStrings["cn_res_demo"].ConnectionString;
            _cnn = null;
        }

        /// <summary>
        /// Retorna la conexion al servidor de base de datos
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetSqlConnection()
        {
            if (_cnn == null)
            {
                _cnn = new SqlConnection(_cnnString);
            }

            return _cnn;
        }

        /// <summary>
        /// Cierra la conexion existende al servidor de base de datos
        /// </summary>
        public void CloseSqlConnection()
        {
            if (_cnn != null)
            {
                _cnn.Close();
                _cnn.Dispose();
                _cnn = null;
            }
        }

    }
}
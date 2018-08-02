
using QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QueueSystem.Services
{
    public interface IDBConnect
    {

        DataTable Command(string command, string TableName);

    }

    public class SqlConnect : IDBConnect
    {
        private SqlConnection _conn;

        private string connstring = "Data Source=172.16.4.35;Initial Catalog=ALDar_Hospital;Persist Security Info=True;User ID=app;Password=ml1234";

        public SqlConnect()
        {
            _conn = new SqlConnection(connstring);
            _conn.Open();
        }

        public DataTable Command(string command, string TableName)
        {
            SqlCommand comm = new SqlCommand(command, _conn);

            SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();

            mySqlDataAdapter.SelectCommand = comm;

            DataSet myDataSet = new DataSet();

            mySqlDataAdapter.Fill(myDataSet, TableName);

            DataTable myDataTable = myDataSet.Tables[TableName];

            return myDataTable;

        }

        public void InsertCommand(Ticket ticket)
        {
            string f = "";

            if (ticket.Floor == "الدور الأرضي")
            {
                f = "Ticket1";
            }
            else if (ticket.Floor == "الدور الأول")
            {
                f = "Ticket2";
            }

            string query = $"INSERT INTO[dbo].[{f}]( [Speciality], [Doctor], [DoctorCode], [Floor], [Time])    VALUES(@Speciality, @Doctor, @DoctorCode, @Floor, @Time)";

            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@Speciality", SqlDbType.VarChar, 150).Value = ticket.Speciality;
                cmd.Parameters.Add("@Doctor", SqlDbType.VarChar, 150).Value = ticket.Doctor;
                cmd.Parameters.Add("@DoctorCode", SqlDbType.Int, 150).Value = ticket.DoctorCode;
                cmd.Parameters.Add("@Floor", SqlDbType.VarChar, 150).Value = ticket.Floor;
                cmd.Parameters.Add("@Time", SqlDbType.DateTime, 50).Value = DateTime.Now;

                cmd.ExecuteNonQuery();

            }

        }
    }
}


using QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace QueueSystem.Services
{
    public interface ITicketsData
    {
        Ticket Get(string floor);
    }



    public class SqlTicket : ITicketsData
    {

        IDBConnect connect = new SqlConnect();

        public Ticket Get(string floor)
        {
            Ticket ticket = new Ticket();

            string CommandText = "";

            string f = "";


            if (floor == "الدور الأرضي")
            {
                f = "Ticket1";


            }
            else if (floor == "الدور الأول")
            {

                f = "Ticket2";
            }

            CommandText = $"SELECT TOP (1) [ID],[Speciality],[Doctor],[Floor],[Time] FROM [ALDar_Hospital].[dbo].[{f}]  ORDER BY Time DESC";

            DataTable myDataTable = connect.Command(CommandText, f);

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in myDataTable.Rows)
            {
                ticket.ID = Convert.ToInt32(row["ID"]);
                ticket.Speciality = row["Speciality"].ToString();
                ticket.Doctor = row["Doctor"].ToString();
                ticket.Floor = row["Floor"].ToString();
                ticket.Time = Convert.ToDateTime(row["Time"]);
            }

            return ticket;
        }



    }
}

using Microsoft.Data.SqlClient;
using SnapPexOverview.DomainLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace SnapPexOverview.PersistenceLayer
{
    public class MachineRepository : Repository
    {
        private List<Machine> machines = new List<Machine>();




        public Machine GetByID(int machineNr)
        {
            Machine result = null;

            using (SqlConnection con = CreateConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM MACHINE WHERE MachineNr = @MachineNr", con))
            {
                cmd.Parameters.AddWithValue("@machineNr", machineNr);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Machine mac = new Machine(machineNr);

                        mac.MachineNr = (int)reader["MachineNr"];
                        mac.Status = (MachineStatus)(int)reader["MachineStatus"];
                        result = mac;
                    }
                }
            }
            return result;
        }

        public void Update(Machine machine)
        {
            using (SqlConnection con = CreateConnection())
            using (SqlCommand cmd = new SqlCommand("UPDATE MACHINE SET " +
                "MachineStatus = @MachineStatus " +
                "WHERE MachineNr = @machineNr;", con))
            {
                cmd.Parameters.AddWithValue("@MachineStatus", machine.Status);
                cmd.Parameters.AddWithValue("@machineNr", machine.MachineNr);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

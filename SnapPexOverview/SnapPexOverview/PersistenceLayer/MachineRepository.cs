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

        public MachineRepository()
        {
            GetAll();
        }

        // returns id of machine
        public int Add(Machine machine)
        {
            int result = -1;

            using (SqlConnection con = CreateConnection())
            using (SqlCommand cmd = new SqlCommand("spInsertMachine", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MachineStatus", (int)machine.Status);
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }

        public List<Machine> GetAll()
        {
            machines.Clear();

            using(SqlConnection con = CreateConnection())
            using(SqlCommand cmd = new SqlCommand("SELECT * FROM MACHINE", con))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int nr = Convert.ToInt32(reader["MachineNr"]);
                        Machine machine = new Machine(nr);
                        machine.Status = (MachineStatus)(int)reader["MachineStatus"];

                        machines.Add(machine);
                    }
                }
            }

            return machines;
        }

        public Machine GetByID(int machineNr)
        {
            Machine result = null;

            using (SqlConnection con = CreateConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM MACHINE WHERE MachineNr = @MachineNr", con))
            {
                cmd.Parameters.AddWithValue("@MachineNr", machineNr);
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
                cmd.Parameters.AddWithValue("@MachineStatus", (int)machine.Status);
                cmd.Parameters.AddWithValue("@machineNr", machine.MachineNr);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

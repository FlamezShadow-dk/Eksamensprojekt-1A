using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using SnapPexOverview.DomainLayer;
using System.Data;

namespace SnapPexOverview.PersistenceLayer
{
    public class ComponentRepository : Repository
    {
        private List<Component> components = new List<Component>();

        public ComponentRepository()
        {
            GetAll();
        }
            
        public void Add(Component component)
        {
            using (SqlConnection con = CreateConnection())
            using (SqlCommand cmd = new SqlCommand("spInsertComponent", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ComponentName", component.ComponentName);
                cmd.Parameters.AddWithValue("@AmountPerMachine", component.AmountPerMachine);
                cmd.Parameters.AddWithValue("@AmountInStock", component.AmountInStock);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Component> GetAll()
        {
            components.Clear();
            using (SqlConnection con = CreateConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM COMPONENT", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Component component = new Component();
                        component.ComponentName = reader["ComponentName"].ToString();
                        component.AmountPerMachine = (int)reader["AmountPerMachine"];
                        component.AmountInStock = (int)reader["AmountInStock"];
                        components.Add(component);
                    }
                }
            }
            return components;
        }

        //public Component CreateComponent(string NewComponentName, int NewAmountPermachine, int NewAmountInStock)
        //{
        //    Component component = new Component();
        //    component.ComponentName = NewComponentName;
        //    component.AmountPerMachine = NewAmountPermachine;
        //    component.AmountInStock = NewAmountInStock;
        //    return component;
        //}

    }
}

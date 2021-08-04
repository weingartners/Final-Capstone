﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class ForumSqlDao : IForumDao
    {
        private readonly string connectionString;
        public ForumSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Forum CreateForum(string forumTitle)
        {
            Forum returnForum = null;
            int newForumId = 0;

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO forums_list (forum_title) " +
                        "OUTPUT INSERTED.*" +
                        "VALUES (@forum_title)");
                    cmd.Parameters.AddWithValue("@forum_title", forumTitle);

                    newForumId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            throw new NotImplementedException();
        }

        public void DeleteForum(int forumId)
        {
            throw new NotImplementedException();
        }

        public Forum GetForum(int forumId)
        {
            throw new NotImplementedException();
        }

        public List<Forum> GetForums()
        {
            List<Forum> forums = new List<Forum>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT forum_id, forum_title "+
                                                    "FROM forums_list", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        forums.Add(GetForumFromReader(reader));
                    }
                }
            }
            catch(SqlException e)
            {
                throw;
            }
            return forums;
        }

        private Forum GetForumFromReader(SqlDataReader reader)
        {
            Forum f = new Forum()
            {
                ForumId = Convert.ToInt32(reader["forum_id"]),
                ForumTitle = Convert.ToString(reader["forum_title"])
            };
            return f;
        }
    }
}

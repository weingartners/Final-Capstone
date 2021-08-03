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
    public class ReplySqlDao : IReplyDao
    {
        private readonly string connectionString;
        public ReplySqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Reply GetReply(int postId, string username, string content, DateTime postedDate)
        {
            Reply returnReply = null; 

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("  SELECT * " +
                        "FROM replies " +
                        "WHERE post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        returnReply = GetReplyFromReader(reader);
                    }
                }
            }
            catch  (SqlException e)
            {
                throw;
            }

            return returnReply;
        }

        public List<Reply> GetReplies(int postId)
        {
            List<Reply> replies = new List<Reply>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT reply_id, post_id, username, content, posted_date " +
                        "FROM replies " +
                        "WHERE post_id = @postId", conn);
                    cmd.Parameters.AddWithValue("post_id", postId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        replies.Add(GetReplyFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return replies;
        }
        public Reply CreateReply(int postId, string username, string content)
        {
            throw new NotImplementedException();
        }

        private Reply GetReplyFromReader(SqlDataReader reader)
        {
            Reply r = new Reply()
            {
                ReplyId = Convert.ToInt32(reader["reply_id"]),
                PostId = Convert.ToInt32(reader["post_id"]),
                Username = Convert.ToString(reader["username"]),
                Content = Convert.ToString(reader["content"]),
                PostedDate = Convert.ToDateTime(reader["posted_date"])
            };
            return r;
        }

    }
}

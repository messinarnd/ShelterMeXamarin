﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShelterMeObjects;

namespace ShelterMeDataAccess {
    public class UserInfoData {
        UserInfo userInfo = new UserInfo();
        public Dictionary<string, string> getUsernameAndPassword() {
            Dictionary<string, string> user = new Dictionary<string, string>();
            using (SqlConnection conn = new SqlConnection(
                "Data Source = tcp:shelterme.database.windows.net,1433; Initial Catalog = ShelterMe; User ID = Harsh_Jain; Password = Nadiad1998")) {
                SqlDataReader rdr = null;
                conn.Open();
                SqlCommand command = new SqlCommand(@"SELECT EMAIL, PASSWORD FROM USER_INFORMATION", conn);
                rdr = command.ExecuteReader();
                while (rdr.Read()) {
                    userInfo.Email = rdr[0].ToString();
                    userInfo.Password = rdr[1].ToString();
                    user.Add(userInfo.Email, userInfo.Password);
                }
                conn.Close();
            }
            return user;
        }

        public List<string> getPassword() {
            List<string> password = new List<string>();
            using (SqlConnection conn = new SqlConnection(
                "Data Source=tcp:shelterme.database.windows.net,1433;Initial Catalog=ShelterMe;User ID=Harsh_Jain;Password=Nadiad1998")) {
                SqlDataReader rdr = null;
                conn.Open();
                SqlCommand command = new SqlCommand(@"SELECT PASSWORD FROM USER_INFORMATION", conn);
                rdr = command.ExecuteReader();
                while (rdr.Read()) {
                    userInfo.Password = rdr[0].ToString();
                    password.Add(userInfo.Password);
                }
                conn.Close();
            }
            return password;
        }
        public UserInfo getUserData(string username) {
            UserInfo user = new UserInfo();
            using (SqlConnection conn = new SqlConnection(
                "Data Source=tcp:shelterme.database.windows.net,1433;Initial Catalog=ShelterMe;User ID=Harsh_Jain;Password=Nadiad1998")) {
                SqlDataReader rdr = null;
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM USER_INFORMATION WHERE EMAIL='{username}'", conn);
                rdr = command.ExecuteReader();
                while (rdr.Read()) {
                    user.FirstName = rdr[0].ToString();
                    user.LastName = rdr[1].ToString();
                    user.Email = rdr[2].ToString();
                    user.Password = rdr[3].ToString();
                    user.UserType = rdr[4].ToString();
                }
                conn.Close();
            }
            return user;
        }
        public void EnterUserData(string firstName, string lastName, string email, string password, string userType) {
            using (SqlConnection conn = new SqlConnection(
                "Data Source=tcp:shelterme.database.windows.net,1433;Initial Catalog=ShelterMe;User ID=Harsh_Jain;Password=Nadiad1998")) {
                conn.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO USER_INFORMATION VALUES('{firstName}', '{lastName}', '{email}', '{password}', '{userType}')", conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}

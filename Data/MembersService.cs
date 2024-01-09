using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class MembersService
    {
        public static List<Member> GetAll()
        {
            string appMemberFilePath = Utils.GetAppMemberFilePath();
            if (!File.Exists(appMemberFilePath))
            {
                return new List<Member>();
            }

            var json = File.ReadAllText(appMemberFilePath);

            return JsonSerializer.Deserialize<List<Member>>(json);
        }



        public static List<Member> Create(string UserName, string MemberName, string PhoneNumber)
        {
            List<Member> members = GetAll();
            bool membernameExists = members.Any(x => x.Name == MemberName);

            if (membernameExists)
            {
                throw new Exception("Membername already exists.");
            }

            members.Add(
                new Member
                {
                    Name = MemberName,
                    Username = UserName,
                    PhoneNumber = PhoneNumber


                }
            );
            SaveAll(members);
            return members;
        }

        private static void SaveAll(List<Member> members)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appMemberFilePath = Utils.GetAppMemberFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(members);
            File.WriteAllText(appMemberFilePath, json);
        }



        public static List<Member> Delete(Guid id)
        {
            List<Member> members = GetAll();
            Member member = members.FirstOrDefault(x => x.Id == id);

            if (member == null)
            {
                throw new Exception("Member not found.");
            }


            members.Remove(member);
            SaveAll(members);

            return members;
        }

        public static List<Member> Update(string MemberName, string UserName, string PhoneNumber)
        {
            List<Member> members = GetAll();
            Member member = members.FirstOrDefault(x => x.Username == UserName);

            if (member == null)
            {
                throw new Exception("Member not found.");
            }


            member.Name = MemberName;
            member.PhoneNumber = PhoneNumber;
            member.Username = UserName;


            SaveAll(members);
            return members;
        }
    }
}


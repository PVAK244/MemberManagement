using BusinessObject;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class MemberDAO
    {
        private static List<MemberObject> MemberList = new List<MemberObject>()
        {
            new MemberObject{ MemberID=1, MemberName="John Sena", Email="johnSena@fstore.com", Password="123456", City="New York", Country="America"},
            new MemberObject{ MemberID=2, MemberName="Harry Potter", Email="harryPotter@fstore.com", Password="123456", City="London", Country="England"}
        };
        private static MemberDAO instance = null;
        private static readonly object instanceLook = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock(instanceLook){
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                }
                return instance;
            }
        }
        public List<MemberObject> GetMemberList()
        {
            var ketqua = from members in MemberList
                         orderby members.MemberName descending
                         select members;
            List<MemberObject> list = new List<MemberObject>();
            foreach (MemberObject member in ketqua) list.Add(member);
            return list;
        }
        public MemberObject GetMemberByID(int memberID)
        {
            MemberObject member = MemberList.SingleOrDefault(pro => pro.MemberID == memberID);
            return member;
        }
        public void AddNew(MemberObject member)
        {
            MemberObject pro = GetMemberByID(member.MemberID);
            if (pro == null)
            {
                MemberList.Add(member);
            }
            else
            {
                throw new Exception("Member is already exists.");
            }
        }
        public void Update(MemberObject member)
        {
            MemberObject m = GetMemberByID(member.MemberID);
            if (m != null)
            {
                var index = MemberList.IndexOf(m);
                MemberList[index] = member;
            }
            else
            {
                throw new Exception("Member does not already exists.");
            }
        }
        public void Remove(int memberID)
        {
            MemberObject m = GetMemberByID(memberID);
            if (m != null)
            {
                MemberList.Remove(m);
            }
            else
            {
                throw new Exception("Member does not already exists.");
            }
        }

        public List<MemberObject> GetMemberByName(string name)
        {
            var ketqua = from members in MemberList
                         where members.MemberName.ToLower().Contains(name)
                         select members;
            List<MemberObject> list = new List<MemberObject>();
            foreach (MemberObject member in ketqua) list.Add(member);
            return list;
        }

        public List<MemberObject> SortMembers(string filter)
        {
            var ketqua = from members in MemberList
                         select members; ;
            if (filter.ToLower().Equals("city"))
            {
                ketqua = from members in MemberList
                             orderby members.City ascending
                             select members;
            }
            if (filter.ToLower().Equals("country")){
                ketqua = from members in MemberList
                             orderby members.Country ascending
                             select members;
            }
            
            List<MemberObject> list = new List<MemberObject>();
            foreach (MemberObject member in ketqua) list.Add(member);
            return list;
        }
    }


}

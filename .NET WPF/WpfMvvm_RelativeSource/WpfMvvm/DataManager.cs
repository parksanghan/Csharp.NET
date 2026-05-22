using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvm.Models;

namespace WpfMvvm
{
    internal static class DataManager
    {

        private static List<MemberInfoModel> _db = new List<MemberInfoModel>
        {
            new MemberInfoModel()
            {
                ID="id123",
                Name = "홍길동",
                Contact = "010-1234-5678",
                Address = "서울시",
                Birth = new DateTime( 2022, 11, 11 ),
                JoinDate = new DateTime( 2022, 11, 10 )
            },
            new MemberInfoModel()
            {
                ID="id456",
                Name = "고길동",
                Contact = "010-1111-2222",
                Address = "부산시",
                Birth = new DateTime( 2022, 10, 22 ),
                JoinDate = new DateTime( 2022, 8, 6 )
            }
        };

        public static MemberInfoModel FindMemberInfo(string id )
        {
            // DB에서 데이터를 검색한다고 가정.
            // query = $"SELECT  ID
            //                ,  Name
            //                ,  Contact
            //                ,  Address
            //                ,  Birth
            //                ,  JoinDate
            //             FROM  member_info_table
            //            WHERE  ID='{id}'"


            var res = _db.Where( i => i.ID == id ).Select( i => i ).ToList();

            if ( res.Count != 0 ) return res[0];
            else return new MemberInfoModel
            {
                ID = "-",
                Name = "-",
                Contact = "-",
                Address = "-",
                Birth = null,
                JoinDate = null
            };
        }
    }
}

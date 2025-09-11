using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;          // Include 확장 메서드
namespace DXWebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new MyDBEntities())
            {
                var list = context.Product
             .AsNoTracking()
             .Include(p => p.Category)
             .Include(p => p.Manufacturer)
             .Select(p => new {
                 p.ProductId,
                 p.ProductName,
                 p.ProductPrice,
                 CategoryName = p.Category.CategoryName,      
                 ManufacturerName = p.Manufacturer.ManufacturerName
             })
             .ToList();
                //ASPxGridView1.AutoGenerateColumns = true;

                //ASPxGridView1.DataSource = list;
                //ASPxGridView1.DataBind();                    
            }
        }
    }
}
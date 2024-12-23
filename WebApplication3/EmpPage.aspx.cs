using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication3.Model;

namespace WebApplication3
{
    public partial class EmpPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             BindView();
                    }

        }

        private void BindView()
        {
            EmployeeDAL empDAL = new EmployeeDAL();

            List<Employee> employees = empDAL.GetEmployees();

            GridViewEmployees.DataSource = employees;
            GridViewEmployees.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee
            {
                EmpName = txtEmpName.Text,
                Position = txtPosition.Text,
                Salary = Convert.ToDecimal(txtSalary.Text),
            };
            EmployeeDAL empDAL = new EmployeeDAL();
            empDAL.AddEmp(emp);
            BindView();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32(txtId.Text);
            Employee emp = new Employee
            {
                Id = empId,
                EmpName = txtEmpName.Text,

                Position = txtPosition.Text,
                Salary = Convert.ToDecimal(txtSalary.Text)
            };
            EmployeeDAL empDAL = new EmployeeDAL();
            empDAL.UpdateEmp(emp);
            BindView();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32(txtId.Text);

            EmployeeDAL empDAL = new EmployeeDAL();
            empDAL.DeleteEmp(empId);
            BindView();

        }

        protected void txtId_TextChanged(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32(txtId.Text);
            EmployeeDAL empDAL =new EmployeeDAL(); 
            Employee employee =  empDAL.GetEmpById(empId);
            if (employee != null)
            {
                txtEmpName.Text = employee.EmpName;
                txtPosition.Text = employee.Position;
                txtSalary.Text = Convert.ToDecimal(employee.Salary).ToString();
                txtId.Text = Convert.ToInt32(employee.Id).ToString();
            }
        }
    }
}
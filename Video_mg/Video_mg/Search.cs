﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Video_mg
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
            DataLoad();
        }
        SqlConnection sql;
        SqlCommand sqlCS;
        SqlDataAdapter sqlAS;
        DataSet DSS;

        public void DataLoad()
        {
            string s = @"server = jump8779;database = Videomg;uid = sa; pwd = std001";
            sql = new SqlConnection(s);
        }

        private void Bt_search_Click(object sender, EventArgs e)
        {
            string s, Os, Os1;
            if (rb_1.Checked == true)
            {
                if (TBgenre.Text == "전체")
                {
                    Os = "V.vcode = L.vcode";
                }
                else
                {
                    Os = "L.vcode = V.vcode AND V.genre = @genre";
                }
                s = "SELECT 'lcount'=COUNT(L.vcode)," +
                    "V._subject,V.genre,V.out_date " +
                    "FROM videomg as V INNER JOIN lent as L " +
                    "ON " + Os + " GROUP BY V.genre,V._subject,V.out_date " +
                    "ORDER BY COUNT(L.vcode) DESC";
            }
            else if (rb_3.Checked == true)
            {
                if (TBsinbun.Text == "전체")
                {
                    Os = "M.mcode=L.mcode OR M.mcode=-(L.mcode)";
                }
                else
                {
                    Os = "(M.mcode=L.mcode OR M.mcode=-(L.mcode))" +
                        "AND M.sinbun=@sinbun";
                }
                s = "SELECT 'mcount'=COUNT(M.mcode)," +
                    "M.iname,M.sinbun,M.sex,M.phone,M.pcs,M._address " +
                    "FROM membermg as M INNER JOIN lent as L " +
                    "ON " + Os + " GROUP BY M.iname,M.sinbun,M.sex,M.phone,M.pcs,M._address " +
                    "ORDER BY COUNT(M.mcode) DESC";
            }
            else if (rb_2.Checked == true)
            {
                if (TBgenre.Text == "전체")
                {
                    Os = "L.mcode=M.mcode AND L.return_date2='대여중'";
                    Os1 = "L.vcode=V.vcode";
                }
                else
                {
                    Os = "L.mcode=M.mcode AND L.return_date2='대여중'";
                    Os1 = "L.vcode=V.vcode AND V.genre=@genre";
                }
                s = "SELECT V._subject,V.genre,L.lent_date," +
                    "L.return_date1,return_date2,M.iname,M.phone " +
                    "FROM membermg as M INNER JOIN lent as L " +
                    "ON " + Os + " INNER JOIN videomg as V " +
                    "ON " + Os1 + " ORDER BY V._subject ASC";
            }
            else
            {
                s = "";
            }

            sqlCS = new SqlCommand(s, sql);
            sqlCS.Parameters.Add("@genre", SqlDbType.VarChar, 20);
            sqlCS.Parameters.Add("@sinbun", SqlDbType.VarChar, 10);
            sqlCS.Parameters["@genre"].Value = TBgenre.Text;
            sqlCS.Parameters["@sinbun"].Value = TBsinbun.Text;

            sqlAS = new SqlDataAdapter();
            sqlAS.SelectCommand = sqlCS;
            try
            {
                DSS = new DataSet();
                DSS.Clear();
                sqlAS.TableMappings.Add("Table", "Search");
                sqlAS.Fill(DSS, "Search");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dgSearch.DataSource = DSS;
            dgSearch.DataMember = "Search";
        }

        private void Bt_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Rb_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1.Checked == true)
            {
                TBgenre.Enabled = true;
                TBsinbun.Enabled = false;
            }
        }

        private void Rb_2_CheckedChanged(object sender, EventArgs e)
        {
            if(rb_2.Checked == true)
            {
                TBgenre.Enabled = true;
                TBsinbun.Enabled = false;
            }
        }

        private void Rb_3_CheckedChanged(object sender, EventArgs e)
        {
            if(rb_3.Checked == true)
            {
                TBgenre.Enabled = false;
                TBsinbun.Enabled = true;
            }
        }
    }
}

using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetoCalculoSalario.Entites.BLL;
using ProjetoCalculoSalario.Entites.DTO;
using Microsoft.VisualBasic;

namespace ProjetoCalculoSalario
{
    public partial class CalculadoraSalario : Form
    {
        CalculoBLL bll = new CalculoBLL();
        CalculoDTO dto = new CalculoDTO();


        public CalculadoraSalario()
        {
            InitializeComponent();
        }

        private void CalculadoraSalario_Load(object sender, EventArgs e)
        {

        }

        private void txtDias_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSalario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAdiantamento_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCalc75_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBaseCalcInss_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCacladicional_TextChanged(object sender, EventArgs e)
        {

        }

        public Boolean IsBrazilNationalHoliday(DateTime checkDate)
        {
            DateTime dateToBeChecked = new DateTime(checkDate.Year, checkDate.Month, checkDate.Day);
            int anoCheckDate = checkDate.Year;
            int a = (int)anoCheckDate % 4;
            int b = (int)anoCheckDate % 7;
            int c = (int)anoCheckDate % 19;
            int d = (int)((19 * c) + 24) % 30;
            int e = (int)((2 * a) + (4 * b) + (6 * d) + 5) % 7;
            int diaPascoa = 22 + d + e;
            int mesPascoa = 3;
            if (diaPascoa > 31)
            {
                diaPascoa = d + e - 9;
                mesPascoa = 4;
            }
            List<String> feriados = new List<String>();
            feriados.Add(new DateTime(anoCheckDate, mesPascoa, diaPascoa).ToString()); //Pascoa
            feriados.Add(new DateTime(anoCheckDate, mesPascoa, diaPascoa).AddDays(-2).ToString()); //Sexta Feira da Paixão
            feriados.Add(new DateTime(anoCheckDate, mesPascoa, diaPascoa).AddDays(60).ToString()); //Corpus Cristi
            feriados.Add(new DateTime(checkDate.Year, mesPascoa, diaPascoa).AddDays(-47).ToString()); //Carnaval
            feriados.Add(new DateTime(anoCheckDate, 1, 1).ToString()); //Dia de Ano Novo (Confraternização Universal)
            feriados.Add(new DateTime(anoCheckDate, 4, 21).ToString()); //Dia de Tiradentes
            feriados.Add(new DateTime(anoCheckDate, 5, 1).ToString()); //Dia do Trabalho
            feriados.Add(new DateTime(anoCheckDate, 7, 1).ToString()); // Nao tem Feriado nesse Mês
            feriados.Add(new DateTime(anoCheckDate, 8, 1).ToString()); // Nao tem Feriado nesse Mês
            feriados.Add(new DateTime(anoCheckDate, 9, 7).ToString()); //Dia da Independência
            feriados.Add(new DateTime(anoCheckDate, 10, 12).ToString()); //Dia da Padroeira do Brasil
            feriados.Add(new DateTime(anoCheckDate, 11, 2).ToString()); //Dia de Finados
            feriados.Add(new DateTime(anoCheckDate, 11, 15).ToString()); //Data da Proclamação da República
            feriados.Add(new DateTime(anoCheckDate, 11, 20).ToString()); //Dia da Conciência Negra
            feriados.Add(new DateTime(anoCheckDate, 12, 25).ToString()); //Natal
            return feriados.Contains(dateToBeChecked.ToString());
        }

        public void CalcularSalario()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int dias = int.Parse(txtDias.Text.ToString());
            int mes = System.DateTime.DaysInMonth(year, month);
            double hora = double.Parse(txtSalarioBase.Text.ToString());
            if (dias < 30)
            {
                dias = System.DateTime.DaysInMonth(year, month) + 1;

            }
            else if (dias > 30)
            {
                dias = System.DateTime.DaysInMonth(year, month) - 1;
            }
            else
            {
                dias = System.DateTime.DaysInMonth(year, month);
            }
            txtDias.Text = mes.ToString() ;
            txSalario.Text = (hora * dias * 7.33333).ToString("N2");
            txSalario.Enabled = false;
            txtSalarioBase.Enabled = false;
        }

        public void Adiantamento()
        {
            double adian = double.Parse(txSalario.Text.ToString());
            txtAdiantamento.Text = (adian * 0.40).ToString("N2");
            txtAdiantamento.Enabled = false;
        }

        public void HExtra75()
        {
            double extra75 = double.Parse(txtHExtra75.Text.ToString());
            double hora = double.Parse(txtSalarioBase.Text.ToString());
            txtCalc75.Text = (hora * extra75 * 1.75).ToString("N2");
            txtCalc75.Enabled = false;
            txtHExtra75.Enabled = false;
        }

        public void HExtra100()
        {
            double extra100 = double.Parse(txtHExtra100.Text.ToString());
            double hora = double.Parse(txtSalarioBase.Text.ToString());
            txtCalc100.Text = (hora * extra100 * 2.0).ToString("N2");
            txtCalc100.Enabled = false;
            txtHExtra100.Enabled = false;
        }

        public void DSR()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int domingos = 0;
            int diasNoMes = DateTime.DaysInMonth(year, month);
            int domingosDoMes = 0;
            for (int i = 1; i <= diasNoMes; i++)
            {
                if (!IsBrazilNationalHoliday(new DateTime(year, month, i))
                    && (int)new DateTime(year, month, i).DayOfWeek != 1
                    && (int)new DateTime(year, month, i).DayOfWeek != 2
                    && (int)new DateTime(year, month, i).DayOfWeek != 3
                    && (int)new DateTime(year, month, i).DayOfWeek != 4
                    && (int)new DateTime(year, month, i).DayOfWeek != 5
                    && (int)new DateTime(year, month, i).DayOfWeek != 6
                    )
                {
                    domingosDoMes++;
                }
            }
            domingos = domingosDoMes;
            double dsr = double.Parse(txtDSR.Text.ToString());
            double extra75 = double.Parse(txtCalc75.Text.ToString());
            double extra100 = double.Parse(txtCalc100.Text.ToString());
            if (extra75 != 0 || extra100 != 0)
            {
                txtDSR.Text = domingos.ToString();
            }
            txtCalcDsr.Text = ((extra75 + extra100) / (30 - domingos) * domingos).ToString("N2");
            txtCalcDsr.Enabled = false;
            txtDSR.Enabled = false;
        }

        public void AdicionaNoturno()
        {
            double adicional = double.Parse(txtANoturno.Text.ToString());
            double hora = double.Parse(txtSalarioBase.Text.ToString());
            txtCacladicional.Text = (hora * 0.35 * adicional).ToString("N2");
            txtCacladicional.Enabled = false;
            txtANoturno.Enabled = false;
        }

        public void AtrazosFaltas()
        {
            double atrazos = double.Parse(txtAtrazos.Text.ToString());
            double hora = double.Parse(txtSalarioBase.Text.ToString());
            txtCalcAtrazos.Text = (hora * atrazos).ToString("N2");
            txtCalcAtrazos.Enabled = false;
            txtAtrazos.Enabled = false;
        }

        public void BaseInss()
        {
            double salario = double.Parse(txSalario.Text.ToString());
            double extra75 = double.Parse(txtCalc75.Text.ToString());
            double extra100 = double.Parse(txtCalc100.Text.ToString());
            double adicional = double.Parse(txtCacladicional.Text.ToString());
            double dsr = double.Parse(txtCalcDsr.Text.ToString());
            double atrazos = double.Parse(txtCalcAtrazos.Text.ToString());
            double desconto = double.Parse(txtDescontos.Text.ToString());
            txtBaseCalcInss.Text = (salario + extra75 + extra100 + adicional + dsr - atrazos - desconto).ToString("N2");
            txtBaseCalcInss.Enabled = false;
            txtDescontos.Enabled = false;
        }

        public void DescontoInss()
        {
            double inss = double.Parse(txtBaseCalcInss.Text.ToString());
            if (inss < 1039.01)
            {
                txtDescontoInss.Text = (inss * 0.075).ToString("N2");
            }
            else if (inss < 2089.61)
            {
                txtDescontoInss.Text = ((inss - 1039.00) * 0.09 + 1039.00 * 0.075).ToString("N2");
            }
            else if (inss < 3134.41)
            {
                txtDescontoInss.Text = ((inss - 2089.61) * 0.12 + (2089.60 - 1039.01) * 0.09 + 1039.00 * 0.075).ToString("N2");
            }
            else if (inss < 6101.07)
            {
                txtDescontoInss.Text = ((inss - 3134.40) * 0.14 + (3134.40 - 2089.61) * 0.12 + (2089.60 - 1039.01) * 0.09 + 1039.00 * 0.075).ToString("N2");
            }
            else
            {
                txtDescontoInss.Text = ((6101.06 - 3134.41) * 0.14 + (3134.40 - 2089.61) * 0.12 + (2089.60 - 1039.01) * 0.09 + 1039.00 * 0.075).ToString("N2");
            }
        }

        public void AliquotaInss()
        {
            double desconto = double.Parse(txtDescontoInss.Text.ToString());
            double inss = double.Parse(txtBaseCalcInss.Text.ToString());
            if (inss < 6101.07)
            {
                txtAliquotaInss.Text = (((desconto - inss) / inss + 1) * 100).ToString("N2") + '%';
            }
            else
            {
                txtAliquotaInss.Text = (((desconto - 6101.06) / 6101.06 + 1) * 100).ToString("N2") + '%';
            }
        }

        public void BaseIrrf()
        {
            double irrf = double.Parse(txtBaseCalcInss.Text.ToString());
            double descontoIrrf = double.Parse(txtDescontoInss.Text.ToString());
            int dependente = int.Parse(numericDependente.Value.ToString());
            txtBaseCalcIrrf.Text = (irrf - descontoIrrf - dependente * 189.59).ToString("N2");
            txtBaseCalcIrrf.Enabled = false;
        }

        public void DescontoIrrf()
        {
            double irrf = double.Parse(txtBaseCalcIrrf.Text.ToString());
            if (irrf < 1903.99)
            {
                txtDescontoIrrf.Text = 0.00.ToString("N2");
            }
            else if (irrf < 2826.66)
            {
                txtDescontoIrrf.Text = (irrf * 0.075 - 142.80).ToString("N2");
            }
            else if (irrf < 3751.06)
            {
                txtDescontoIrrf.Text = (irrf * 0.15 - 354.80).ToString("N2");
            }
            else if (irrf < 4664.69)
            {
                txtDescontoIrrf.Text = (irrf * 0.225 - 636.13).ToString("N2");
            }
            else
            {
                txtDescontoIrrf.Text = (irrf * 0.275 - 869.36).ToString("N2");
            }
        }

        public void AliquotaIrrf()
        {
            double irrf = double.Parse(txtBaseCalcIrrf.Text.ToString());
            if (irrf < 1903.99)
            {
                txtAliquotaIrrf.Text = ("").ToString();
            }
            else if (irrf < 2826.66)
            {
                txtAliquotaIrrf.Text = (7.5).ToString("N2") + "%";
            }
            else if (irrf < 3751.06)
            {
                txtAliquotaIrrf.Text = (15.0).ToString("N2") + "%";
            }
            else if (irrf < 4664.69)
            {
                txtAliquotaIrrf.Text = (22.5).ToString("N2") + "%";
            }
            else
            {
                txtAliquotaIrrf.Text = (27.5).ToString("N2") + "%";
            }
        }

        public void Fgts()
        {
            double fgts = double.Parse(txtBaseCalcInss.Text.ToString());
            txtFgts.Text = (fgts * 0.08).ToString("N2");
            txtFgts.Enabled = false;
        }

        public void Descontos()
        {
            double descontos = double.Parse(txtDescontos.Text.ToString());
            double atrazos = double.Parse(txtCalcAtrazos.Text.ToString());
            double descontoIrrf = double.Parse(txtDescontoInss.Text.ToString());
            double descontoInss = double.Parse(txtDescontoIrrf.Text.ToString());
            double adiantamento = double.Parse(txtAdiantamento.Text.ToString());
            txtDesconto.Text = (descontos + atrazos + descontoIrrf + descontoInss + adiantamento).ToString("N2");
            txtDesconto.Enabled = false;
        }

        public void Vencimentos()
        {
            double vencimentos = double.Parse(txtBaseCalcInss.Text.ToString());
            txtVencimentos.Text = vencimentos.ToString("N2");
            txtVencimentos.Enabled = false;
        }

        public void SalarioLiquido()
        {
            double vencimentos = double.Parse(txtBaseCalcInss.Text.ToString());
            double descontos = double.Parse(txtDesconto.Text.ToString());
            txtSalarioLiquido.Text = (vencimentos - descontos).ToString("N2");
            txtSalarioLiquido.Enabled = false;
        }

        private void buttonCalculo_Click(object sender, EventArgs e)
        {
            DesabilitaComandos();
            CalcularSalario();
            Adiantamento();
            HExtra75();
            HExtra100();
            DSR();
            AdicionaNoturno();
            AtrazosFaltas();
            BaseInss();
            DescontoInss();
            AliquotaInss();
            BaseIrrf();
            DescontoIrrf();
            AliquotaIrrf();
            Fgts();
            Descontos();
            Vencimentos();
            SalarioLiquido();
            btnCalcular.Enabled = false;
            btnNovoCalculo.Enabled = true;
            MessageBox.Show("Cáculo efetuado com sucesso!" , "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DesabilitaComandos()
        {
            btnNovoCalculo.Enabled = false;
            btnCalcular.Enabled = false;
            txtCabecalho.Enabled = false;
            txtDias.Enabled = false;
            txtDescontoIrrf.Enabled = false;
            txtDescontoInss.Enabled = false;
            txtAliquotaInss.Enabled = false;
            txtAliquotaIrrf.Enabled = false;
            numericDependente.Enabled = false;
            txt29.Enabled = false;
            txt28.Enabled = false;
            txt30.Enabled = false;
            txt31.Enabled = false;
            txt32.Enabled = false;
            txt33.Enabled = false;
            txt34.Enabled = false;
            txt35.Enabled = false;
            txt36.Enabled = false;
            txt37.Enabled = false;
            txt38.Enabled = false;
            txt39.Enabled = false;
            txt40.Enabled = false;
            txt41.Enabled = false;
            txt42.Enabled = false;
            txt43.Enabled = false;
            txt5.Enabled = false;
            txt1.Enabled = false;
            txt2.Enabled = false;
            txt3.Enabled = false;
            txt6.Enabled = false;
            txt24.Enabled = false;
            txt25.Enabled = false;
            txt26.Enabled = false;
            txt27.Enabled = false;
            txt17.Enabled = false;
            txt18.Enabled = false;
            txt19.Enabled = false;
            txt20.Enabled = false;
            txt21.Enabled = false;
            txt22.Enabled = false;
            txt23.Enabled = false;
            txt10.Enabled = false;
            txt9.Enabled = false;
            txt8.Enabled = false;
            txt11.Enabled = false;
            txt12.Enabled = false;
            txt13.Enabled = false;
            txt14.Enabled = false;
            txt15.Enabled = false;
            txt16.Enabled = false;
            txt17.Enabled = false;
            txt7.Enabled = false;
        }

        private void btnNovoCalculo_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Gotaria de efetuar um novo cálculo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                MessageBox.Show("Registro apagado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limparTextBoxes(Controls);
                Application.Restart();
            }
            else
            {
                Application.Exit();
            }

        }

        private void limparTextBoxes(Control.ControlCollection controles)
        {
            //Faz um laço para todos os controles passados no parâmetro
            foreach (Control ctrl in controles)
            {
                //Se o contorle for um TextBox...
                if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = String.Empty;
                }
            }
        }
    }
}

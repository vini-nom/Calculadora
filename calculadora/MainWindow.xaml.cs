using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculadora
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        double ultimoNumero, resultado;
        operacaoSelecionada operacaoSelecionada;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_ac_Click(object sender, RoutedEventArgs e)
        {
            lbl_resultado.Content = "0";
            resultado = 0;
            ultimoNumero = 0;
        }

        private void btn_num(object sender, RoutedEventArgs e)
        {
            int num = int.Parse((sender as Button).Content.ToString());

            if (lbl_resultado.Content.ToString() == "0")
            {
                lbl_resultado.Content = $"{num}";
            }
            else 
            {
                lbl_resultado.Content = $"{lbl_resultado.Content}{num}";
            }
        }

        private void btn_porcentagem_Click(object sender, RoutedEventArgs e)
        {
            double tempNum;
            if (double.TryParse(lbl_resultado.Content.ToString(), out tempNum))
            {
                tempNum /= 100;
                if (ultimoNumero != 0) 
                {
                    tempNum *= ultimoNumero;
                }
                lbl_resultado.Content = tempNum.ToString();
            }
        }

        private void btn_operacao_click(object sender, RoutedEventArgs e)
        { 
            if (double.TryParse(lbl_resultado.Content.ToString(), out ultimoNumero))
            {
                lbl_resultado.Content = "0";
            }

            if (sender == btn_soma)
            {
                operacaoSelecionada = operacaoSelecionada.adicao;
            }
            else if (sender == btn_subtracao)
            {
                operacaoSelecionada = operacaoSelecionada.subtracao;
            }
            else if (sender == btn_multiplicacao)
            {
                operacaoSelecionada = operacaoSelecionada.multiplicacao;
            }
            else if (sender == btn_divisao) 
            {
                operacaoSelecionada = operacaoSelecionada.divisao;
            }
        }

        private void btn_calcula_Click(object sender, RoutedEventArgs e)
        {
            double novoNumero;

            if (double.TryParse(lbl_resultado.Content.ToString(), out novoNumero))
            {
                switch (operacaoSelecionada) 
                {
                    case operacaoSelecionada.adicao:
                        resultado = SimpleMath.adicao(ultimoNumero, novoNumero);
                        break;

                    case operacaoSelecionada.subtracao:
                        resultado = SimpleMath.subtracao(ultimoNumero, novoNumero);
                        break;

                    case operacaoSelecionada.multiplicacao:
                        resultado = SimpleMath.multiplicacao(ultimoNumero, novoNumero);
                        break;

                    case operacaoSelecionada.divisao:
                        if (ultimoNumero == 0 | novoNumero == 0)
                        {
                            MessageBox.Show("Impossível dividir números por zero", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else 
                        {
                            resultado = SimpleMath.divisao(ultimoNumero, novoNumero);
                        }
                        
                        break;
                }

                lbl_resultado.Content = resultado.ToString();
            }

        }

        private void btn_ponto_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_resultado.Content.ToString().Contains(","))
            {
                //não adicione o ponto
            }
            else {
                lbl_resultado.Content = $"{lbl_resultado.Content},";
            }
        }

        private void btn_negativo_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(lbl_resultado.Content.ToString(), out ultimoNumero)) {
                        ultimoNumero *= -1;
                        lbl_resultado.Content = ultimoNumero.ToString();
            }
        }
    }

    public enum operacaoSelecionada
    { 
        adicao,
        subtracao,
        multiplicacao,
        divisao
    }

    public class SimpleMath 
    {
        public static double adicao(double n1, double n2) {
            return n1 + n2;
        }

        public static double subtracao(double n1, double n2)
        {
            return n1 - n2;
        }

        public static double multiplicacao(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double divisao(double n1, double n2)
        {
            return n1 / n2;
        }
    }
}

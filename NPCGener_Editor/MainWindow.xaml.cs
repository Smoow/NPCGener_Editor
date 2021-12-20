﻿using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace NPCGener_Editor
{
    public partial class MainWindow : Window
    {
        int counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            ReadDataFromNPCGener();
            FillListView("");
        }

        private void FillListView(string specific)
        {
            lst1.Items.Clear();
            int counter = 0;
            int counterMobs = 0;
            if (File.Exists("../NPCGener.txt"))
            {
                string[] lines = File.ReadAllLines("../NPCGener.txt");

                if (specific.Equals(""))
                {
                    foreach (string line in lines)
                    {
                        if (line.Contains("#"))
                        {
                            string show = line.Substring(2) + " -" + lines[counter - 2].Substring(2);
                            lst1.Items.Add(show);
                            counterMobs++;
                        }
                        counter++;
                    }

                    totalMobs.Text = counterMobs.ToString();
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if (line.ToLower().Contains(specific.ToLower()) && line.Contains("//"))
                        {
                            string show = lines[counter + 2].Substring(2) + " -" + lines[counter].Substring(2);
                            lst1.Items.Add(show);
                            counterMobs++;
                        }
                        else if (line.ToLower().Contains("#\t" + specific.ToLower()))
                        {
                            string show = lines[counter].Substring(2) + " -" + lines[counter - 2].Substring(2);
                            lst1.Items.Add(show);
                            counterMobs++;
                        }

                        counter++;
                    }

                    totalMobs.Text = counterMobs.ToString();
                }


            }
        }

        private void getInfoSelectedMob(object sender, RoutedEventArgs e)
        {
            if (lst1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um NPC/MOB primeiro.", "Erro ao editar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            int counterData = -1;
            int counterDataInside = 0;
            string[,] x = new string[10000, 30];
            string[] id = lst1.SelectedItem.ToString().Split(" ");
            string[] lines = File.ReadAllLines("../NPCGener.txt");


            foreach (string line in lines)
            {
                // Checa o header
                if (line.Contains("//") && !line.Contains('*'))
                {
                    counterData++;
                    counterDataInside = 0;
                    x[counterData, counterDataInside] = line;
                }

                // Linha normal
                else
                {
                    counterDataInside++;
                    x[counterData, counterDataInside] = line;
                }

            }

            // Insere as linhas com as modificacoes feitas.
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {

                    if (x[i, j] != null)
                    {

                        if (x[i, 2].Contains(id[0]))
                        {
                            if (x[i, j].Contains("//") && !x[i, j].Contains('*')) text1.Text = x[i, j][3..];
                            else if (x[i, j].Contains("MinuteGenerate")) text2.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("MaxNumMob")) text3.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("MinGroup")) text4.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("MaxGroup")) text5.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Leader")) text6.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Follower")) text7.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("RouteType")) text8.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Formation")) text9.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartX")) text10.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartY")) text11.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartRange")) text12.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartWait")) text13.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestX")) text14.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestY")) text15.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestRange")) text16.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestWait")) text17.Text = x[i, j].Split(":")[1].Trim();
                        }
                    }
                }
            }
        }

        private void ReadDataFromNPCGener()
        {
            feedbackAction.Text = "";
            requestIndexText.Text = "";

            if (File.Exists("I_NPCGener.txt"))
            {
                string[] lines = File.ReadAllLines("I_NPCGener.txt");
                foreach (string line in lines)
                {
                    if (line.Contains("INDEX"))
                    {
                        counter++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Falha ao encontrar I_NPCGener.txt", "Erro ao abrir arquivo", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }


        }

        private async void GenerateIndexForNPCGener(object sender, RoutedEventArgs e)
        {
            feedbackAction.Text = "";
            requestIndexText.Text = "";
            int localCounter = 0;

            using StreamWriter file = new("../NPCGener.txt");
            string[] lines = File.ReadAllLines("I_NPCGener.txt");
            foreach (string line in lines)
            {
                if (line.Contains("INDEX"))
                {
                    localCounter++;
                    await file.WriteLineAsync("#\t[" + localCounter + "]");
                    continue;
                }

                await file.WriteLineAsync(line);
            }

            file.Close();

            totalMobs.Text = counter.ToString();
            FillListView("");
        }


        private async void SendDataToNPCGenerGeneric(object sender, RoutedEventArgs e)
        {
            // Evita a quebra pela W2PP
            try
            {
                int minNumeric = System.Convert.ToInt32(text4.Text);
                int maxNumeric = System.Convert.ToInt32(text5.Text);
                if ((maxNumeric - minNumeric) < 0)
                {
                    MessageBox.Show("MaxGroup não pode ser menor que MinGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("Apenas valores numéricos em MinGroup e MaxGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            string desc = text1.Text;
            string minute = text2.Text;
            string maxnum = text3.Text;
            string mingroup = text4.Text;
            string maxgroup = text5.Text;
            string leader = text6.Text;
            string follower = text7.Text;
            string routetype = text8.Text;
            string formation = text9.Text;
            string startX = text10.Text;
            string startY = text11.Text;
            string startrange = text12.Text;
            string startwait = text13.Text;
            string destX = text14.Text;
            string destY = text15.Text;
            string destRange = text16.Text;
            string destWait = text17.Text;


            using StreamWriter file = new("I_NPCGener.txt", append: true);
            await file.WriteLineAsync("\n\n// " + desc);
            await file.WriteLineAsync("// ********************************************");
            await file.WriteLineAsync("#\t[INDEX]");
            await file.WriteLineAsync("\tMinuteGenerate:\t" + minute);
            await file.WriteLineAsync("\tMaxNumMob:\t" + maxnum);
            await file.WriteLineAsync("\tMinGroup:\t" + mingroup);
            await file.WriteLineAsync("\tMaxGroup:\t" + maxgroup);
            await file.WriteLineAsync("\tLeader:\t" + leader);
            await file.WriteLineAsync("\tFollower:\t" + follower);
            await file.WriteLineAsync("\tRouteType:\t" + routetype);
            await file.WriteLineAsync("\tFormation:\t" + formation);
            await file.WriteLineAsync("\tStartX:\t" + startX);
            await file.WriteLineAsync("\tStartY:\t" + startY);
            await file.WriteLineAsync("\tStartRange:\t" + startrange);
            await file.WriteLineAsync("\tStartWait:\t" + startwait);
            await file.WriteLineAsync("\tDestX:\t" + destX);
            await file.WriteLineAsync("\tDestY:\t" + destY);
            await file.WriteLineAsync("\tDestRange:\t" + destRange);
            await file.WriteLineAsync("\tDestWait:\t" + destWait);

            file.Close();

            SendDataToNPCGener(++counter);
        }

        private async void SendDataToNPCGener(int counter)
        {
            string desc = text1.Text;
            string minute = text2.Text;
            string maxnum = text3.Text;
            string mingroup = text4.Text;
            string maxgroup = text5.Text;
            string leader = text6.Text;
            string follower = text7.Text;
            string routetype = text8.Text;
            string formation = text9.Text;
            string startX = text10.Text;
            string startY = text11.Text;
            string startrange = text12.Text;
            string startwait = text13.Text;
            string destX = text14.Text;
            string destY = text15.Text;
            string destRange = text16.Text;
            string destWait = text17.Text;


            using StreamWriter file = new("../NPCGener.txt", append: true);
            await file.WriteLineAsync("\n\n// " + desc);
            await file.WriteLineAsync("// ********************************************");
            await file.WriteLineAsync("#\t[" + counter + "]");
            await file.WriteLineAsync("\tMinuteGenerate:\t" + minute);
            await file.WriteLineAsync("\tMaxNumMob:\t" + maxnum);
            await file.WriteLineAsync("\tMinGroup:\t" + mingroup);
            await file.WriteLineAsync("\tMaxGroup:\t" + maxgroup);
            await file.WriteLineAsync("\tLeader:\t" + leader);
            await file.WriteLineAsync("\tFollower:\t" + follower);
            await file.WriteLineAsync("\tRouteType:\t" + routetype);
            await file.WriteLineAsync("\tFormation:\t" + formation);
            await file.WriteLineAsync("\tStartX:\t" + startX);
            await file.WriteLineAsync("\tStartY:\t" + startY);
            await file.WriteLineAsync("\tStartRange:\t" + startrange);
            await file.WriteLineAsync("\tStartWait:\t" + startwait);
            await file.WriteLineAsync("\tDestX:\t" + destX);
            await file.WriteLineAsync("\tDestY:\t" + destY);
            await file.WriteLineAsync("\tDestRange:\t" + destRange);
            await file.WriteLineAsync("\tDestWait:\t" + destWait);

            file.Close();
            totalMobs.Text = counter.ToString();
            FillListView("");
            feedbackAction.Text = "NPC/MOB adicionado com sucesso! Não se esqueça de gerar o index.";
        }

        private async void saveEditInfo(object sender, RoutedEventArgs e)
        {
            if (lst1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um NPC/MOB primeiro.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Evita a quebra pela W2PP
            try
            {
                int minNumeric = System.Convert.ToInt32(text4.Text);
                int maxNumeric = System.Convert.ToInt32(text5.Text);
                if ((maxNumeric - minNumeric) < 0)
                {
                    MessageBox.Show("MaxGroup não pode ser menor que MinGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("Apenas valores numéricos em MinGroup e MaxGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            int counterData = -1;
            int counterDataInside = 0;
            string[] id = lst1.SelectedItem.ToString().Split(" ");
            string[,] x = new string[10000, 30];
            using StreamWriter file = new("X_NPCGener.txt");
            if (File.Exists("../NPCGener.txt"))
            {
                string[] lines = File.ReadAllLines("../NPCGener.txt");
                foreach (string line in lines)
                {
                    // Checa o header
                    if (line.Contains("//") && !line.Contains('*'))
                    {
                        counterData++;
                        counterDataInside = 0;
                        x[counterData, counterDataInside] = line;
                    }

                    // Linha normal
                    else
                    {
                        counterDataInside++;
                        x[counterData, counterDataInside] = line;
                    }

                }

                // Insere as linhas com as modificacoes feitas.
                for (int i = 0; i < x.GetLength(0); i++)
                {
                    for (int j = 0; j < x.GetLength(1); j++)
                    {

                        if (x[i, j] != null)
                        {

                            if (x[i, 2].Contains(id[0]))
                            {
                                if (x[i, j].Contains("//") && !x[i, j].Contains('*'))   x[i, j] = "// " + text1.Text;
                                else if (x[i, j].Contains("MinuteGenerate"))            x[i, j] = "\tMinuteGenerate:\t" + text2.Text;
                                else if (x[i, j].Contains("MaxNumMob"))                 x[i, j] = "\tMaxNumMob:\t" + text3.Text;
                                else if (x[i, j].Contains("MinGroup"))                  x[i, j] = "\tMinGroup:\t" + text4.Text;
                                else if (x[i, j].Contains("MaxGroup"))                  x[i, j] = "\tMaxGroup:\t" + text5.Text;
                                else if (x[i, j].Contains("Leader"))                    x[i, j] = "\tLeader:\t" + text6.Text;
                                else if (x[i, j].Contains("Follower"))                  x[i, j] = "\tFollower:\t" + text7.Text;
                                else if (x[i, j].Contains("RouteType"))                 x[i, j] = "\tRouteType:\t" + text8.Text;
                                else if (x[i, j].Contains("Formation"))                 x[i, j] = "\tFormation:\t" + text9.Text;
                                else if (x[i, j].Contains("StartX"))                    x[i, j] = "\tStartX:\t" + text10.Text;
                                else if (x[i, j].Contains("StartY"))                    x[i, j] = "\tStartY:\t" + text11.Text;
                                else if (x[i, j].Contains("StartRange"))                x[i, j] = "\tStartRange:\t" + text12.Text;
                                else if (x[i, j].Contains("StartWait"))                 x[i, j] = "\tStartWait:\t" + text13.Text;
                                else if (x[i, j].Contains("DestX"))                     x[i, j] = "\tDestX:\t" + text14.Text;
                                else if (x[i, j].Contains("DestY"))                     x[i, j] = "\tDestY:\t" + text15.Text;
                                else if (x[i, j].Contains("DestRange"))                 x[i, j] = "\tDestRange:\t" + text16.Text;
                                else if (x[i, j].Contains("DestWait"))                  x[i, j] = "\tDestWait:\t" + text17.Text;
                            }

                            await file.WriteLineAsync(x[i, j]);
                        }
                    }

                }
                file.Close();
                TransformIdToIndex();
                feedbackAction.Text = "NPC/MOB editado com sucesso!";
                requestIndexText.Text = " Não se esqueça de gerar o index.";
            }
            else
            {
                MessageBox.Show("Falha ao encontrar NPCGener.txt", "Erro ao abrir arquivo", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private async void TransformIdToIndex()
        {
            using StreamWriter file = new("I_NPCGener.txt");
            string[] lines = File.ReadAllLines("X_NPCGener.txt");
            foreach (string line in lines)
            {
                if (line.Contains("#"))
                {
                    await file.WriteLineAsync("#\t[INDEX]");
                    continue;
                }

                await file.WriteLineAsync(line);
            }

            file.Close();
            FillListView("");
            File.Delete("X_NPCGener.txt");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cleanData(object sender, RoutedEventArgs e)
        {
            text1.Text = "";
            text2.Text = "";
            text3.Text = "";
            text4.Text = "";
            text5.Text = "";
            text6.Text = "";
            text7.Text = "";
            text8.Text = "";
            text9.Text = "";
            text10.Text = "";
            text11.Text = "";
            text12.Text = "";
            text13.Text = "";
            text14.Text = "";
            text15.Text = "";
            text16.Text = "";
            text17.Text = "";
        }

        private void searchMob(object sender, RoutedEventArgs e)
        {
            string searchString = campoBusca.Text;
            FillListView(searchString);
        }

        private async void deleteMob(object sender, RoutedEventArgs e)
        {
            if (lst1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um NPC/MOB primeiro.", "Erro ao excluir", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            int counterData = -1;
            int counterDataInside = 0;
            string[] id = lst1.SelectedItem.ToString().Split(" ");
            string[,] x = new string[10000, 30];
            using StreamWriter file = new("X_NPCGener.txt");
            if (File.Exists("../NPCGener.txt"))
            {
                string[] lines = File.ReadAllLines("../NPCGener.txt");
                foreach (string line in lines)
                {
                    // Checa o header
                    if (line.Contains("//") && !line.Contains('*'))
                    {
                        counterData++;
                        counterDataInside = 0;
                        x[counterData, counterDataInside] = line;
                    }

                    // Linha normal
                    else
                    {
                        counterDataInside++;
                        x[counterData, counterDataInside] = line;
                    }

                }

                // Insere as linhas com as modificacoes feitas.
                for (int i = 0; i < x.GetLength(0); i++)
                {
                    for (int j = 0; j < x.GetLength(1); j++)
                    {

                        if (x[i, j] != null)
                        {

                            // Encontra o indice e pula para que "exclua".
                            if (x[i, 2].Contains(id[0]))
                            {
                                break;
                            }

                            await file.WriteLineAsync(x[i, j]);
                        }
                    }

                }
                file.Close();
                TransformIdToIndex();
                feedbackAction.Text = "NPC/MOB apagado com sucesso!";
                requestIndexText.Text = " Não se esqueça de gerar o index.";
            }
            else
            {
                MessageBox.Show("Falha ao encontrar NPCGener.txt", "Erro ao abrir arquivo", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private static void lineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }

    }
}

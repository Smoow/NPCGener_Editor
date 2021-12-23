using System.IO;
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
            string[,] x = new string[10000, 40];
            string[] id = lst1.SelectedItem.ToString().Split(" ");
            string[] lines = File.ReadAllLines("../NPCGener.txt");

            // Reseta todas as checkbox para que seja verificada novamente.
            // Assim como seus fields de texto.
            resetAllCheckBoxes();
            resetAllSegmentFields();


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
                            if (x[i, j].Contains("//") && !x[i, j].Contains('*')) desc.Text = x[i, j][3..];
                            else if (x[i, j].Contains("MinuteGenerate")) minute.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("MaxNumMob")) maxnum.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("MinGroup")) mingroup.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("MaxGroup")) maxgroup.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Leader")) leader.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Follower")) follower.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("RouteType")) routetype.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Formation")) formation.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartX")) startX.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartY")) startY.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartRange")) startRange.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartWait")) startWait.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestX")) destX.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestY")) destY.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestRange")) destRange.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("DestWait")) destWait.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment1X"))
                            {
                                segment1x.IsEnabled = true;
                                segment1y.IsEnabled = true;
                                segment1wait.IsEnabled = true;
                                segment1range.IsEnabled = true;
                                segment1x.Text = x[i, j].Split(":")[1].Trim();
                                segment1CheckBox.IsChecked = true;
                            }
                            else if (x[i, j].Contains("Segment1Y")) segment1y.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment1Wait")) segment1wait.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment1Range")) segment1range.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment2X"))
                            {
                                segment2x.IsEnabled = true;
                                segment2y.IsEnabled = true;
                                segment2wait.IsEnabled = true;
                                segment2range.IsEnabled = true;
                                segment2x.Text = x[i, j].Split(":")[1].Trim();
                                segment2CheckBox.IsChecked = true;
                            }
                            else if (x[i, j].Contains("Segment2Y")) segment2y.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment2Wait")) segment2wait.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment2Range")) segment2range.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment3X"))
                            {
                                segment3x.IsEnabled = true;
                                segment3y.IsEnabled = true;
                                segment3wait.IsEnabled = true;
                                segment3range.IsEnabled = true;
                                segment3x.Text = x[i, j].Split(":")[1].Trim();
                                segment3CheckBox.IsChecked = true;
                            }
                            else if (x[i, j].Contains("Segment3Y")) segment3y.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment3Wait")) segment3wait.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("Segment3Range")) segment3range.Text = x[i, j].Split(":")[1].Trim();
                            else if (x[i, j].Contains("StartAction"))
                            {
                                startActionText.IsEnabled = true;
                                startActionText.Text = x[i, j].Split(":")[1].Trim();
                                startActionCheckBox.IsChecked = true;
                            }
                            else if (x[i, j].Contains("Segment1Action"))
                            {
                                segment1ActionText.IsEnabled = true;
                                segment1ActionText.Text = x[i, j].Split(":")[1].Trim();
                                segment1ActionCheckBox.IsChecked = true;
                            }
                            else if (x[i, j].Contains("Segment2Action"))
                            {
                                segment2ActionText.IsEnabled = true;
                                segment2ActionText.Text = x[i, j].Split(":")[1].Trim();
                                segment2ActionCheckBox.IsChecked = true;
                            }
                            else if (x[i, j].Contains("Segment3Action"))
                            {
                                segment3ActionText.IsEnabled = true;
                                segment3ActionText.Text = x[i, j].Split(":")[1].Trim();
                                segment3ActionCheckBox.IsChecked = true;
                            }
                        }
                    }
                }
            }
        }

        private void resetAllSegmentFields()
        {
            segment1x.Text = "";
            segment1y.Text = "";
            segment1wait.Text = "";
            segment1range.Text = "";
            segment2x.Text = "";
            segment2y.Text = "";
            segment2wait.Text = "";
            segment2range.Text = "";
            segment3x.Text = "";
            segment3y.Text = "";
            segment3wait.Text = "";
            segment3range.Text = "";
            startActionText.Text = "";
            segment1ActionText.Text = "";
            segment2ActionText.Text = "";
            segment3ActionText.Text = "";
        }

        private void resetAllCheckBoxes()
        {
            segment1CheckBox.IsChecked = false;
            segment2CheckBox.IsChecked = false;
            segment3CheckBox.IsChecked = false;
            startActionCheckBox.IsChecked = false;
            segment1ActionCheckBox.IsChecked = false;
            segment2ActionCheckBox.IsChecked = false;
            segment3ActionCheckBox.IsChecked = false;
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
                int minNumeric = System.Convert.ToInt32(this.mingroup.Text);
                int maxNumeric = System.Convert.ToInt32(this.maxgroup.Text);
                if ((maxNumeric - minNumeric) < 0)
                {
                    MessageBox.Show("MaxGroup não pode ser menor que MinGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Apenas valores numéricos em MinGroup e MaxGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            string desc = this.desc.Text;
            string minute = this.minute.Text;
            string maxnum = this.maxnum.Text;
            string mingroup = this.mingroup.Text;
            string maxgroup = this.maxgroup.Text;
            string leader = this.leader.Text;
            string follower = this.follower.Text;
            string routetype = this.routetype.Text;
            string formation = this.formation.Text;
            string startX = this.startX.Text;
            string startY = this.startY.Text;
            string startrange = startRange.Text;
            string startwait = startWait.Text;
            string destX = this.destX.Text;
            string destY = this.destY.Text;
            string destRange = this.destRange.Text;
            string destWait = this.destWait.Text;
            string seg1x = segment1x.Text;
            string seg1y = segment1y.Text;
            string seg1w = segment1wait.Text;
            string seg1r = segment1range.Text;
            string seg2x = segment2x.Text;
            string seg2y = segment2y.Text;
            string seg2w = segment2wait.Text;
            string seg2r = segment2range.Text;
            string seg3x = segment3x.Text;
            string seg3y = segment3y.Text;
            string seg3w = segment3wait.Text;
            string seg3r = segment3range.Text;
            string startaction = startActionText.Text;
            string seg1action = segment1ActionText.Text;
            string seg2action = segment2ActionText.Text;
            string seg3action = segment3ActionText.Text;


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

            if (startActionCheckBox.IsChecked == true)
            {
                if (!startaction.Equals("")) await file.WriteLineAsync("\tStartAction:\t" + startaction);
            }

            if (segment1CheckBox.IsChecked == true)
            {
                if (!seg1x.Equals(""))
                {
                    await file.WriteLineAsync("\tSegment1X:\t" + seg1x);
                    await file.WriteLineAsync("\tSegment1Y:\t" + seg1y);
                    await file.WriteLineAsync("\tSegment1Range:\t" + seg1r);
                    await file.WriteLineAsync("\tSegment1Wait:\t" + seg1w);
                }

                if (!seg1action.Equals("")) await file.WriteLineAsync("\tSegment1Action:\t" + seg1action);
            }

            if (segment2CheckBox.IsChecked == true)
            {
                if (!seg2x.Equals(""))
                {
                    await file.WriteLineAsync("\tSegment2X:\t" + seg2x);
                    await file.WriteLineAsync("\tSegment2Y:\t" + seg2y);
                    await file.WriteLineAsync("\tSegment2Range:\t" + seg2r);
                    await file.WriteLineAsync("\tSegment2Wait:\t" + seg2w);
                }

                if (!seg2action.Equals("")) await file.WriteLineAsync("\tSegment2Action:\t" + seg2action);
            }

            if (segment3CheckBox.IsChecked == true)
            {
                if (!seg3x.Equals(""))
                {
                    await file.WriteLineAsync("\tSegment3X:\t" + seg3x);
                    await file.WriteLineAsync("\tSegment3Y:\t" + seg3y);
                    await file.WriteLineAsync("\tSegment3Range:\t" + seg3r);
                    await file.WriteLineAsync("\tSegment3Wait:\t" + seg3w);
                }

                if (!seg3action.Equals("")) await file.WriteLineAsync("\tSegment3Action:\t" + seg3action);
            }

            file.Close();

            SendDataToNPCGener(++counter);
        }

        private async void SendDataToNPCGener(int counter)
        {
            string desc = this.desc.Text;
            string minute = this.minute.Text;
            string maxnum = this.maxnum.Text;
            string mingroup = this.mingroup.Text;
            string maxgroup = this.maxgroup.Text;
            string leader = this.leader.Text;
            string follower = this.follower.Text;
            string routetype = this.routetype.Text;
            string formation = this.formation.Text;
            string startX = this.startX.Text;
            string startY = this.startY.Text;
            string startrange = startRange.Text;
            string startwait = startWait.Text;
            string destX = this.destX.Text;
            string destY = this.destY.Text;
            string destRange = this.destRange.Text;
            string destWait = this.destWait.Text;
            string seg1x = segment1x.Text;
            string seg1y = segment1y.Text;
            string seg1w = segment1wait.Text;
            string seg1r = segment1range.Text;
            string seg2x = segment2x.Text;
            string seg2y = segment2y.Text;
            string seg2w = segment2wait.Text;
            string seg2r = segment2range.Text;
            string seg3x = segment3x.Text;
            string seg3y = segment3y.Text;
            string seg3w = segment3wait.Text;
            string seg3r = segment3range.Text;
            string startaction = startActionText.Text;
            string seg1action = segment1ActionText.Text;
            string seg2action = segment2ActionText.Text;
            string seg3action = segment3ActionText.Text;



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

            if (startActionCheckBox.IsChecked == true)
            {
                if (!startaction.Equals("")) await file.WriteLineAsync("\tStartAction:\t" + startaction);
            }

            if (segment1CheckBox.IsChecked == true)
            {
                if (!seg1x.Equals(""))
                {
                    await file.WriteLineAsync("\tSegment1X:\t" + seg1x);
                    await file.WriteLineAsync("\tSegment1Y:\t" + seg1y);
                    await file.WriteLineAsync("\tSegment1Range:\t" + seg1r);
                    await file.WriteLineAsync("\tSegment1Wait:\t" + seg1w);
                }

                if (!seg1action.Equals("")) await file.WriteLineAsync("\tSegment1Action:\t" + seg1action);
            }

            if (segment2CheckBox.IsChecked == true)
            {
                if (!seg2x.Equals(""))
                {
                    await file.WriteLineAsync("\tSegment2X:\t" + seg2x);
                    await file.WriteLineAsync("\tSegment2Y:\t" + seg2y);
                    await file.WriteLineAsync("\tSegment2Range:\t" + seg2r);
                    await file.WriteLineAsync("\tSegment2Wait:\t" + seg2w);
                }

                if (!seg2action.Equals("")) await file.WriteLineAsync("\tSegment2Action:\t" + seg2action);
            }

            if (segment3CheckBox.IsChecked == true)
            {
                if (!seg3x.Equals(""))
                {
                    await file.WriteLineAsync("\tSegment3X:\t" + seg3x);
                    await file.WriteLineAsync("\tSegment3Y:\t" + seg3y);
                    await file.WriteLineAsync("\tSegment3Range:\t" + seg3r);
                    await file.WriteLineAsync("\tSegment3Wait:\t" + seg3w);
                }

                if (!seg3action.Equals("")) await file.WriteLineAsync("\tSegment3Action:\t" + seg3action);
            }

            file.Close();
            totalMobs.Text = counter.ToString();
            FillListView("");
            feedbackAction.Text = "NPC/MOB adicionado com sucesso!";
            requestIndexText.Text = "Não se esqueça de gerar o index.";
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
                int minNumeric = System.Convert.ToInt32(mingroup.Text);
                int maxNumeric = System.Convert.ToInt32(maxgroup.Text);
                if ((maxNumeric - minNumeric) < 0)
                {
                    MessageBox.Show("MaxGroup não pode ser menor que MinGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Apenas valores numéricos em MinGroup e MaxGroup.", "Erro ao salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            int counterData = -1;
            int counterDataInside = 0;
            string[] id = lst1.SelectedItem.ToString().Split(" ");
            string[,] x = new string[10000, 40];
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
                int startFlag = 0;
                int segment1Flag = 0;
                int segment2Flag = 0;
                int segment3Flag = 0;
                int segment1ActionFlag = 0;
                int segment2ActionFlag = 0;
                int segment3ActionFlag = 0;
                for (int i = 0; i < x.GetLength(0); i++)
                {
                    for (int j = 0; j < x.GetLength(1); j++)
                    {

                        if (x[i, j] != null)
                        {

                            if (x[i, 2].Contains(id[0]))
                            {
                                if (x[i, j].Contains("//") && !x[i, j].Contains('*')) x[i, j] = "// " + desc.Text;
                                else if (x[i, j].Contains("MinuteGenerate")) x[i, j] = "\tMinuteGenerate:\t" + minute.Text;
                                else if (x[i, j].Contains("MaxNumMob")) x[i, j] = "\tMaxNumMob:\t" + maxnum.Text;
                                else if (x[i, j].Contains("MinGroup")) x[i, j] = "\tMinGroup:\t" + mingroup.Text;
                                else if (x[i, j].Contains("MaxGroup")) x[i, j] = "\tMaxGroup:\t" + maxgroup.Text;
                                else if (x[i, j].Contains("Leader")) x[i, j] = "\tLeader:\t" + leader.Text;
                                else if (x[i, j].Contains("Follower")) x[i, j] = "\tFollower:\t" + follower.Text;
                                else if (x[i, j].Contains("RouteType")) x[i, j] = "\tRouteType:\t" + routetype.Text;
                                else if (x[i, j].Contains("Formation")) x[i, j] = "\tFormation:\t" + formation.Text;
                                else if (x[i, j].Contains("StartX")) x[i, j] = "\tStartX:\t" + startX.Text;
                                else if (x[i, j].Contains("StartY")) x[i, j] = "\tStartY:\t" + startY.Text;
                                else if (x[i, j].Contains("StartRange")) x[i, j] = "\tStartRange:\t" + startRange.Text;
                                else if (x[i, j].Contains("StartWait")) x[i, j] = "\tStartWait:\t" + startWait.Text;
                                else if (x[i, j].Contains("DestX")) x[i, j] = "\tDestX:\t" + destX.Text;
                                else if (x[i, j].Contains("DestY")) x[i, j] = "\tDestY:\t" + destY.Text;
                                else if (x[i, j].Contains("DestRange")) x[i, j] = "\tDestRange:\t" + destRange.Text;
                                else if (x[i, j].Contains("DestWait")) x[i, j] = "\tDestWait:\t" + destWait.Text;

                                //
                                // StartAction
                                //
                                else if (x[i, j].Contains("StartAction"))
                                {
                                    if (startActionCheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tStartAction:\t" + startActionText.Text;
                                        startFlag = 1;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }


                                //
                                // Segment1
                                //
                                else if (x[i, j].Contains("Segment1X"))
                                {
                                    if (segment1CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment1X:\t" + segment1x.Text;
                                        segment1Flag = 1;

                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment1Y"))
                                {
                                    if (segment1CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment1Y:\t" + segment1y.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment1Wait"))
                                {
                                    if (segment1CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment1Wait:\t" + segment1wait.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment1Range"))
                                {
                                    if (segment1CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment1Range:\t" + segment1range.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment1Action"))
                                {
                                    if (segment1ActionCheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment1Action:\t" + segment1ActionText.Text;
                                        segment1ActionFlag = 1;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }


                                //
                                // Segment2
                                //
                                else if (x[i, j].Contains("Segment2X"))
                                {
                                    if (segment2CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment2X:\t" + segment2x.Text;
                                        segment2Flag = 1;

                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment2Y"))
                                {
                                    if (segment2CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment2Y:\t" + segment2y.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment2Wait"))
                                {
                                    if (segment2CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment2Wait:\t" + segment2wait.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment2Range"))
                                {
                                    if (segment2CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment2Range:\t" + segment2range.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment2Action"))
                                {
                                    if (segment2ActionCheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment2Action:\t" + segment2ActionText.Text;
                                        segment2ActionFlag = 1;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                //
                                // Segment3
                                //
                                else if (x[i, j].Contains("Segment3X"))
                                {
                                    if (segment3CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment3X:\t" + segment3x.Text;
                                        segment3Flag = 1;

                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment3Y"))
                                {
                                    if (segment3CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment3Y:\t" + segment3y.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment3Wait"))
                                {
                                    if (segment3CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment3Wait:\t" + segment3wait.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment3Range"))
                                {
                                    if (segment3CheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment3Range:\t" + segment3range.Text;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Contains("Segment3Action"))
                                {
                                    if (segment3ActionCheckBox.IsChecked == true)
                                    {
                                        x[i, j] = "\tSegment3Action:\t" + segment3ActionText.Text;
                                        segment3ActionFlag = 1;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                else if (x[i, j].Equals("")) continue;
                            }

                            await file.WriteLineAsync(x[i, j]);
                        }
                    }

                    // Verifica se não é nulo e contém o que precisamos para inserir o novo campo.
                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        if (startActionCheckBox.IsChecked == true && startFlag == 0)
                        {
                            await file.WriteLineAsync("\tStartAction:\t" + startActionText.Text);
                        }
                    }

                    // Verifica se não é nulo e contém o que precisamos para inserir o novo campo.
                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        if (segment1CheckBox.IsChecked == true && segment1Flag == 0)
                        {
                            await file.WriteLineAsync("\tSegment1X:\t" + segment1x.Text);
                            await file.WriteLineAsync("\tSegment1Y:\t" + segment1y.Text);
                            await file.WriteLineAsync("\tSegment1Range:\t" + segment1range.Text);
                            await file.WriteLineAsync("\tSegment1Wait:\t" + segment1wait.Text);
                        }
                    }

                    // Verifica se não é nulo e contém o que precisamos para inserir o novo campo.
                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        if (segment1ActionCheckBox.IsChecked == true && segment1ActionFlag == 0)
                        {
                            await file.WriteLineAsync("\tSegment1Action:\t" + segment1ActionText.Text);
                        }
                    }

                    // Verifica se não é nulo e contém o que precisamos para inserir o novo campo.
                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        if (segment2CheckBox.IsChecked == true && segment2Flag == 0)
                        {
                            await file.WriteLineAsync("\tSegment2X:\t" + segment2x.Text);
                            await file.WriteLineAsync("\tSegment2Y:\t" + segment2y.Text);
                            await file.WriteLineAsync("\tSegment2Range:\t" + segment2range.Text);
                            await file.WriteLineAsync("\tSegment2Wait:\t" + segment2wait.Text);
                        }
                    }

                    // Verifica se não é nulo e contém o que precisamos para inserir o novo campo.
                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        if (segment2ActionCheckBox.IsChecked == true && segment2ActionFlag == 0)
                        {
                            await file.WriteLineAsync("\tSegment2Action:\t" + segment2ActionText.Text);
                        }
                    }

                    // Verifica se não é nulo e contém o que precisamos para inserir o novo campo.
                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        if (segment3CheckBox.IsChecked == true && segment3Flag == 0)
                        {
                            await file.WriteLineAsync("\tSegment3X:\t" + segment3x.Text);
                            await file.WriteLineAsync("\tSegment3Y:\t" + segment3y.Text);
                            await file.WriteLineAsync("\tSegment3Range:\t" + segment3range.Text);
                            await file.WriteLineAsync("\tSegment3Wait:\t" + segment3wait.Text);
                        }
                    }

                    // Verifica se não é nulo e contém o que precisamos para inserir o novo campo.
                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        if (segment3ActionCheckBox.IsChecked == true && segment3ActionFlag == 0)
                        {
                            await file.WriteLineAsync("\tSegment3Action:\t" + segment3ActionText.Text);
                        }
                    }

                    if (x[i, 2] != null && x[i, 2].Contains(id[0]))
                    {
                        await file.WriteLineAsync("");
                    }

                }
                file.Close();
                TransformIdToIndex();
                feedbackAction.Text = "NPC/MOB editado com sucesso!";
                requestIndexText.Text = "Não se esqueça de gerar o index.";
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
            if (lst1.Items.Count != 0) getInfoSelectedMob(sender, e);
        }

        private void cleanData(object sender, RoutedEventArgs e)
        {
            desc.Text = "";
            minute.Text = "";
            maxnum.Text = "";
            mingroup.Text = "";
            maxgroup.Text = "";
            leader.Text = "";
            follower.Text = "";
            routetype.Text = "";
            formation.Text = "";
            startX.Text = "";
            startY.Text = "";
            startRange.Text = "";
            startWait.Text = "";
            destX.Text = "";
            destY.Text = "";
            destRange.Text = "";
            destWait.Text = "";
            resetAllCheckBoxes();
            resetAllSegmentFields();
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
                requestIndexText.Text = "Não se esqueça de gerar o index.";
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

        private void segment1func(object sender, RoutedEventArgs e)
        {
            segment1x.IsEnabled = (segment1CheckBox.IsChecked == true ? true : false);
            segment1y.IsEnabled = (segment1CheckBox.IsChecked == true ? true : false);
            segment1range.IsEnabled = (segment1CheckBox.IsChecked == true ? true : false);
            segment1wait.IsEnabled = (segment1CheckBox.IsChecked == true ? true : false);

            if (segment1CheckBox.IsChecked == false)
            {
                segment1x.Text = "";
                segment1y.Text = "";
                segment1wait.Text = "";
                segment1range.Text = "";
            }
        }

        private void segment2func(object sender, RoutedEventArgs e)
        {
            segment2x.IsEnabled = (segment2CheckBox.IsChecked == true ? true : false);
            segment2y.IsEnabled = (segment2CheckBox.IsChecked == true ? true : false);
            segment2range.IsEnabled = (segment2CheckBox.IsChecked == true ? true : false);
            segment2wait.IsEnabled = (segment2CheckBox.IsChecked == true ? true : false);

            if (segment2CheckBox.IsChecked == false)
            {
                segment2x.Text = "";
                segment2y.Text = "";
                segment2wait.Text = "";
                segment2range.Text = "";
            }
        }

        private void segment3func(object sender, RoutedEventArgs e)
        {
            segment3x.IsEnabled = (segment3CheckBox.IsChecked == true ? true : false);
            segment3y.IsEnabled = (segment3CheckBox.IsChecked == true ? true : false);
            segment3range.IsEnabled = (segment3CheckBox.IsChecked == true ? true : false);
            segment3wait.IsEnabled = (segment3CheckBox.IsChecked == true ? true : false);

            if (segment3CheckBox.IsChecked == false)
            {
                segment3x.Text = "";
                segment3y.Text = "";
                segment3wait.Text = "";
                segment3range.Text = "";
            }
        }

        private void startActionFunc(object sender, RoutedEventArgs e)
        {
            startActionText.IsEnabled = (startActionCheckBox.IsChecked == true ? true : false);

            if (startActionCheckBox.IsChecked == false) { startActionText.Text = ""; }
        }

        private void segment1ActionFunc(object sender, RoutedEventArgs e)
        {
            segment1ActionText.IsEnabled = (segment1ActionCheckBox.IsChecked == true ? true : false);
            if (segment1ActionCheckBox.IsChecked == false) { segment1ActionText.Text = ""; }
        }

        private void segment2ActionFunc(object sender, RoutedEventArgs e)
        {
            segment2ActionText.IsEnabled = (segment2ActionCheckBox.IsChecked == true ? true : false);
            if (segment2ActionCheckBox.IsChecked == false) { segment2ActionText.Text = ""; }
        }

        private void segment3ActionFunc(object sender, RoutedEventArgs e)
        {
            segment3ActionText.IsEnabled = (segment3ActionCheckBox.IsChecked == true ? true : false);
            if (segment3ActionCheckBox.IsChecked == false) { segment3ActionText.Text = ""; }
        }

    }
}

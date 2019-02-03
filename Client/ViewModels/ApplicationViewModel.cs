using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClassesLibaryBilling;
using System.Collections.ObjectModel;
using Client.Commands;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Client.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {

        // для связи с сервером 
        int port;
        IPAddress ip;
        IPEndPoint ep;
        TcpClient client;


        public ObservableCollection<ClassesLibaryBilling.Client> ClientsList { get; set; }

        public ObservableCollection<ClassesLibaryBilling.Project> ProjectList { get; set; }

        public ObservableCollection<ClassesLibaryBilling.Payment> PaymentList { get; set; }

        ClassesLibaryBilling.Client _selectedClient;
        public ClassesLibaryBilling.Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value;
                OnPropertyChanged();
            }
        }

        ClassesLibaryBilling.Project _selectedProject;
        public ClassesLibaryBilling.Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
            }
        }

         ClassesLibaryBilling.Payment _selectedPayment;
        public ClassesLibaryBilling.Payment SelectedPayment
        {
            get { return _selectedPayment; }
            set {
                _selectedPayment = value;
                OnPropertyChanged();
            }
        }


        // для клиента 
        string _fio = "";
        public string Fio
        {
            get { return _fio; }
            set { _fio = value;
                OnPropertyChanged();
            }
        }

        string _phone = "";
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        string _email = "";
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        string _clientComment = "";
        public string ClientComment
        {
            get { return _clientComment; }
            set
            {
                _clientComment = value;
                OnPropertyChanged();
            }
        }

        // для поиска клиента 
        
        bool radio2;
        public bool Radio2
        {
            set
            {
                radio2 = value;
                OnPropertyChanged();
            }
        }
        string _searchText = "";
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }


        // для проекта 
        string _projectName = ""; 
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; OnPropertyChanged(); }
        }

        string _projectPayStatus;
        public string ProjectPayStatus
        {
            get { return _projectPayStatus; }
            set { _projectPayStatus = value; OnPropertyChanged(); }
        }

        string _projectEmail = "";
        public string ProjectEmail
        {
            get { return _projectEmail; }
            set { _projectEmail = value; OnPropertyChanged(); }
        }

        string _projectComment = "";
        public string ProjectComment
        {
            get { return _projectComment; }
            set { _projectComment = value; OnPropertyChanged(); }
        }

        DateTime _projectNextPay; 
        public DateTime ProjectNextPay
        {
            get { return _projectNextPay /*.ToShortDateString()*/; }
            set { _projectNextPay = value; OnPropertyChanged(); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        // команды 


        public RelayCommand AddClient { get; set; }
        public RelayCommand DellClient { get; set; }
        public RelayCommand FindClient { get; set; }
        public RelayCommand ClearClients { get; set; }
        public RelayCommand EditClient { get; set; }
        public RelayCommand AddProject { get; set; }
        public RelayCommand DellProject { get; set; }



        #region Конструктор

        public ApplicationViewModel()
        {
            ip = IPAddress.Parse("127.0.0.1");
            port = 9001;
            ep = new IPEndPoint(ip, port);

            AddClient = new RelayCommand(AddClientFunc);
            DellClient = new RelayCommand(DellClientFunc);
            FindClient = new RelayCommand(FindClientFunc);
            ClearClients = new RelayCommand(LoadClientsFunc);
            EditClient = new RelayCommand(EditClientsFunc);
            AddProject = new RelayCommand(AddProjectFunc);
            DellProject = new RelayCommand(DellProjectFunc);

            ClientsList = new ObservableCollection<ClassesLibaryBilling.Client>();
            ProjectList = new ObservableCollection<ClassesLibaryBilling.Project>();
            PaymentList = new ObservableCollection<ClassesLibaryBilling.Payment>();
           



            LoadClients();
            LoadProjects();
            LoadPayments();
        }

        #endregion

        // методы 
        public void AddClientFunc(object obj)
        {
            if (_fio == "" || _phone == "" || _email == "" || _clientComment == "" || SelectedProject == null)           
                MessageBox.Show("Обязательно заполните все поля!");
            
            else
            {
                string task = $"addClient#{SelectedProject.Name}#{_fio}#{_phone}#{_email}#{_clientComment}";

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(task);
                sw.Flush();

                byte[] data = new byte[256];
                int bytes = ns.Read(data, 0, data.Length);
                string response = Encoding.UTF8.GetString(data, 0, bytes);


                sw.Close();
                ns.Close();
                client.Close();

                MessageBox.Show(response);
                LoadClients();
            }
                
        }

        public void DellClientFunc(object obj)
        {
            try
            {
                string task = $"dellClient#{_selectedClient.Id}";

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(task);
                sw.Flush();

                byte[] data = new byte[256];
                int bytes = ns.Read(data, 0, data.Length);
                string response = Encoding.UTF8.GetString(data, 0, bytes);


                sw.Close();
                ns.Close();
                client.Close();

                MessageBox.Show(response);

                LoadClients();

            }
            catch (Exception)
            {
                MessageBox.Show("Выберите клиента для удаления");
            }
        }

        public void FindClientFunc(object obj)
        {
            try
            {
                if (_searchText != "")
                {
                    
                    string task;
                    if (radio2 == true)
                        task = $"findClientByProject#{_searchText}";
                    else                      
                        task = $"findClientByClient#{_searchText}";

                    client = new TcpClient();
                    client.Connect(ep);

                    NetworkStream ns = client.GetStream();
                    StreamWriter sw = new StreamWriter(ns);
                    sw.WriteLine(task);
                    sw.Flush();


                    byte[] data = new byte[50000];
                    int bytes = ns.Read(data, 0, data.Length);
                    ObservableCollection<ClassesLibaryBilling.Client> cl = new ObservableCollection<ClassesLibaryBilling.Client>();
                    cl = FromByteArray<ObservableCollection<ClassesLibaryBilling.Client>>(data);
                    ClientsList.Clear();

                    foreach (var item in cl)
                        ClientsList.Add(item);

                    sw.Close();
                    ns.Close();
                    client.Close();
                }
                else
                    MessageBox.Show("Введите текст для поиска");


            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }

        public void LoadClientsFunc(object obj)
        {
            LoadClients();
            SearchText = "";
        }

        public void EditClientsFunc(object obj)
        {

            try
            { 
                string task = $"editClient#{_selectedClient.Id}#{_selectedClient.FIO}#" +
                    $"{_selectedClient.Email}#{_selectedClient.Phone}#{_selectedProject.Name}#{_selectedClient.Comment}";

                if (_selectedClient.FIO == "" || _selectedClient.Email == "" || _selectedClient.Phone == "" || _selectedClient.Comment == "" || _selectedProject == null)
                {
                    MessageBox.Show("Обязательно заполните все поля!");
                    LoadClients();
                }
                else
                {
                    client = new TcpClient();
                    client.Connect(ep);

                    NetworkStream ns = client.GetStream();
                    StreamWriter sw = new StreamWriter(ns);
                    sw.WriteLine(task);
                    sw.Flush();

                    byte[] data = new byte[256];
                    int bytes = ns.Read(data, 0, data.Length);
                    string response = Encoding.UTF8.GetString(data, 0, bytes);


                    sw.Close();
                    ns.Close();
                    client.Close();

                    MessageBox.Show(response);

                    LoadClients();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Выберите клиента для удаления");
            }
        }

        public void AddProjectFunc(object obj)
        {
            if (_projectName == "" || _projectEmail == "" || _projectComment == "" )
            MessageBox.Show("Обязательно заполните все поля!");
            else
            {
                MessageBox.Show($"{_projectName}, {_projectEmail}, {_projectComment}"); 
                string task = $"addProject#{_projectName}#{_projectEmail}#{_projectComment}";

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(task);
                sw.Flush();

                byte[] data = new byte[256];
                int bytes = ns.Read(data, 0, data.Length);
                string response = Encoding.UTF8.GetString(data, 0, bytes);


                sw.Close();
                ns.Close();
                client.Close();

                MessageBox.Show(response);
                LoadProjects();
            }
        }

        public void DellProjectFunc ( object obj)
        {
            try
            {
                string task = $"dellProject#{_selectedProject.Id}";

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(task);
                sw.Flush();

                byte[] data = new byte[256];
                int bytes = ns.Read(data, 0, data.Length);
                string response = Encoding.UTF8.GetString(data, 0, bytes);


                sw.Close();
                ns.Close();
                client.Close();

                MessageBox.Show(response);

                LoadClients();
                LoadProjects(); 

            }
            catch (Exception)
            {
                MessageBox.Show("Выберите проект для удаления");
            }
        }


        public void LoadClients()
        {         try
            {
                ClientsList.Clear();

                string task = "loadClients";

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(task);
                sw.Flush();

                byte[] data = new byte[50000];
                int bytes = ns.Read(data, 0, data.Length);
                ObservableCollection<ClassesLibaryBilling.Client> cl = new ObservableCollection<ClassesLibaryBilling.Client>();
                cl = FromByteArray<ObservableCollection<ClassesLibaryBilling.Client>>(data);


                foreach (var item in cl)
                    ClientsList.Add(item);

                sw.Close();
                ns.Close();
                client.Close();
            }
                    
            catch (Exception)
            {
                MessageBox.Show("Сервер выключен либо отсутствует соидинение с интеренетом");
            }
}

        private void LoadProjects()
        {
            try
            {
                ProjectList.Clear(); 
                string task = "loadProjects";

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(task);
                sw.Flush();


                byte[] data = new byte[50000];
                int bytes = ns.Read(data, 0, data.Length);
                ObservableCollection<ClassesLibaryBilling.Project> pl = new ObservableCollection<ClassesLibaryBilling.Project>();
                pl = FromByteArray<ObservableCollection<ClassesLibaryBilling.Project>>(data);

                foreach (var item in pl)
                    ProjectList.Add(item);


                sw.Close();
                ns.Close();
                client.Close();
            }                    
            catch (Exception)
            {
                MessageBox.Show("Сервер выключен либо отсутствует соидинение с интеренетом");
            }

}

        public void LoadPayments()
        {
            try
            {
                PaymentList.Clear();

                string task = "loadPayments";

                client = new TcpClient();
                client.Connect(ep);

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(task);
                sw.Flush();

                byte[] data = new byte[50000];
                int bytes = ns.Read(data, 0, data.Length);
                ObservableCollection<ClassesLibaryBilling.Payment> pl = new ObservableCollection<ClassesLibaryBilling.Payment>();
                pl = FromByteArray<ObservableCollection<ClassesLibaryBilling.Payment>>(data);


                foreach (var item in pl)
                    PaymentList.Add(item);

                sw.Close();
                ns.Close();
                client.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Сервер выключен либо отсутствует соидинение с интеренетом");
            }

        }

        byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public T FromByteArray<T>(byte[] data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

    }
}

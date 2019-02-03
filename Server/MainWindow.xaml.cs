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
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Server.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;
using Server.Jobs;

namespace Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int port;
        IPAddress ip;
        IPEndPoint ep;
        TcpListener listener;
        Thread listenerThread;

        DataContex db = new DataContex();

        public MainWindow()
        {
            InitializeComponent();

            EmailSheduler.Start();

            // для получения 
            ip = IPAddress.Parse("127.0.0.1");
            port = 9001;

            ep = new IPEndPoint(ip, port);
            listener = new TcpListener(ep);
            listener.Start();

            listenerThread = new Thread(new ThreadStart(ListenerWorkItem));
            listenerThread.IsBackground = true;
            listenerThread.Start();


            //db.Users.Add(new User()
            //{

            //    FIO = "Быков Евгений",
            //    Login = "Eugeniy",
            //    Password = "123"
            //});
            //db.SaveChanges();
        }

        private void ListenerWorkItem()  // получения сообщений в постоянном цикле 
        {
            try
            {
                while (true)
                {
                    TcpClient acceptor = listener.AcceptTcpClient();
                    NetworkStream ns = acceptor.GetStream();
                    StreamReader sr = new StreamReader(ns);
                    string mess = sr.ReadLine();

                    string[] parts = mess.Split('#');

                    if (parts[0] == "loadClients")
                    {
                        ObservableCollection<ClassesLibaryBilling.Client> ClientsList = new ObservableCollection<ClassesLibaryBilling.Client>();

                        var clientsList = db.Clients.ToList();
                        for (int i = 0; i < clientsList.Count; i++)
                        {
                            ClientsList.Add(new ClassesLibaryBilling.Client()
                            {
                                Id = clientsList[i].Id,
                                FIO = clientsList[i].FIO,
                                CompanyName = clientsList[i].Project.Name,
                                Email = clientsList[i].Email,
                                Phone = clientsList[i].Phone,
                                Comment = clientsList[i].Comment
                            });
                        }

                        byte[] arr = ObjectToByteArray(ClientsList);
                        ns.Write(arr, 0, arr.Length);
                    }

                    else if (parts[0] == "loadProjects")
                    {
                        ObservableCollection<ClassesLibaryBilling.Project> ProjectList = new ObservableCollection<ClassesLibaryBilling.Project>();

                        var projectList = db.Projects.ToList();
                        for (int i = 0; i < projectList.Count; i++)
                        {
                            ProjectList.Add(new ClassesLibaryBilling.Project()
                            {
                                Id = projectList[i].Id,
                                Name = projectList[i].Name,
                                Status = projectList[i].Status,
                                SendAddress = projectList[i].SendAddress,
                                Comment = projectList[i].Comment,
                                NextPay = projectList[i].NextPay
                            });
                        }

                        byte[] arr = ObjectToByteArray(ProjectList);
                        ns.Write(arr, 0, arr.Length);
                    }

                    else if (parts[0] == "addClient")
                    {
                        string pName = parts[1];
                        var proj = db.Projects.Where(p => p.Name == pName).FirstOrDefault();
                        db.Clients.Add(new Client()
                        {
                            ProjectId = proj.Id,
                            FIO = parts[2],
                            Phone = parts[3],
                            Email = parts[4],
                            Comment = parts[5]
                        });
                        db.SaveChanges();


                        byte[] arr = System.Text.Encoding.UTF8.GetBytes("Клиент успешно добавлен");
                        ns.Write(arr, 0, arr.Length);
                    }

                    else if (parts[0] == "dellClient")
                    {
                        int client_id = Convert.ToInt32(parts[1]);
                        var client = db.Clients.Where(c => c.Id == client_id).FirstOrDefault();
                        db.Clients.Remove(client);
                        db.SaveChanges();

                        byte[] arr = System.Text.Encoding.UTF8.GetBytes("Клиент успешно удален");
                        ns.Write(arr, 0, arr.Length);
                    }

                    else if (parts[0] == "findClientByClient")
                    {
                        ObservableCollection<ClassesLibaryBilling.Client> ClientsList = new ObservableCollection<ClassesLibaryBilling.Client>();

                        string clientToFind = parts[1];
                        var cl = db.Clients.Where(c => c.FIO.Contains(clientToFind) == true).ToList();

                        for (int i = 0; i < cl.Count; i++)
                        {
                            ClientsList.Add(new ClassesLibaryBilling.Client()
                            {
                                Id = cl[i].Id,
                                FIO = cl[i].FIO,
                                CompanyName = cl[i].Project.Name,
                                Email = cl[i].Email,
                                Phone = cl[i].Phone,
                                Comment = cl[i].Comment
                            });
                        }

                        byte[] arr = ObjectToByteArray(ClientsList);
                        ns.Write(arr, 0, arr.Length);
                    }

                    else if (parts[0] == "findClientByProject")
                    {
                        ObservableCollection<ClassesLibaryBilling.Client> ClientsList = new ObservableCollection<ClassesLibaryBilling.Client>();

                        string clientToFind = parts[1];
                        var cl = db.Clients.Where(c => c.Project.Name.Contains(clientToFind) == true).ToList();

                        for (int i = 0; i < cl.Count; i++)
                        {
                            ClientsList.Add(new ClassesLibaryBilling.Client()
                            {
                                Id = cl[i].Id,
                                FIO = cl[i].FIO,
                                CompanyName = cl[i].Project.Name,
                                Email = cl[i].Email,
                                Phone = cl[i].Phone,
                                Comment = cl[i].Comment
                            });
                        }

                        byte[] arr = ObjectToByteArray(ClientsList);
                        ns.Write(arr, 0, arr.Length);
                    }

                    else if (parts[0] == "editClient")
                    {
                        int client_id = Convert.ToInt32(parts[1]);
                        string proj_Name = parts[5];
                        var project = db.Projects.Where(p => p.Name == proj_Name).FirstOrDefault();
                        var client = db.Clients.Where(c => c.Id == client_id).FirstOrDefault();
                        client.FIO = parts[2];
                        client.Email = parts[3];
                        client.Phone = parts[4];
                        client.ProjectId = project.Id;
                        client.Comment = parts[6];

                        db.SaveChanges();


                        byte[] arr = System.Text.Encoding.UTF8.GetBytes("Данные обновлены");
                        ns.Write(arr, 0, arr.Length);

                        sr.Close();
                        ns.Close();
                        acceptor.Close();
                    }

                    else if (parts[0] == "loadPayments")
                    {
                        ObservableCollection<ClassesLibaryBilling.Payment> PaymentsList =
                            new ObservableCollection<ClassesLibaryBilling.Payment>();

                        var payList = db.Payments.ToList();
                        for (int i = 0; i < payList.Count; i++)
                        {
                            PaymentsList.Add(new ClassesLibaryBilling.Payment()
                            {
                                Id = payList[i].Id,
                                ProjectName = payList[i].Project.Name,
                                PayDate = payList[i].PayDate,
                                PayPeriod = payList[i].PayPeriod,
                                Sum = payList[i].Sum
                            });
                        }

                        byte[] arr = ObjectToByteArray(PaymentsList);
                        ns.Write(arr, 0, arr.Length);
                    }

                    else if (parts[0] == "addProject")
                    {                       
                        db.Projects.Add(new Project()
                        {
                            Name = parts[1],
                            Status = "Не оплачен",
                            SendAddress = parts[2],
                            Comment = parts[3]
                        });
                        db.SaveChanges();


                        byte[] arr = System.Text.Encoding.UTF8.GetBytes("Проект успешно добавлен");
                        ns.Write(arr, 0, arr.Length);

                        
                    }

                    else if (parts[0] == "dellProject")
                    {
                        int project_id = Convert.ToInt32(parts[1]);
                        var project = db.Projects.Where(p => p.Id == project_id).FirstOrDefault();
                        db.Projects.Remove(project);
                        db.SaveChanges();

                        byte[] arr = System.Text.Encoding.UTF8.GetBytes("Проект успешно удален");
                        ns.Write(arr, 0, arr.Length);
                    }
                    
                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
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

        public T FromByteArray<T> (byte[] data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj; 
            }
        }




        /// <summary>
        ///             АСИНХРОННЫЙ ВАРИАНТ 
        /// </summary>

        //async void RunServer()
        //{
        //    while (true) 
        //    {
        //        var tcpClient = await listener.AcceptTcpClientAsync();
        //        ProcessAsync(tcpClient); // await не нужен
        //    }
        //}

        //public async Task ProcessAsync(TcpClient c)
        //{
        //    NetworkStream ns = c.GetStream(); 
        //}

    }
}



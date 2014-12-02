using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;
using System.ComponentModel;
using System.Xml.Linq;


namespace Project1_final
{
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Lay duong dan cua Filename test.xml phuc vu luu tru
        /// </summary>
        /// <returns></returns>
        public string getXMLUri()
        {
            //lay duong dan hien tai
            string a=System.IO.Directory.GetCurrentDirectory().ToString();
            string Filename = a + @"\..\..\\test.xml";
            return Filename;
        }
        /// <summary>
        /// Hàm chạy khi chương trình bắt đầu chạy, load data từ file test.xml cho datacontext rồi load vào combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void load(object sender, RoutedEventArgs e)
        {
            string FileName = getXMLUri();
            ProfilesData.Source = null;
            ProfilesData.Source = new Uri(FileName);
        }
        
        /// <summary>
        /// khai báo một DispatcherTimer làm nhiệm vụ đếm cứ 1s load lại Profile 1 lần
        /// </summary>
        public DispatcherTimer timer = new DispatcherTimer();

        
        public MainWindow()
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Start();
            
            timer.Tick += dispatcherTimer_Tick;
        }
        /// <summary>
        /// sự kiện timer_tick, sau 1s lấy lại thông tin về IP, subnet, gateway, dns, hostname của máy, hiển thị lên tab Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dispatcherTimer_Tick(object sender, object e)
        {
            //khởi tạo 1 class function, sử dụng hàm GetIP lấy thông tin
            function fn = new function();
            string[] ipAdresses=null;
            string[] subnets = null;
            string[] gateways = null;
            string[] dnses = null;
            string hostname = null;
            ///Hàm GetIP trả về lại các thông tin ngay trên chuỗi đầu vào
            fn.GetIP(out ipAdresses, out subnets, out gateways, out dnses, out hostname);
            
                tblIP.Text = ipAdresses[0];
                tblSubnet.Text = subnets[0];
                tblGateway.Text = gateways[0];
                tblDNS.Text = dnses[0];
                tblHostname.Text = hostname;
            ///Kiểm tra xem máy có kết nối internet hay không
            ///nếu máy không có kết nối, ẩn label, textbox thuộc tính, hiện label thông báo
                if (ipAdresses[0] != "")
                {
                    //ẩn label thông báo ko có kết nối, hiện các thông số
                    lblalert1.Visibility = Visibility.Hidden;
                    lblalert2.Visibility = Visibility.Hidden;
                    lblIP.Visibility = Visibility;
                    lblSubnet.Visibility = Visibility;
                    lblGateway.Visibility = Visibility;
                    lblDNS.Visibility = Visibility;
                    lblhost.Visibility = Visibility;
                    tblIP.Visibility = Visibility;
                    tblSubnet.Visibility = Visibility;
                    tblGateway.Visibility = Visibility;
                    tblDNS.Visibility = Visibility;
                    tblHostname.Visibility = Visibility;
                    //hiện buton reset
                    btReset.Visibility = Visibility;
                }
                else
                {
                    lblalert1.Visibility = Visibility;
                    lblalert2.Visibility = Visibility;
                    lblIP.Visibility = Visibility.Hidden;
                    lblSubnet.Visibility = Visibility.Hidden;
                    lblGateway.Visibility = Visibility.Hidden;
                    lblDNS.Visibility = Visibility.Hidden;
                    lblhost.Visibility = Visibility.Hidden;
                    tblIP.Visibility = Visibility.Hidden;
                    tblSubnet.Visibility = Visibility.Hidden;
                    tblGateway.Visibility = Visibility.Hidden;
                    tblDNS.Visibility = Visibility.Hidden;
                    tblHostname.Visibility = Visibility.Hidden;
                    //ẩn buton reset
                    btReset.Visibility = Visibility.Hidden;
                }

            
        }

        
        private void TabItem_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((TabControl)sender).SelectedIndex == 1)
            {
                //ImageBrush tab_config_n = new ImageBrush();
                //tab_config_n.ImageSource = new BitmapImage(new Uri("tab_config_n.jpg", UriKind.Relative));
                //config.SourceUpdated = tab_config_n; 
                
                //BitmapImage bi = new BitmapImage(new Uri("tab_config_n.jpg", UriKind.Relative));
                //tabconfig.Source = bi;
            }
        }

       
        /// <summary>
        /// Hàm thay đổi background header của các tabItem mỗi khi được chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void change_background(object sender, SelectionChangedEventArgs e)
        {
            ///Khai báo các ảnh Bitmap làm background
            BitmapImage tab_about_n = new BitmapImage(new Uri("tab_about_n.jpg", UriKind.Relative));
            BitmapImage tab_profile_n = new BitmapImage(new Uri("tab_profile_n.jpg", UriKind.Relative));
            BitmapImage tab_config_n = new BitmapImage(new Uri("tab_config_n.jpg", UriKind.Relative));
            BitmapImage tab_status_n = new BitmapImage(new Uri("tab_status_n.jpg", UriKind.Relative));
            BitmapImage tab_about = new BitmapImage(new Uri("tab_about.jpg", UriKind.Relative));
            BitmapImage tab_profile = new BitmapImage(new Uri("tab_profile.jpg", UriKind.Relative));
            BitmapImage tab_config = new BitmapImage(new Uri("tab_config.jpg", UriKind.Relative));
            BitmapImage tab_status = new BitmapImage(new Uri("tab_status.jpg", UriKind.Relative));
            
            ///nếu tab about được chọn, đổi background header, pause video
            if (about.IsSelected == true)
            {
                
                image_about.Source = tab_about_n;
                image_profile.Source = tab_profile;
                image_config.Source = tab_config;
                image_status.Source = tab_status;
                myMediaElement.Pause();
            }
            if (profile.IsSelected == true)
            {

                image_about.Source = tab_about;
                image_profile.Source = tab_profile_n;
                image_config.Source = tab_config;
                image_status.Source = tab_status;
            }
            if (config.IsSelected == true)
            {

                image_about.Source = tab_about;
                image_profile.Source = tab_profile;
                image_config.Source = tab_config_n;
                image_status.Source = tab_status;
            }
            if (status.IsSelected == true)
            {

                image_about.Source = tab_about;
                image_profile.Source = tab_profile;
                image_config.Source = tab_config;
                image_status.Source = tab_status_n;
                
            }

        }
        /// <summary>
        /// Hàm cấu hình tự động IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reset(object sender, RoutedEventArgs e)
        {
            function fn = new function();
            fn.SetDHCP();
        }
        /// <summary>
        /// thay đổi địa chỉ IP, subnet, gateway, dns, hostname của máy tính dựa trên giá trị nhập vào
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void set_click(object sender, RoutedEventArgs e)
        {
            function fn = new function();
            fn.SetIP(tbConfig_IP.Text, tbConfig_Subnet.Text, tbConfig_Gateway.Text, tbConfig_DNS.Text, tbConfig_Hostname.Text);
            MessageBox.Show("Thiết lập thành công !");
        }

        //save
        
       /// <summary>
       /// lưu các giá trị đã thiết lập vào file xml đã đựng sẵn
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void save_click(object sender, RoutedEventArgs e)
        {
                //Lấy đường dẫn file Test.xml
                string FileName = getXMLUri();
                string data = File.ReadAllText(FileName);
                XDocument xmlDoc = XDocument.Parse(data);                
                ///duyệt từng profile trong file xml
                ///ghi các profile ra mảng Profiles
                bool trung=false;
                foreach (var item in xmlDoc.Descendants("Profile"))
                {
                    if (tbConfig_Name.Text == item.Element("Address").Value) trung = true;
                }
                if (trung == true) MessageBox.Show("Địa chỉ đã tồn tại");
                else
                {
                    ///ghi ra file xml
                    WriterToXml();
                    ///cập nhật lại Source của DataContext, thay đổi được hiển thị trên combobox
                    ProfilesData.Source = null;
                    ProfilesData.Source = new Uri(FileName);
                    MessageBox.Show("Lưu thiết lập thành công !");
                }
            
        }

        
        
        /// <summary>
        /// sự kiện thay đổi lựa chọn trên combobox
        /// lưu giá trị Address lựa chọn ra chuỗi
        /// duyệt file xml, tìm profile chứa Addresss, hiển thị các thông tin ra textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbinfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (profile.IsSelected == true)
            {
                ///đặt đường dẫn đến file xml
                string FileName = getXMLUri();
                string data = File.ReadAllText(FileName);
                XDocument xmlDoc = XDocument.Parse(data);
                ///lưu giá trị Address đã chọn trên combobox ra chuỗi value 
                string value = cbInfo.SelectedValue.ToString();
                ///khai báo 1 mảng các profile
                List<profile> Profiles = new List<profile>();
                ///duyệt từng profile trong file xml
                ///ghi các profile ra mảng Profiles
                foreach (var item in xmlDoc.Descendants("Profile"))
                {
                    profile p = new profile();
                    p.Address = item.Element("Address").Value;
                    p.IP = item.Element("IP").Value;
                    p.Subnet = item.Element("Subnet").Value;
                    p.Gateway = item.Element("Gateway").Value;
                    p.DNS = item.Element("DNS").Value;
                    p.Hostname = item.Element("Hostname").Value;
                    Profiles.Add(p);

                }
                ///duyệt mảng profiles
                ///tìm ra profile có Address phù hợp
                ///hiển thị info ra textbox bên ngoài
                foreach (profile ch in Profiles)
                {
                    if (ch.Address == value)
                    {
                        txtIP_profile.Text = ch.IP;
                        txtSubnet_profile.Text = ch.Subnet;
                        txtGateway_profile.Text = ch.Gateway;
                        txtDNS_profile.Text = ch.DNS;
                        txtHostname_profile.Text = ch.Hostname;
                    }
                    else
                    {
                        //do nothing
                    }

                }
            }
            else
            {
                //do nothing
            }
        }
        /// <summary>
        /// Hàm ghi Info ra file xml
        /// </summary>
        private void WriterToXml()
        {
            string FileName = getXMLUri();
            XDocument xmlDoc = XDocument.Load(FileName);

            xmlDoc.Element("Profiles").Add(
                new XElement("Profile",
                    new XElement("Address", tbConfig_Name.Text),
                    new XElement("IP", tbConfig_IP.Text),
                    new XElement("Subnet", tbConfig_Subnet.Text),
                    new XElement("Gateway", tbConfig_Gateway.Text),
                    new XElement("DNS", tbConfig_DNS.Text),
                    new XElement("Hostname", tbConfig_Hostname.Text)
                    )
             );
            xmlDoc.Save(FileName);
        }
        /// <summary>
        /// pause the video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_pause_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Pause();
        }
        /// <summary>
        /// resume the video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_play_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Play();
        }
        /// <summary>
        /// stop the video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_stop_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
        }
        /// <summary>
        /// thay đổi volume video theo các vị trí của slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMediaElement.Volume = volume.Value;
        }
        /// <summary>
        /// Hiện các button, slider cua video khi re chuột vào cửa sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            nav.Visibility = Visibility;
            
        }
        /// <summary>
        /// Ẩn các button, slider volume khi rê chuột ra ngoài cửa sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            nav.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Button bt_saved_set được kick,
        /// kiểm tra xem thông số của profile chọn trong combobox đã được đưa ra textbox bên ngoài chưa
        /// setIP và đưa ra thông báo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_saved_set_Click(object sender, RoutedEventArgs e)
        {
            if (txtIP_profile.Text != "Your ip here")
            {
                function fn = new function();
                fn.SetIP(txtIP_profile.Text, txtSubnet_profile.Text, txtGateway_profile.Text, txtDNS_profile.Text, txtHostname_profile.Text);
                MessageBox.Show("Thiết lập thành công !");
            }
            else
            {
                MessageBox.Show("Chưa có Profile !");
            }
        }
        /// <summary>
        /// Button fullscreen player được kích
        /// mở ra form player mới to hơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player pl = new player();
            myMediaElement.Pause();
            pl.Show();
        }
      
        
       
    }
}

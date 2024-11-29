using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AppIOT
{
    public partial class Form1 : Form
    {
        //TODO : Change the base_url to the correct URL
        private string base_url = "http://localhost:44392/api/somiod/";
        private string appName;
        private string containerName;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateApp_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(base_url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            //Create the object
            //Send the request to base_url using post and a XML object as the body
            //Get the response and show it in the textbox

            //XML format
            /*
            <application>
                <name>name</name>
                <id>**</id>
                <description>description</description>
            </application>
             */

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode appNode = doc.CreateElement("application");
            doc.AppendChild(appNode);

            XmlNode nameNode = doc.CreateElement("name");
            nameNode.AppendChild(doc.CreateTextNode(txtBoxAppName.Text));
            appNode.AppendChild(nameNode);

            XmlNode idNode = doc.CreateElement("id");
            idNode.AppendChild(doc.CreateTextNode("0"));
            appNode.AppendChild(idNode);

            XmlNode descNode = doc.CreateElement("description");
            descNode.AppendChild(doc.CreateTextNode("description"));
            appNode.AppendChild(descNode);

            //Convert the XML to send it via POST
            StringContent content = new StringContent(doc.OuterXml, Encoding.UTF8, "application/xml");

            //Send the request
            HttpResponseMessage response = client.PostAsync(base_url,content).Result;

            //req will contain the request and the response
            string req = response.RequestMessage + Environment.NewLine + response.Content.ReadAsStringAsync().Result;

            //Show the response
            txtBoxMosquitto.Text = req;

            //if response is OK, extra url is the name of the app and activate the button to create a container
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                appName = txtBoxAppName.Text;
                btnCreateContainer.Enabled = true;
            }

        }

        private void btnCreateContainer_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(base_url + appName + "/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            //Create the object
            //Send the request to base_url using post and a XML object as the body
            //Get the response and show it in the textbox

            //XML format
            /*
                <container>
                    <name>name</name>
                    <id>**</id>
                    <description>description</description>
                </container>
             */

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode appNode = doc.CreateElement("container");
            doc.AppendChild(appNode);

            XmlNode nameNode = doc.CreateElement("name");
            nameNode.AppendChild(doc.CreateTextNode(txtBoxNomeSensor.Text));
            appNode.AppendChild(nameNode);

            XmlNode idNode = doc.CreateElement("id");
            idNode.AppendChild(doc.CreateTextNode("0"));

            appNode.AppendChild(idNode);

            XmlNode descNode = doc.CreateElement("description");
            descNode.AppendChild(doc.CreateTextNode("description"));

            appNode.AppendChild(descNode);
            doc.AppendChild(docNode);


            //Convert the XML to send it via POST
            StringContent content = new StringContent(doc.OuterXml, Encoding.UTF8, "application/xml");

            //Send the request
            HttpResponseMessage response = client.PostAsync(base_url + appName + "/", content).Result;

            //req will contain the request and the response
            string req = response.RequestMessage + Environment.NewLine + response.Content.ReadAsStringAsync().Result;

            //Show the response
            txtBoxMosquitto.Text = req;

            //if response is OK, extra url is the name of the app/sensor
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                containerName = txtBoxNomeSensor.Text;
            }


        }

        private async void SubscribeToNotifications(string endpoint)
        {
            string url = $"{base_url}/{appName}/{containerName}";

            using (HttpClient client = new HttpClient())
            {
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                XmlNode appNode = doc.CreateElement("notification");
                doc.AppendChild(appNode);

                XmlNode endpointNode = doc.CreateElement("endpoint");
                endpointNode.AppendChild(doc.CreateTextNode(endpoint));
                appNode.AppendChild(endpointNode);

                XmlNode nameNode = doc.CreateElement("name");
                nameNode.AppendChild(doc.CreateTextNode("notification"));
                appNode.AppendChild(nameNode);

                string xml = doc.OuterXml;


                HttpContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Subscribed to notifications!");
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}");
                }
            }
        }


    }

}

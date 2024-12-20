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
        // TODO: Alterar para o URL correto
        private string base_url;
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

        private async void btnCreateApp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxAppName.Text))
            {
                MessageBox.Show("Application name cannot be empty.");
                return;
            }

            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(base_url)
                };
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

                XmlDocument doc = CreateXmlDocument("application", txtBoxAppName.Text);
                StringContent content = new StringContent(doc.OuterXml, Encoding.UTF8, "application/xml");

                string response = await SendHttpRequestAsync(client, "api/somiod/application/", content);

                if (!string.IsNullOrEmpty(response))
                {
                    appName = txtBoxAppName.Text;
                    btnCreateContainer.Enabled = true;
                    txtBoxMosquitto.Text = response;
                    MessageBox.Show("Application created successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating application: {ex.Message}");
            }
        }

        private XmlDocument CreateXmlDocument(string rootName, string name)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode rootNode = doc.CreateElement(rootName);
            doc.AppendChild(rootNode);

            XmlNode nameNode = doc.CreateElement("name");
            nameNode.AppendChild(doc.CreateTextNode(name));
            rootNode.AppendChild(nameNode);

            return doc;
        }

        private void btnCreateContainer_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(base_url + appName + "/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

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

            StringContent content = new StringContent(doc.OuterXml, Encoding.UTF8, "application/xml");

            HttpResponseMessage response = client.PostAsync(base_url + appName + "/", content).Result;

            string req = response.RequestMessage + Environment.NewLine + response.Content.ReadAsStringAsync().Result;

            txtBoxMosquitto.Text = req;

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

        private async Task<string> SendHttpRequestAsync(HttpClient client, string endpoint, StringContent content)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"HTTP Error {response.StatusCode}: {error}");
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }

        private void btnSetUrl_Click(object sender, EventArgs e)
        {
            base_url = txtBoxUrl.Text;
            btnCreateApp.Enabled = true;
            MessageBox.Show("Base URL saved successfully!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

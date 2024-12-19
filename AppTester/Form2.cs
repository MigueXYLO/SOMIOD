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

namespace AppTester
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            string payload = "<command><name>turn_on</name></command>";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/xml");

                try
                {
                    // Log the request details
                    Console.WriteLine($"Sending request to {url} with payload: {payload}");

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Log the response status code
                    Console.WriteLine($"Response status code: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Light turned on successfully!");
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error: {response.StatusCode}\n{error}");
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    MessageBox.Show($"HTTP Request Exception: {httpEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            string payload = "<command><name>turn_off</name></command>";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/xml");

                try
                {
                    // Log the request details
                    Console.WriteLine($"Sending request to {url} with payload: {payload}");

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Log the response status code
                    Console.WriteLine($"Response status code: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Light turned off successfully!");
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error: {response.StatusCode}\n{error}");
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    MessageBox.Show($"HTTP Request Exception: {httpEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception: {ex.Message}");
                }
            }
        }
    }
}

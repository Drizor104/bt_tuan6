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
//using Newtonsoft.Json;



namespace bt_tuan6
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<TodoItem> items = await GetTodoItemsAsync();

            // clear the list view
            lvTodoItems.Items.Clear();

            // add each item to the list view
            foreach (TodoItem item in items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Id.ToString());
                listViewItem.SubItems.Add(item.Title);
                listViewItem.SubItems.Add(item.UserId.ToString());
                listViewItem.SubItems.Add(item.Completed ? "Yes" : "No");
                lvTodoItems.Items.Add(listViewItem);
            }
        }
        private async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            List<TodoItem> items = null;
            HttpResponseMessage response = await _client.GetAsync("https://jsonplaceholder.typicode.com/todos");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
            }
            return items;
        }
        public class TodoItem
        {
            public int UserId { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public bool Completed { get; set; }
        }
    }


}



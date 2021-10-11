using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp
{
    public partial class mainMenu : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<int, string> categories = new Dictionary<int, string>();
        static HttpClient client = new HttpClient();
        static int cat;
        static string difficulty;
        static string type;
        static string url;
       
       
        public mainMenu()
        {
            InitializeComponent();
           
           
            answerButton.Enabled = false;
            int counter = 1;
            var list = new List<string>();
            foreach (var item in accordionControlElement3.Elements)
            {
                list.Add(item.Text);
            }
            for (int i = 1 + 8; i < 25 + 8; i++)
            {
                categories.Add(i, list[i - 9]);
                counter++;
            }
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            cat = categories.FirstOrDefault(x => x.Value == accordionControlElement4.Text).Key;
        }

        private void accordionControlElement4_Click_1(object sender, EventArgs e)
        {
            cat = categories.FirstOrDefault(x => x.Value == accordionControlElement4.Text).Key;
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            cat = categories.FirstOrDefault(x => x.Value == accordionControlElement5.Text).Key;
           
            Check();
        }

        private void accordionControlElement29_Click(object sender, EventArgs e)
        {
            difficulty = "easy";
            Check();
        }

        private void accordionControlElement33_Click(object sender, EventArgs e)
        {
            type = "multiple";
            Check();
        }

        private void mainMenu_Load(object sender, EventArgs e)
        {
            layoutControlGroup1.Expanded = true;
            string selectedQuestions = string.Empty;

        }

        public void GetQuestions(string url)
        {
            Random random = new Random();
            var json = new WebClient().DownloadString(url);
            var root = JsonConvert.DeserializeObject<Root>(json);
            questionBox.Text = root.results.Select(x => x.question.Replace("&quot", " ")).FirstOrDefault();
            var answers = new List<string>();
            foreach (var item in root.results.Select(x => x.incorrect_answers).ToList())
            {
                answers.Add(item.ToString());
            }
            answers.Add(root.results.Select(x => x.correct_answer).ToString());
           
            answer1.Text = answers[random.Next(0,5)];
        }

        public bool Check()
        {
            if(cat > 1 && difficulty != "" && type != "")
            {
                answerButton.Enabled = true;
                return true;
            }
            return false;
        }

        private void answerButton_Click(object sender, EventArgs e)
        {
            url = $"https://opentdb.com/api.php?amount=10&category={cat}&difficulty={difficulty}&type={type}";
            GetQuestions(url);
        }

      
    }
}
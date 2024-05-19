//using Microsoft.UI.Xaml.Controls;
using maui = Microsoft.Maui.Controls;
//using Microsoft.UI.Xaml.Controls;
namespace Yi
{
    public class frmYiWin : ContentPage
    {
        private Grid grid;
        private maui.Editor rtQuestion;
        private Editor rtAnswer;
        private Button btnCopy;
        private Button btnEval;

        public frmYiWin()
        {
            // Create the UI elements
            rtQuestion = new Editor { Text = "Enter your question" };
            rtAnswer = new Editor {IsReadOnly = true };
            btnCopy = new Button { Text = "Copy" };
            btnEval = new Button { Text = "Evaluate" };

            // Create the grid layout
            grid = new Grid
            {
                //RowDefinitions = Rows.Define(100, 100, 100, 100),
                //ColumnDefinitions = Columns.Define(100, 100)
            };

            grid.Add(btnCopy, 1, 1);
            grid.Add(btnEval, 1, 0);

            // Set the content of the page
            Content = grid;

            // Add event handlers
            btnCopy.Clicked += btnCopy_Click;
            btnEval.Clicked += btnEval_Click;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var full = $"Question to I Ching:\n {rtQuestion.Text}\n I Ching answered:\n{rtAnswer.Text}\nWould you please interpret?";
            Clipboard.SetTextAsync(full);
        }

        private void btnEval_Click(object sender, EventArgs e)
        {
            // Get the text from the RichTextBox control
            string question = rtQuestion.Text;

            // Your evaluation logic here
            string answer = EvaluateQuestion(question);

            // Set the answer in the RichTextBox control
            rtAnswer.Text = answer;
        }

        private string EvaluateQuestion(string question)
        {
            // Your evaluation logic here
            return "Answer to your question";
        }
    }
}


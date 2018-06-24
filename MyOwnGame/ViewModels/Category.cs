using MyOwnGame.Models;
using System.Collections.ObjectModel;

namespace MyOwnGame.ViewModels
{
    public class Category
    {
        public string Topic { get; set; }

        public ObservableCollection<Question> Items { get; set; }
    }
}